using Newtonsoft.Json;

namespace Application.Models;

public class TokenResponse
{
    [JsonProperty("token")]
    public string? Token { get; set; }
}