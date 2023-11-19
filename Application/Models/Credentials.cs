using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Application.Models;

public class Credentials
{
    [Required]
    [JsonProperty("username")]
    public string? Username { get; set; }
    [Required]
    [JsonProperty("password")]
    public string? Password { get; set; }
}