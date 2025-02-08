namespace Avalonia.SimplePreferences.Interfaces;

public interface IPreferences
{
    bool ContainsKey(string key, string? sharedName = null);
    
    void Remove(string key, string? sharedName = null);

    void Clear(string? sharedName = null);

    void Set<T>(string key, T value, string? sharedName = null);

    T? Get<T>(string key, T? defaultValue, string? sharedName = null);
}