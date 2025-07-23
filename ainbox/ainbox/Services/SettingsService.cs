using ainbox.Shared.Services;
using Microsoft.Maui.Storage;

namespace ainbox.Services;

public class SettingsService : ISettingsService
{
    public async Task<string?> GetSetting(string key)
    {
        try
        {
            return await SecureStorage.GetAsync(key);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task StoreSetting(string key, string value)
    {
        try
        {
            await SecureStorage.SetAsync(key, value);
        }
        catch (Exception)
        {
            // Handle SecureStorage errors silently
        }
    }
}
