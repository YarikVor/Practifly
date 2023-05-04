using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class GroupFaker : FakerFkRandomizer<Group>, IFakerGenerate<Group>
{
    public GroupFaker(int count) : base(count)
    {
        RuleFor(g => g.Id, f => f.IndexFaker + 1);
        RuleFor(g => g.Name, f => f.Company.CompanyName());
        RuleFor(g => g.Note, f => f.Lorem.Sentence());
        RuleFor(g => g.FoundationDate, f => f.Date.PastDateOnly());
        RuleFor(g => g.TerminationDate, f => f.Date.FutureDateOnly());
    }
}