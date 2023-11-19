using Newtonsoft.Json;

namespace Application.Models;

public class IncidentsFilters
{
    [JsonProperty("issuerId")]
    public int? IssuerId { get; set; }
    [JsonProperty("closedById")]
    public int? ClosedById { get; set; }
    [JsonProperty("solved")]
    public bool? Solved { get; set; }
    [JsonProperty("sectionId")]
    public int? SectionId { get; set; }
}