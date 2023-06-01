using System.ComponentModel.DataAnnotations;

namespace CRMBackend.Models
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
