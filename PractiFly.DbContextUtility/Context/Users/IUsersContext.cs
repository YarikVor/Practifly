using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Abstraction;
using PractiFly.DbEntities.Users;

namespace PractiFly.DbContextUtility.Context.Users;

public interface IUsersContext : IDisposable, IBasicDbContext
{
    DbSet<GroupCourse> GroupCourses { get; }
    DbSet<Group> Groups { get; }
    DbSet<User> Users { get; }
    DbSet<UserCourse> UserCourses { get; }
    DbSet<UserGroup> UserGroups { get; }

    DbSet<UserHeading> UserHeadings { get; }

    DbSet<UserMaterial> UserMaterials { get; }
    DbSet<UserTheme> UserThemes { get; }
    DbSet<Role> Roles { get; }

    DbSet<ApplicationUser> ApplicationUsers { get; }
}