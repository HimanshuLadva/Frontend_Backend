namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTWhatsAppBusinessData
    {
        public long Id { get; set; }
        public string BusinessId { get; set; }
        public string PhNoId { get; set; }
        public string WAToken { get; set; }
        public string PhoneNo { get; set; }
        public long ChatBotId { get; set; }
        public BOTChatBot ChatBot { get; set; }

        public string errorMessageID { get; set; }
    }
}
