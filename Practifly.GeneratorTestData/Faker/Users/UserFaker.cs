using Bogus;
using PractiFly.Tests.EntityFromDb;
using PractiFly.WebApi.EntityDb.Users;

namespace Practifly.GeneratorTestData.Faker.Users;

public sealed class UserFaker : Faker<User>, IFakerGenerate<User> {
    public UserFaker(string lang = "uk"): base(lang) {
        RuleFor(o => o.Id, f => f.IndexFaker + 1);
        RuleFor(o => o.FirstName, f => f.Name.FirstName());
        RuleFor(o => o.LastName, f => f.Name.LastName());
        RuleFor(o => o.Email, f => f.Internet.Email());
        RuleFor(o => o.Phone, f => f.Phone.PhoneNumber());
        RuleFor(o => o.FilePhoto, f => f.Internet.Avatar());
        RuleFor(o => o.RegistrationDate, f => f.Date.PastDateOnly());
        RuleFor(o => o.Note, f => f.Lorem.Sentence());
    }


}