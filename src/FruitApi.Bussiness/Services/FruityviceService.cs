using FruitApi.Bussiness.Interface;
using FruitApi.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FruitApi.Bussiness.Services
{
    public class FruityviceService : IFruityviceService
    {
        private readonly ILogger<FruityviceService> logger;
        private readonly IApiService apiService;
        private readonly IOptions<FruitApiSettings> configuration;
        string getAllFruitUrl = "fruit/all";
        string getFruitWithFamilyUrl = "fruit/family/{0}";


        public FruityviceService(ILogger<FruityviceService> logger, IApiService apiService, IOptions<FruitApiSettings> configuration)
        {
            this.logger = logger;
            this.apiService = apiService;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<Fruits>> GetAllFruits(CancellationToken cancellationToken)
        {
            this.ConfigureService();
            return await this.apiService.GetData<IEnumerable<Fruits>>(getAllFruitUrl, cancellationToken);
        }

        public async Task<IEnumerable<Fruits>> RetriveAllFruitsForFamnily(string familyName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(familyName))
            {
                throw new ArgumentNullException(nameof(familyName));
            }
            this.ConfigureService();
            return await this.apiService.GetData<IEnumerable<Fruits>>(string.Format(getFruitWithFamilyUrl,familyName), cancellationToken);
        }
        private void ConfigureService()
        {
            this.apiService.Configure(this.configuration.Value.Baseurl)
                .AddAdditionalHeaders(new Dictionary<string, string>() { { HeaderNames.Accept, "application/json" } });
        }
    }
}
