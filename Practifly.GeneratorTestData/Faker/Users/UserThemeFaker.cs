using Bogus;
using PractiFly.Tests.EntityFromDb;
using PractiFly.WebApi.EntityDb.Users;

namespace Practifly.GeneratorTestData.Faker.Users;

public sealed class UserThemeFaker: Faker<UserTheme>, IFakerGenerate<UserTheme>
{
    public UserThemeFaker(string lang = "uk") : base(lang)
    {
        RuleFor(ut => ut.UserId, f => f.RandomId());
        RuleFor(ut => ut.ThemeId, f => f.RandomId());
        RuleFor(ut => ut.LevelId, f => f.RandomId());
        RuleFor(ut => ut.IsCompleted, f => f.Random.Bool());
        RuleFor(ut => ut.Grade, f => f.Random.Int(0, 100));
        RuleFor(ut => ut.Note, f => f.Lorem.Sentence());
    }
}