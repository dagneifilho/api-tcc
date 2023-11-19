using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;
using Newtonsoft.Json;

namespace Application.Models;

public class UserDTO
{
    [Required]
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("password")]
    [Required]
    public string Password { get; set; }
    [JsonProperty("role")]
    [Required]
    public Role Role { get; set; }
}