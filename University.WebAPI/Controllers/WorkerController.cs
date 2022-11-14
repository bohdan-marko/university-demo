using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIS.DAL.Models;
using University.WebAPI.Services.Abstract;

namespace University.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : Controller
    {
        private readonly IBaseService<Worker> _service;

        public WorkerController(IBaseService<Worker> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


        [HttpGet("{workerId}")]
        public async Task<IActionResult> GetWorker(int workerId)
        {
            var worker = await _service.Get(workerId);
            return worker is null ? NotFound() : Ok(worker);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetWorkers()
        {
            var workers = await _service.GetAll();
            return workers is null ? NotFound() : Ok(workers);
        }

        
        [HttpPost("post")]
        public async Task<IActionResult> CreateWorker([FromBody] Worker worker)
        {
            var result = await _service.Insert(new Worker
            {
                WorkerID = worker.WorkerID,
                Name = worker.Name,
                EmailAddress = worker.EmailAddress,
                IsAdmin = worker.IsAdmin,
                WorkplaceID = worker.WorkplaceID,
                Workplace = worker.Workplace,
                Jobs = worker.Jobs
            });

            return result ? BadRequest() : Ok();
        }

        
        [HttpPut("put")]
        public async Task<IActionResult> UpdateWorker([FromBody] Worker worker)
        {
            var result = await _service.Update(new Worker
            {
                WorkerID = worker.WorkerID,
                Name = worker.Name,
                EmailAddress = worker.EmailAddress,
                IsAdmin = worker.IsAdmin,
                WorkplaceID = worker.WorkplaceID,
                Workplace = worker.Workplace,
                Jobs = worker.Jobs
            });
            
            return result ? BadRequest() : Ok();
        }

        
        [HttpDelete("{workerId}/delete")]
        public async Task<IActionResult> DeleteWorker(int workerId)
        {
            var result = await _service.Delete(workerId);
            return result ? BadRequest() : Ok();
        }
    }
}
