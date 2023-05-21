using Bogus;
using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class UserFaker : Faker<User>, IFakerGenerate<User>
{
    public UserFaker(string lang = "uk") : base(lang)
    {
        RuleFor(o => o.FirstName, f => f.Name.FirstName());
        RuleFor(o => o.LastName, f => f.Name.LastName());
        RuleFor(o => o.Email, f => f.Internet.Email());
        RuleFor(o => o.PhoneNumber, f => f.Phone.PhoneNumber());
        //RuleFor(o => o.FilePhoto, f => f.Internet.Avatar());
        RuleFor(o => o.RegistrationDate, f => f.Date.PastDateOnly());
        RuleFor(o => o.Note, f => f.Lorem.Sentence());
        RuleFor(o => o.PasswordHash, f => f.Internet.Password());
        RuleFor(o => o.Birthday, f => f.Date.PastDateOnly());
    }
}