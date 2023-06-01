using System.ComponentModel.DataAnnotations;

namespace CRMBackend.Models
{
    public class SignUpUserModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
