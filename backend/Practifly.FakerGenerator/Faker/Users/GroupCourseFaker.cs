using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class GroupCourseFaker : FakerFkRandomizer<GroupCourse>, IFakerGenerate<GroupCourse>
{
    public GroupCourseFaker(int count) : base(count)
    {
        RuleFor(gc => gc.Id, f => f.IndexFaker + 1);
        RuleFor(gc => gc.GroupId, RandomId);
        RuleFor(gc => gc.CourseId, RandomId);
        RuleFor(gc => gc.LevelId, RandomId);
        RuleFor(gc => gc.IsCompleted, f => f.Random.Bool());
        RuleFor(gc => gc.Note, f => f.Lorem.Sentence());
    }
}