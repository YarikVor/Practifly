using Bogus;
using Practifly.GeneratorTestData;
using PractiFly.WebApi.EntityDb.Courses;

namespace PractiFly.Tests.EntityFromDb;

public sealed class CourseHeadingFaker : Faker<CourseHeading>, IFakerGenerate<CourseHeading>
{
    public CourseHeadingFaker()
    {
        RuleFor(ch => ch.Id, f => f.IndexFaker + 1);
        RuleFor(ch => ch.CourseId, f => f.RandomId());
        RuleFor(ch => ch.HeadingId, f => f.RandomId());

        RuleFor(ch => ch.IsBasic, f => f.Random.Bool());
        RuleFor(ch => ch.Note, f => f.Lorem.Sentence());
    }
}