using Bogus;
using Practifly.GeneratorTestData;
using PractiFly.WebApi.EntityDb.Courses;

namespace PractiFly.Tests.EntityFromDb;

public sealed class CourseCompotencyFaker : Faker<CourseCompetency>, IFakerGenerate<CourseCompetency>
{
    public CourseCompotencyFaker()
    {
        RuleFor(cc => cc.CourseId, f => f.RandomId());
        RuleFor(cc => cc.CompetencyId, f => f.RandomId());
        RuleFor(cc => cc.Note, f => f.Lorem.Sentence());
        
    }
}