using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Abstraction;
using PractiFly.DbEntities.Materials;

namespace PractiFly.DbContextUtility.Context.Materials;

public interface IMaterialsContext : IDisposable, IBasicDbContext
{
    DbSet<Competency> Competencies { get; }
    DbSet<Heading> Headings { get; }
    DbSet<HeadingCompetency> HeadingCompetencies { get; }
    DbSet<HeadingMaterial> HeadingMaterials { get; }
    DbSet<Language> Languages { get; }
    DbSet<Level> Levels { get; }
    DbSet<Material> Materials { get; }
    DbSet<MaterialBlock> MaterialBlocks { get; }
    DbSet<MaterialCompetency> MaterialCompetencies { get; }
    DbSet<Unit> Units { get; }
}