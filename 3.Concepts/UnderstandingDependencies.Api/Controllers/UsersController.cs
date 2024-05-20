using Microsoft.AspNetCore.Mvc;
using UnderstandingDependencies.Api.Repositories;
using UnderstandingDependencies.Api.Services;

namespace UnderstandingDependencies.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController()
    {
        _userService = new UserService(new UserRepository());
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }
}
