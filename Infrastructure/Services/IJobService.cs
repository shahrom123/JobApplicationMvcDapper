using Domain.Dtos;
using System;


namespace Infrastructure.Services;

public interface IJobService
{
    Task<List<JobDto>> GetJobsAsync(string? title); 
    Task<JobDto> GetJobById(int id);
    Task<JobDto> AddJob(JobDto job);
    Task<JobDto> UpdateJob(JobDto job);
    Task<bool> DeleteJob(int id);
}