using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.Context;

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