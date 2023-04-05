using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.Db.Configuration.Users;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.WebApi.Context;

public class UsersContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    //public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<Group> Groups { get; set; }

    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
        Database.OpenConnection();
        Database.EnsureCreated();
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
    }
}

public static class DbContextEx
{
    public static void AddOrUpdate<T>(this DbSet<T> dbSet, T entity) where T : class
    {
        if (dbSet.Local.Any(e => e == entity))
        {
            dbSet.Update(entity);
        }
        else
        {
            dbSet.Add(entity);
        }
    }
}