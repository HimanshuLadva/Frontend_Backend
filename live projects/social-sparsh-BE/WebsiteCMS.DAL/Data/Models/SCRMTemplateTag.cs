using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMTemplateTag
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public SCRMTemplate Template { get; set; }
        public int TagId { get; set; }
        public SCRMTag Tag { get; set; }
    }
}
