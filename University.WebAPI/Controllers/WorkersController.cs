using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Application.Models.Create;
using University.Application.Models.Update;
using University.Application.Services.Abstract;

namespace University.Application.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkersController : Controller
    {
        private readonly IWorkerService _service;

        public WorkersController(IWorkerService service)
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


        [HttpPost]
        public async Task<IActionResult> CreateWorker([FromBody] WorkerCreateRequest worker)
        {
            var result = await _service.Insert(worker);
            return result ? BadRequest() : Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateWorker([FromBody] WorkerUpdateRequest worker)
        {
            var result = await _service.Update(worker);

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
