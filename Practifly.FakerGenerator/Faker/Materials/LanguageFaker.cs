using Bogus;
using PractiFly.DbEntities.Materials;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Materials;

public sealed class LanguageFaker : Faker<Language>, IFakerGenerate<Language>
{
    public LanguageFaker(string lang = "uk") : base(lang)
    {
        RuleFor(l => l.Code, f => f.Random.String(2));
        RuleFor(l => l.Name, f => f.Random.RandomLocale().Substring(0, 2)); //TODO:
        RuleFor(l => l.OriginalName, f => f.Random.RandomLocale());
        RuleFor(l => l.Note, f => f.Lorem.Sentence());
    }
}