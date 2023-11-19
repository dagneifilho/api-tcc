using Newtonsoft.Json;

namespace Application.Models;

public class CloseIncident
{
    [JsonProperty("userId")]
    public int UserId { get; set; }
}