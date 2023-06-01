namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMTag : SCRMBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public string TagImage { get; set; }
    }
}
