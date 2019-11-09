using MLC.General.Contract;
using MLC.General.Domain;
using MLC.General.Repository;
using MLC.General.Services;
using MLC.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLC.General.WebApi
{
    /// <summary>
    /// Service Dependency
    /// </summary>
    public static class ServiceDependency
    {
        /// <summary>
        /// InjectService Dependency
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InjectServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<MongoDatabaseContext>();

            services.AddScoped<ApplicationContext>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<IApplicationSettingRepository, ApplicationSettingRepository>();
            
            return services;
        }
    }
}
