namespace PractiFly.WebApi.Services.AuthenticationOptions;

public abstract class ConfigurationAbstraction
{
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly IConfigurationSection _section;

    protected ConfigurationAbstraction(IConfiguration configuration)
    {
        _section = configuration.GetSectionEx(SectionName);
    }

    protected abstract string SectionName { get; }

    protected string GetValue(string key)
    {
        return _section.GetValueEx(key);
    }
}