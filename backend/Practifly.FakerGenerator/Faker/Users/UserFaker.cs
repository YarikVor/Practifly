using Bogus;
using Microsoft.AspNetCore.Identity;
using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Users;

public sealed class UserFaker : Faker<User>, IFakerGenerate<User>
{
    public UserFaker() : base("uk")
    {
        var passwordHasher = new PasswordHasher<object>();

        RuleFor(o => o.UserName, f => f.Internet.UserName());
        RuleFor(o => o.NormalizedUserName, (_, u) => u.UserName.ToUpper());
        RuleFor(o => o.Email, f => f.Internet.Email());
        RuleFor(o => o.EmailConfirmed, true);
        RuleFor(o => o.PasswordHash, _ => passwordHasher.HashPassword(null!, "Qwerty_123"));
        RuleFor(o => o.SecurityStamp, f => string.Concat(f.Random.Uuid().ToByteArray().Select(e => e.ToString("X2"))));
        RuleFor(o => o.ConcurrencyStamp, f => f.Random.Uuid().ToString());
        RuleFor(o => o.PhoneNumber, f => f.Phone.PhoneNumber());
        RuleFor(o => o.PhoneNumberConfirmed, true);
        RuleFor(o => o.TwoFactorEnabled, false);
        RuleFor(o => o.LockoutEnd, (DateTimeOffset?)null);
        RuleFor(o => o.LockoutEnabled, false);
        RuleFor(o => o.AccessFailedCount, 0);

        RuleFor(o => o.FirstName, f => f.Name.FirstName());
        RuleFor(o => o.LastName, f => f.Name.LastName());
        RuleFor(o => o.IsCustomPhoto, false);
        RuleFor(o => o.Birthday, f => f.Date.PastDateOnly());
        RuleFor(o => o.RegistrationDate, f => f.Date.PastDateOnly());
        RuleFor(o => o.Note, f => f.Lorem.Sentence());
    }
}