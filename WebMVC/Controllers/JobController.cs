using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers;

public class JobController : Controller
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? title)
    {
        ViewData["title"] = title;
        var job = await _jobService.GetJobsAsync(title);
        return View(job);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new JobDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(JobDto job)
    {
        if (ModelState.IsValid)
        {
            await _jobService.AddJob(job);
            return RedirectToAction("Index");
        }
        else
        {
            return View(job);
        }
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var existing = await _jobService.GetJobById(id);
        var result = (new JobDto()
        {
            Id = existing.Id,
            Title = existing.Title,
            CategoryId =existing.CategoryId,
            Location = existing.Location,
            Description = existing.Description
        });
        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> Update(JobDto job)
    {
        if (ModelState.IsValid)
        {
            await _jobService.UpdateJob(job); 
            return RedirectToAction("Index"); 
        }
        else
        {
            return View(job);
        }
    }
    public IActionResult Delete(int id)
    {
        _jobService.DeleteJob(id);
        return RedirectToAction("Index");
    }
}