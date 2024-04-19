namespace Testing.Tests;

using Testing.Fixtures;

using App.Contracts;
using App.Controllers;
using App.Data;

using Microsoft.AspNetCore.Mvc;

public class FamilyTest : IClassFixture<TestFixtureFamilyController>
{
    private readonly FamilyController _controller;
    public FamilyTest(TestFixtureFamilyController fixture)
    {
        _controller = fixture.Controller;
    }

    [Fact]
    public async void CreateFamily_ReturnsOkResult()
    {
        // Arrange
        var family = new FamilyPostDto()
        {
            Name = "Family 4"
        };

        // Act
        var okResult = await _controller.CreateFamily(family);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsType<FamilyGetDto>(result.Value);
        Assert.Equal("Family 4", model.Name);
    }

    [Fact]
    public async void GetFamilies_ReturnsOkResult()
    {
        // Act
        var okResult = await _controller.GetFamiliesList();

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<FamilyGetDto>>(result.Value);
        Assert.True(model.Count() > 0);
    }

    [Fact]
    public async void GetFamilies_ReturnsFullDetails()
    {
        // Act
        var okResult = await _controller.GetFamiliesList("full");

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsAssignableFrom<IEnumerable<FamilyGetDetailDto>>(result.Value);
        Assert.True(model.Count() > 0);
    }

    [Fact]
    public async void GetFamilyById_ReturnsOkResult()
    {
        // Act
        var okResult = await _controller.GetFamilyById(1);

        // Assert
        var result = Assert.IsType<OkObjectResult>(okResult);
        var model = Assert.IsType<FamilyGetDetailDto>(result.Value);
        Assert.Equal(1, model.Id);
    }

    [Fact]
    public async void GetFamilyById_ReturnsNotFound()
    {
        // Act
        var notFoundResult = await _controller.GetFamilyById(100);

        // Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }

    [Fact]
    public async void DeleteFamilySoftDefault_ReturnsNoContent()
    {
        // Arrange
        int id = 1;

        // Act
        var noContentResult = await _controller.DeleteFamily(id);
        var result = await _controller.GetFamilyById(id) as OkObjectResult;
        var model = Assert.IsType<FamilyGetDetailDto>(result?.Value);

        // Assert
        Assert.IsType<NoContentResult>(noContentResult);
        Assert.True(model.IsDown);
        Assert.NotNull(model.DownAt);
    }

    [Fact]
    public async void DeleteFamilyHard_ReturnsNoContent()
    {
        // Given
        int id = 4;

        // When
        var noContentResult = await _controller.DeleteFamily(id, "hard");
        var result = await _controller.GetFamilyById(id);

        // Then
        Assert.IsType<NoContentResult>(noContentResult);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async void UpFamily_ReturnsOkResult()
    {
        // Given
        int id = 1;
        await _controller.DeleteFamily(id);

        // When
        var okResult = await _controller.UpFamily(id);
        var family = await _controller.GetFamilyById(id) as OkObjectResult;
        var model = family?.Value as FamilyGetDetailDto;

        // Then
        Assert.IsType<OkObjectResult>(okResult);
        Assert.True(model != null);
        Assert.False(model.IsDown);
        Assert.Null(model.DownAt);
    }

    [Fact]
    public async void UpFamily_ReturnsNotFound()
    {
        // Given
        int id = 100;

        // When
        var notFoundResult = await _controller.UpFamily(id);

        // Then
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }

    [Fact]
    public async void UpdateFamily_ReturnsOkResult()
    {
        // Given
        int id = 1;
        var family = new FamilyPatchDto()
        {
            Name = "Family 1 Updated"
        };

        // When
        var okResult = await _controller.UpdateFamily(id, family);
        var result = await _controller.GetFamilyById(id) as OkObjectResult;
        var model = Assert.IsType<FamilyGetDetailDto>(result?.Value);

        // Then
        Assert.IsType<OkObjectResult>(okResult);
        Assert.Equal("Family 1 Updated", model.Name);
    }

    [Fact]
    public async void UpdateFamily_ReturnsNotFound()
    {
        // Given
        int id = 100;
        var family = new FamilyPatchDto()
        {
            Name = "Family 1 Updated"
        };

        // When
        var notFoundResult = await _controller.UpdateFamily(id, family);

        // Then
        Assert.IsType<NotFoundObjectResult>(notFoundResult);
    }
}