using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;

namespace Avalonia.SimplePreferences.Storages;

public partial class BrowserStorage: AbstractStorage
{
    public override async Task<bool> TryPersistAsync<T>(string key, T value, CancellationToken ct)
    {
        try
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, value, (JsonSerializerOptions?)null, ct);
            stream.Position = 0;
            var serializedObjJson = Encoding.UTF8.GetString(stream.ToArray());
            JsSetItem(key, serializedObjJson);
            return true;
        }
        catch (Exception)
        {
            return await Task.FromResult(false);
        }
    }

    public override async Task<T?> LoadAsync<T>(string key, T? defaultValue, CancellationToken ct) where T : default
    {
        try
        {
            var rawValue = JsGetItem(key);
            if (string.IsNullOrEmpty(rawValue))
            {
                return defaultValue;
            }
            
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(rawValue));
            return await JsonSerializer.DeserializeAsync<T?>(stream, cancellationToken: ct) ?? defaultValue;
        }
        catch (Exception)
        {
            return await Task.FromResult(defaultValue);
        }
    }

    public override bool ContainsKey(string key, string? sharedName = null) => JsGetItem(key) is not null;

    public override async Task<bool> RemoveAsync(string key, string? sharedName = null, CancellationToken? ct = null)
    {
        JsRemoveItem(key);
        return await Task.FromResult(!ContainsKey(key));
    }

    public override async Task<int> ClearAsync(string? sharedName = null, CancellationToken? ct = null)
    {
        JsClear();
        // todo: determine amount of deleted  values via window.localStorage.length
        return await Task.FromResult(int.MaxValue);
    }
    
    
    [JSImport("globalThis.localStorage.getItem")]
    private static partial string? JsGetItem(string key);
    
    [JSImport("globalThis.localStorage.setItem")]
    private static partial void JsSetItem(string key, string value);
    
    [JSImport("globalThis.localStorage.removeItem")]
    private static partial void JsRemoveItem(string key);
    
    [JSImport("globalThis.localStorage.clear")]
    private static partial void JsClear();

}