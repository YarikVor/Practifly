namespace PractiFly.WebApi.Context;

public static class UserRoles
{
    public const string Admin = "admin";
    public const string Manager = "manager";

    public const string Teacher = "teacher";
    public const string User = "user";
    public static readonly IEnumerable<string> RolesEnumerable = new string[] { Admin, Manager, Teacher, User };
}