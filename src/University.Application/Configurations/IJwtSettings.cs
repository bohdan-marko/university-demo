namespace University.Application.Configurations;

public interface IJwtSettings
{
    string Secret { get; }
    string Issuer { get; }
    string Audience { get; }
}