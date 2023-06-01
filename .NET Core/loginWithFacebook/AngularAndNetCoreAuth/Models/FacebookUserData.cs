using System.ComponentModel.DataAnnotations.Schema;

namespace AngularAndNetCoreAuth.Models
{
    public class FacebookUserData
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string name { get; set; }
        public string name_format { get; set; }
        public string email { get; set; }
    }
}
