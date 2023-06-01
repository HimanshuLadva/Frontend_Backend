using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSProductFields
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FieldTypeId { get; set; }
        public WCMSFieldType FieldType { get; set; }
        public ICollection<WCMSUserProductFields>? UserProductFields { get; set; }
    }
}
