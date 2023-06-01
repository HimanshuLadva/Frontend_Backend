using System.ComponentModel.DataAnnotations;

namespace CRMBackend.Models
{
    public class ViewRoleModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
