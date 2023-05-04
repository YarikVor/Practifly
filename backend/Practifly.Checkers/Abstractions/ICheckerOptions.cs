namespace Practifly.Checkers.Abstractions;

public interface ICheckerOptions
{
    IReadOnlyCollection<Type> SkipTypes { get; }
    IReadOnlyCollection<string> SkipStartsWith { get; }
    IReadOnlyCollection<string> SkipEndsWith { get; }
    IReadOnlyCollection<string> SkipSubstrings { get; }
    IReadOnlyDictionary<Type, object?> DefaultValues { get; }
    bool SkipNullableTypes { get; }
}