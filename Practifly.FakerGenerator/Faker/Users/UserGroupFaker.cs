using Bogus;
using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class UserGroupFaker : Faker<UserGroup>, IFakerGenerate<UserGroup>
{
    public UserGroupFaker(string lang = "uk") : base(lang)
    {
        RuleFor(ug => ug.Id, f => f.IndexFaker + 1);
        RuleFor(ug => ug.UserId, f => f.RandomId());
        RuleFor(ug => ug.GroupId, f => f.RandomId());
        RuleFor(ug => ug.IsActive, f => f.Random.Bool());
        RuleFor(ug => ug.Note, f => f.Lorem.Sentence());
    }
}