namespace Testing.Fixtures;

using App.Data;
using App.Controllers;
using App.Services;

using Testing.Data;

using Microsoft.EntityFrameworkCore;

public class TestFixtureFamilyController : IDisposable
{
    public EntityDataContext Context { get; private set; }
    public FamilyController Controller { get; private set; }

    public TestFixtureFamilyController()
    {
        var options = new DbContextOptionsBuilder<EntityDataContext>()
            .UseInMemoryDatabase(databaseName: "FamilyTestDb")
            .Options;

        Context = new EntityDataContext(options);
        Context.Database.EnsureCreated();
        Initialize(Context);

        var familyRepository = new FamilyRepository(Context);
        var service = new FamilyService(familyRepository);
        Controller = new FamilyController(service);
    }

    private static void Initialize(EntityDataContext context)
    {
        TestDataInitializer.InitializeFamilies(context);
        context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
