using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Materials;

public sealed class MaterialBlockFaker : FakerFkRandomizer<MaterialBlock>, IFakerGenerate<MaterialBlock>
{
    public MaterialBlockFaker(int count) : base(count)
    {
        RuleFor(mb => mb.Id, f => f.IndexFaker + 1);
        RuleFor(mb => mb.ParentId, RandomId);
        RuleFor(mb => mb.ChildId, RandomId);
        RuleFor(mb => mb.Number, f => f.Random.Number(1, 5));
        RuleFor(mb => mb.Note, f => f.Lorem.Sentence());
    }
}