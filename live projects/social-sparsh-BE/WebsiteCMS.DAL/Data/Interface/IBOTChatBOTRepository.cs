using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBOTChatBOTRepository
    {
        Task<BOTChatBot> AddBotAsync(BOTChatBot model);
        Task<bool> DeleteBotAsync(long id);
        Task<List<BOTChatBot>> GetAllBotAsync();
        Task<BOTChatBot?> GetBotByIdAsync(long id);
        Task<BOTChatBot?> GetBotByUserIdAsync(string id);
        Task<List<BOTChatBot>> GetBotListByUserIdAsync(string id);
        Task<BOTChatBot?> UpdateBotAsync(BOTChatBot model);
        Task<BOTChatBot?> GetBotFlow(long id);
        Task<List<BOTChatBot>> GetExceptQuestionBot(BOTChatBot model);
    }
}