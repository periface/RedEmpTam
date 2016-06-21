using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using MercadoCinotam.EntityFramework;

namespace MercadoCinotam.Migrator
{
    [DependsOn(typeof(MercadoCinotamDataModule))]
    public class MercadoCinotamMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<MercadoCinotamDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}