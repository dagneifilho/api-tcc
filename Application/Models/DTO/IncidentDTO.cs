using Newtonsoft.Json;

namespace Application.Models;

public class IncidentDTO
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }
    [JsonProperty("issuerId")]
    public int IssuerId { get; set; }
    [JsonProperty("issuerUsername")]
    public string IssuerUsername { get; set; }
    [JsonProperty("closedById")]
    public int? ClosedById { get; set; }
    [JsonProperty("closedByUsername")]
    public string ClosedByUsername { get; set; }
    [JsonProperty("sectionId")]
    public int SectionId { get; set; }
    [JsonProperty("sectionName")]
    public string SectionName { get; set; }
    [JsonProperty("opened")]
    public DateTime Opened { get; set; }
    [JsonProperty("closed")]
    
    public DateTime? Closed { get; set; }

    [JsonProperty("solved")] public bool Solved { get; set; }
}