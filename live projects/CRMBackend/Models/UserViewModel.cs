namespace CRMBackend.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public List<string> Role { get; set; }
        public string Email { get; set; }
        public int SessionExpriryTime { get; set; }
        public string Token { get; set; }
    }
}
