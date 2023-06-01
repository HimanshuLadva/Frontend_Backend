using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTQuestionLink : BOTQuestionBase
    {
        public long Id { get; set; }
        public string LinkTitle { get; set; }
        public string LinkUrl { get; set; }
        public long QuestionId { get; set; }
        public BOTQuestion Question { get; set; }
    }
}
