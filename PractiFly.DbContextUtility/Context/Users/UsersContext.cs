using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Materials;
using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace PractiFly.DbContextUtility.Context.Users;

public class UsersContext : DbContext, IUsersContext
{
    private readonly IFakerManager _fakerManager;
    
    public UsersContext(DbContextOptions<UsersContext> options, IFakerManager fakerManager) : base(options)
    {
        _fakerManager = fakerManager;
    }

    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<GroupCourse> GroupCourses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }

    public DbSet<UserHeading> UserHeadings { get; set; }

    public DbSet<UserMaterial> UserMaterials { get; set; }
    public DbSet<UserTheme> UserThemes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Level>();
        modelBuilder.Entity<Course>();
        modelBuilder.Entity<Theme>();
    }
}