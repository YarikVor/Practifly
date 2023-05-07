using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class UserThemeFaker : FakerFkRandomizer<UserTheme>, IFakerGenerate<UserTheme>
{
    public UserThemeFaker(int count) : base(count)
    {
        RuleFor(ut => ut.UserId, RandomId);
        RuleFor(ut => ut.ThemeId, RandomId);
        RuleFor(ut => ut.LevelId, RandomId);
        RuleFor(ut => ut.IsCompleted, f => f.Random.Bool());
        RuleFor(ut => ut.Grade, f => f.Random.Int(0, 100));
        RuleFor(ut => ut.Note, f => f.Lorem.Sentence());
    }
}