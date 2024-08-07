using AutoFixture;
using Example.DataAccess.Models;
using Example.DataAccess.Repositories;
using Example.DataAccess.Repositories.Contracts;
using FluentAssertions;

namespace Example.DataAccess.Tests;

public class BreedRepositoryTests
{
    private readonly IBreedRepository _repository;
    private readonly IFixture _fixture = new Fixture();

    public BreedRepositoryTests()
    {
        _repository = new BreedRepository(InMemoryDbContext.GetInMemoryDbContext());
    }
    
    [Fact]
    public async Task Create_ShouldInsertNewBreed()
    {
        // Arrange
        var newBreed = _fixture.Create<Breed>();

        // Act
        await _repository.Create(newBreed);

        // Assert
        var addedBreed = _repository.GetBy(b => b.Name == newBreed.Name);
        addedBreed.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Delete_ShouldRemoveBreed()
    {
        // Arrange
        var breeds = _fixture.CreateMany<Breed>(3).ToList();
        await FillBreedData(breeds);
        var breedToRemoveId = breeds.First().Id;

        // Act
        await _repository.Delete(breedToRemoveId);

        // Assert
        var breedsFromDb = _repository.Get();
        breedsFromDb.Should().NotContain(b => b.Id == breedToRemoveId);
    }
    
    [Fact]
    public async Task Update_ShouldUpdateBreed()
    {
        // Arrange
        var breeds = _fixture.CreateMany<Breed>(3).ToList();
        await FillBreedData(breeds);

        var breedToUpdate = breeds.First();
        breedToUpdate.Name = "UPD name";

        // Act
        await _repository.Update(breedToUpdate);

        // Assert
        var updatedBreed = _repository.GetBy(b => b.Name == "UPD name");
        updatedBreed.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Get_ShouldReturnListOfBreed()
    {
        // Arrange
        var breeds = _fixture.CreateMany<Breed>(5).ToList();
        await FillBreedData(breeds);

        // Act
        var breedsFromDb = _repository.Get();

        // Assert
        breedsFromDb.Should().HaveCount(breeds.Count);
    }
    
    [Fact]
    public async Task Get_WithPredicate_ShouldReturnListOfBreed()
    {
        // Arrange
        var breeds = _fixture.CreateMany<Breed>(3).ToList();
        const string testData = "some text";
        const int testNumber = 3;
        for (var i = 0; i < testNumber; i++)
        { 
            breeds.Add(new Breed { Id = Guid.NewGuid(), Name = testData + i });
        }
        await FillBreedData(breeds);

        // Act
        var breedsFromDb = _repository.Get(b => b.Name.Contains(testData));

        // Assert
        breedsFromDb.Should().HaveCount(testNumber);
    }
    
    [Fact]
    public async Task GetBy_ShouldReturnSpecificBreed()
    {
        // Arrange
        var breeds = _fixture.CreateMany<Breed>(3).ToList();
        const string testData = "some text";
        breeds.Add(new Breed { Id = Guid.NewGuid(), Name = testData });
        await FillBreedData(breeds);

        // Act
        var breedsFromDb = _repository.GetBy(b => b.Name == testData);

        // Assert
        breedsFromDb.Should().NotBeNull();
        breedsFromDb?.Name.Should().Be(testData);
    }

    private async Task FillBreedData(List<Breed> breeds)
        => breeds.ForEach(async breed => await _repository.Create(breed));
}