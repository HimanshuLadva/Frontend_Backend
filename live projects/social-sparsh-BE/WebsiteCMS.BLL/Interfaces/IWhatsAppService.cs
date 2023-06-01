using RestSharp;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IWhatsAppService
    {
        Task<string> GetTemplateStatusAsync(string tempId, long chatbotID);
        Task<string> RegisterBotFlow(long BotId);
        Task<BOTWhatsAppBusinessDataModel> RegisterBotOnWhatsAppAsync(BOTWhatsAppBusinessDataModel model);
        Task<RestResponse> RegisterTemplateAsync(BOTWhatsappMessageTemplateModel model);
    }
}