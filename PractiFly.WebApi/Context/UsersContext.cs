using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.WebApi.Context;

public class UsersContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<Group> Groups { get; set; }

    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserGroup>().HasNoKey();
    }
}