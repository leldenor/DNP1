using System.Text.Json.Serialization;

namespace FamilyInfo.Models
{
    public class User
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("securityLevel")]
        public int SecurityLevel { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
}