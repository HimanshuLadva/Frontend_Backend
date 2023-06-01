using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IBOTGetflowService
    {
        Task<bool> editFlow(BOTChatBotModel model);
        Task<BOTChatBotViewModel?> GetBotById(long BotId);
        Task<List<BOTQuestionViewModel>?> GetFlow(long BotId);
        List<BOTQuestionViewModel> UpdateIndex(List<BOTQuestionViewModel> models);
    }
}