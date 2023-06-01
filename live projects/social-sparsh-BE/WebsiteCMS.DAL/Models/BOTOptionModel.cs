using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Models
{
    public class BOTOptionModel
    {
        public long? Id { get; set; }
        [Required]
        public string Value { get; set; }
        public string Target { get; set; } = string.Empty;
        public long QuestionId { get; set; }
    }
}
