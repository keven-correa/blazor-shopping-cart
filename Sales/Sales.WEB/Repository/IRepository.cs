namespace Sales.WEB.Repository
{
    public interface IRepository
    {
        Task<HttpResponseMessages<T>> Get<T>(string url);
        Task<HttpResponseMessages<object>>Post<T>(string url, T model);
        Task<HttpResponseMessages<TResponse>> Post<T, TResponse>(string url, T model);
    }
}
