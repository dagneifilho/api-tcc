using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Application.Models;

public class CreateIncident
{
    [Required]
    [JsonProperty("description")]
    public string Description { get; set; }

    [Required] [JsonProperty("issuerId")] public int IssuerId { get; set; }

    [Required]
    [JsonProperty("sectionId")]
    public int SectionId { get; set; }

}