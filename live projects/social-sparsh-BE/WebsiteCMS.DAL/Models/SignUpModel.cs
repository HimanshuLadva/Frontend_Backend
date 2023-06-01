using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "* FirstName is Reuired")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* LastName is Reuired")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "* Email is Reuired")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Password is Reuired")]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "* Confirm Password is Reuired")]
        public string ConfirmPassword { get; set; }
    }
}
