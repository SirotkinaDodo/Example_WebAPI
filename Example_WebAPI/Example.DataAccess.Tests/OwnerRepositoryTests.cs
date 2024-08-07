using AutoFixture;
using Example.DataAccess.Models;
using Example.DataAccess.Repositories;
using Example.DataAccess.Repositories.Contracts;
using FluentAssertions;

namespace Example.DataAccess.Tests;

public class OwnerRepositoryTests
{
    private readonly IOwnerRepository _repository = new OwnerRepository(InMemoryDbContext.GetInMemoryDbContext());
    private readonly IFixture _fixture = new Fixture();

    
    
    [Fact]
    public async Task GetFull_ShouldReturnSpecificBreed()
    {
        // Arrange
        var breeds = _fixture.CreateMany<Breed>(3).ToList();
        const string testData = "some text";
        breeds.Add(new Breed { Id = Guid.NewGuid(), Name = testData });
        await FillBDbData(breeds);

        // Act
        var breedsFromDb = _repository.GetFull();

        // Assert
        breedsFromDb.ToList().Should().NotBeEmpty();
    }
    
    private async Task FillBDbData(List<Breed> breeds)
    {
        var dog = new Dog { Id = Guid.NewGuid(), Name = "Tracy", Breed = Breeds[7] };
        var owners = new List<Owner>
        {
            new Owner
            {
                Id = Guid.NewGuid(),
                FirstName = "Jack",
                LastName = "Pitersen",
                Dogs = new List<Dog>
                {
                    new Dog { Id = Guid.NewGuid(), Name = "Missy", Breed = Breeds[3] },
                    new Dog { Id = Guid.NewGuid(), Name = "Mailo", Breed = Breeds[4] },
                }
            },
            new Owner
            {
                Id = Guid.NewGuid(),
                FirstName = "Medison",
                LastName = "Adams",
                Dogs = new List<Dog> { dog }
            },
            new Owner
            {
                Id = Guid.NewGuid(),
                FirstName = "Sam",
                LastName = "Adams",
                Dogs = new List<Dog> { dog }
            },
        };
        owners.ForEach(async owner => await _repository.Create(owner));
    }

    private List<Breed> Breeds = new List<Breed>
    {
        new Breed { Id = Guid.NewGuid(), Name = "Alaskan Klee Kai" },
        new Breed { Id = Guid.NewGuid(), Name = "Airedale Terrier" },
        new Breed { Id = Guid.NewGuid(), Name = "American Staffordshire Terrier" },
        new Breed { Id = Guid.NewGuid(), Name = "Akita" },
        new Breed { Id = Guid.NewGuid(), Name = "Alaskan Malamute" },
        new Breed { Id = Guid.NewGuid(), Name = "Bernese Mountain Dog" },
        new Breed { Id = Guid.NewGuid(), Name = "Australian Shepherd" },
        new Breed { Id = Guid.NewGuid(), Name = "Poodle (Standard)" },
    };
}