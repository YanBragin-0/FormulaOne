namespace FormulaOne.Infrastructure.Storage.Redis
{
    public interface IRedisCache
    {
        Task Set<T>(string key, T val, TimeSpan? expiry = null);
        Task<List<T>?> Get<T>(string key);
        Task RemoveAllByPrefixAsync(string prefix);
        Task RemoveByPrefixAsync(string prefix);
    }
}
