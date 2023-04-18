using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Materials;
using PractiFly.DbEntities.Users;


namespace PractiFly.DbContextUtility.Context.PractiflyDb;

public class PractiFlyContext : DbContext, IPractiflyContext
{
    
    public PractiFlyContext(DbContextOptions<PractiFlyContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<CourseCompetency> CourseCompetencies { get; set; } = null!;
    public DbSet<CourseDependency> CourseDependencies { get; set; } = null!;
    public DbSet<CourseDependencyType> CourseDependencyTypes { get; set; } = null!;
    public DbSet<CourseHeading> CourseHeadings { get; set; } = null!;
    public DbSet<CourseMaterial> CourseMaterials { get; set; } = null!;
    public DbSet<Theme> Themes { get; set; } = null!;
    public DbSet<ThemeMaterial> ThemeMaterials { get; set; } = null!;
    public DbSet<Heading> Headings { get; set; } = null!;
    public DbSet<Competency> Competencies { get; set; } = null!;
    public DbSet<HeadingCompetency> HeadingCompetencies { get; set; } = null!;
    public DbSet<HeadingMaterial> HeadingMaterials { get; set; } = null!;
    public DbSet<Language> Languages { get; set; } = null!;
    public DbSet<Level> Levels { get; set; } = null!;
    public DbSet<Material> Materials { get; set; } = null!;
    public DbSet<MaterialBlock> MaterialBlocks { get; set; } = null!;
    public DbSet<MaterialCompetency> MaterialCompetencies { get; set; } = null!;
    public DbSet<Unit> Units { get; set; } = null!;
    public DbSet<GroupCourse> GroupCourses { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserCourse> UserCourses { get; set; } = null!;
    public DbSet<UserGroup> UserGroups { get; set; } = null!;
    public DbSet<UserHeading> UserHeadings { get; set; } = null!;
    public DbSet<UserMaterial> UserMaterials { get; set; } = null!;
    public DbSet<UserTheme> UserThemes { get; set; } = null!;
    public DbSet<Role> Roles { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

}