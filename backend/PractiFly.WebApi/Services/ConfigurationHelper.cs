namespace PractiFly.WebApi.Services;

internal static class ConfigurationHelper
{
    public const string SectionNotFoundMessage = "Section {0} not found in configuration";
    public const string KeyNotFoundMessage = "Key {0} not found in {1} section";


    public static string SectionNotFound(string sectionName)
    {
        return string.Format(SectionNotFoundMessage, sectionName);
    }

    public static string KeyNotFound(string key, string sectionName)
    {
        return string.Format(KeyNotFoundMessage, key, sectionName);
    }

    public static Exception SectionNotFoundException(string sectionName)
    {
        return new NullReferenceException(SectionNotFound(sectionName));
    }

    public static Exception KeyNotFoundException(string key, string sectionName)
    {
        return new NullReferenceException(KeyNotFound(key, sectionName));
    }

    public static IConfigurationSection GetSectionEx(this IConfiguration configuration, string sectionName)
    {
        return configuration.GetSection(sectionName)
               ?? throw SectionNotFoundException(sectionName);
    }

    public static string GetValueEx(this IConfigurationSection section, string key)
    {
        return section[key]
               ?? throw KeyNotFoundException(key, section.Path);
    }
}