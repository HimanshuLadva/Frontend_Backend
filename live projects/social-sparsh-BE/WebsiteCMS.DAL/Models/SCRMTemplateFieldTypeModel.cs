using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTemplateFieldTypeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Field Type")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
