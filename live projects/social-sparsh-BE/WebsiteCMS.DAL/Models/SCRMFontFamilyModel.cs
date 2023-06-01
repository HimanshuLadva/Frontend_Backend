using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMFontFamilyModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Font Family Name")]
        public string Name { get; set; }
        public string? Path { get; set; }
        public bool IsActive { get; set; }
    }
}
