using Domain.Models;

namespace Domain.Interfaces;

public interface IAuthRepository : IDisposable
{
    Task<UserDb> GetUserByUsername(string username);
    Task CreateUser(UserDb user);
    Task<UserDb> GetUserById(int id);
}