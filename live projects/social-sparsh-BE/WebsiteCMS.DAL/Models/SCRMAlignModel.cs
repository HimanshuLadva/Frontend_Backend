using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMAlignModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Select Text Align")]
        public string Name { get; set; }
    }
}
