using Ecommerce.Domain.RepositoryContracts;
using Ecommerce.Infrastructure.DBContext;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure
{
    public static class ServiceExtentions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AngularEcommerceContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));
            //services.AddSingleton<DapperContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
