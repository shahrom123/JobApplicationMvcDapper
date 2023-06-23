using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("Controller")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetUsers")]
    public async Task<List<UserDto>> GetUsers(string? name)
    {
        return await _userService.GetUsers(name);
    }

    [HttpGet("GetUserById")]
    public async Task<UserDto> GetUserById(int id)
    {
        return await _userService.GetUserById(id);
    }

    [HttpPost("AddUser")]
    public async Task<UserDto> AddUser(UserDto user)
    {
        return await _userService.AddUser(user);
    }

    [HttpPut("UpdateUser")]
    public async Task<UserDto> UpdateUser(UserDto user)
    {
        return await _userService.UpdateUser(user);
    }

    [HttpDelete("DeleteUser")]
    public async Task<bool> DeleteUser(int id)
    {
        return await _userService.DeleteUser(id);
    }
}