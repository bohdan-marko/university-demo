using University.Application.Models;

namespace University.Application.Services.Abstract;

public interface ICookieUserService
{
    Task<bool> Login(UserRequest userRequest);
}
