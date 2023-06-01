using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMTemplateFieldText
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public SCRMTemplate Template { get; set; }
        public int TemplateFieldId { get; set; }
        public SCRMTemplateField TemplateField { get; set; }
        public int FontFamilyId { get; set; } = 1;
        public SCRMFontFamily FontFamily { get; set; }
        public bool IsDisplay { get; set; } = true;
        public double X { get; set; } = 10;
        public double Y { get; set; }
        public int AlignId { get; set; } = 1;
        public SCRMAlign Align { get; set; }
        public int Size { get; set; } = 20;
        public string Color { get; set; } = "#000000";
    }
}
