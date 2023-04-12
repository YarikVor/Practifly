using Bogus;
using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Materials;

public sealed class UnitFaker : Faker<Unit>, IFakerGenerate<Unit>
{
    public UnitFaker(string lang = "uk") : base(lang)
    {
        RuleFor(u => u.Id, f => f.IndexFaker + 1);
        RuleFor(u => u.MaterialId, f => f.RandomId());
        RuleFor(u => u.Text, f => f.Lorem.Sentence());
        RuleFor(u => u.Url, f => f.Internet.Url());
    }
}