using System.IO.IsolatedStorage;
using System.Text.Json;

namespace Avalonia.SimplePreferences.Storages;

public class IsolatedStorage: AbstractStorage
{
    private static IsolatedStorageFile Store => IsolatedStorageFile.GetUserStoreForDomain();
    private static readonly SemaphoreSlim Sema = new(1, 1);

    public override bool ContainsKey(string key, string? sharedName = null) => Store.FileExists(key);

    public override async Task<bool> RemoveAsync(string key, string? sharedName = null, CancellationToken? ct = null)
    {
        await Sema.WaitAsync(ct ?? CancellationToken.None);
        try
        {
            if (!ContainsKey(key) || HasTokenBeenCancelled(ct)) return false;
            Store.DeleteFile(key);
            return true;
        }
        finally
        {
            Sema.Release();
        }
    }

    public override async Task<int> ClearAsync(string? sharedName = null, CancellationToken? ct = null)
    {
        await Sema.WaitAsync(ct ?? CancellationToken.None);
        try
        {
            var fileNames = Store.GetFileNames();
            foreach (var file in fileNames)
            {
                if (HasTokenBeenCancelled(ct))
                {
                    return -1;
                }
                Store.DeleteFile(file);
            }

            return fileNames.Length;
        }
        catch (Exception)
        {
            return -1;
        }
        finally
        {
            Sema.Release();
        }
    }
    
    public override async Task<bool> TryPersistAsync<T>(string key, T value, CancellationToken ct)
    {
        await Sema.WaitAsync(ct);
        try
        {
            await using var stream = Store.OpenFile(key, FileMode.Create, FileAccess.Write);
            await JsonSerializer.SerializeAsync(stream, value, (JsonSerializerOptions?)null, ct);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            Sema.Release();
        }
    }

    public override async Task<T?> LoadAsync<T>(string key, T? defaultValue, CancellationToken ct) where T: default
    {
        await Sema.WaitAsync(ct);

        // it may happen, that a value type changes and can't be deserialized
        // so prevent exceptions in this case
        try
        {
            await using var stream = Store.OpenFile(key, FileMode.Open);
            return await JsonSerializer.DeserializeAsync<T>(stream, (JsonSerializerOptions?)null, ct);
        }
        catch (Exception)
        {
            return defaultValue;
        }
        finally
        {
            Sema.Release();
        }
    }
}