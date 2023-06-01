using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Models
{
    public class BOTChatBotModel
    {
        public long? Id { get; set; } = null;
        [Required]
        public string Name { get; set; }
        public string? DisplayName { get; set; }
        [Required]
        public string Colour { get; set; }
        public string? Avatar { get; set; }
        public IFormFile? AvtarImage { get; set; }
        public string? ApplicationUserId { get; set; }
        public ICollection<BOTQuestionModel>? Questions { get; set; }
        public ICollection<BOTPlatformModel>? Platforms { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}

