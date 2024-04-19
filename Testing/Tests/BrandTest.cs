namespace Testing.Tests;

using Testing.Fixtures;

using App.Contracts;
using App.Controllers;
using App.Data;

using Microsoft.AspNetCore.Mvc;

public class BrandTest : IClassFixture<TestFixtureBrandcontroller>
{
    private readonly BrandController _controller;

    public BrandTest(TestFixtureBrandcontroller fixture)
    {
        _controller = fixture.Controller;
    }

    [Fact]
    public async void GetBrands_ReturnsOkResult()
    {
        // Act
        var okResult = await _controller.GetBrandsList();

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<BrandGetDto>>(result.Value);
        Assert.True(model.Count() > 0);
    }

    [Fact]
    public async void GetBrands_ReturnsFullDetails()
    {
        // Act
        var okResult = await _controller.GetBrandsList("full");

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<BrandGetDetailDto>>(result.Value);
        Assert.True(model.Count() > 0);
    }

    [Fact]
    public async void CreateBrand_ReturnsOkResult()
    {
        // Arrange
        var brand = new BrandPostDto()
        {
            Name = "Brand 4"
        };

        // Act
        var okResult = await _controller.CreateBrand(brand);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsType<BrandGetDto>(result.Value);
        Assert.Equal("Brand 4", model.Name);
    }

    [Fact]
    public async void GetBrandById_ReturnsOkResult()
    {
        // Arrange
        var id = 1;

        // Act
        var okResult = await _controller.GetBrand(id);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsType<BrandGetDetailDto>(result.Value);
        Assert.Equal("Brand 1", model.Name);
    }

    [Fact]
    public async void GetBrandById_ReturnsNotFoundResult()
    {
        // Arrange
        var id = 99;

        // Act
        var notFoundResult = await _controller.GetBrand(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }

    [Fact]
    public async void UpdateBrandById_ReturnsOkResult()
    {
        // Arrange
        var id = 4;
        var brand = new BrandPatchDto()
        {
            Name = "Brand 1 Updated"
        };

        // Act
        var okResult = await _controller.UpdateBrand(id, brand);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsType<BrandGetDto>(result.Value);
        Assert.Equal("Brand 1 Updated", model.Name);
    }

    [Fact]
    public async void UpdateBrandById_ReturnsNotFound()
    {
        // Arrange
        var id = 99;
        var brand = new BrandPatchDto()
        {
            Name = "Brand 1 Updated"
        };

        // Act
        var notFoundResult = await _controller.UpdateBrand(id, brand);

        // Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }

    [Fact]
    public async void DeleteBrandSoftDefault_ReturnsNoContent()
    {
        // Arrange
        var id = 1;

        // Act
        var noContentResult = await _controller.DeleteBrand(id);
        var brand = await _controller.GetBrand(id) as OkObjectResult;
        var model = brand?.Value as BrandGetDetailDto;

        // Assert
        Assert.IsType<NoContentResult>(noContentResult);
        Assert.True(model != null);
        Assert.True(model?.IsDown);
        Assert.NotNull(model?.DownAt);
    }

    [Fact]
    public async void DeleteBrandHard_ReturnsNoContent()
    {
        // Arrange
        var id = 2;

        // Act
        var noContentResult = await _controller.DeleteBrand(id, "hard");
        var brand = await _controller.GetBrand(id) as NotFoundObjectResult;

        // Assert
        Assert.IsType<NoContentResult>(noContentResult);
        Assert.IsType<NotFoundObjectResult>(brand);
    }

    [Fact]
    public async void UpBrand_ReturnsOkResult()
    {
        // Arrange
        var id = 1;

        // Act
        var okResult = await _controller.UpBrand(id);
        var brand = await _controller.GetBrand(id) as OkObjectResult;
        var model = brand?.Value as BrandGetDetailDto;

        // Assert
        Assert.IsType<OkResult>(okResult);

        Assert.False(model?.IsDown);
        Assert.Null(model?.DownAt);
    }

    [Fact]
    public async void UpBrand_ReturnsNotFound()
    {
        // Arrange
        var id = 99;

        // Act
        var notFoundResult = await _controller.UpBrand(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }
}