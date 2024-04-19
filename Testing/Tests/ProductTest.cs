namespace Testing.Tests;

using Testing.Fixtures;

using App.Contracts;
using App.Controllers;

using Microsoft.AspNetCore.Mvc;

public class ProductTest : IClassFixture<TestFixtureProductController>
{
    public ProductController _controller;
    public ProductTest(TestFixtureProductController fixture)
    {
        _controller = fixture.Controller;
    }

    [Fact]
    public async void CreateProduct_ReturnsOkResult()
    {
        // Arrange
        var product = new ProductPostDto()
        {
            Code = "P6",
            Name = "Product 6",
            Description = "Description 6",
            PriceCost = 50,
            PriceSale = 100,
            Stock = 10,
            FamilyId = 1,
            BrandId = 1,
            Tags = new List<string> { "tag-1", "tag-2" }
        };

        // Act
        var okResult = await _controller.CreateProduct(product);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsType<ProductGetDetailDto>(result.Value);
        Assert.Equal("Product 6", model.Name);
    }

    [Fact]
    public async void CreateProduct_ReturnsNotFoundBrand()
    {
        // Arrange
        var product = new ProductPostDto()
        {
            Code = "P101",
            Name = "Product 101",
            Description = "Description 101",
            PriceCost = 50,
            PriceSale = 100,
            Stock = 10,
            FamilyId = 1,
            BrandId = 99,
            Tags = new List<string> { "tag-1", "tag-2" }
        };

        // Act
        var notFoundResult = await _controller.CreateProduct(product);

        // Assert
        var result = Assert.IsType<NotFoundObjectResult>(notFoundResult);
        Assert.Equal("Brand not found or has been deleted.", result.Value);
    }

    [Fact]
    public async void CreateProduct_ReturnsNotFoundFamily()
    {
        // Arrange
        var product = new ProductPostDto()
        {
            Code = "P100",
            Name = "Product 100",
            Description = "Description 100",
            PriceCost = 50,
            PriceSale = 100,
            Stock = 10,
            FamilyId = 99,
            BrandId = 1,
            Tags = new List<string> { "tag-1", "tag-2" }
        };

        // Act
        var notFoundResult = await _controller.CreateProduct(product);

        // Assert
        var result = Assert.IsType<NotFoundObjectResult>(notFoundResult);
        Assert.Equal("Family not found or has been deleted.", result.Value);
    }

    [Fact]
    public async void CreateProduct_ReturnsBadRequest_ExistsProduct()
    {
        // Arrange
        var product = new ProductPostDto()
        {
            Code = "P1",
            Name = "Product 5",
            Description = "Description 5",
            PriceCost = 50,
            PriceSale = 100,
            Stock = 10,
            FamilyId = 1,
            BrandId = 1,
            Tags = new List<string> { "tag-1", "tag-2" }
        };

        // Act
        var badRequestResult = await _controller.CreateProduct(product);

        // Assert
        var result = Assert.IsType<BadRequestObjectResult>(badRequestResult);
        Assert.Equal("Product already exists. We have updated the existing product. Now it is active.", result.Value);
    }

    [Fact]
    public async void GetProductById_ReturnsOkResult()
    {
        // Arrange
        var id = 1;

        // Act
        var okResult = await _controller.GetProductById(id);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsType<ProductGetDetailDto>(result.Value);
        Assert.NotEmpty(model.Name);
    }

    [Fact]
    public async void GetProductById_RetursNotFound()
    {
        // Arrange
        var id = 100;

        // Act
        var notFoundResult = await _controller.GetProductById(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }

    [Fact]
    public async void GetListProducts_ReturnsOkResult()
    {
        // Act
        var okResult = await _controller.GetProductsList();

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<ProductGetDto>>(result.Value);
        Assert.True(model.Count() > 0);
    }

    [Fact]
    public async void GetListProducts_ReturnsFilteredByStatus()
    {
        // Arrange
        string status = "down";
        int expected = 1;

        // Act
        var okResult = await _controller.GetProductsList(status: status);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<ProductGetDto>>(result.Value);
        Assert.Equal(expected, model.Count());
    }

    [Fact]
    public async void GetListProducts_ReturnsFilteredByStatusUp()
    {
        // Arrange
        string status = "up";
        int expected = 5;

        // Act
        var okResult = await _controller.GetProductsList(status: status);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<ProductGetDto>>(result.Value);
        Assert.Equal(expected, model.Count());
    }

    [Fact]
    public async void GetListProducts_ReturnsFullDetails()
    {
        // Act
        var okResult = await _controller.GetProductsList(details: "full");

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<ProductGetDetailDto>>(result.Value);
        Assert.True(model.Count() > 0);
    }

    [Fact]
    public async void GetListProducts_ReturnsFilteredGreaterThan()
    {
        // Arrange
        int stockGreaterThan = 19;
        int expected = 3;

        // Act
        var okResult = await _controller.GetProductsList(stockGreaterThan: stockGreaterThan);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<ProductGetDto>>(result.Value);
        Assert.True(model.Count() >= expected);
    }

    [Fact]
    public async void GetListProducts_ReturnsFilteredLessThan()
    {
        // Arrange
        int stockLessThan = 11;
        int expected = 1;

        // Act
        var okResult = await _controller.GetProductsList(stockLessThan: stockLessThan);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<ProductGetDto>>(result.Value);
        Assert.True(model.Count() >= expected);
    }

    [Fact]
    public async void GetListProducts_ReturnsFilteredCode()
    {
        // Arrange
        string code = "P1";
        int expected = 1;

        // Act
        var okResult = await _controller.GetProductsList(code: code);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<ProductGetDto>>(result.Value);
        Assert.True(model.Count() >= expected);
    }

    [Fact]
    public async void GetListProducts_ReturnsFilteredName()
    {
        // Arrange
        string name = "Product 1";
        int expected = 1;

        // Act
        var okResult = await _controller.GetProductsList(name: name);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<ProductGetDto>>(result.Value);
        Assert.True(model.Count() >= expected);
    }

    [Fact]
    public async void GetListProducts_ReturnsFilteredBrand()
    {
        // Arrange
        int brandId = 1;
        int expected = 1;

        // Act
        var okResult = await _controller.GetProductsList(brandId: brandId);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<ProductGetDto>>(result.Value);
        Assert.True(model.Count() >= expected);
    }

    [Fact]
    public async void GetListProducts_ReturnsFilteredFamily()
    {
        // Arrange
        int familyId = 1;
        int expected = 1;

        // Act
        var okResult = await _controller.GetProductsList(familyId: familyId);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<ProductGetDto>>(result.Value);
        Assert.True(model.Count() >= expected);
    }

    [Fact]
    public async void GetProductById_ReturnsOkResultFullDetails()
    {
        // Arrange
        var id = 1;

        // Act
        var okResult = await _controller.GetProductById(id);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsType<ProductGetDetailDto>(result.Value);
        Assert.Equal("Product 1", model.Name);
    }

    [Fact]
    public async void GetProductById_ReturnsNotFound()
    {
        // Arrange
        var id = 100;

        // Act
        var notFoundResult = await _controller.GetProductById(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }

    [Fact]
    public async void DeleteProductSoftDefault_ReturnsNoContent()
    {
        // Arrange
        var productResult = await _controller.CreateProduct(new ProductPostDto()
        {
            Code = "P1000",
            Name = "Product 1000",
            Description = "Description 1000",
            PriceCost = 50,
            PriceSale = 100,
            Stock = 10,
            FamilyId = 1,
            BrandId = 1,
            Tags = new List<string> { "tag-1", "tag-2" }
        }) as OkObjectResult;
        var productToDelete = (productResult as OkObjectResult)?.Value as ProductGetDetailDto;
        var id = productToDelete != null ? productToDelete.Id : 0;

        // Act
        var noContentResult = await _controller.DeleteProduct(id);
        var result = await _controller.GetProductById(id) as OkObjectResult;
        var model = result?.Value as ProductGetDetailDto;

        // Assert
        Assert.IsType<NoContentResult>(noContentResult);
        Assert.True(model != null);
        Assert.True(model.IsDown);
        Assert.NotNull(model.DownAt);

        // Clean
        await _controller.DeleteProduct(id, "hard");
    }

    [Fact]
    public async void DeleteProductHardDefault_ReturnsNoContent()
    {
        // Arrange
        string type = "hard";
        var productResult = await _controller.CreateProduct(new ProductPostDto()
        {
            Code = "P1000",
            Name = "Product 1000",
            Description = "Description 1000",
            PriceCost = 50,
            PriceSale = 100,
            Stock = 10,
            FamilyId = 1,
            BrandId = 1,
            Tags = new List<string> { "tag-1", "tag-2" }
        }) as OkObjectResult;
        var productToDelete = (productResult as OkObjectResult)?.Value as ProductGetDetailDto;
        var id = productToDelete != null ? productToDelete.Id : 0;

        // Act
        var noContentResult = await _controller.DeleteProduct(id, type);
        var result = await _controller.GetProductById(id);

        // Assert
        Assert.IsType<NoContentResult>(noContentResult);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async void PatchProduct_ReturnsOkResult()
    {
        // Arrange
        var id = 1;
        var product = new ProductPatchDto()
        {
            Name = "Product 1 Updated"
        };

        // Act
        var okResult = await _controller.UpdateProduct(id, product);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsType<ProductGetDetailDto>(result.Value);
        Assert.Equal("Product 1 Updated", model.Name);
    }

    [Fact]
    public async void PatchProductStock_StockLessThanZero_ReturnsBadRequest()
    {
        // Arrange
        var id = 1;
        var product = new ProductPatchDto()
        {
            Stock = -1
        };

        // Act
        var badRequestResult = await _controller.UpdateProduct(id, product);

        // Assert
        Assert.IsType<BadRequestObjectResult>(badRequestResult);
    }

    [Fact]
    public async void PatchProduct_ReturnsNotFound()
    {
        // Arrange
        var id = 99;
        var product = new ProductPatchDto()
        {
            Name = "Product 1 Updated"
        };

        // Act
        var notFoundResult = await _controller.UpdateProduct(id, product);

        // Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }

    [Fact]
    public async void PatchFullProduct_ReturnsOkResult()
    {
        // Arrange
        var id = 1;
        var product = new ProductPatchDto()
        {
            Name = "Product 1 Updated",
            Description = "Description 1 Updated",
            PriceCost = 60,
            PriceSale = 120,
            Stock = 20,
            FamilyId = 2,
            BrandId = 2,
            Tags = new List<string> { "tag-1", "tag-2", "tag-3" }
        };

        // Act
        var okResult = await _controller.UpdateProduct(id, product);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsType<ProductGetDetailDto>(result.Value);

        Assert.Equal("Product 1 Updated", model.Name);
        Assert.Equal("Description 1 Updated", model.Description);
        Assert.Equal(60, model.PriceCost);
        Assert.Equal(120, model.PriceSale);
        Assert.Equal(20, model.Stock);
        Assert.Equal(2, model.Family.Id);
        Assert.Equal(2, model.Brand.Id);
        if (model.Tags != null){
            Assert.Equal(5, model.Tags.Count());
        }
    }

    [Fact]
    public async void PatchFullProduct_ReturnsNotFound()
    {
        // Arrange
        var id = 99;
        var product = new ProductPatchDto()
        {
            Name = "Product 1 Updated",
            Description = "Description 1 Updated",
            PriceCost = 60,
            PriceSale = 120,
            Stock = 20,
            FamilyId = 2,
            BrandId = 2,
            Tags = new List<string> { "tag-1", "tag-2", "tag-3" }
        };

        // Act
        var notFoundResult = await _controller.UpdateProduct(id, product);

        // Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }

    [Fact]
    public async void PatchFullProduct_ReturnsNotFoundBrand()
    {
        // Arrange
        var id = 1;
        var product = new ProductPatchDto()
        {
            Name = "Product 1 Updated",
            Description = "Description 1 Updated",
            PriceCost = 60,
            PriceSale = 120,
            Stock = 20,
            FamilyId = 2,
            BrandId = 99,
            Tags = new List<string> { "tag-1", "tag-2", "tag-3" }
        };

        // Act
        var notFoundResult = await _controller.UpdateProduct(id, product);

        // Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }

    [Fact]
    public async void PatchFullProduct_ReturnsNotFoundFamily()
    {
        // Arrange
        var id = 1;
        var product = new ProductPatchDto()
        {
            Name = "Product 1 Updated",
            Description = "Description 1 Updated",
            PriceCost = 60,
            PriceSale = 120,
            Stock = 20,
            FamilyId = 99,
            BrandId = 2,
            Tags = new List<string> { "tag-1", "tag-2", "tag-3" }
        };

        // Act
        var notFoundResult = await _controller.UpdateProduct(id, product);

        // Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }

    [Fact]
    public async void UpProduct_ReturnsOkResult()
    {
        // Arrange
        var id = 1;

        // Act
        var okResult = await _controller.UpProduct(id);
        var result = await _controller.GetProductById(id) as OkObjectResult;
        var model = result?.Value as ProductGetDetailDto;

        // Assert
        Assert.IsType<OkResult>(okResult);
        Assert.True(model != null);
        Assert.False(model.IsDown);
    }

    [Fact]
    public async void UpProduct_ReturnsNotFound()
    {
        // Arrange
        var id = 100;

        // Act
        var notFoundResult = await _controller.UpProduct(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }
}