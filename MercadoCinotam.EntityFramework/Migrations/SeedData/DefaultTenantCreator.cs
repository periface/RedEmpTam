using System.Linq;
using MercadoCinotam.EntityFramework;
using MercadoCinotam.MultiTenancy;

namespace MercadoCinotam.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly MercadoCinotamDbContext _context;

        public DefaultTenantCreator(MercadoCinotamDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
