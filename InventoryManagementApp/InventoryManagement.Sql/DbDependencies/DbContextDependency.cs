﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static InventoryManagement.Sql.Entities.Identities.IdentityModel;

namespace InventoryManagement.Sql.DbDependencies
{
    public static class DbContextDependency
    {
        public static void AddDbContextDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("WebAppConnection");
            services.AddDbContext<WebAppContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.LogTo(Console.WriteLine);
                options.UseSqlServer(connectionString);
            });

            services
             .AddIdentity<User, Role>(options =>
             {
                 options.Password.RequiredLength = 6;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireUppercase = false;
             })
             .AddEntityFrameworkStores<WebAppContext>()
             .AddDefaultTokenProviders();

        }
    }

}
