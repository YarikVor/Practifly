using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class UserCourseFaker : FakerFkRandomizer<UserCourse>, IFakerGenerate<UserCourse>
{
    public UserCourseFaker(int count) : base(count)
    {
        RuleFor(uc => uc.UserId, RandomId);
        RuleFor(uc => uc.CourseId, RandomId);
        RuleFor(uc => uc.LevelId, RandomId);
        RuleFor(uc => uc.IsCompleted, f => f.Random.Bool());
        RuleFor(uc => uc.LastTime, f => f.Date.Past().ToUniversalTime());
        RuleFor(uc => uc.LastThemeId, RandomId);
        RuleFor(uc => uc.Grade, f => f.Random.Int(0, 100));
        RuleFor(uc => uc.Note, f => f.Lorem.Sentence());
    }
}