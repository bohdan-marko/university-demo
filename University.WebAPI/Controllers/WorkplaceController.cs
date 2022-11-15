using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIS.DAL.Models;
using University.Application.Services.Abstract;

namespace University.Application.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkplaceController : Controller
    {
        private readonly IBaseService<Workplace> _service;

        public WorkplaceController(IBaseService<Workplace> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


        [HttpGet("{workplaceId}")]
        public async Task<IActionResult> GetWorkplace(int workplaceId)
        {
            var workplace = await _service.Get(workplaceId);
            return workplace is null ? NotFound() : Ok(workplace);
        }


        [HttpGet]
        public async Task<IActionResult> GetWorkplace()
        {
            var workplaces = await _service.GetAll();
            return workplaces is null ? NotFound() : Ok(workplaces);
        }


        [HttpPost("post")]
        public async Task<IActionResult> CreateWorkplace([FromBody] Workplace workplace)
        {
            var result = await _service.Insert(new Workplace
            {
                ShortName = workplace.ShortName,
                Workers = workplace.Workers,
                LongName = workplace.LongName,
                City = workplace.City
            });

            return result ? BadRequest() : Ok();
        }


        [HttpPut("put")]
        public async Task<IActionResult> UpdateWorkplace([FromBody] Workplace workplace)
        {
            var result = await _service.Update(new Workplace
            {
                WorkplaceID = workplace.WorkplaceID,
                ShortName = workplace.ShortName,
                Workers = workplace.Workers,
                LongName = workplace.LongName,
                City = workplace.City
            });

            return result ? BadRequest() : Ok();
        }


        [HttpDelete("{workplaceId}/delete")]
        public async Task<IActionResult> DeleteWorkplace(int workplaceId)
        {
            var result = await _service.Delete(workplaceId);
            return result ? BadRequest() : Ok();
        }
    }
}
