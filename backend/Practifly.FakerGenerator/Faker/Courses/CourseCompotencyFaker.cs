using PractiFly.DbEntities.Courses;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Courses;

public sealed class CourseCompotencyFaker : FakerFkRandomizer<CourseCompetency>, IFakerGenerate<CourseCompetency>
{
    public CourseCompotencyFaker(int count) : base(count)
    {
        RuleFor(cc => cc.CourseId, RandomId);
        RuleFor(cc => cc.CompetencyId, RandomId);
        RuleFor(cc => cc.Note, f => f.Lorem.Sentence());
    }
}