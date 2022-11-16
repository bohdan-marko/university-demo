namespace University.Application.Models;

public class AuthenticationResult
{
    public string Token { get; set; }
    public List<string> ErrorMessages { get; set; }
}
