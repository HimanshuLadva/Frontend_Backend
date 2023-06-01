using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMLanguageModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
