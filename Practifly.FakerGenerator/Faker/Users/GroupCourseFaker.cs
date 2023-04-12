using Bogus;
using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class GroupCourseFaker : Faker<GroupCourse>, IFakerGenerate<GroupCourse>
{
    public GroupCourseFaker(string lang = "uk") : base(lang)
    {
        RuleFor(gc => gc.Id, f => f.IndexFaker + 1);
        RuleFor(gc => gc.GroupId, f => f.RandomId());
        RuleFor(gc => gc.CourseId, f => f.RandomId());
        RuleFor(gc => gc.LevelId, f => f.RandomId());
        RuleFor(gc => gc.IsCompleted, f => f.Random.Bool());
        RuleFor(gc => gc.Note, f => f.Lorem.Sentence());
    }
}