using Bogus;
using PractiFly.Tests.EntityFromDb;
using PractiFly.WebApi.EntityDb.Users;

namespace Practifly.GeneratorTestData.Faker.Users;

public sealed class UserGroupFaker: Faker<UserGroup>, IFakerGenerate<UserGroup>
{
    public UserGroupFaker(string lang = "uk") : base(lang)
    {
        RuleFor(ug => ug.UserId, f => f.RandomId());
        RuleFor(ug => ug.GroupId, f => f.RandomId());
        RuleFor(ug => ug.IsActive, f => f.Random.Bool());
        RuleFor(ug => ug.Note, f => f.Lorem.Sentence());
    }
}