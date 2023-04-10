using Practifly.Checkers.Abstractions;
using Practifly.Checkers.Options;

namespace Practifly.Checkers.Builder;

public class CheckerOptionBuilder : ICheckerOptionBuilder
{
    private CheckerOptions? _checkerOptions;

    ICheckerOptionBuilder ICheckerOptionBuilder.SkipType<T>()
    {
        _checkerOptions!.AddSkipType(typeof(T));

        return this;
    }

    ICheckerOptionBuilder ICheckerOptionBuilder.SkipType(Type type)
    {
        _checkerOptions!.AddSkipType(type);

        return this;
    }

    ICheckerOptionBuilder ICheckerOptionBuilder.SkipNullableTypes()
    {
        _checkerOptions!.SetSkipNullableTypes();

        return this;
    }

    ICheckerOptionBuilder ICheckerOptionBuilder.SkipStartWith(string startWith)
    {
        _checkerOptions!.AddSkipStartWith(startWith);

        return this;
    }

    ICheckerOptionBuilder ICheckerOptionBuilder.SkipEndWith(string endWith)
    {
        _checkerOptions!.AddSkipEndWith(endWith);

        return this;
    }

    ICheckerOptionBuilder ICheckerOptionBuilder.SkipSubstring(string substring)
    {
        _checkerOptions!.AddSkipSubstring(substring);

        return this;
    }

    ICheckerOptionBuilder ICheckerOptionBuilder.SetDefaultValue<TDefault>(TDefault defaultValue)
    {
        _checkerOptions!.AddDefaultValue(typeof(TDefault), defaultValue);

        return this;
    }

    ICheckerOptionBuilder ICheckerOptionBuilder.SetDefaultValue(Type type, object? defaultValue)
    {
        _checkerOptions!.AddDefaultValue(type, defaultValue);

        return this;
    }

    ICheckerOptions ICheckerOptionBuilder.Build()
    {
        var options = _checkerOptions!;

        _checkerOptions = null;

        return options;
    }


    public ICheckerOptionBuilder Init()
    {
        _checkerOptions ??= new CheckerOptions();

        return this;
    }
}