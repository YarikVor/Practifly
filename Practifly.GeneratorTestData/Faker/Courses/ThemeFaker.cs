using Bogus;
using Practifly.GeneratorTestData;
using PractiFly.WebApi.EntityDb.Courses;

namespace PractiFly.Tests.EntityFromDb;

public sealed class ThemeFaker : Faker<Theme>, IFakerGenerate<Theme>
{
    public ThemeFaker()
    {
        RuleFor(t => t.Id, f => f.IndexFaker + 1);
        RuleFor(t => t.Name, f => f.Lorem.Word());
        RuleFor(cm => cm.CourseId, f => f.RandomId());
        RuleFor(cm => cm.LevelId, f => f.RandomId());
        RuleFor(cm => cm.ParentId, f => f.RandomId());
        RuleFor(cm => cm.Number, f => f.Random.Int(0, 100));
        RuleFor(t => t.Note, f => f.Lorem.Sentence());
        RuleFor(t => t.Description, f => f.Lorem.Sentence());
    }
}