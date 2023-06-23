using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(string? name)
    {
        ViewData["name"] = name; 
        var user = await _userService.GetUsers(name);
        return View(user); 
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View(new UserDto());
    }

    [HttpPost]
    public IActionResult Create(UserDto user)
    {
        if (ModelState.IsValid)
        {
            _userService.AddUser(user);
            return RedirectToAction("Index"); 
        }
        else
        {
            return View(user);  
        }
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var existing = await _userService.GetUserById(id);
        var result = (new UserDto()
        {
            Id = existing.Id,
            FirstName =existing.FirstName,
            LastName = existing.LastName,
            Email = existing.Email,
            PhoneNumber = existing.PhoneNumber,
            Address = existing.Address,
            JobId = existing.JobId
           
        });
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UserDto user)
    {
        if (ModelState.IsValid)
        {
            await _userService.UpdateUser(user); 
            return RedirectToAction("Index"); 
        }
        else
        {
            return View(user);
        }
    }
    public IActionResult Delete(int id)
    {
        _userService.DeleteUser(id);
        return RedirectToAction("Index");
    }
}