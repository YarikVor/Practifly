using PractiFly.DbEntities.Courses;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Courses;

public sealed class CourseHeadingFaker : FakerFkRandomizer<CourseHeading>, IFakerGenerate<CourseHeading>
{
    public CourseHeadingFaker(int count) : base(count)
    {
        RuleFor(ch => ch.Id, f => f.IndexFaker + 1);
        RuleFor(ch => ch.CourseId, RandomId);
        RuleFor(ch => ch.HeadingId, RandomId);

        RuleFor(ch => ch.IsBasic, f => f.Random.Bool());
        RuleFor(ch => ch.Note, f => f.Lorem.Sentence());
    }
}