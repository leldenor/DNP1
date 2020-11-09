using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FamilyInfo.Models {
public class Pet {
    [JsonPropertyName("ID")]
    public int Id { get; set; }
    [Required]
    [JsonPropertyName("Species")]
    public string Species { get; set; }
    [Required]
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [Required]
    [JsonPropertyName("Age")]
    public int Age { get; set; }
}
}