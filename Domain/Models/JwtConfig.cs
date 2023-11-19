namespace Domain.Models;

public class JwtConfig
{
    public string JwtSecret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}