using InventoryManagement.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Core.Acls;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Service.Services.Configurations;
using InventoryManagement.Service.Services.EnrolConfigurations;
using InventoryManagement.Service.Services;

namespace InventoryManagement.Service.Dependency
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ISignInHelper, SignInHelper>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
           
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IOpenQuantityService, OpenQuantityService>();
            services.AddScoped<IWareHouseRepository, WareHouseRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IPurchasesMasterService, PurchasesMasterService>();
            services.AddScoped<IReturnService, ReturnService>();
            services.AddScoped<ISalesService, SalesService>();
            services.AddScoped<IPurchaseDetailsService, PurchaseDetailsService>();
            services.AddScoped<IDamageService, DamageService>();

        }
    }
}
