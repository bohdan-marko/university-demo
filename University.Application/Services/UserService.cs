using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PIS.DAL.Contracts;
using PIS.DAL.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using University.Application.Models;
using University.Application.Services.Abstract;
using University.WebAPI.Configurations;

namespace University.Application.Services
{
    public class UserService : IJwtUserService, ICookieUserService
    {
        private readonly JwtSettings _settings;
        private readonly IBaseRepository<User> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        private const string Country = "Ukraine";

        public UserService(IOptions<JwtSettings> options, IBaseRepository<User> repository, IHttpContextAccessor httpContextAccessor)
        {
            _settings = options.Value;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticationResult> Authenticate(UserRequest userRequest)
        {
            var user = (await _repository.GetAllAsync())
                .SingleOrDefault(x => x.Username == userRequest.Username 
                                   && x.Password == userRequest.Password);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new()
                    {
                        "Username or password is incorrect.",
                        "Current user doesn't exist."
                    }
                };
            }
            
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Country, Country)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            await _repository.SaveChangesAsync();

            return new AuthenticationResult 
            { 
                UserResponse = new UserResponse 
                {
                    UserID = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Token = user.Token
                }
            };
        }

        public async Task<IEnumerable<UserResponse>> GetAll()
            => (await _repository.GetAllAsync()).Select(user =>
            new UserResponse
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
            });

        public async Task<bool> Login(UserRequest userRequest)
        {
            bool userIsValid = _repository.CurrentSet
                .Any(u => u.Username == userRequest.Username 
                       && u.Password == userRequest.Password);

            if (userIsValid)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userRequest.Username),
                    new Claim(ClaimTypes.NameIdentifier, userRequest.Username),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Country, "Ukraine")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    // Refreshing the authentication session should be allowed.

                    IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. Required when setting the 
                    // ExpireTimeSpan option of CookieAuthenticationOptions 
                    // set with AddCookie. Also required when setting 
                    // ExpiresUtc.
                };

                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return true;
            }
            
            return false;
        }
    }
}
