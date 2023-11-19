using Application.Models;
namespace Application.Interfaces;

public interface IAuthenticationAppService : IDisposable
{
    Task<TokenResponse> LogIn(Credentials credentials);
    Task<string> Register(UserDTO credentials);
}