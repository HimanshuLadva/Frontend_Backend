using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Models
{
    public class BOTWhatsAppBusinessDataModel
    {
        public long Id { get; set; }    
        [Required]
        public string BusinessId { get; set; }
        [Required]
        public string PhNoId { get;set; }
        [Required]
        public string WAToken { get;set; }
        public string PhoneNo { get;set; }
        public long ChatBotId { get; set; }
        public string? errorMessageID { get; set; }
    }
}
