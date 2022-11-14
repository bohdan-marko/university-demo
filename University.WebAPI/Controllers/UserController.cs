using Microsoft.AspNetCore.Mvc;
using University.WebAPI.Models;
using University.WebAPI.Services.Abstract;

namespace University.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetAll();
            return users is null ? NotFound() : Ok(users);
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserRequest userRequest)
        {
            var userToken = new TokenResponse
            {
                JwtToken = await _service.Authenticate(userRequest.Username, userRequest.Password)
            };
            
            return string.IsNullOrEmpty(userToken.JwtToken) ? BadRequest() : Ok(userToken);
        }
    }
}
