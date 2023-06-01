namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTHistory
    {
        public long Id { get; set; }
        public DateTime? SentDate { get; set; } = DateTime.Now;
        public long ChatBotId { get; set; }
        public BOTChatBot ChatBot { get; set; }
        public long QuestionId { get; set; }
        public BOTQuestion Question { get; set; }
        public string QuestionText { get; set; }
        public string? Reply { get; set; }
        public bool IsBotMessage { get; set; }
        public long VisitorId { get; set; }
        public BOTVisitor Visitor { get; set; }
    }
}
