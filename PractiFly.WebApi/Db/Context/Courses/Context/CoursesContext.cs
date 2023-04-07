using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Courses;

namespace PractiFly.WebApi.Context;

public class CoursesContext: DbContext, ICoursesContext
{
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<CourseCompetency> CourseCompetencies { get; set; } = null!;
    public DbSet<CourseDependency> CourseDependencies { get; set; } = null!;
    public DbSet<CourseDependencyType> CourseDependencyTypes { get; set; } = null!;
    public DbSet<CourseHeading> CourseHeadings { get; set; } = null!;
    public DbSet<CourseMaterial> CourseMaterials { get; set; } = null!;
    public DbSet<Theme> Themes { get; set; } = null!;
    public DbSet<ThemeMaterial> ThemeMaterials { get; set; } = null!;

    public CoursesContext(DbContextOptions<CoursesContext> options) : base(options)
    {
        Database.OpenConnection();
        Database.EnsureCreated();
    }

}