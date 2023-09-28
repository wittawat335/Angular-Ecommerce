﻿using Ecommerce.Core.AutpMapper;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce.Core
{
    public static class ServiceExtentions
    {
        public static void AddAppSetting(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        }
        public static void AddService(this IServiceCollection services)
        {   //Scoped : จะถูกสร้างใหม่ทุกครั้งที่ Client Request (1 Connection = 1 Client Request)
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IAuthenService, AuthenService>();
            //services.AddTransient<IEmailService, EmailService>();
            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommonService, CommonService>();
        }

        public static void AuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });
        }
    }
}
