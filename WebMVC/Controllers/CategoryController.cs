using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers;

public class CategoryController:Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService )
    {
        _categoryService = categoryService;
    }
     
    [HttpGet]
    public async Task<IActionResult> Index(string? title)
    {
        ViewData["title"] = title;
        var user = await _categoryService.GetCategories(title);
        return View(user);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CategoryDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto category)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.AddCategory(category);
            return RedirectToAction("Index"); 
        }
        else
        {
            return View(category);
        }
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var existing = await _categoryService.GetCategoryById(id);
        var result = (new CategoryDto()
        {
            Id = existing.Id,
            CategoryName = existing.CategoryName
        });
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(CategoryDto category)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.UpdateCategory(category); 
            return RedirectToAction("Index"); 
        }
        else
        {
            return View(category);
        }
    }

    public IActionResult Delete(int id)
    {
        _categoryService.DeleteCategory(id);
        return RedirectToAction("Index");
    }
 

}