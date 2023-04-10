using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.WebApi.Context;

public interface IUsersContext : IDisposable, IDbContext
{
    DbSet<GroupCourse> GroupCourses { get; }
    DbSet<Group> Groups { get; }
    DbSet<User> Users { get; }
    DbSet<UserCourse> UserCourses { get; }
    DbSet<UserGroup> UserGroups { get; }

    DbSet<UserHeading> UserHeadings { get; }

    DbSet<UserMaterial> UserMaterials { get; }
    DbSet<UserTheme> UserThemes { get; }
}