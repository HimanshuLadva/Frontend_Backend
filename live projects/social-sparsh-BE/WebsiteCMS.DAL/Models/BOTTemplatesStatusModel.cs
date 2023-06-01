using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Models
{
    public class BOTTemplatesStatusModel
    {
        public long Id { get; set; }
        public string TemplateId { get; set; }
        public string Status { get; set; }
        
    }
}
