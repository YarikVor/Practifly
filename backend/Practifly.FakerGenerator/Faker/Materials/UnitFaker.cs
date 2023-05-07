using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Materials;

public sealed class UnitFaker : FakerFkRandomizer<Unit>, IFakerGenerate<Unit>
{
    public UnitFaker(int count) : base(count)
    {
        RuleFor(u => u.MaterialId, RandomId);
        RuleFor(u => u.Text, f => f.Lorem.Sentence());
        RuleFor(u => u.Url, f => f.Internet.Url());
    }
}