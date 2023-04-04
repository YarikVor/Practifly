using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.Context;

public class PractiflyContext: DbContext, IPractiflyContext
{
    public DbSet<Heading> Headings { get; set; }
}