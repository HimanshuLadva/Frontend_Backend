namespace CRMBackend.Models
{
    public class AWSS3ObjectModel
    {
        public string? Name { get; set; }
        public MemoryStream? InputStream { get; set; }
        public string? BucketName { get; set; }
    }
}
