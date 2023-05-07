using PractiFly.DbEntities.Courses;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Courses;

public sealed class CourseDependencyFaker : FakerFkRandomizer<CourseDependency>, IFakerGenerate<CourseDependency>
{
    public CourseDependencyFaker(int count) : base(count)
    {
        RuleFor(cd => cd.CourseId, RandomId);
        RuleFor(cd => cd.BaseCourseId, RandomId);
        RuleFor(cd => cd.CourseDependencyTypeId, RandomId);
        RuleFor(cd => cd.Note, f => f.Lorem.Sentence());
    }
}