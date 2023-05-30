namespace WebApplication2.Services.Interfaces
{
    public interface ICacheService
    {
        void AddToCache(string key, object value);

        object GetFromCache(string key);

        void RemoveFromCache(string key);
    }
}
