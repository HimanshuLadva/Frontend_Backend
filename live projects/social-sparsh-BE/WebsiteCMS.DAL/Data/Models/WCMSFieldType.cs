namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSFieldType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<WCMSFieldsMaster>? FieldsMasters { get; set; }
    }
}