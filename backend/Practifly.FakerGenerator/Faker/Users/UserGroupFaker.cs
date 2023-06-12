using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class UserGroupFaker : FakerFkRandomizer<UserGroup>, IFakerGenerate<UserGroup>
{
    public UserGroupFaker(int count) : base(count)
    {
        RuleFor(ug => ug.UserId, RandomId);
        RuleFor(ug => ug.GroupId, RandomId);
        RuleFor(ug => ug.IsActive, f => f.Random.Bool());
        RuleFor(ug => ug.Note, f => f.Lorem.Sentence());
    }
}