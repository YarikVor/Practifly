namespace Practifly.Checkers.Abstractions;

public interface ICheckerOptionBuilder
{
    ICheckerOptionBuilder SkipType<T>();
    ICheckerOptionBuilder SkipType(Type type);

    [Obsolete("Use SkipType<T>() instead")]
    ICheckerOptionBuilder SkipNullableTypes();

    ICheckerOptionBuilder SkipStartWith(string startWith);
    ICheckerOptionBuilder SkipEndWith(string endWith);
    ICheckerOptionBuilder SkipSubstring(string contains);
    ICheckerOptionBuilder SetDefaultValue<TDefault>(TDefault defaultValue);
    ICheckerOptionBuilder SetDefaultValue(Type type, object defaultValue);

    ICheckerOptions Build();
}