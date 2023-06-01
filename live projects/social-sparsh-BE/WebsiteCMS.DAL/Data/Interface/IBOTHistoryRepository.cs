using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBOTHistoryRepository
    {
        Task<BOTHistory> AddReplyAsync(BOTHistory model);
        Task<BOTHistory?> GetHistory(long id);
        Task<BOTHistory?> GetHistoryByVisitorId(long id);
        Task<bool> DeleteLastQuesAsync(long id);
    }
}