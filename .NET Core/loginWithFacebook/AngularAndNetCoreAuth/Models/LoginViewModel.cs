namespace AngularAndNetCoreAuth.Models
{
    /// <summary>
    /// Normally, I would create a ViewModel folder but since this is a small project, I'll just leave it in the model folder.
    /// </summary>
    public class LoginViewModel
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string EmailAddress { get; set; }

    }
}
