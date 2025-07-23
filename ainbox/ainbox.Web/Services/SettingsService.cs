using ainbox.Shared.Services;
using Microsoft.JSInterop;

namespace ainbox.Web.Services;

public class SettingsService : ISettingsService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly Dictionary<string, string> _cache = new();
    private bool _isInitialized = false;

    public SettingsService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string?> GetSetting(string key)
    {
        try
        {
            if (_jsRuntime is IJSInProcessRuntime inProcessRuntime)
            {
                return inProcessRuntime.Invoke<string?>("localStorage.getItem", key);
            }
            else
            {
                return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
            }
        }
        catch (InvalidOperationException)
        {
            // JS interop not available during prerendering, return cached value
            return _cache.TryGetValue(key, out var cachedValue) ? cachedValue : null;
        }
        catch (JSException)
        {
            return null;
        }
    }

    public async Task StoreSetting(string key, string value)
    {
        // Cache the value immediately
        _cache[key] = value;

        try
        {
            if (_jsRuntime is IJSInProcessRuntime inProcessRuntime)
            {
                inProcessRuntime.InvokeVoid("localStorage.setItem", key, value);
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
            }
        }
        catch (InvalidOperationException)
        {
            // JS interop not available during prerendering, value is cached for later
        }
        catch (JSException)
        {
            // Handle localStorage errors silently
        }
    }
}
