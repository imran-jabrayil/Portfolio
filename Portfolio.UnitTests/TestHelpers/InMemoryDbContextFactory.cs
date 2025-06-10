using Microsoft.EntityFrameworkCore;

namespace Portfolio.UnitTests.TestHelpers;

public static class InMemoryDbContextFactory
{
    public static T CreateInMemoryDbContext<T>() where T : DbContext
    {
        string dbName = Guid.NewGuid().ToString();
        
        DbContextOptions<T> options = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(dbName)
            .Options;
        
        var context = Activator.CreateInstance(typeof(T), options) as T;
        return context!;
    }
}