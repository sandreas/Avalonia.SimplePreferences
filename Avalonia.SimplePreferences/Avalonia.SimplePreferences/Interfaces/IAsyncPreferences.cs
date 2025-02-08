namespace Avalonia.SimplePreferences.Interfaces;

public interface IAsyncPreferences: IPreferences
{
    void RemoveAsync(string key, string? sharedName = null);

    void ClearAsync(string? sharedName = null);

    void SetAsync<T>(string key, T value, string? sharedName = null);

    T GetAsync<T>(string key, T defaultValue, string? sharedName = null);
}