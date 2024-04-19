namespace Testing.Fixtures;

using App.Data;
using App.Controllers;
using App.Services;

using Testing.Data;

using Microsoft.EntityFrameworkCore;

public class TestFixtureBrandcontroller : IDisposable
{
    public EntityDataContext Context { get; private set; }
    public BrandController Controller { get; private set; }

    public TestFixtureBrandcontroller()
    {
        var options = new DbContextOptionsBuilder<EntityDataContext>()
            .UseInMemoryDatabase(databaseName: "BrandTestDb")
            .Options;

        Context = new EntityDataContext(options);
        Context.Database.EnsureCreated();
        Initialize(Context);

        var brandRepository = new BrandRepository(Context);
        var service = new BrandService(brandRepository);
        Controller = new BrandController(service);
    }

    private static void Initialize(EntityDataContext context)
    {
        TestDataInitializer.InitializeBrands(context);
        context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
