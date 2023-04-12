using Bogus;
using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Materials;

public sealed class MaterialBlockFaker : Faker<MaterialBlock>, IFakerGenerate<MaterialBlock>
{
    public MaterialBlockFaker(string lang = "uk") : base(lang)
    {
        RuleFor(mb => mb.Id, f => f.IndexFaker + 1);
        RuleFor(mb => mb.ParentId, f => f.RandomId());
        RuleFor(mb => mb.ChildId, f => f.RandomId());
        RuleFor(mb => mb.Number, f => f.Random.Number(1, 5));
        RuleFor(mb => mb.Note, f => f.Lorem.Sentence());
    }
}