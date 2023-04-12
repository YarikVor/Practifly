using Bogus;
using PractiFly.DbEntities.Courses;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Courses;

public sealed class CourseDependencyFaker : Faker<CourseDependency>, IFakerGenerate<CourseDependency>
{
    public CourseDependencyFaker()
    {
        RuleFor(cd => cd.Id, f => f.IndexFaker + 1);
        RuleFor(cd => cd.CourseId, f => f.RandomId());
        RuleFor(cd => cd.BaseCourseId, f => f.RandomId());
        RuleFor(cd => cd.CourseDependencyTypeId, f => f.RandomId());
        RuleFor(cd => cd.Note, f => f.Lorem.Sentence());
    }
}