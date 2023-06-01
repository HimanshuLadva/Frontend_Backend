namespace WebsiteCMS.DAL.Models
{
    public class BOTVisitorModel
    {
        public long? Id { get; set; }
        public string Platform { get; set; } = string.Empty;
        public Guid? VisitorUUId { get; set; }
        public ICollection<BOTHistoryModel>? Replies { get; set; }
    }
}
