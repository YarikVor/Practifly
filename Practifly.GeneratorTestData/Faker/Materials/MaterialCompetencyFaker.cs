using Bogus;
using PractiFly.Tests.EntityFromDb;
using PractiFly.WebApi.EntityDb.Materials;

namespace Practifly.GeneratorTestData.Faker.Materials
{
    public sealed class MaterialCompetencyFaker : Faker<MaterialCompetency>, IFakerGenerate<MaterialCompetency>
    {
        public MaterialCompetencyFaker(string lang = "uk") : base(lang)
        {
            RuleFor(mc => mc.Id, f => f.IndexFaker + 1);
            RuleFor(mc => mc.MaterialId, f => f.RandomId());
            RuleFor(mc => mc.CompetencyId, f => f.RandomId());
            RuleFor(mc => mc.Note, f => f.Lorem.Sentence());
        }
    }
}
