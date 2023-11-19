using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Application.Models;

public class CreateSection
{
    [Required]
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("description")]
    [Required]
    public string Description { get; set; }
}