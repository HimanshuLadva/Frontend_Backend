namespace WebsiteCMS.DAL.Models
{
    public class SCRMSubCategroyWiseTemplateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SCRMTemplateWithLayoutModel> TemplateAndLayout { get; set; }
    }
}