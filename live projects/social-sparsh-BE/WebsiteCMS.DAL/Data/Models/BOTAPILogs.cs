using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTAPILogs
    {
        [Key]
        public long Id { get; set; }
        public string MethodName { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
