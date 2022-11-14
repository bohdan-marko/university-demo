using PIS.DAL.Contracts;
using University.WebAPI.Models;

namespace University.WebAPI.Services.Abstract
{
    public interface IUserService
    {
        Task<string> Authenticate(string username, string password);
        Task<IEnumerable<UserResponse>> GetAll();
    }
}
