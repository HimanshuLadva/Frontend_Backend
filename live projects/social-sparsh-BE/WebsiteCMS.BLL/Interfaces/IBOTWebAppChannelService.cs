using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IBOTWebAppChannelService
    {
        Task<object?> Execute(BOTHistoryModel model);
        Task<object> GenerateResponse(BOTHistoryModel model);
    }
}