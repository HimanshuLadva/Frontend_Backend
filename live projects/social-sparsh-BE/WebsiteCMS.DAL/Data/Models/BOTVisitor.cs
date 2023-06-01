using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTVisitor
    {
        [Key]
        public long Id { get; set; }
        public string Platform { get; set; } = string.Empty;
        public Guid VisitorUUId { get; set; }
        public ICollection<BOTHistory>? Replies { get; set; }
        public BOTWebHookResponse webHookResponse { get; set; }
    }
}
