using Ecommerce.Core.AutpMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core
{
    public static class ServiceExtentions
    {
        public static void AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            //services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // Add Service
            //services.AddTransient<IEmailService, EmailService>();
            //services.AddScoped<IMasterService, MasterService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<ICommonService, CommonService>();
        }
    }
}
