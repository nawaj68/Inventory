using InventoryManagement.Core.Sqls;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Sql.DbDependencies
{
    public static class RepositoryDependency
    {
        public static void AddRepositoryDependency(this IServiceCollection services)
        {
            services.AddScoped(typeof(ISqlRepository<>), typeof(SqlRepository<>));
            //services.AddScoped<IRequestLogRepository, RequestLogRepository>();
        }
    }
}
