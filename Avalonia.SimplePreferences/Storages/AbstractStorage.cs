using Avalonia.SimplePreferences.Helpers;
using Avalonia.SimplePreferences.Interfaces;

namespace Avalonia.SimplePreferences.Storages;

public abstract class AbstractStorage: IAsyncPreferences
{
    protected static bool HasTokenBeenCancelled(CancellationToken? ct)
    {
        return ct is { CanBeCanceled: true, IsCancellationRequested: true };
    }

    public abstract Task<bool> TryPersistAsync<T>(string key, T value, CancellationToken ct);
    public abstract Task<T?> LoadAsync<T>(string key, T? defaultValue, CancellationToken ct);

    public abstract bool ContainsKey(string key, string? sharedName = null);


    public void Set<T>(string key, T? value, string? sharedName = null) =>
        AsyncHelper.RunSync(() => SetAsync(key, value, sharedName));

    public virtual async Task<bool>
        SetAsync<T>(string key, T? value, string? sharedName, CancellationToken? ct = null) =>
        await TryPersistAsync(key, value, ct ?? CancellationToken.None);

    public T? Get<T>(string key, T? defaultValue, string? sharedName = null) =>
        AsyncHelper.RunSync(() => GetAsync(key, defaultValue, sharedName));

    public virtual async Task<T?> GetAsync<T>(string key, T? defaultValue, string? sharedName = null,
        CancellationToken? ct = null) =>
        !ContainsKey(key) ? defaultValue : await LoadAsync<T?>(key, defaultValue, ct ?? CancellationToken.None);

    public void Remove(string key, string? sharedName = null) =>
        AsyncHelper.RunSync(() => RemoveAsync(key, sharedName));

    public abstract Task<bool> RemoveAsync(string key, string? sharedName = null, CancellationToken? ct = null);


    public void Clear(string? sharedName = null) => AsyncHelper.RunSync(() => ClearAsync(sharedName));

    public abstract Task<int> ClearAsync(string? sharedName = null, CancellationToken? ct = null);
}