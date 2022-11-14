using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIS.DAL.Models;
using University.WebAPI.Services.Abstract;

namespace University.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private readonly IBaseService<Job> _service;

        public JobController(IBaseService<Job> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


        [HttpGet("{jobId}")]
        public async Task<IActionResult> GetJob(int jobId)
        {
            var job = await _service.Get(jobId);
            return job is null ? NotFound() : Ok(job);
        }


        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await _service.GetAll();
            return jobs is null ? NotFound() : Ok(jobs);
        }


        [HttpPost("post")]
        public async Task<IActionResult> CreateJob([FromBody] Job job)
        {
            var result = await _service.Insert(new Job
            {
                JobID = job.JobID,
                Description = job.Description,
                Priority = job.Priority,
                Workers = job.Workers
            });

            return result ? BadRequest() : Ok();
        }


        [HttpPut("put")]
        public async Task<IActionResult> UpdateJob([FromBody] Job job)
        {
            var result = await _service.Update(new Job
            {
                JobID = job.JobID,
                Description = job.Description,
                Priority = job.Priority,
                Workers = job.Workers
            });

            return result ? BadRequest() : Ok();
        }


        [HttpDelete("{jobId}/delete")]
        public async Task<IActionResult> DeleteJob(int jobId)
        {
            var result = await _service.Delete(jobId);
            return result ? BadRequest() : Ok();
        }
    }
}
