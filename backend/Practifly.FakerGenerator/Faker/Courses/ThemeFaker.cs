using PractiFly.DbEntities.Courses;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Courses;

public sealed class ThemeFaker : FakerFkRandomizer<Theme>, IFakerGenerate<Theme>
{
    public ThemeFaker(int count) : base(count)
    {
        RuleFor(t => t.Name, f => f.Lorem.Word());
        RuleFor(cm => cm.CourseId, RandomId);
        RuleFor(cm => cm.LevelId, RandomId);
        RuleFor(cm => cm.Number, f => f.Random.Int(0, 100));
        RuleFor(t => t.Note, f => f.Lorem.Sentence());
        RuleFor(t => t.Description, f => f.Lorem.Sentence());
    }
}