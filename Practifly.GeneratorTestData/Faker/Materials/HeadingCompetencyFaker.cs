using Bogus;
using PractiFly.Tests.EntityFromDb;
using PractiFly.WebApi.EntityDb.Materials;

namespace Practifly.GeneratorTestData.Faker.Materials;

public sealed class HeadingCompetencyFaker : Faker<HeadingCompetency>, IFakerGenerate<HeadingCompetency>
{
    public HeadingCompetencyFaker(string lang = "uk") : base(lang)
    {
        RuleFor(hc => hc.Id, f => f.IndexFaker + 1);
        RuleFor(hc => hc.CompetencyId, f => f.RandomId());
        RuleFor(hc => hc.LevelId, f => f.RandomId());
        RuleFor(hc => hc.Note, f => f.Lorem.Sentence());
    }
}