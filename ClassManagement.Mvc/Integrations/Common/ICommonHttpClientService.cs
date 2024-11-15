namespace ClassManagement.Mvc.Integrations.Common
{
    public interface ICommonHttpClientService<T, TKey> where T : class
    {
        Task<T> GetByIdAsync(TKey id);
    }
}
