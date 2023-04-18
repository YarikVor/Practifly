using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Materials;
using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace PractiFly.DbContextUtility.Context.Users;

public class UsersContext : DbContext, IUsersContext
{

    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }

    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<GroupCourse> GroupCourses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }

    public DbSet<UserHeading> UserHeadings { get; set; }

    public DbSet<UserMaterial> UserMaterials { get; set; }
    public DbSet<UserTheme> UserThemes { get; set; }
    //public DbSet<Role> Roles { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<IdentityUser> IdentityUser { get; set; }
    public DbSet<IdentityRole<int>> IdentityRole { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        
        modelBuilder.Entity<Level>();
        modelBuilder.Entity<Course>();
        modelBuilder.Entity<Theme>();
    }
}