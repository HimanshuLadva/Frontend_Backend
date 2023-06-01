using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSCategoryWiseProducts
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public WCMSProductCategories ProductCategory { get; set; }
        public ICollection<WCMSUserProductFields> Fields { get; set; }
    }
}
