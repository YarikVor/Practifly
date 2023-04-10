using System.Linq.Expressions;
using System.Reflection;
using Practifly.Checkers.Abstractions;

namespace Practifly.Checkers;

public class Checker
{
    private readonly ICheckerOptions _checkerOptions;

    public Checker(ICheckerOptions checkerOptions)
    {
        _checkerOptions = checkerOptions;
    }

    public void Check<TEntity>(TEntity entity)
    {
        Check(entity, Array.Empty<Expression<Func<TEntity, object>>>());
    }

    public void Check<TEntity>(
        TEntity entity,
        params Expression<Func<TEntity, object>>[] ignoreProperties)
    {
        var entityType = typeof(TEntity);
        var properties = entityType.GetProperties().ToList();

        var ignorePropertiesAsMember = ConvertToMemberExpressions(ignoreProperties);

        SkipByExceptions(ignorePropertiesAsMember, properties);
        SkipNullableIfSet(properties);
        SkipByTypes(properties);
        SkipByStartWith(properties);
        SkipByEndWith(properties);
        SkipBySubstring(properties);

        var emptyProperties = GetNameEmptyProperties(entity, properties);
        if (emptyProperties.Any())
            throw new CheckerPropertiesException(
                $"Class '{entity!.GetType().Name}' has empty properties: {string.Join(", ", emptyProperties)}"
            );
    }

    private void SkipNullableIfSet(List<PropertyInfo> properties)
    {
        if (_checkerOptions.SkipNullableTypes)
            properties.RemoveAll(info => Nullable.GetUnderlyingType(info.PropertyType) != null);
    }

    private static IEnumerable<MemberExpression> ConvertToMemberExpressions<TEntity>(
        Expression<Func<TEntity, object>>[] ignoreProperties)
    {
        return ignoreProperties
            .Select(e => e.Body switch
            {
                UnaryExpression { Operand: MemberExpression memberBody } => memberBody,
                MemberExpression memberBody => memberBody,
                _ => throw new TypeAccessException("Expression isn't member")
            });
    }

    private List<string> GetNameEmptyProperties<TEntity>(TEntity entity, List<PropertyInfo> properties)
    {
        return properties
            .Where(propertyInfo =>
            {
                var value = propertyInfo.GetValue(entity);

                if (propertyInfo.PropertyType.IsValueType)
                {
                    if (_checkerOptions.DefaultValues.TryGetValue(propertyInfo.PropertyType, out var defaultValue))
                        return value!.Equals(defaultValue);

                    return value?.Equals(Activator.CreateInstance(propertyInfo.PropertyType)) ?? true;
                }

                return value == null;
            })
            .Select(propertyInfo => propertyInfo.Name)
            .ToList();
    }

    private void SkipBySubstring(List<PropertyInfo> properties)
    {
        foreach (var substring in _checkerOptions.SkipSubstrings) properties.RemoveAll(e => e.Name.Contains(substring));
    }

    private void SkipByEndWith(List<PropertyInfo> properties)
    {
        foreach (var endWith in _checkerOptions.SkipEndsWith) properties.RemoveAll(e => e.Name.EndsWith(endWith));
    }

    private void SkipByStartWith(List<PropertyInfo> properties)
    {
        foreach (var startWith in _checkerOptions.SkipStartsWith)
            properties.RemoveAll(e => e.Name.StartsWith(startWith));
    }

    private void SkipByTypes(List<PropertyInfo> properties)
    {
        foreach (var type in _checkerOptions.SkipTypes) properties.RemoveAll(e => e.PropertyType == type);
    }

    private static void SkipByExceptions(IEnumerable<MemberExpression> ignorePropertiesAsMember,
        List<PropertyInfo> properties)
    {
        var ignorePropertiesNames
            = ignorePropertiesAsMember
                .Select(e => e.Member.Name)
                .ToList();

        properties.RemoveAll(p => ignorePropertiesNames.Contains(p.Name));
    }
}