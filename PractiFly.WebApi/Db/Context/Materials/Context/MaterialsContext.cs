using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Courses;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.Context;

public class MaterialsContext : DbContext, IMaterialsContext
{
    public MaterialsContext(DbContextOptions<MaterialsContext> options) : base(options)
    {
        Database.OpenConnection();
        Database.EnsureCreated();
    }

    public DbSet<Theme> Themes { get; set; } = null!;
    public DbSet<Competency> Competencies { get; set; } = null!;
    public DbSet<Heading> Headings { get; set; } = null!;
    public DbSet<HeadingCompetency> HeadingCompetencies { get; set; } = null!;
    public DbSet<HeadingMaterial> HeadingMaterials { get; set; } = null!;
    public DbSet<Language> Languages { get; set; } = null!;
    public DbSet<Level> Levels { get; set; } = null!;
    public DbSet<Material> Materials { get; set; } = null!;
    public DbSet<MaterialBlock> MaterialBlocks { get; set; } = null!;
    public DbSet<MaterialCompetency> MaterialCompetencies { get; set; } = null!;
    public DbSet<Unit> Units { get; set; } = null!;
}