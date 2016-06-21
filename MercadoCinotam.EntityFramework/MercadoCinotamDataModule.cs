using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using MercadoCinotam.EntityFramework;

namespace MercadoCinotam
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(MercadoCinotamCoreModule))]
    public class MercadoCinotamDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MercadoCinotamDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
