using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMTemplateField : SCRMBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TemplateFieldTypeId { get; set; }
        public SCRMTemplateFieldType TemplateFieldType { get; set; }
        public string? Value { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<SCRMUserMetaData>? UserMetaData { get; set; }
    }
}
