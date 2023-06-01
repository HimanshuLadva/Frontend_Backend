namespace CRMBackend.Data.Models
{
    public class States
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Districts>? Districts { get; set; }
        public ICollection<Contacts>? Contacts { get; set; }
    }

    public class Districts
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int StateId { get; set; }
        public States? State { get; set; }
        public ICollection<Cities>? Cities { get; set; }
        public ICollection<Contacts>? Contacts { get; set; }
    }

    public class Cities
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DistrictId { get; set; }
        public Districts? District { get; set; }
        public ICollection<Contacts>? Contacts { get; set; }
    }
}
