using FluentAssertions;
using NSubstitute;
using UnderstandingDependencies.Api.Models;
using UnderstandingDependencies.Api.Repositories;
using UnderstandingDependencies.Api.Services;

namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();

    public UserServiceTests()
    {
        _sut = new UserService(_userRepository);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns(Array.Empty<AppUser>());

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
        users.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAListOfUsers_WhenUsersExist()
    {
        // Arrange
        var expectedUsers = new[]
        {
            new AppUser()
            {
                Id = 1,
                FullName = "Emir TARTAR",
                Age = 33,
                DateOfBirthDate = new(1993, 04, 29)
            }
        };

        _userRepository.GetAllAsync().Returns(expectedUsers);

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
        users.Should().ContainSingle(x => x.FullName == "Emir TARTAR");
        users.Should().NotBeEmpty();
    }
}
