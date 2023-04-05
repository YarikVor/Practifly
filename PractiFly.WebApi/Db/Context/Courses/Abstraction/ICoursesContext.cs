using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Courses;

namespace PractiFly.WebApi.Context;

public interface ICoursesContext : IDisposable, IDbContext
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