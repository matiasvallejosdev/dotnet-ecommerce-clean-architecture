namespace Testing.Data;

using App.Data;

public static class TestDataInitializer
{
    public static void InitializeBrands(EntityDataContext context)
    {
        context.Brands.AddRange(
            new Brand { Id = 1, Name = "Brand 1" },
            new Brand { Id = 2, Name = "Brand 2" },
            new Brand { Id = 3, Name = "Brand 3" },
            new Brand { Id = 4, Name = "Brand 4" },
            new Brand { Id = 5, Name = "Brand 5" }
        );
        context.SaveChanges();
    }

    public static void InitializeFamilies(EntityDataContext context)
    {
        context.Families.AddRange(
            new Family { Id = 1, Name = "Family 1" },
            new Family { Id = 2, Name = "Family 2" },
            new Family { Id = 3, Name = "Family 3" },
            new Family { Id = 4, Name = "Family 4" },
            new Family { Id = 5, Name = "Family 5" }
        );
        context.SaveChanges();
    }

    public static void InitializeTags(EntityDataContext context)
    {
        context.Tags.AddRange(
            new Tag { Id = 1, Name = "tag-1" },
            new Tag { Id = 2, Name = "tag-2" },
            new Tag { Id = 3, Name = "tag-3" },
            new Tag { Id = 4, Name = "tag-4" }
        );
        context.SaveChanges();
    }

    public static void InitializeProducts(EntityDataContext context)
    {
        InitializeBrands(context);
        InitializeFamilies(context);
        InitializeTags(context);

        var brands = context.Brands.ToList();
        var families = context.Families.ToList();
        var tags = context.Tags.ToList();

        var product1 = new Product
        {
            Id = 1,
            Code = "P1",
            Name = "Product 1",
            Description = "Description 1",
            PriceCost = 50,
            PriceSale = 100,
            Stock = 10,
            Family = families[0],
            Brand = brands[0],
            Tags = new List<Tag> { tags[0], tags[1] }
        };

        var product2 = new Product
        {
            Id = 2,
            Code = "P2",
            Name = "Product 2",
            Description = "Description 2",
            PriceCost = 100,
            PriceSale = 200,
            Stock = 20,
            Family = families[1],
            Brand = brands[1],
            Tags = new List<Tag> { tags[1], tags[2] }
        };

        var product3 = new Product
        {
            Id = 3,
            Code = "P3",
            Name = "Product 3",
            Description = "Description 3",
            PriceCost = 150,
            PriceSale = 300,
            Stock = 30,
            Family = families[2],
            Brand = brands[2],
            Tags = new List<Tag> { tags[2], tags[3] }
        };

        var product4 = new Product
        {
            Id = 4,
            Code = "P4",
            Name = "Product 4",
            Description = "Description 4",
            PriceCost = 200,
            PriceSale = 400,
            Stock = 40,
            Family = families[3],
            Brand = brands[3],
            Tags = new List<Tag> { tags[3], tags[0] }
        };

        var product5 = new Product
        {
            Id = 5,
            Code = "P5",
            Name = "Product 5",
            Description = "Description 5",
            PriceCost = 250,
            PriceSale = 500,
            Stock = 50,
            Family = families[4],
            Brand = brands[4],
            Tags = new List<Tag> { tags[0], tags[1], tags[2] },
            IsDown = true,
            DownAt = DateTime.Now
        };

        context.Products.AddRange(product1, product2, product3, product4, product5);
        context.SaveChanges();
    }
}