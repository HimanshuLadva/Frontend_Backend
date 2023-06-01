namespace WebsiteCMS.DAL.Models
{
    public class WCMSTemplatePageFieldsModel
    {
        public int Id { get; set; }
        public int TemplatePageId { get; set; }
        public int FieldMasterId { get; set; }
        public string? MasterType { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int ParentId { get; set; }
        public List<Child> Childs { get; set; } = new List<Child>();
        public string? Value { get; set; }
    }

    public class Child
    {
        public int childId { get; set; }
        public List<WCMSTemplatePageFieldsModel> ChildFields { get; set; } = new List<WCMSTemplatePageFieldsModel>();
    }

    public class FieldsRespose
    {
        public List<string>? Pages { get; set; }
        public List<KeyValuePair<string, List<WCMSTemplatePageFieldsModel>>>? Fields { get; set; }
    }
}