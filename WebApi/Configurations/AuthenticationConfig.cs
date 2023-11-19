using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Configurations;

public static class AuthenticationConfig
{
    public static void ConfigureAuthentication(this IServiceCollection services)
    {
        var key = Environment.GetEnvironmentVariable("JwtSecret");
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        
        services.AddMvc(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            config.Filters.Add(new AuthorizeFilter(policy));
        }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("manager", policy => policy.RequireClaim("Role", "Manager"));
            opt.AddPolicy("employee", policy => policy.RequireClaim("Role", "Employee"));
        });
    }
}