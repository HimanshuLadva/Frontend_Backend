namespace WebsiteCMS.DAL.Models
{
    public class BOTQuestionViewModel : BOTQuestionBaseViewModel
    {
        public long? Id { get; set; }
        public string Question { get; set; }
        public List<BOTOptionViewModel>? Options { get; set; }
        public long ComponentTypeId { get; set; }
        public long ChatBotId { get; set; }
        public string? QuestionType { get; set; }
        public string FrontendId { get; set; }
        public string InputType { get; set; }
        public long Sequence { get; set; }
        public string? ImageOrFilePath { get; set; }
        public List<BOTQuestionLinkViewModel>? Links { get; set; }
    }
}
