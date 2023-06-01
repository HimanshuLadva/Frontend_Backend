namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSMasterType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<WCMSFieldsMaster>? FieldsMasters { get; set; }
        public virtual ICollection<WCMSFieldsMasterChild>? TemplateFieldsMaster { get; set; }
    }
}
