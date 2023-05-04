namespace FruitApi.Bussiness
{
    public interface IApiService
    {
        IApiService AddAdditionalHeaders(Dictionary<string, string> headers);
        IApiService Configure(string baseurl);
        IApiService UpdateUrl(string subUrl);
        Task<T> GetData<T>();
        Task<Response> PostData<Request,Response>(Request request);
    }
}