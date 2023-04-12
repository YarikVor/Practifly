using Bogus;
using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class UserCourseFaker : Faker<UserCourse>, IFakerGenerate<UserCourse>
{
    public UserCourseFaker(string lang = "uk") : base(lang)
    {
        RuleFor(uc => uc.Id, f => f.IndexFaker + 1);
        RuleFor(uc => uc.UserId, f => f.RandomId());
        RuleFor(uc => uc.CourseId, f => f.RandomId());
        RuleFor(uc => uc.LevelId, f => f.RandomId());
        RuleFor(uc => uc.IsCompleted, f => f.Random.Bool());
        RuleFor(uc => uc.LastTime, f => f.Date.Past());
        RuleFor(uc => uc.LastThemeId, f => f.RandomId());
        RuleFor(uc => uc.Grade, f => f.Random.Int(0, 100));
        RuleFor(uc => uc.Note, f => f.Lorem.Sentence());
    }
}