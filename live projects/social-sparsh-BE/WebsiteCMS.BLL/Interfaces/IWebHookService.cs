using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IWebHookService
    {
        Task<string?> HandleWebhookRequest(BOTWebhookModel model);
    }
}