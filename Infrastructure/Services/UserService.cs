using Dapper;
using Domain.Dtos;
using Infrastructure.Context;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly DapperContext _context;

    public UserService(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<UserDto>> GetUsers(string? name)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = " select id, first_name as FirstName, last_name as LastName, email, phone_number as PhoneNumber, address, job_id as JobId  from users ";
            if (name != null)
            {
                sql = " select id, first_name as FirstName, last_name as LastName, email, phone_number as PhoneNumber, address, job_id as JobId from users where lower(first_name)  LIKE lower(@name) ";
            }

            var result = await conn.QueryAsync<UserDto>(sql, new { name = $"%{name}%" });
            return result.ToList();
        }
    }

    public async Task<UserDto> GetUserById(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql =
                $" select id, first_name as FirstName, last_name as LastName, email, phone_number as PhoneNumber, address, job_id as JobId from Users where id = @id";
            var result = await conn.QuerySingleAsync<UserDto>(sql, new { id });
            return result;
        }
    }


    public async Task<UserDto> AddUser(UserDto user)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql =
                " insert into Users(first_name, last_name, email, phone_number, address, job_Id) values(@FirstName, @LastName, @Email,  @PhoneNumber, @Address, @JobId) returning id";
            var result = await conn.ExecuteScalarAsync<int>(sql, user);
            user.Id = result;
            return user;
        }
    }

    public async Task<bool> DeleteUser(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = " delete from users where id = @id";
            var result = await conn.ExecuteScalarAsync<int>(sql, new { id });
            if (result > 0) return true;
            return false;
        }
    }

    public async Task<UserDto> UpdateUser(UserDto user)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql =
                $" update users set first_name =@FirstName, last_name = @LastName, email =@Email , phone_number = @PhoneNumber, address = @Address, job_Id = @JobId  where id = @Id";
            var result = await conn.ExecuteAsync(sql, user);
            user.Id = result;
            return user;
        }
    }
}