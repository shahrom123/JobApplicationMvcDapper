using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("controller")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("GetCategories")]
    public async Task<List<CategoryDto>> GetCategories(string? name)
    {
        return await _categoryService.GetCategories(name);
    }

    [HttpGet("GetCategoryById")]
    public async Task<CategoryDto> GetCategoryById(int id)
    {
        return await _categoryService.GetCategoryById(id);
    }
    [HttpPost("AddCategory")]
    public async Task<CategoryDto> AddCategory(CategoryDto category)
    {
        return await _categoryService.AddCategory(category);
    }

    [HttpPut("UpdateCategory")]
    public async Task<CategoryDto> UpdateCategory([FromForm] CategoryDto category)
    {
        return await _categoryService.UpdateCategory(category);
    }

    [HttpDelete("DeleteCategory")]
    public async Task<bool> DeleteCategory(int id)
    {
        return await _categoryService.DeleteCategory(id);
    }
}