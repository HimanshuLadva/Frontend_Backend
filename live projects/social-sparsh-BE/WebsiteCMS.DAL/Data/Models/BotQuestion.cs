using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTQuestion : BOTQuestionBase
    {
        [Key]
        public long Id { get; set; }
        public string Question { get; set; }
        public ICollection<BOTOption>? Options { get; set; }
        public long ComponentTypeId { get; set; }
        public BOTComponent ComponentType { get; set; }
        public long ChatBotId { get; set; }
        public BOTChatBot ChatBot { get; set; }
        public long Sequence { get; set; }
        public string FrontendId { get; set; }
        public bool IsSkippable { get; set; }
        public bool IsLeadMessage { get; set; }
        public ICollection<BOTQuestionLink>? Links { get; set; }
    }
}
