namespace WebApi.Configurations;

public static class JwtConfig
{
    public static void ConfigureJwt(this IServiceCollection services)
    {
        var jwtConfig = new Domain.Models.JwtConfig()
        {
            JwtSecret = Environment.GetEnvironmentVariable("JwtSecret"),
            Audience = Environment.GetEnvironmentVariable("Audience"),
            Issuer = Environment.GetEnvironmentVariable("Issuer")
        };
        services.AddSingleton<Domain.Models.JwtConfig>(jwtConfig);
    }
}