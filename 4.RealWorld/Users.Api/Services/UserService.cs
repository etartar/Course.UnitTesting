using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics;
using Users.Api.DTOs;
using Users.Api.Logging;
using Users.Api.Models;
using Users.Api.Repositories;

namespace Users.Api.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILoggerAdapter<UserService> _logger;

    public UserService(IUserRepository userRepository, ILoggerAdapter<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving all users");

        var stopWatch = Stopwatch.StartNew();

        try
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while retrieving all users");
            throw;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("All users retrieved in {0}ms", stopWatch.ElapsedMilliseconds);
        }
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving user with id : {0}", id);

        var stopWatch = Stopwatch.StartNew();

        try
        {
            return await _userRepository.GetByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while retrieving user with id : {0}", id);
            throw;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("User with id : {0} retrieved in {1}ms", id, stopWatch.ElapsedMilliseconds);
        }
    }

    public async Task<bool> CreateAsync(CreateUserDto request, CancellationToken cancellationToken = default)
    {
        CreateUserDtoValidator validationRules = new CreateUserDtoValidator();

        ValidationResult result = validationRules.Validate(request);

        if (!result.IsValid)
        {
            throw new ValidationException(string.Join(", ", result.Errors.Select(s => s.ErrorMessage)));
        }

        bool nameIsExist = await _userRepository.NameIsExist(request.FullName, cancellationToken);

        if (nameIsExist)
        {
            throw new ArgumentException("Name already exist");
        }

        User user = CreateUserDtoToUserObject(request);

        _logger.LogInformation("Creating user with id {0} and name: {1}", user.Id, user.FullName);

        var stopWatch = Stopwatch.StartNew();

        try
        {
            return await _userRepository.CreateAsync(user, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while creating a user");
            throw;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("User with id: {0} created in {1}ms", user.Id, stopWatch.ElapsedMilliseconds);
        }
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        User? user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            throw new ArgumentException("User not found");
        }

        _logger.LogInformation("Deleting user with id : {0}", user.Id);

        var stopWatch = Stopwatch.StartNew();

        try
        {
            return await _userRepository.DeleteAsync(user, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while deleting user");
            throw;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("User with id: {0} deleted in {1}ms", user.Id, stopWatch.ElapsedMilliseconds);
        }
    }

    public User CreateUserDtoToUserObject(CreateUserDto request)
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName
        };

        return user;
    }
}