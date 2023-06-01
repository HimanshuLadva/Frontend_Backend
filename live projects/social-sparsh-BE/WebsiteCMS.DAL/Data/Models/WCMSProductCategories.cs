using System.ComponentModel.DataAnnotations.Schema;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSProductCategories
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsUserDefined { get; set; }
        public ICollection<WCMSCategoryWiseProducts>? Products { get; set; }

    }
}
