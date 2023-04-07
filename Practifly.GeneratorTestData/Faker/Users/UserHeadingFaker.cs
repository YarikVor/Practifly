using Bogus;
using PractiFly.Tests.EntityFromDb;
using PractiFly.WebApi.EntityDb.Users;

namespace Practifly.GeneratorTestData.Faker.Users;

public sealed class UserHeadingFaker: Faker<UserHeading>, IFakerGenerate<UserHeading>
{
    public UserHeadingFaker(string lang = "uk") : base(lang)
    {
        RuleFor(uh => uh.UserId, f => f.RandomId());
        RuleFor(uh => uh.HeadingId, f => f.RandomId());
        RuleFor(uh => uh.LevelId, f => f.RandomId());
        RuleFor(uh => uh.Note, f => f.Lorem.Sentence());
    }
}