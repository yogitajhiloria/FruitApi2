using FruitApi.Bussiness.Interface;
using FruitApi.Bussiness.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitApi.Bussiness
{
    public class DependencyInjectionService
    {
        public DependencyInjectionService() { }

        public static void AddBussinessDependency(IServiceCollection services) 
        {
            services.AddScoped<IApiService,BaseApiService>();
            services.AddScoped<IFruityviceService,FruityviceService>();
            
            
        }
    }
}
