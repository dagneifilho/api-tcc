using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Application.Models;

public class SectionDTO
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }

}