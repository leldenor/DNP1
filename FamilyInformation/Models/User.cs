namespace FamilyInformation.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int SecurityLevel { get; set; }
        public string Role { get; set; }
    }
}