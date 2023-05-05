namespace FruitApi.Bussiness
{
    public interface IApiService
    {
        IApiService AddAdditionalHeaders(Dictionary<string, string> headers);
        IApiService Configure(string baseurl);
        
        Task<T> GetData<T>(string subUrl, CancellationToken cancellationToken);
        Task<Response> PostData<Request,Response>(Request request, string subUrl, CancellationToken cancellationToken);
    }
}