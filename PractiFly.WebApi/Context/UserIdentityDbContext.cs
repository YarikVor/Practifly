using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Users;

namespace PractiFly.WebApi.Context;

public class UserIdentityDbContext: IdentityDbContext<User, Role, int>
{
    public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) : base(options)
    {
    }
}