using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Models
{
    public class BOTComponentModel
    {
        public long Id { get; set; }
        [Required]
        public string Label { get; set; }
        public string? DefaultQuestion { get; set; }
        [Required]
        public string? QuestionType { get; set; }
        public string? InputType { get; set; }
        public string? IconUrl { get; set; }
        public IFormFile? Icon { get; set; }
    }
}