using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("controller")]
public class JobController : ControllerBase
{
    private readonly IJobService _quoteService;

    public JobController(IJobService quoteService)
    {
        _quoteService = quoteService;
    }

    [HttpGet("GetJobsAsync")]
    public async Task<List<JobDto>> GetJobAsync(string? title)
    {
        return await _quoteService.GetJobsAsync(title); 
    }

    [HttpGet("GetJobById")]
    public async Task<JobDto> GetJobById(int jobId)
    {
        return await _quoteService.GetJobById(jobId);
    }
    

    [HttpPost("AddJob")]
    public async Task<JobDto> AddJob(JobDto job)
    {
        return await _quoteService.AddJob(job);
    }

    [HttpPut("UpdateJob")]
    public async Task<JobDto> UpdateJob(JobDto job)
    {
        return await _quoteService.UpdateJob(job);
    }

    [HttpDelete("DeleteJob")]
    public async Task<bool> DeleteJob(int id)
    {
        return await _quoteService.DeleteJob(id);
    }
}