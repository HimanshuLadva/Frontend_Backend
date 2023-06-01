namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTOption : BOTQuestionBase
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public long QuestionId { get; set; }
        public BOTQuestion Question { get; set; }
    }
}
