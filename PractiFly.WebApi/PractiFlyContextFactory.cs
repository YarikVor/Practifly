using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using Practifly.FakerGenerator;
using PractiFly.FakerManager;

namespace PractiFly.WebApi;

public class PractiFlyContextFactory : IDesignTimeDbContextFactory<PractiflyContext>
{
    public PractiflyContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PractiflyContext>();
        optionsBuilder.UseNpgsql(
            "User ID=YarikVor;password=uPGpfLbjt9Z4;Database=testdb;host=ep-tight-moon-762347.eu-central-1.aws.neon.tech");

        return new PractiflyContext(optionsBuilder.Options);
    }
}