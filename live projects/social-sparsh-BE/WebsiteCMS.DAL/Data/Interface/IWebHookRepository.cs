using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IWebHookRepository
    {
        Task<BOTWebHookResponse> AddResponseAsync(BOTWebHookResponse model);
        Task<BOTWebHookResponse?> GetResponseByFromAsync(string FromNo);
    }
}