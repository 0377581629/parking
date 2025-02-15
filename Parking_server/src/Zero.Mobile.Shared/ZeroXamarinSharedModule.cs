﻿using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Zero
{
    [DependsOn(typeof(ZeroClientModule), typeof(AbpAutoMapperModule))]
    public class ZeroXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroXamarinSharedModule).GetAssembly());
        }
    }
}