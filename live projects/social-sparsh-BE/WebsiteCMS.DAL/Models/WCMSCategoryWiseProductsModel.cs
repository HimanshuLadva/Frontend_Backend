namespace WebsiteCMS.DAL.Models
{
    public class WCMSCategoryWiseProductsModel
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public ICollection<WCMSUserProductFieldsModel> Fields { get; set; }
        public ICollection<string> files { get; set; } = new List<string>();
    }
}
