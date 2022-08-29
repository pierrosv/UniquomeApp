namespace UniquomeApp.Infrastructure.BaseSecurity;

public class JwtSettings
{
    public string Secret { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public TimeSpan TokenLifetime { get; set; }
    public int TokenLifetimeInMinutes { get; set; }
}