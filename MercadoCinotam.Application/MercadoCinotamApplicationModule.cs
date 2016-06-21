using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace MercadoCinotam
{
    [DependsOn(typeof(MercadoCinotamCoreModule), typeof(AbpAutoMapperModule))]
    public class MercadoCinotamApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
