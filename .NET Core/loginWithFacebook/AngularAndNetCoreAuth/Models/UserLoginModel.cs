namespace AngularAndNetCoreAuth.Models
{
    public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RememberMe { get; set; }

    }
}
