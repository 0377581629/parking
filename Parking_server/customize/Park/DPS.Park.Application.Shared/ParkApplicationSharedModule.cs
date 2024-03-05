using Abp.Modules;
using Abp.Reflection.Extensions;
using DPS.Park.Core.Shared;

namespace DPS.Park.Application.Shared
{
    [DependsOn(typeof(ParkCoreSharedModule))]
    public class ParkApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ParkApplicationSharedModule).GetAssembly());
        }
    }
}