# Avalonia.SimplePreferences
Cross-Platform Preferences library for Avalonia


# TL;DR

Here is how to use it.


### Default API
The default API aims to be compatible with MAUI `IPreferences` interface with some async extensions (see below).

```c#
var defaultValue = -1;
        
// retrieve value
var counter = Preferences.Get("Counter", defaultValue);
if (counter == defaultValue)
{
    // store value
    Preferences.Set("Counter", 1);
}

// remove single value
Preferences.Remove("Counter");

// remove all values
Preferences.Clear();
```

### Async API
The async API works similar to the default API with some extensions regarding return types and cancellation tokens.

```c#
var cts = new CancellationTokenSource();
var defaultValue = -1;

// retrieve value
var counter = await Preferences.GetAsync("Counter", defaultValue, null, cts.Token);
if (counter == defaultValue)
{
    // store value (with success indicator)
    var success = await Preferences.SetAsync("Counter", 1, null, cts.Token);
    if (!success)
    {
        Console.WriteLine("Failed to set counter");
        return;
    }
}

// remove single value (with success indicator)
var removeSuccess = await Preferences.RemoveAsync("Counter");
if (!removeSuccess)
{
    Console.WriteLine("Failed to remove counter");
    return;
}

// remove all values (with removed items count)
// Note: This value is not accurate on some platforms, e.g. browser)
var removedItemsCount = await Preferences.ClearAsync();
Console.WriteLine($"Removed items: {removedItemsCount} ");
```

### IoC / Dependency Injection
If you prefer IoC / Dependency injection for some reason (e.g. Unit-Testing), you can do the following:
```c#
// Program.cs
services.AddSingleton<PreferencesService>();

// ViewModels/HomeViewModel.cs
public partial class HomeViewModel : ViewModelBase
{
    private readonly PreferencesService _preferences;

    public HomeViewModel(PreferencesService prefs)
    {
        _preferences = prefs;
    }
    // ...
}
```

### Implementing storages

If you need to implement a custom storage, you can do this by implementing `IAsyncPreferences` or use the `AbstractStorage` for convenience.
It's recommended to [take a look at the existing storages to get an idea](https://github.com/sandreas/Avalonia.SimplePreferences/tree/main/Avalonia.SimplePreferences/Storages).

```c#
// MyCustomStorage.cs
class MyCustomStorage: AbstractStorage {
// ... implement mandatory members ...
}

// Program.cs
services.AddSingleton<PreferencesService>(s => new PreferencesService(new MyCustomStorage());
// alternatively: Preferences.Current = new MyCustomStorage();
```

