using Bogus;
using Practifly.GeneratorTestData;
using PractiFly.WebApi.EntityDb.Courses;

namespace PractiFly.Tests.EntityFromDb;

public sealed class ThemeMaterialFaker : Faker<ThemeMaterial>, IFakerGenerate<ThemeMaterial>
{
    public ThemeMaterialFaker()
    {
        RuleFor(tm => tm.ThemeId, f => f.RandomId());
        RuleFor(tm => tm.MaterialId, f => f.RandomId());
        RuleFor(tm => tm.Number, f => f.Random.Int(0, 100));
        RuleFor(tm => tm.IsBasic, f => f.Random.Bool());
        RuleFor(tm => tm.LevelId, f => f.RandomId());
        RuleFor(tm => tm.Note, f => f.Lorem.Sentence());
    }
}