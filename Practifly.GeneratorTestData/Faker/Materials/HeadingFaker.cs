using Bogus;
using PractiFly.Tests.EntityFromDb;
using PractiFly.WebApi.EntityDb.Materials;

namespace Practifly.GeneratorTestData.Faker.Materials;

public sealed class HeadingFaker : Faker<Heading>, IFakerGenerate<Heading>
{
    public HeadingFaker(string lang = "uk") : base(lang)
    {
        RuleFor(h => h.Id, f => f.IndexFaker + 1);
        RuleFor(h => h.Code, f => f.Random.Replace("##.##.##.##"));
        RuleFor(h => h.Name, f => f.Company.CompanyName());
        RuleFor(h => h.Udc, f => f.Random.Replace("##-###"));
        RuleFor(h => h.Note, f => f.Lorem.Sentence());
        RuleFor(h => h.Description, f => f.Lorem.Text());
    }
}