using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Materials;

public sealed class CompetencyFaker : FakerFkRandomizer<Competency>, IFakerGenerate<Competency>
{
    public CompetencyFaker(int count) : base(count)
    {
        RuleFor(x => x.Id, f => f.IndexFaker + 1);
        RuleFor(x => x.Name, f => f.Lorem.Word());
        RuleFor(x => x.HeadingId, RandomId);
        RuleFor(x => x.ParentId, () => null);
        RuleFor(x => x.Note, f => f.Lorem.Sentence());
        RuleFor(x => x.Description, f => f.Lorem.Sentence());
    }
}