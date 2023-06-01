namespace WebsiteCMS.DAL.Models
{
    public class BOTOptionViewModel : BOTQuestionBaseViewModel
    {
        public long? Id { get; set; }
        public string Value { get; set; }
        public long QuestionId { get; set; }
    }
}
