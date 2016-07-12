using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using MercadoCinotam.Authorization;
using MercadoCinotam.Authorization.Roles;
using MercadoCinotam.Editions;
using MercadoCinotam.MultiTenancy.Dto;
using MercadoCinotam.Pyme;
using MercadoCinotam.StartupSettings;
using MercadoCinotam.Themes;
using MercadoCinotam.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MercadoCinotam.MultiTenancy
{
    public class TenantAppService : MercadoCinotamAppServiceBase, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly RoleManager _roleManager;
        private readonly EditionManager _editionManager;
        private readonly PymeProvider _pymeProvider;
        private readonly ThemeProvider _themeProvider;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly ISettingStore _settingStore;
        public TenantAppService(
            TenantManager tenantManager,
            RoleManager roleManager,
            EditionManager editionManager,
            IAbpZeroDbMigrator abpZeroDbMigrator, ISettingStore settingStore, PymeProvider pymeProvider, ThemeProvider themeProvider)
        {
            _tenantManager = tenantManager;
            _roleManager = roleManager;
            _editionManager = editionManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _settingStore = settingStore;
            _pymeProvider = pymeProvider;
            _themeProvider = themeProvider;
        }

        [AbpAuthorize(PermissionNames.Pages_Tenants)]
        public ListResultOutput<TenantListDto> GetTenants()
        {
            return new ListResultOutput<TenantListDto>(
                _tenantManager.Tenants
                    .OrderBy(t => t.TenancyName)
                    .ToList()
                    .MapTo<List<TenantListDto>>()
                );
        }

        [AbpAuthorize(PermissionNames.Pages_Tenants)]
        public async Task CreateTenant(CreateTenantInput input)
        {
            //Create tenant
            var tenant = input.MapTo<Tenant>();
            tenant.ConnectionString = input.ConnectionString.IsNullOrEmpty()
                ? null
                : SimpleStringCipher.Instance.Encrypt(input.ConnectionString);

            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            CheckErrors(await TenantManager.CreateAsync(tenant));
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new tenant's id.

            //Create tenant database
            _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

            //We are working entities of new tenant, so changing tenant filter
            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                //Create static roles for new tenant
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync(); //To get static role ids

                //grant all permissions to admin role
                var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                //Create admin user for the tenant
                var adminUser = User.CreateTenantAdminUser(tenant.Id, input.AdminEmailAddress, User.DefaultPassword);
                CheckErrors(await UserManager.CreateAsync(adminUser));
                await CurrentUnitOfWork.SaveChangesAsync(); //To get admin user's id

                //Assign admin user to role!
                CheckErrors(await UserManager.AddToRoleAsync(adminUser.Id, adminRole.Name));
                await CurrentUnitOfWork.SaveChangesAsync();

                //Assign Default Theme
                await _settingStore.CreateAsync(Settings.ThemeInitialConfig(tenant.Id));
                var theme = await _themeProvider.GetThemeByThemeName(ValueConst.MainTheme, HttpContext.Current.Server);
                _pymeProvider.SetMainPageContent(theme, false);
                //Paypal config
                await _settingStore.CreateAsync(Settings.PayPalInitialConfig(tenant.Id));
                await _settingStore.CreateAsync(Settings.PayPalSecretKey(tenant.Id));


            }
        }







        public async Task<int?> GetTenantIdByName(string tenancyName)
        {
            var tenancy = await _tenantManager.FindByTenancyNameAsync(tenancyName);
            return tenancy?.Id;
        }

        public async Task<string> GetTenantNameById(int tenantId)
        {
            var tenant = await _tenantManager.GetByIdAsync(tenantId);
            return tenant.Name;
        }
    }
}