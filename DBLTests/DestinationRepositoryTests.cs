using DBLApi.Models;
using DBLApi.Repositories;
using DBLApi.Data;
using Moq;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DBLTests;

[TestClass]
public class DestinationRepositoryTests
{
    private readonly DestinationRepository _destinationRepo;
    private readonly Mock<DataContext> _context = new Mock<DataContext>();

    public DestinationRepositoryTests()
    {
        _destinationRepo = new DestinationRepository(_context.Object);
    }

    [TestMethod]
    public async Task DestinationExists_Test()
    {
        //arrange
        var destinations = new List<Destination>
        {
        new Destination{Id = 1,Geolocation = "New York, USA",Title = "Exploring the Big Apple",Image = "image1.jpg",IsPublic = true,Description = "Experience the hustle and bustle of New York City."},
        new Destination{Id = 2,Geolocation = "Paris, France",Title = "A Romantic Getaway",Image = "image2.jpg",IsPublic = true,Description = "Discover the charm and beauty of Paris."},
        new Destination{Id = 3,Geolocation = "Tokyo, Japan",Title = "Immersive Cultural Experience",Image = "image3.jpg",IsPublic = true,Description = "Immerse yourself in the vibrant culture of Tokyo."}
        };

        //configuring fake db to take those values
        _context.Setup(x => x.Destinations).ReturnsDbSet(destinations);

        //
        var resultExistenceTrue = await _destinationRepo.DestinationExists("Exploring the Big Apple");
        var resultExistenceFalse = await _destinationRepo.DestinationExists("Some random input");

        //assert
        Assert.AreEqual(resultExistenceFalse, false);
        Assert.AreEqual(resultExistenceTrue, true);
    }


    [TestMethod]
    public async Task AddUserDestination_Test()
    {
        //arrange
        var destinations = new List<Destination>
        {
        new Destination{Id = 1,Geolocation = "New York, USA",Title = "Exploring the Big Apple",Image = "image1.jpg",IsPublic = true,Description = "Experience the hustle and bustle of New York City."},
        new Destination{Id = 2,Geolocation = "Paris, France",Title = "A Romantic Getaway",Image = "image2.jpg",IsPublic = true,Description = "Discover the charm and beauty of Paris."},
        new Destination{Id = 3,Geolocation = "Tokyo, Japan",Title = "Immersive Cultural Experience",Image = "image3.jpg",IsPublic = true,Description = "Immerse yourself in the vibrant culture of Tokyo."}
        };

        var users = new List<User>
        {
        new User{Id = 1,Username = "john_doe",Password = "password123",Email = "john.doe@example.com",Role = Roles.User,Salt = "somesaltvalue",Birthday = new DateTime(1990, 5, 15)},
        new User{Id = 2,Username = "jane_smith",Password = "pa$$w0rd",Email = "jane.smith@example.com",Role = Roles.User,Salt = "anothersaltvalue",Birthday = new DateTime(1985, 10, 22)},
        new User{Id = 3,Username = "alexander",Password = "securepassword",Email = "alexander@example.com",Role = Roles.User,Salt = "yetsomeothersalt",Birthday = new DateTime(1995, 8, 3)}
        };

        //configuring fake db to take those values
        _context.Setup(x => x.Destinations).ReturnsDbSet(destinations);
        _context.Setup(x => x.Users).ReturnsDbSet(users);
        _context.Setup(x => x.StayDates).ReturnsDbSet(new List<StayDates> { new StayDates { Id = 1, UserId = 3, DestinationId = 1, StartDate = new DateTime(2022, 2, 1), EndDate = new DateTime(2022, 5, 4) }, });

        //
        await _destinationRepo.AddUserDestination(1, 1, new DateTime(2023, 6, 8), new DateTime(2024, 1, 2));
        //var stayDatesAddResult = await _destinationRepo.GetUserDestinations(1, 1, 10);
        var expectedList = new List<StayDates?>() { new StayDates { Id = 1, UserId = 1, DestinationId = 1, StartDate = new DateTime(2023, 6, 8), EndDate = new DateTime(2024, 1, 2) } };

        //assert
        Assert.AreEqual(expectedList.Count, _context.Object.StayDates.ToList().Count - 1);
    }

    [TestMethod]
    public async Task DeleteUserDestination_Test()
    {
        //arrange
        var destinations = new List<Destination>
        {
        new Destination{Id = 1,Geolocation = "New York, USA",Title = "Exploring the Big Apple",Image = "image1.jpg",IsPublic = true,Description = "Experience the hustle and bustle of New York City."},
        new Destination{Id = 2,Geolocation = "Paris, France",Title = "A Romantic Getaway",Image = "image2.jpg",IsPublic = true,Description = "Discover the charm and beauty of Paris."},
        new Destination{Id = 3,Geolocation = "Tokyo, Japan",Title = "Immersive Cultural Experience",Image = "image3.jpg",IsPublic = true,Description = "Immerse yourself in the vibrant culture of Tokyo."}
        };

        var users = new List<User>
        {
        new User{Id = 1,Username = "john_doe",Password = "password123",Email = "john.doe@example.com",Role = Roles.User,Salt = "somesaltvalue",Birthday = new DateTime(1990, 5, 15)},
        new User{Id = 2,Username = "jane_smith",Password = "pa$$w0rd",Email = "jane.smith@example.com",Role = Roles.User,Salt = "anothersaltvalue",Birthday = new DateTime(1985, 10, 22)},
        new User{Id = 3,Username = "alexander",Password = "securepassword",Email = "alexander@example.com",Role = Roles.User,Salt = "yetsomeothersalt",Birthday = new DateTime(1995, 8, 3)}
        };

        var stayDates = new List<StayDates>{
        new StayDates{Id =1,UserId =1, DestinationId =1, StartDate = new DateTime(2023, 6, 8), EndDate = new DateTime(2023, 6, 8)},
        new StayDates{Id =2,UserId =1, DestinationId =3, StartDate = new DateTime(2021, 2, 1), EndDate = new DateTime(2021, 2, 7)},
        new StayDates{Id =3,UserId =2, DestinationId =3, StartDate = new DateTime(2020, 1, 8), EndDate = new DateTime(2020, 4, 2)},
        new StayDates{Id =4,UserId =3, DestinationId =1, StartDate = new DateTime(2022, 2, 1), EndDate = new DateTime(2022, 5, 4)},
        new StayDates{Id =5,UserId =3, DestinationId =2, StartDate = new DateTime(2022, 1, 2), EndDate = new DateTime(2022, 8, 9)}
        };
        //configuring fake db to take those values
        _context.Setup(x => x.Destinations).ReturnsDbSet(destinations);
        _context.Setup(x => x.Users).ReturnsDbSet(users);
        _context.Setup(x => x.StayDates).ReturnsDbSet(stayDates);

        //
        await _destinationRepo.DeleteUserDestination(1, 1);
        var stayDatesDeleteResult = await _context.Object.StayDates.FirstOrDefaultAsync(x => x.UserId == 1);
        var expectedResult = 3;

        //assert
        Assert.AreEqual(expectedResult, stayDatesDeleteResult.DestinationId);
    }

}