using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTagModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Tag Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Tag Status")]
        public bool IsActive { get; set; }
        public string? TagImageUrl { get; set; }
        public IFormFile? TagImage { get; set; }
    }
}
