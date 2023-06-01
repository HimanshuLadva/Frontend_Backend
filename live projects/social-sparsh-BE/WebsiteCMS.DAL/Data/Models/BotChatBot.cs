using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTChatBot
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string? DisplayName { get; set; }
        public string Colour { get; set; }
        public string? Avatar { get; set; }
        public string ApplicationUserId { get; set; }
        public ICollection<BOTQuestion>? Questions { get; set; }
        public ICollection<BOTPlatform>? Platforms { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
