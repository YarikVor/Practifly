using Bogus;
using PractiFly.DbEntities.Courses;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Courses;

public sealed class ThemeMaterialFaker : Faker<ThemeMaterial>, IFakerGenerate<ThemeMaterial>
{
    public ThemeMaterialFaker()
    {
        RuleFor(tm => tm.Id, f => f.IndexFaker + 1);
        RuleFor(tm => tm.ThemeId, f => f.RandomId());
        RuleFor(tm => tm.MaterialId, f => f.RandomId());
        RuleFor(tm => tm.Number, f => f.Random.Int(0, 100));
        RuleFor(tm => tm.IsBasic, f => f.Random.Bool());
        RuleFor(tm => tm.LevelId, f => f.RandomId());
        RuleFor(tm => tm.Note, f => f.Lorem.Sentence());
    }
}