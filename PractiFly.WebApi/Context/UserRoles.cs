namespace PractiFly.WebApi.Context;

public static class UserRoles
{
    public const string Admin = "admin";
    public const string User = "user";
    public const string Manager = "manager";

    public static readonly IEnumerable<string> RolesEnumerable = new string[] { Admin, User, Manager };
}