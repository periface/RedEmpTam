using Abp.Modules;
using System.Reflection;

namespace FileSaver
{
    public class FileManagerModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
