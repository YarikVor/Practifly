using Bogus;
using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Materials;

public sealed class HeadingFaker : Faker<Heading>, IFakerGenerate<Heading>
{
    public HeadingFaker(string lang = "uk") : base(lang)
    {
        RuleFor(h => h.Code, f => f.Random.Replace("##.##.##.##"));
        RuleFor(h => h.Name, f => f.Company.CompanyName());
        RuleFor(h => h.Udc, f => f.Random.Replace("##-###"));
        RuleFor(h => h.Note, f => f.Lorem.Sentence());
        RuleFor(h => h.Description, f => f.Lorem.Text());
    }
}