using Bogus;
using PractiFly.Tests.EntityFromDb;
using PractiFly.WebApi.EntityDb.Materials;

namespace Practifly.GeneratorTestData.Faker.Materials;
public sealed class UnitFaker : Faker<Unit>, IFakerGenerate<Unit>
{
    public UnitFaker(string lang = "uk") : base(lang)
    {
        RuleFor(u => u.MaterialId, f => f.RandomId());
        RuleFor(u => u.Text, f => f.Lorem.Sentence());
        RuleFor(u => u.Url, f => f.Internet.Url());
    }
}