using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using University.DAL.Domain;
using University.DAL.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using University.Application.Models;
using University.Application.Services.Abstract;
using University.Application.Configurations;
using AutoMapper;

namespace University.Application.Services
{
    public class UserService : IJwtUserService, ICookieUserService
    {
        private readonly IBaseRepository<User> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtSettings _settings;
        private readonly IMapper _mapper;
        
        private const string Country = "Ukraine";
        private const string Role = "Admin";

        public UserService(IBaseRepository<User> repository, IHttpContextAccessor httpContextAccessor, IJwtSettings settings, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AuthenticationResult> Authenticate(UserRequest userRequest)
        {
            var user = (await _repository.GetAllAsync())
                .SingleOrDefault(x => x.Username == userRequest.Username 
                                   && x.Password == userRequest.Password);

            if (user is null)
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
                UserResponse = _mapper.Map<UserResponse>(user),
                ErrorMessages = new List<string>()
            };
        }

        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<List<UserResponse>>(users);
        }

        public async Task<bool> Login(UserRequest userRequest)
        {
            bool userIsValid = (await _repository.GetAllAsync())
                .Any(u => u.Username == userRequest.Username 
                       && u.Password == userRequest.Password);

            if (userIsValid)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userRequest.Username),
                    new Claim(ClaimTypes.NameIdentifier, userRequest.Username),
                    new Claim(ClaimTypes.Role, Role),
                    new Claim(ClaimTypes.Country, Country)
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
