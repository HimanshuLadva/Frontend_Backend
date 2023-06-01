namespace CRMBackend.Data.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int GroupId { get; set; }
        public Groups? Group { get; set; }
        public ICollection<SubCategories>? SubCategories { get; set; }
    }
}
