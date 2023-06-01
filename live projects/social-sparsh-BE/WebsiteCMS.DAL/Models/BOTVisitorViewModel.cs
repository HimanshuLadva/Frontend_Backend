namespace WebsiteCMS.DAL.Models
{
    public class BOTVisitorViewModel
    {
        public long? Id { get; set; }
        public string Platform { get; set; } = string.Empty;
        public Guid VisitorUUId { get; set; }
        public ICollection<BOTHistoryViewModel>? Replies { get; set; }
    }
}
