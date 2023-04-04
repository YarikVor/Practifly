using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.Context;

public interface IPractiflyContext
{
    DbSet<Heading> Headings { get; set; }
}