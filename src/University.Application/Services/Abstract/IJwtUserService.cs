using University.Application.Models;

namespace University.Application.Services.Abstract;

public interface IJwtUserService
{
    Task<AuthenticationResult> Authenticate(UserRequest userRequest);
    Task<IEnumerable<UserResponse>> GetAll();
}
