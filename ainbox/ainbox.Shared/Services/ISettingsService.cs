namespace ainbox.Shared.Services;

public interface ISettingsService
{
    Task StoreSetting(string key, string value);
    Task<string?> GetSetting(string key);
}
