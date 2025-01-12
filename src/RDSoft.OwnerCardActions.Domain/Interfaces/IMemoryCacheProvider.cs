namespace RDSoft.OwnerCardActions.Domain.Interfaces;

public interface IMemoryCacheProvider
{
    T Get<T>(string key);
    
    void Set<T>(string key, T value);

    bool TryGetValue<T>(string key, out T value);

    void Remove(string key);
}