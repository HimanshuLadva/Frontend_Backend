using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTemplateTagModel
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string? Template { get; set; }
        public int TagId { get; set; }
        public string? Tag { get; set; }
    }
}
