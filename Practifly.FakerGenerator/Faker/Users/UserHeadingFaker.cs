using Bogus;
using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class UserHeadingFaker : Faker<UserHeading>, IFakerGenerate<UserHeading>
{
    public UserHeadingFaker(string lang = "uk") : base(lang)
    {
        RuleFor(uh => uh.Id, f => f.IndexFaker + 1);
        RuleFor(uh => uh.UserId, f => f.RandomId());
        RuleFor(uh => uh.HeadingId, f => f.RandomId());
        RuleFor(uh => uh.LevelId, f => f.RandomId());
        RuleFor(uh => uh.Note, f => f.Lorem.Sentence());
    }
}