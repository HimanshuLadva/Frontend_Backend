namespace CRMBackend.Data.Models
{
    public class SubCategories
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public Categories? Category { get; set; }
    }
}
