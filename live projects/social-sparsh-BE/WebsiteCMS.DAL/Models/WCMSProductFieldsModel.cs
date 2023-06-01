namespace WebsiteCMS.DAL.Models
{
    public class WCMSProductFieldsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FieldTypeId { get; set; }
        public ICollection<WCMSUserProductFieldsModel>? UserProductFields { get; set; }
    }
}
