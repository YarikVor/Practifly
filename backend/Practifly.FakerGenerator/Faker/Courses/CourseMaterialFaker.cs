using PractiFly.DbEntities.Courses;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Courses;

public sealed class CourseMaterialFaker : FakerFkRandomizer<CourseMaterial>, IFakerGenerate<CourseMaterial>
{
    public CourseMaterialFaker(int count) : base(count)
    {
        RuleFor(cm => cm.CourseId, RandomId);
        RuleFor(cm => cm.MaterialId, RandomId);
        RuleFor(cm => cm.PriorityLevel, f => f.Random.Int(0, 100));
        RuleFor(cm => cm.IsReserved, f => f.Random.Bool());
        RuleFor(cm => cm.Note, f => f.Lorem.Sentence());
    }
}