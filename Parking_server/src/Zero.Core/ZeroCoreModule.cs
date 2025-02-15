﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using Abp;
using Abp.AspNetZeroCore;
using Abp.AspNetZeroCore.Timing;
using Abp.AutoMapper;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Json;
using Abp.MailKit;
using Abp.Net.Mail.Smtp;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Zero;
using Abp.Zero.Configuration;
using Abp.Zero.Ldap;
using Castle.MicroKernel.Registration;
using MailKit.Security;
using Zero.Authorization.Delegation;
using Zero.Authorization.Roles;
using Zero.Authorization.Users;
using Zero.Chat;
using Zero.Configuration;
using Zero.DashboardCustomization.Definitions;
using Zero.DynamicEntityProperties;
using Zero.Features;
using Zero.Friendships;
using Zero.Friendships.Cache;
using Zero.Localization;
using Zero.MultiTenancy;
using Zero.Net.Emailing;
using Zero.Notifications;
using Zero.WebHooks;
using Newtonsoft.Json;
using Zero.Customize.Cache;

namespace Zero
{
    [DependsOn(
        typeof(ZeroCoreSharedModule),
        typeof(AbpZeroCoreModule),
        typeof(AbpZeroLdapModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpAspNetZeroCoreModule),
        typeof(AbpMailKitModule))]
    public class ZeroCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            //workaround for issue: https://github.com/aspnet/EntityFrameworkCore/issues/9825
            //related github issue: https://github.com/aspnet/EntityFrameworkCore/issues/10407
            AppContext.SetSwitch("Microsoft.EntityFrameworkCore.Issue9825", true);

            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            ZeroLocalizationConfigurer.Configure(Configuration.Localization);

            //Adding feature providers
            Configuration.Features.Providers.Add<AppFeatureProvider>();

            //Adding setting providers
            Configuration.Settings.Providers.Add<AppSettingProvider>();

            //Adding notification providers
            Configuration.Notifications.Providers.Add<AppNotificationProvider>();

            //Adding webhook definition providers
            Configuration.Webhooks.Providers.Add<AppWebhookDefinitionProvider>();
            Configuration.Webhooks.TimeoutDuration = TimeSpan.FromMinutes(1);
            Configuration.Webhooks.IsAutomaticSubscriptionDeactivationEnabled = false;

            //Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = ZeroConst.MultiTenancyEnabled;

            //Enable LDAP authentication 
            //Configuration.Modules.ZeroLdap().Enable(typeof(AppLdapAuthenticationSource));

            //Twilio - Enable this line to activate Twilio SMS integration
            //Configuration.ReplaceService<ISmsSender,TwilioSmsSender>();

            //Adding DynamicEntityParameters definition providers
            Configuration.DynamicEntityProperties.Providers.Add<AppDynamicEntityPropertyDefinitionProvider>();

            // MailKit configuration
            Configuration.Modules.AbpMailKit().SecureSocketOption = SecureSocketOptions.Auto;
            Configuration.ReplaceService<IMailKitSmtpBuilder, ZeroMailKitSmtpBuilder>(DependencyLifeStyle.Transient);

            //Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            if (SystemConfig.DisableMailService)
            {
                //Disabling email sending in debug mode
                Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
            }

            Configuration.ReplaceService(typeof(IEmailSenderConfiguration), () =>
            {
                Configuration.IocManager.IocContainer.Register(
                    Component.For<IEmailSenderConfiguration, ISmtpEmailSenderConfiguration>()
                        .ImplementedBy<ZeroSmtpEmailSenderConfiguration>()
                        .LifestyleTransient()
                );
            });

            Configuration.Caching.Configure(FriendCacheItem.CacheName, cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(30); });

            Configuration.Caching.Configure(CustomTenantCacheItem.CacheName, cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(30); });

            Configuration.Caching.Configure(CustomTenantCacheItem.ByNameCacheName, cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(30); });

            Configuration.Caching.Configure(CustomTenantCacheItem.ByDomainCacheName, cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(30); });

            IocManager.Register<DashboardConfiguration>();

            Configuration.ReplaceService<IBackgroundJobManager, MyBackgroundJobManager>();
            
            Configuration.MultiTenancy.TenantIdResolveKey = "abp.tenantid";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroCoreModule).GetAssembly());

            RegisterCustomTenantCache();
        }

        public override void PostInitialize()
        {
            IocManager.RegisterIfNot<IChatCommunicator, NullChatCommunicator>();
            IocManager.Register<IUserDelegationConfiguration, UserDelegationConfiguration>();

            IocManager.Resolve<ChatUserStateWatcher>().Initialize();
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
        
        private void RegisterCustomTenantCache()
        {
            if (IocManager.IsRegistered<ICustomTenantCache>())
            {
                return;
            }

            using var entityTypes = IocManager.ResolveAsDisposable<IAbpZeroEntityTypes>();
            var implType = typeof (CustomTenantCache<>).MakeGenericType(entityTypes.Object.Tenant);

            IocManager.Register(typeof (ICustomTenantCache), implType, DependencyLifeStyle.Transient);
        }
    }

    /// <summary>
    /// Default implementation of <see cref="IBackgroundJobManager"/>.
    /// </summary>
    public class MyBackgroundJobManager : AsyncPeriodicBackgroundWorkerBase, IBackgroundJobManager, ISingletonDependency
    {
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// Interval between polling jobs from <see cref="IBackgroundJobStore"/>.
        /// Default value: 5000 (5 seconds).
        /// </summary>
        public static int JobPollPeriod { get; set; }

        private readonly IIocResolver _iocResolver;
        private readonly IBackgroundJobStore _store;

        static MyBackgroundJobManager()
        {
            JobPollPeriod = 5000;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundJobManager"/> class.
        /// </summary>
        public MyBackgroundJobManager(
            IIocResolver iocResolver,
            IBackgroundJobStore store,
            AbpAsyncTimer timer)
            : base(timer)
        {
            _store = store;
            _iocResolver = iocResolver;

            EventBus = NullEventBus.Instance;

            Timer.Period = JobPollPeriod;
        }

        [UnitOfWork]
        public virtual async Task<string> EnqueueAsync<TJob, TArgs>(TArgs args,
            BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJobBase<TArgs>
        {
            var jobInfo = new BackgroundJobInfo
            {
                JobType = typeof(TJob).AssemblyQualifiedName,
                JobArgs = args.ToJsonString(),
                Priority = priority
            };

            if (delay.HasValue)
            {
                jobInfo.NextTryTime = Clock.Now.Add(delay.Value);
            }

            await _store.InsertAsync(jobInfo);
            await CurrentUnitOfWork.SaveChangesAsync();

            return jobInfo.Id.ToString();
        }

        [UnitOfWork]
        public virtual string Enqueue<TJob, TArgs>(TArgs args,
            BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJobBase<TArgs>
        {
            var jobInfo = new BackgroundJobInfo
            {
                JobType = typeof(TJob).AssemblyQualifiedName,
                JobArgs = args.ToJsonString(),
                Priority = priority
            };

            if (delay.HasValue)
            {
                jobInfo.NextTryTime = Clock.Now.Add(delay.Value);
            }

            _store.Insert(jobInfo);
            CurrentUnitOfWork.SaveChanges();

            return jobInfo.Id.ToString();
        }

        public async Task<bool> DeleteAsync(string jobId)
        {
            if (long.TryParse(jobId, out long finalJobId) == false)
            {
                throw new ArgumentException($"The jobId '{jobId}' should be a number.", nameof(jobId));
            }

            var jobInfo = await _store.GetAsync(finalJobId);

            await _store.DeleteAsync(jobInfo);
            return true;
        }

        public bool Delete(string jobId)
        {
            if (long.TryParse(jobId, out long finalJobId) == false)
            {
                throw new ArgumentException($"The jobId '{jobId}' should be a number.", nameof(jobId));
            }

            var jobInfo = _store.Get(finalJobId);

            _store.Delete(jobInfo);
            return true;
        }

        protected override async Task DoWorkAsync()
        {
            var waitingJobs = await _store.GetWaitingJobsAsync(1000);

            foreach (var job in waitingJobs)
            {
                await TryProcessJobAsync(job);
            }
        }

        private async Task TryProcessJobAsync(BackgroundJobInfo jobInfo)
        {
            try
            {
                jobInfo.TryCount++;
                jobInfo.LastTryTime = Clock.Now;

                var jobType = Type.GetType(jobInfo.JobType);
                using (var job = _iocResolver.ResolveAsDisposable(jobType))
                {
                    try
                    {
                        var jobExecuteMethod = job.Object.GetType().GetTypeInfo()
                                                   .GetMethod(nameof(IBackgroundJob<object>.Execute)) ??
                                               job.Object.GetType().GetTypeInfo()
                                                   .GetMethod(nameof(IAsyncBackgroundJob<object>.ExecuteAsync));

                        if (jobExecuteMethod == null)
                        {
                            throw new AbpException(
                                $"Given job type does not implement {typeof(IBackgroundJob<>).Name} or {typeof(IAsyncBackgroundJob<>).Name}. " +
                                "The job type was: " + job.Object.GetType());
                        }

                        var argsType = jobExecuteMethod.GetParameters()[0].ParameterType;
                        var argsObj = JsonConvert.DeserializeObject(jobInfo.JobArgs, argsType);

                        if (jobExecuteMethod.Name == nameof(IAsyncBackgroundJob<object>.ExecuteAsync))
                        {
                            var result = (Task)jobExecuteMethod.Invoke(job.Object, new[] { argsObj });
                            if (result != null) await result;
                        }
                        else
                        {
                            jobExecuteMethod.Invoke(job.Object, new[] { argsObj });
                        }

                        await _store.DeleteAsync(jobInfo);
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn(ex.Message, ex);

                        var nextTryTime = jobInfo.CalculateNextTryTime();
                        if (nextTryTime.HasValue)
                        {
                            jobInfo.NextTryTime = nextTryTime.Value;
                        }
                        else
                        {
                            jobInfo.IsAbandoned = true;
                        }

                        await TryUpdateAsync(jobInfo);

                        await EventBus.TriggerAsync(
                            this,
                            new AbpHandledExceptionData(
                                new BackgroundJobException(
                                    "A background job execution is failed. See inner exception for details. See BackgroundJob property to get information on the background job.",
                                    ex
                                )
                                {
                                    BackgroundJob = jobInfo,
                                    JobObject = job.Object
                                }
                            )
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);

                jobInfo.IsAbandoned = true;

                await TryUpdateAsync(jobInfo);
            }
        }

        private async Task TryUpdateAsync(BackgroundJobInfo jobInfo)
        {
            try
            {
                await _store.UpdateAsync(jobInfo);
            }
            catch (Exception updateEx)
            {
                Logger.Warn(updateEx.ToString(), updateEx);
            }
        }
    }
}