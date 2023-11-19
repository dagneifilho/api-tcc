using Domain.Enums;

namespace Domain.Models;

public class UserDb
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public Role? Role { get; set; }
}