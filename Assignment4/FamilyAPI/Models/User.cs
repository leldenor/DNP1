using System.ComponentModel.DataAnnotations;

namespace FamilyAPI.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public int SecurityLevel { get; set; }
        public string Role { get; set; }
    }
}