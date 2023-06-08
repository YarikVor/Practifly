namespace PractiFly.WebApi.Services.AuthenticationOptions;

public abstract class ConfigurationAbstraction
{
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly IConfigurationSection Section;

    protected ConfigurationAbstraction(IConfiguration configuration)
    {
        Section = configuration.GetSectionEx(SectionName);
    }

    protected abstract string SectionName { get; }

    protected string GetValue(string key)
    {
        return Section.GetValueEx(key);
    }
}