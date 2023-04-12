using Bogus;
using PractiFly.DbEntities.Courses;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Courses;

public sealed class CourseMaterialFaker : Faker<CourseMaterial>, IFakerGenerate<CourseMaterial>
{
    public CourseMaterialFaker()
    {
        RuleFor(cm => cm.Id, f => f.IndexFaker + 1);
        RuleFor(cm => cm.CourseId, f => f.RandomId());
        RuleFor(cm => cm.MaterialId, f => f.RandomId());
        RuleFor(cm => cm.PriorityLevel, f => f.Random.Int(0, 100));
        RuleFor(cm => cm.IsReserved, f => f.Random.Bool());
        RuleFor(cm => cm.Note, f => f.Lorem.Sentence());
    }
}