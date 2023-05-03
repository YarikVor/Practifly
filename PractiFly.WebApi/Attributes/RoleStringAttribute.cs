using System.ComponentModel.DataAnnotations;
using PractiFly.WebApi.Context;

namespace PractiFly.WebApi.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class RoleStringAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
            return true;
        
        if (value is not string role)
            throw new InvalidCastException("Value is not a string");

        return role.Length == 0 || UserRoles.RolesEnumerable.Contains(role);
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The {name} must be {string.Join(" or ", UserRoles.RolesEnumerable)}";
    }
}