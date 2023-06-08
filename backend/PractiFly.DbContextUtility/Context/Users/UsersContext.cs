using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Materials;
using PractiFly.DbEntities.Users;

namespace PractiFly.DbContextUtility.Context.Users;

public class UsersContext : DbContext, IUsersContext
{
    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {}

    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<GroupCourse> GroupCourses { get; set; }  = null!;
    public DbSet<UserCourse> UserCourses { get; set; } = null!;
    public DbSet<UserGroup> UserGroups { get; set; } = null!;
    public DbSet<UserHeading> UserHeadings { get; set; } = null!;
    public DbSet<UserMaterial> UserMaterials { get; set; } = null!;
    public DbSet<UserTheme> UserThemes { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Level>();
        modelBuilder.Entity<Course>();
        modelBuilder.Entity<Theme>();
    }
}