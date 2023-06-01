using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBOTAnalyticsRepository
    {
        Task<Dictionary<string, object>> GetLeadsByBotAsync(long botId, int pageNumber);
        Task<Dictionary<string, object>> GetLeadsByUserAsync(int pageNumber);
        Task<BOTVisitorViewModel> GetVisitorChatAsync(Guid visitorUid);
    }
}