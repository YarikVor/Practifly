using Bogus;
using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class GroupFaker : Faker<Group>, IFakerGenerate<Group>
{
    public GroupFaker(string lang = "uk") : base(lang)
    {
        RuleFor(g => g.Id, f => f.IndexFaker + 1);
        RuleFor(g => g.Name, f => f.Company.CompanyName());
        RuleFor(g => g.Note, f => f.Lorem.Sentence());
        RuleFor(g => g.FoundationDate, f => f.Date.PastDateOnly());
        RuleFor(g => g.TerminationDate, f => f.Date.FutureDateOnly());
    }
}