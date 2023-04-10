using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Courses;
using PractiFly.WebApi.EntityDb.Materials;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.WebApi.Context;

public class UsersContext: DbContext, IUsersContext
{
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<GroupCourse> GroupCourses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    
    public DbSet<UserHeading> UserHeadings { get; set; }

    public DbSet<UserMaterial> UserMaterials { get; set; }
    public DbSet<UserTheme> UserThemes { get; set; }

    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Level>();
        modelBuilder.Entity<Course>();
        modelBuilder.Entity<Theme>();
    }
}