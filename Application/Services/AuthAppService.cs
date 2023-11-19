using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Application.Models;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthAppService : IAuthenticationAppService
{
    private readonly IAuthRepository _repository;
    private readonly JwtConfig _jwtConfig;

    public AuthAppService(IAuthRepository repository, JwtConfig jwtConfig)
    {
        _repository = repository;
        _jwtConfig = jwtConfig;
    }


    public async Task<TokenResponse> LogIn(Credentials credentials)
    {
        var userDb = await _repository.GetUserByUsername(credentials.Username);
        if (userDb is null)
            return new TokenResponse();
        if (!MatchingCredentials(credentials, userDb))
            return new TokenResponse();
        var token = GenerateToken(userDb);
        return new TokenResponse()
        {
            Token = token
        };
    }

    public async Task<string> Register(UserDTO credentials)
    {
        var userDb = await _repository.GetUserByUsername(credentials.Username);
        if (userDb is not null)
            return "Username already in use.";
        var newUser = CreateUserDb(credentials);
        await _repository.CreateUser(newUser);
        return string.Empty;
    }

    private UserDb CreateUserDb(UserDTO credentials)
    {
        (var passwordHash, var passwordSalt) = GeneratePasswordHash(credentials.Password);
        return new UserDb()
        {
            Username = credentials.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = credentials.Role
        };

    }

    private (byte[], byte[]) GeneratePasswordHash(string password)
    {
        using (var hmac = new HMACSHA512())
        {
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (passwordHash, passwordSalt);
        }
    }
    private bool MatchingCredentials(Credentials credentials, UserDb userDb)
    {
        using (var hmac = new HMACSHA512(userDb.PasswordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(credentials.Password));
            return userDb.Username.Equals(credentials.Username) && computedHash.SequenceEqual(userDb.PasswordHash);
        } 
    }

    private string GenerateToken(UserDb userDb)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim("Id",userDb.Id.ToString()),
            new Claim("Username", userDb.Username),
            new Claim("Role", userDb.Role.ToString())
        };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.JwtSecret));
        var cred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddSeconds(300),
            signingCredentials:cred
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    public void Dispose()
    {
        _repository.Dispose();
        GC.SuppressFinalize(this);
    }
}