using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DPS.Park.Core.Shared
{
    public class ParkCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ParkCoreSharedModule).GetAssembly());
        }
    }
}