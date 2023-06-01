using System.ComponentModel.DataAnnotations.Schema;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class WCMSProductCategoriesModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string? ApplicationUserId { get; set; }
        public bool IsUserDefined { get; set; }
        public ICollection<WCMSCategoryWiseProductsModel>? Products { get; set; }
    }
}
