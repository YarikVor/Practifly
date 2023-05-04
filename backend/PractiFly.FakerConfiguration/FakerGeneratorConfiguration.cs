using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practifly.FakerGenerator;
using PractiFly.FakerManager;

namespace PractiFly.FakerConfiguration;

public class FakerGeneratorConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    private const int NUMBER_GENERATE_ENTITIES = 5;
    private static readonly IFakerManager _fakerManager = new PractiFlyFakerManager();

    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasData(_fakerManager.Generate<T>(NUMBER_GENERATE_ENTITIES));
        //builder.HasKey(e => e.Id)
    }
}