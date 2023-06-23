using Dapper;
using Domain.Dtos;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class JobService : IJobService
{
    private readonly DapperContext _context;

    public JobService(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<JobDto>> GetJobsAsync(string? title)
    {
        using (var conn = _context.CreateConnection())
        {

            var sql = " select id, title, category_id as CategoryId, location, description from Jobs ";
            if (title != null)
            {
                
                sql = " select id, title, category_id as CategoryId, location, description  from jobs where lower(title) LIKE lower(@title) ";
            }
            var result = await conn.QueryAsync<JobDto>(sql,new {title=$"%{title}%"});
            return result.ToList(); 
        }
    }

    public async Task<JobDto> GetJobById(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql =
                $" select id, title,category_id as CategoryId, Location , Description from Jobs where id = @id";
            var result = await conn.QuerySingleAsync<JobDto>(sql, new { id });
            return result;
        }
    }
    public async Task<JobDto> AddJob(JobDto job)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = " insert into Jobs(Title, category_id,Location , Description) values(@Title, @CategoryId, @Location , @Description) returning id";
            var result = await conn.ExecuteScalarAsync<int>(sql, job);
            job.Id = result;
            return job;
        }
    }

    public async Task<JobDto> UpdateJob(JobDto job)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql =
                $" update Jobs set Title = @Title,category_id = @CategoryId, Location = @Location, Description = Description where id = @Id";
            var result = await conn.ExecuteAsync(sql, job);
            job.Id = result;
            return job;
        }
    }

    public async Task<bool> DeleteJob(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = " delete from Jobs where id = @id";
            var result = await conn.ExecuteAsync(sql, new { id });
            if (result > 0) return true;
            return false;
        }
    }
}