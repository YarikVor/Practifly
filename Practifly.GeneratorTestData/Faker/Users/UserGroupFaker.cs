using Bogus;
using Practifly.GeneratorTestData;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.Tests.EntityFromDb;

public class UserGroupFaker: Faker<UserGroup>
{
    public UserGroupFaker(string lang = "uk") : base(lang)
    {
        RuleFor(ug => ug.UserId, f => f.RandomId());
        RuleFor(ug => ug.GroupId, f => f.RandomId());
        RuleFor(ug => ug.IsActive, f => f.Random.Bool());
        RuleFor(ug => ug.Note, f => f.Random.String(256));
    }
}