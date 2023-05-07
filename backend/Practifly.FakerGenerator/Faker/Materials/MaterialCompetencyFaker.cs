using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Materials;

public sealed class MaterialCompetencyFaker : FakerFkRandomizer<MaterialCompetency>, IFakerGenerate<MaterialCompetency>
{
    public MaterialCompetencyFaker(int count) : base(count)
    {
        RuleFor(mc => mc.MaterialId, RandomId);
        RuleFor(mc => mc.CompetencyId, RandomId);
        RuleFor(mc => mc.Note, f => f.Lorem.Sentence());
    }
}