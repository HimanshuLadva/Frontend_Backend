using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class BOTQuestionLinkViewModel : BOTQuestionBaseViewModel
    {
        public long Id { get; set; }
        public string? LinkTitle { get; set; }
        public string? LinkUrl { get; set; }
        public long QuestionId { get; set; }
    }
}
