using System.ComponentModel.DataAnnotations;

namespace CRMBackend.Data.Models
{
    public class UserPhotoModel
    {
        public int Id { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public int ContactsId { get; set; }
    }
    public class UserPhotoViewModel
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int ContactsId { get; set; }
    }
    public class UserNoteModel
    {
        public int Id { get; set; }
        [Required]
        public string? Description { get; set; }
        public int ContactsId { get; set; }
    }
    public class UserDocumentModel
    {
        public int Id { get; set; }
        [Required]
        public IFormFile Document { get; set; }
        public int ContactsId { get; set; }
    }
    public class UserDocumentViewModel
    {
        public int Id { get; set; }
        public string? DocumentUrl { get; set; }
        public int ContactsId { get; set; }
    }
}
