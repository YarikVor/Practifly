using Bogus;
using PractiFly.DbEntities.Courses;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Courses;

public sealed class CourseCompotencyFaker : Faker<CourseCompetency>, IFakerGenerate<CourseCompetency>
{
    public CourseCompotencyFaker()
    {
        RuleFor(cc => cc.Id, f => f.IndexFaker + 1);
        RuleFor(cc => cc.CourseId, f => f.RandomId());
        RuleFor(cc => cc.CompetencyId, f => f.RandomId());
        RuleFor(cc => cc.Note, f => f.Lorem.Sentence());
    }
}