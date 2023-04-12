using Bogus;
using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class UserMaterialFaker : Faker<UserMaterial>, IFakerGenerate<UserMaterial>
{
    public UserMaterialFaker(string lang = "uk") : base(lang)
    {
        RuleFor(um => um.Id, f => f.IndexFaker + 1);
        RuleFor(um => um.UserId, f => f.RandomId());
        RuleFor(um => um.MaterialId, f => f.RandomId());
        RuleFor(um => um.IsCompleted, f => f.Random.Bool());
        RuleFor(um => um.ResultUrl, f => f.Internet.Url());
        RuleFor(um => um.Grade, f => f.Random.Int(0, 100));
        RuleFor(um => um.Note, f => f.Lorem.Sentence());
    }
}