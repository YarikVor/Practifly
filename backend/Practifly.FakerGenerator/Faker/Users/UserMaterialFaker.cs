using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class UserMaterialFaker : FakerFkRandomizer<UserMaterial>, IFakerGenerate<UserMaterial>
{
    public UserMaterialFaker(int count) : base(count)
    {
        RuleFor(um => um.Id, f => f.IndexFaker + 1);
        RuleFor(um => um.UserId, RandomId);
        RuleFor(um => um.MaterialId, RandomId);
        RuleFor(um => um.IsCompleted, f => f.Random.Bool());
        RuleFor(um => um.ResultUrl, f => f.Internet.Url());
        RuleFor(um => um.Grade, f => f.Random.Int(0, 100));
        RuleFor(um => um.Note, f => f.Lorem.Sentence());
    }
}