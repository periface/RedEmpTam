using Abp.Zero.EntityFramework;
using MercadoCinotam.Authorization.Roles;
using MercadoCinotam.Certifications.Entities;
using MercadoCinotam.MultiTenancy;
using MercadoCinotam.ProductFeatures.Entities;
using MercadoCinotam.Products.Entities;
using MercadoCinotam.Pyme.Entities;
using MercadoCinotam.Themes.Entities;
using MercadoCinotam.Users;
using System.Data.Common;
using System.Data.Entity;

namespace MercadoCinotam.EntityFramework
{
    public class MercadoCinotamDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public IDbSet<Product> Products { get; set; }
        public IDbSet<ProductCertification> ProductCertifications { get; set; }
        public IDbSet<ProductFeatureSection> ProductFeatureSections { get; set; }
        public IDbSet<ProductSlider> ProductSliders { get; set; }
        public IDbSet<Feature> Features { get; set; }
        public IDbSet<Certification> Certifications { get; set; }
        public IDbSet<PymeInfo> PymeInfos { get; set; }
        public IDbSet<PymeContactInfo> PymeContactInfos { get; set; }
        public IDbSet<PymeOwner> PymeOwners { get; set; }
        public IDbSet<MainPageContent> MainPageContents { get; set; }
        public IDbSet<Theme> Themes { get; set; }
        public IDbSet<ThemeRequiredField> ThemeRequiredFields { get; set; }
        public IDbSet<ThemePreview> ThemePreviews { get; set; }
        public MercadoCinotamDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in MercadoCinotamDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of MercadoCinotamDbContext since ABP automatically handles it.
         */
        public MercadoCinotamDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public MercadoCinotamDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
