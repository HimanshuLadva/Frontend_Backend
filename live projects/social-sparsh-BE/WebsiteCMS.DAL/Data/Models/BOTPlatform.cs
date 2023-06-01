namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTPlatform
    {
        public long Id { get; set; }
        public long ChatBotId { get; set; }
        public BOTChatBot ChatBot { get; set; }
        public string Platform { get; set; }
    }
}
