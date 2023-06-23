using Dapper;
using Domain.Dtos;
using Infrastructure.Context;


namespace Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private DapperContext _context;

    public CategoryService(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> GetCategories(string? title)
    {
        using (var conn = _context.CreateConnection())
        {

            var sql = " select id, category_name as CategoryName from categories ";
            if (title != null)
            {
                
                  sql = " select id, category_name as CategoryName from categories where lower(category_name) LIKE lower(@title) ";
            }
            var result = await conn.QueryAsync<CategoryDto>(sql,new {title=$"%{title}%"});
            return result.ToList(); 
        }
    }

    public async Task<CategoryDto> AddCategory(CategoryDto category)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $" insert into categories(category_name)values(@CategoryName) returning id";
            var result = await conn.ExecuteScalarAsync<int>(sql, category); 
            category.Id = result;
            return category;
        }
    }

    public async Task<CategoryDto> GetCategoryById(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $" select id, category_name as CategoryName  from categories where id = @id";
            var result = await conn.QuerySingleOrDefaultAsync<CategoryDto>(sql, new { id });
            return result;
        }
    }

    public async Task<CategoryDto> UpdateCategory(CategoryDto category)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $" update categories set category_name = @CategoryName where id = @Id";
            var result = await conn.ExecuteAsync(sql, category);
            category.Id = result;
            return category;
        }
    }

    public async Task<bool> DeleteCategory(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $" delete from categories where id = @id";
            var result = await conn.ExecuteAsync(sql, new { id });
            if (result > 0) return true;
            return false;
        }
    }
}