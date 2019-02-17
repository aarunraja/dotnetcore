using Bapatla.CMS.Contract;
using Bapatla.CMS.Domain;
using Bapatla.CMS.Repository;
using Bapatla.CMS.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bapatla.CMS.WebApi
{
    public static class ServiceDependency
    {
        public static IServiceCollection InjectServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<BapatlaDataContext>();
            services.AddScoped<IPagesRepository, PagesRepository>();

            return services;
        }
    }
}
