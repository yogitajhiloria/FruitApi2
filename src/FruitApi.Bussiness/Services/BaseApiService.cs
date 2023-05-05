using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace FruitApi.Bussiness
{
    public class BaseApiService : IApiService
    {
        private readonly ILogger<BaseApiService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private Dictionary<string, string> requestHeaders;
        private string apiBaseUrl;

        public BaseApiService(ILogger<BaseApiService> logger, IHttpClientFactory httpClientFactory) 
        {
            _logger = logger;    
            this._httpClientFactory = httpClientFactory;
            requestHeaders = new Dictionary<string, string>();
        }

        public IApiService AddAdditionalHeaders(Dictionary<string, string> headers)
        {
            if (headers == null || headers.Count == 0)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            this.requestHeaders = headers;
            return this;
        }
        public IApiService Configure(string baseurl)
        {
            if (string.IsNullOrEmpty(baseurl))
            {
                throw new ArgumentNullException(nameof(baseurl));
            }
            this.apiBaseUrl = baseurl;
            return this;
        }
        
        public async Task<T> GetData<T>(string subUrl, CancellationToken cancellationToken)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = this.GenerateUrl(subUrl);
                    AddHeader(httpClient);
                    using (var httpResponseMessage = await httpClient.GetAsync(string.Empty, cancellationToken))
                    {
                        return await this.HandleResponse<T>(httpResponseMessage, httpClient.BaseAddress.ToString(), cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error Occured in Get");
                throw;
            }
            
        }
        public async Task<Response> PostData<Request, Response>(Request request, string subUrl, CancellationToken cancellationToken)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = this.GenerateUrl(subUrl);
                    AddHeader(httpClient); 
                    var requestContent = new StringContent(JsonSerializer.Serialize(request),Encoding.UTF8,Application.Json);

                    using (var httpResponseMessage = await httpClient.PostAsync(string.Empty, requestContent, cancellationToken))
                    {
                        return await this.HandleResponse<Response>(httpResponseMessage, httpClient.BaseAddress.ToString(), cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error Occured in Post");
                throw;
            }
        }
        private async Task<T> HandleResponse<T>(HttpResponseMessage httpResponseMessage,string url, CancellationToken cancellationToken)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpResponseMessage it = httpResponseMessage;
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);
                T response = await JsonSerializer.DeserializeAsync<T>(contentStream, 
                    options: new JsonSerializerOptions() 
                { 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }, cancellationToken: cancellationToken);
                return response;
            }
            else
            {
                this._logger.LogError("Api returned with failed status url: {0}, status: {1}, Details: {2}", url, httpResponseMessage.StatusCode, httpResponseMessage.ToString());
            }
            return default(T);
        }
        private void AddHeader(HttpClient httpClient)
        {
            foreach (var item in this.requestHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
        }

       
        
        private Uri GenerateUrl(string subUrl)
        {
            return new Uri(string.Format($"{this.apiBaseUrl}/{subUrl}"));
        }
        

    }
}