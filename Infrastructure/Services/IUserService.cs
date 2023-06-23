using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public interface IUserService
{
    Task<List<UserDto>> GetUsers(string? name);
    Task<UserDto> GetUserById(int id);
    Task<UserDto> AddUser(UserDto user);
    Task<UserDto> UpdateUser(UserDto user);
    Task<bool> DeleteUser(int id);
}