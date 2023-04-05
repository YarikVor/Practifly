using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Courses;

namespace PractiFly.WebApi.Context;

public class CouresContext: DbContext, ICoursesContext
{
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Theme> Themes { get; set; } = null!;

    public CouresContext(DbContextOptions<CouresContext> options) : base(options)
    {
        Database.OpenConnection();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}