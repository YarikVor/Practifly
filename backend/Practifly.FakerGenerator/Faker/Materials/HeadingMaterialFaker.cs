using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Materials;

public sealed class HeadingMaterialFaker : FakerFkRandomizer<HeadingMaterial>, IFakerGenerate<HeadingMaterial>
{
    public HeadingMaterialFaker(int count) : base(count)
    {
        RuleFor(hm => hm.HeadingId, RandomId);
        RuleFor(hm => hm.MaterialId, RandomId);
        RuleFor(hm => hm.Note, f => f.Lorem.Sentence());
    }
}