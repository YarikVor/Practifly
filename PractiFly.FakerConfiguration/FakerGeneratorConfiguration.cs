using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practifly.FakerGenerator;
using PractiFly.FakerManager;

namespace PractiFly.FakerConfiguration;

public class FakerGeneratorConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    static IFakerManager _fakerManager = new PractiFlyFakerManager();
    
    const int NUMBER_GENERATE_ENTITIES = 5;
    
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasData(_fakerManager.Generate<T>(NUMBER_GENERATE_ENTITIES));
        //builder.HasKey(e => e.Id)
    }
}