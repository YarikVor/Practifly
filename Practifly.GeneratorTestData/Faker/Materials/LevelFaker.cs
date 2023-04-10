using Bogus;
using PractiFly.Tests.EntityFromDb;
using PractiFly.WebApi.EntityDb.Materials;

namespace Practifly.GeneratorTestData.Faker.Materials;

public sealed class LevelFaker : Faker<Level>, IFakerGenerate<Level>
{
    public LevelFaker(string lang = "uk") : base(lang)
    {
        RuleFor(l => l.Id, f => f.IndexFaker + 1);
        RuleFor(l => l.Name, f => f.Name.FirstName());
        RuleFor(l => l.Number, f => f.Random.Number(1, 5));
        RuleFor(l => l.Note, f => f.Lorem.Sentence());
        RuleFor(l => l.Description, f => f.Lorem.Text());
    }
}