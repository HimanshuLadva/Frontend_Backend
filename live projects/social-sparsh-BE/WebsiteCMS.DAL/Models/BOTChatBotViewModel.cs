namespace WebsiteCMS.DAL.Models
{
    public class BOTChatBotViewModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string? DisplayName { get; set; }
        public string Colour { get; set; }
        public string? Avatar { get; set; }
        public string? ApplicationUserId { get; set; }
        public ICollection<BOTQuestionViewModel>? Questions { get; set; }
        public ICollection<BOTPlatformModel>? Platforms { get; set; }
    }
}
