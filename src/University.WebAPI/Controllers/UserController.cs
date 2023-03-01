using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Application.Models;
using University.Application.Services.Abstract;

namespace University.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IJwtUserService _service;

        public UserController(IJwtUserService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetAll();
            return users is null ? NotFound() : Ok(users);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserRequest userRequest)
        {
            var response = await _service.Authenticate(userRequest);
            return response.ErrorMessages.Any() ? BadRequest(response) : Ok(response);
        }
    }
}
