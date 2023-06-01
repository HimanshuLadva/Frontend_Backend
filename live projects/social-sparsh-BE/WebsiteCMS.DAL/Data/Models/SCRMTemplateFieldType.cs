namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMTemplateFieldType : SCRMBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<SCRMTemplateField> TemplateField { get; set; }
    }
}
