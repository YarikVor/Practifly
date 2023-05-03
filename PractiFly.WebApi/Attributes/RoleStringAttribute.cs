using System.ComponentModel.DataAnnotations;
using PractiFly.WebApi.Context;

namespace PractiFly.WebApi.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class RoleStringAttribute : ValidationAttribute
{
    private readonly bool _isRequired;

    public RoleStringAttribute(bool isRequired = true)
    {
        _isRequired = isRequired;
    }

    public override bool IsValid(object? value)
    {
        if (value is not string role)
            //throw new InvalidCastException("Value is not a string");
            return false;

        return (!_isRequired && string.IsNullOrEmpty(role)) || UserRoles.RolesEnumerable.Contains(role);
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The {name} must be {string.Join(" or ", UserRoles.RolesEnumerable)}";
    }
}