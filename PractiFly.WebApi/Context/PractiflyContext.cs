using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.Context;

public class PractiflyContext: DbContext, IPractiflyContext
{
    public DbSet<Heading> Headings { get; set; }
    public DbSet<Competency> Competencies { get; set; }
    
    public PractiflyContext(DbContextOptions<PractiflyContext> options) : base(options)
    {
    }
}