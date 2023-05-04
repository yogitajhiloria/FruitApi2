namespace FruitApi.Bussiness
{
    public class BaseApiService : IApiService
    {
        private readonly ILogger<BaseApiService> _logger;

        public BaseApiService(ILogger<BaseApiService> logger) 
        {
            _logger = logger;    
        }

        public IApiService AddAdditionalHeaders(Dictionary<string, string> headers)
        {
            return this;
        }
        public IApiService Configure(string baseurl)
        {
            return this;
        }
        public IApiService UpdateUrl(string subUrl)
        {
            return this;
        }
        public Task<T> GetData<T>()
        {

        }
        public Task<Response> PostData<Request,Response>(Request request)
        {

        }

        //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-7.0
        //https://github.com/ardalis/CleanArchitecture/tree/main/src/Clean.Architecture.Core/Interfaces
    }
}