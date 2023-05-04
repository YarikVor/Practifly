using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Abstraction;
using PractiFly.DbEntities.Courses;

namespace PractiFly.DbContextUtility.Context.Courses;

public interface ICoursesContext : IDisposable, IBasicDbContext
{
    DbSet<Course> Courses { get; }
    DbSet<CourseCompetency> CourseCompetencies { get; }
    DbSet<CourseDependency> CourseDependencies { get; }
    DbSet<CourseDependencyType> CourseDependencyTypes { get; }
    DbSet<CourseHeading> CourseHeadings { get; }
    DbSet<CourseMaterial> CourseMaterials { get; }
    DbSet<Theme> Themes { get; }
    DbSet<ThemeMaterial> ThemeMaterials { get; }
}