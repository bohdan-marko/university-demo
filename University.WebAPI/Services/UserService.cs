using Microsoft.IdentityModel.Tokens;
using PIS.DAL.Contracts;
using PIS.DAL.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using University.WebAPI.Models;
using University.WebAPI.Services.Abstract;

namespace University.WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IBaseRepository<User> _repository;

        public UserService(IConfiguration configuration, IBaseRepository<User> repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var user = (await _repository.GetAllAsync()).SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
                return string.Empty;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Country, "Ukraine")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            await _repository.SaveChangesAsync();
            
            return user.Token;
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
    }
}
