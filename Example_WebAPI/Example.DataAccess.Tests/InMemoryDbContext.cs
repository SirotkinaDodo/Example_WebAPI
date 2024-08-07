using Microsoft.EntityFrameworkCore;

namespace Example.DataAccess.Tests;

public static class InMemoryDbContext
{
    public static ExampleContext GetInMemoryDbContext()
    {
        var builder = new DbContextOptionsBuilder<ExampleContext>();
        builder.UseInMemoryDatabase(databaseName: "ExampleDbInMemory");
        builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        
        var dbContextOptions = builder.Options;
        var exampleContext = new ExampleContext(dbContextOptions);
        // Delete existing db before creating a new one
        exampleContext.Database.EnsureDeleted();
        exampleContext.Database.EnsureCreated();

        return exampleContext;
    }
}