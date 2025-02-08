namespace Avalonia.SimplePreferences.Interfaces;

public interface IAsyncPreferences: IPreferences
{
    Task<bool> RemoveAsync(string key, string? sharedName = null, CancellationToken? cancellationToken = null);

    Task<int> ClearAsync(string? sharedName = null, CancellationToken? cancellationToken = null);

    Task<bool> SetAsync<T>(string key, T value, string? sharedName = null, CancellationToken? cancellationToken = null);

    Task<T?> GetAsync<T>(string key, T defaultValue, string? sharedName = null, CancellationToken? cancellationToken = null);
}