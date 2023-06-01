using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSUserProductFields
    {
        public int Id { get; set; }
        public int ProductsId { get; set; }
        public WCMSCategoryWiseProducts Products { get; set; }
        public int ProductFieldsId { get; set; }
        public WCMSProductFields ProductFields { get; set; }
        public string FieldValue { get; set; }
        public bool IsBannerField { get; set; }
    }
}
