using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Materials;

public sealed class HeadingCompetencyFaker : FakerFkRandomizer<HeadingCompetency>, IFakerGenerate<HeadingCompetency>
{
    public HeadingCompetencyFaker(int count) : base(count)
    {
        RuleFor(hc => hc.Id, f => f.IndexFaker + 1);
        RuleFor(hc => hc.CompetencyId, f => f.RandomId());
        RuleFor(hc => hc.LevelId, f => f.RandomId());
        RuleFor(hc => hc.Note, f => f.Lorem.Sentence());
    }
}