using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Courses;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.Context;

public class MaterialsContext: DbContext, IMaterialsContext
{
    public DbSet<Heading> Headings { get; set; } = null!;
    public DbSet<Material> Materials { get; set; } = null!;
    public DbSet<Theme> Themes { get; set; } = null!;

    public MaterialsContext(DbContextOptions<MaterialsContext> options) : base(options)
    {
        Database.OpenConnection();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Heading>();
        modelBuilder.Entity<Material>();
        modelBuilder.Entity<Theme>();
    }
}