using Abp.Modules;
using System.Reflection;

namespace ImageSaver
{
    public class ImageSaverModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
