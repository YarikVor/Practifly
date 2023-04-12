using Bogus;
using PractiFly.DbEntities.Courses;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Courses;

public sealed class CourseFaker : Faker<Course>, IFakerGenerate<Course>
{
    public CourseFaker()
    {
        RuleFor(x => x.Id, f => f.IndexFaker + 1);
        RuleFor(x => x.Name, f => f.Lorem.Word());
        RuleFor(x => x.OwnerId, f => f.RandomId());
        RuleFor(x => x.Note, f => f.Lorem.Sentence());
        RuleFor(x => x.Description, f => f.Lorem.Sentence());
    }
}