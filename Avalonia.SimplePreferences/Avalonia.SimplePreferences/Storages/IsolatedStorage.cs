using Avalonia.SimplePreferences.Interfaces;

namespace Avalonia.SimplePreferences.Storages;

public class IsolatedStorage: IAsyncPreferences
{
    public bool ContainsKey(string key, string? sharedName = null)
    {
        throw new NotImplementedException();
    }

    public void Remove(string key, string? sharedName = null)
    {
        throw new NotImplementedException();
    }

    public void Clear(string? sharedName = null)
    {
        throw new NotImplementedException();
    }

    public void Set<T>(string key, T value, string? sharedName = null)
    {
        throw new NotImplementedException();
    }

    public T Get<T>(string key, T defaultValue, string? sharedName = null)
    {
        throw new NotImplementedException();
    }

    public void RemoveAsync(string key, string? sharedName = null)
    {
        throw new NotImplementedException();
    }

    public void ClearAsync(string? sharedName = null)
    {
        throw new NotImplementedException();
    }

    public void SetAsync<T>(string key, T value, string? sharedName = null)
    {
        throw new NotImplementedException();
    }

    public T GetAsync<T>(string key, T defaultValue, string? sharedName = null)
    {
        throw new NotImplementedException();
    }
}