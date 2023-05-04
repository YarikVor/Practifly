﻿using Practifly.Checkers.Abstractions;

namespace Practifly.Checkers.Options;

public class CheckerOptions : ICheckerOptions
{
    private readonly Dictionary<Type, object?> _defaultValues = new();
    private readonly List<string> _skipEndsWith = new();
    private readonly List<string> _skipStartsWith = new();
    private readonly List<string> _skipSubstrings = new();
    private readonly List<Type> _skipTypes = new();
    private bool _skipNullableTypes;

    IReadOnlyCollection<Type> ICheckerOptions.SkipTypes => _skipTypes;

    IReadOnlyCollection<string> ICheckerOptions.SkipStartsWith => _skipStartsWith;

    IReadOnlyCollection<string> ICheckerOptions.SkipEndsWith => _skipEndsWith;

    IReadOnlyCollection<string> ICheckerOptions.SkipSubstrings => _skipSubstrings;

    bool ICheckerOptions.SkipNullableTypes => _skipNullableTypes;

    IReadOnlyDictionary<Type, object?> ICheckerOptions.DefaultValues => _defaultValues;

    internal void AddSkipType(Type type)
    {
        _skipTypes.Add(type);
    }

    internal void AddSkipStartWith(string startWith)
    {
        _skipStartsWith.Add(startWith);
    }

    internal void AddSkipEndWith(string endWith)
    {
        _skipEndsWith.Add(endWith);
    }

    internal void AddSkipSubstring(string contains)
    {
        _skipSubstrings.Add(contains);
    }

    internal void SetSkipNullableTypes()
    {
        _skipNullableTypes = true;
    }

    internal void AddDefaultValue(Type type, object? value)
    {
        _defaultValues.Add(type, value);
    }
}