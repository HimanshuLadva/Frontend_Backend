using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Models
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
