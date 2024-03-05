using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DPS.Park.Application.Shared;
using DPS.Park.Core;

namespace DPS.Park.Application
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(typeof(ParkApplicationSharedModule), typeof(ParkCoreModule))]
    public class ParkApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ParkApplicationModule).GetAssembly());
        }
    }
}