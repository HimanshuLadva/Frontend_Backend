using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTComponent
    {
        [Key]
        public long Id { get; set; }
        public string Label { get; set; }
        public string? DefaultQuestion { get; set; }
        public string? QuestionType { get; set; }
        public string? InputType { get; set; }
        public string? IconUrl { get; set; }
    }
}// ok
