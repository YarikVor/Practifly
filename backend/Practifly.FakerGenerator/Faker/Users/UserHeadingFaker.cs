using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class UserHeadingFaker : FakerFkRandomizer<UserHeading>, IFakerGenerate<UserHeading>
{
    public UserHeadingFaker(int count) : base(count)
    {
        RuleFor(uh => uh.Id, f => f.IndexFaker + 1);
        RuleFor(uh => uh.UserId, RandomId);
        RuleFor(uh => uh.HeadingId, RandomId);
        RuleFor(uh => uh.LevelId, RandomId);
        RuleFor(uh => uh.Note, f => f.Lorem.Sentence());
    }
}