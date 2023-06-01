using System.ComponentModel.DataAnnotations;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class BOTQuestionModel
    {
        public long? Id { get; set; }
        [Required]
        public string Question { get; set; }
        public string Target { get; set; } = string.Empty;
        public ICollection<BOTOptionModel>? Options { get; set; }
        [Required]
        public long ComponentTypeId { get; set; }
        [Required]
        public long ChatBotId { get; set; }
        [Required]
        public long Sequence { get; set; }
        [Required]
        public string? FrontendId { get; set; }
        public bool IsSkippable { get; set; } = false;
        public bool IsLeadMessage { get; set; } = false;
        public ICollection<BOTQuestionLinkModel>? Links { get; set; }
        public BOTImageOrFileModel? ImageOrFile { get; set; }
    }
}
