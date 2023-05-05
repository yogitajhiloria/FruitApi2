using FruitApi.Bussiness.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitApi.Bussiness.Services
{
    public class FruitApiSettings
    {
      
        public string Baseurl { get; set; }
            //configuration.GetSection(FruitApiSettingSectionName)?.GetSection("baseurl")?.Value; 
    }
}
