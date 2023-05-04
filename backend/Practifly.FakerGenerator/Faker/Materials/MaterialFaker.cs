using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Materials;

public sealed class MaterialFaker : FakerFkRandomizer<Material>, IFakerGenerate<Material>
{
    public MaterialFaker(int count) : base(count)
    {
        RuleFor(m => m.Id, f => f.IndexFaker + 1);
        RuleFor(m => m.Name, f => f.Name.FirstName());
        RuleFor(m => m.Url, f => f.Internet.Url());
        RuleFor(m => m.IsPractical, f => f.Random.Bool());
        RuleFor(m => m.Note, f => f.Lorem.Sentence());
    }
}