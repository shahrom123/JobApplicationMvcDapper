using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategories(string? name);
    Task<CategoryDto> GetCategoryById(int id);
    Task<CategoryDto> AddCategory(CategoryDto category);
    Task<CategoryDto> UpdateCategory(CategoryDto category);
    Task<bool> DeleteCategory(int id);
}