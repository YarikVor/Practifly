using Microsoft.EntityFrameworkCore;

namespace PractiFly.Tests.EntityFromDb;

public static class DbContextEx
{
    public static void AddOrUpdate<T>(this DbContext dbContext, params T[] entities) where T : class
    {
        var table = dbContext.Set<T>();
        
        foreach (var entity in entities)
        {
            if (table.Find(entity) != null)
            {
                table.Update(entity);
            } else
            {
                table.Add(entity);
            }
        }
        
        dbContext.SaveChanges();
    }
}