namespace Testing.Fixtures;

using App.Data;
using App.Controllers;
using App.Services;

using Testing.Data;

using Microsoft.EntityFrameworkCore;

public class TestFixtureProductController : IDisposable
{
    public EntityDataContext Context { get; private set; }
    public ProductController Controller { get; private set; }

    public TestFixtureProductController()
    {
        var options = new DbContextOptionsBuilder<EntityDataContext>()
            .UseInMemoryDatabase(databaseName: "ProductTestDb")
            .Options;

        Context = new EntityDataContext(options);
        Context.Database.EnsureCreated();
        Initialize(Context);

        var brandRepository = new BrandRepository(Context);
        var productRepository = new ProductRepository(Context);
        var familyRepository = new FamilyRepository(Context);
        var tagRepository = new TagRepository(Context);

        var service = new ProductService(productRepository, brandRepository, familyRepository, tagRepository);
        Controller = new ProductController(service);
    }

    private static void Initialize(EntityDataContext context)
    {
        TestDataInitializer.InitializeProducts(context);
        context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
