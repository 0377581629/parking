using Abp.Modules;
using Abp.Reflection.Extensions;
using DPS.Park.Core.Shared;

namespace DPS.Park.Core
{
    [DependsOn(typeof(ParkCoreSharedModule))]
    public class ParkCoreModule: AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ParkCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
        }
    }
}