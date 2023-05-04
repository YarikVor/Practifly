using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class TodayDateConstraintAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var date = value switch
        {
            DateOnly dateOnly => dateOnly,
            DateTime dateTime => DateOnly.FromDateTime(dateTime),
            _ => throw new InvalidCastException("Value is not a date (DateOnly or DateTime)")
        };

        return date <= DateOnly.FromDateTime(DateTime.Today);
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The {name} must be today or earlier";
    }
}