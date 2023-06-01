namespace WebsiteCMS.DAL.Models
{
    public class BOTHistoryViewModel
    {
        public long? Id { get; set; }
        public DateTime? SentDate { get; set; } = DateTime.Now;
        public long ChatBotId { get; set; }
        public long QuestionId { get; set; }
        public BOTQuestionViewModel Question { get; set; }
        public string QuestionText { get; set; }
        public string? Reply { get; set; }
        public bool IsBotMessage { get; set; } = true;
        public Guid VisitorUUId { get; set; }
    }
}
