namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTAPILogsModel
    {
        public long Id { get; set; }
        public string MethodName { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
