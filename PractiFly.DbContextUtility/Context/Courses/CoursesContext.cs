using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace PractiFly.DbContextUtility.Context.Courses;

public class CoursesContext : DbContext, ICoursesContext
{
    
    
    public CoursesContext(DbContextOptions<CoursesContext> options) : base(options)
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

}