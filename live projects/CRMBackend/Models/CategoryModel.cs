using System.ComponentModel.DataAnnotations;

namespace CRMBackend.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
