using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMTemplateFieldImage
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public SCRMTemplate Template { get; set; }
        public int TemplateFieldId { get; set; }
        public SCRMTemplateField TemplateField { get; set; }
        public bool IsDisplay { get; set; } = true;
        public double X { get; set; } = 340;
        public double Y { get; set; } = 175;
        public double Width { get; set; } = 150;
        public double Height { get; set; } = 150;
    }
}
