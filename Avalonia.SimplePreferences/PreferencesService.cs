using Avalonia.SimplePreferences.Interfaces;

namespace Avalonia.SimplePreferences;

public class PreferencesService : IAsyncPreferences
{
    private IAsyncPreferences Current { get; set; }

    public PreferencesService(IAsyncPreferences? current = null)
    {
        Current = current ?? Preferences.Current;
    }

    public bool ContainsKey(string key, string? sharedName = null) => Current.ContainsKey(key, sharedName);

    public void Remove(string key, string? sharedName = null) => Current.Remove(key, sharedName);

    public void Clear(string? sharedName = null) => Current.Clear();

    public void Set<T>(string key, T value, string? sharedName = null) => Current.Set(key, value, sharedName);

    public T? Get<T>(string key, T? defaultValue, string? sharedName = null) =>
        Current.Get<T>(key, defaultValue, sharedName);

    public Task<bool> RemoveAsync(string key, string? sharedName = null, CancellationToken? cancellationToken = null) =>
        Current.RemoveAsync(key, sharedName, cancellationToken);

    public Task<int> ClearAsync(string? sharedName = null, CancellationToken? cancellationToken = null) =>
        Current.ClearAsync(sharedName, cancellationToken);

    public Task<bool> SetAsync<T>(string key, T value, string? sharedName = null,
        CancellationToken? cancellationToken = null) => Current.SetAsync(key, value, sharedName, cancellationToken);

    public Task<T?> GetAsync<T>(string key, T defaultValue, string? sharedName = null,
        CancellationToken? cancellationToken = null) =>
        Current.GetAsync<T>(key, defaultValue, sharedName, cancellationToken);
}