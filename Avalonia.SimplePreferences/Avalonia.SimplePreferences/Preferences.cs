using System.Runtime.InteropServices;
using Avalonia.SimplePreferences.Interfaces;
using Avalonia.SimplePreferences.Storages;

namespace Avalonia.SimplePreferences;

public static class Preferences
{
    public static IAsyncPreferences Current { get; set; }

    static Preferences()
    {
        Current = InitStorage();
    }

    private static IAsyncPreferences InitStorage() =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER"))
            ? new BrowserStorage()
            : new IsolatedStorage();


    public static bool ContainsKey(string key, string? sharedName = null) => Current.ContainsKey(key, sharedName);

    public static void Remove(string key, string? sharedName = null) => Current.Remove(key, sharedName);

    public static void Clear(string? sharedName = null) => Current.Clear(sharedName);

    public static void Set<T>(string key, T value, string? sharedName = null) => Current.Set(key, value, sharedName);

    public static T Get<T>(string key, T defaultValue, string? sharedName = null) =>
        Current.Get(key, defaultValue, sharedName);


    public static void RemoveAsync(string key, string? sharedName = null) => Current.RemoveAsync(key, sharedName);

    public static void ClearAsync(string? sharedName = null) => Current.ClearAsync(sharedName);

    public static void SetAsync<T>(string key, T value, string? sharedName = null) =>
        Current.SetAsync(key, value, sharedName);

    public static T GetAsync<T>(string key, T defaultValue, string? sharedName = null) =>
        Current.GetAsync(key, defaultValue, sharedName);
}