using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBotWhatsAppRepository
    {
        Task<BOTWhatsAppBusinessData> AddWABAAsync(BOTWhatsAppBusinessData model);
        Task<bool> DeleteWABAccount(long id);
        Task<List<BOTWhatsAppBusinessData>> GetAllWABData();
        Task<BOTWhatsAppBusinessData?> GetWABAByBussinessIdAsync(string id);
        Task<BOTWhatsAppBusinessData?> getWABAByChatBotID(long id);
        Task<BOTWhatsAppBusinessData?> GetWABAByIdAsync(long id);
        Task<BOTWhatsAppBusinessData?> UpdateWABAAsync(BOTWhatsAppBusinessData model);
    }
}