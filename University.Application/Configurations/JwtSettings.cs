using Microsoft.Extensions.Configuration;

namespace University.Application.Configurations;

public class JwtSettings : IJwtSettings
{
    private readonly IConfiguration _configuration;

    public JwtSettings(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string Secret => _configuration["JwtSettings:Secret"];

    public string Issuer => _configuration["JwtSettings:Issuer"];

    public string Audience => _configuration["JwtSettings:Audience"];
}
