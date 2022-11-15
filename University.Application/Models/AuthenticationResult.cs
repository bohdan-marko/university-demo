namespace University.Application.Models;

public class AuthenticationResult
{
    public UserResponse UserResponse { get; set; }
    public List<string> ErrorMessages { get; set; }
}
