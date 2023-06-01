using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBotWhatsApTempStatusRepository
    {
        Task<BOTWhatsAppTemplatesStatus> AddTemplateStatusAsync(BOTWhatsAppTemplatesStatus model);
        Task<bool> DeleteTemplateStatusAsync(int id);
        Task<List<BOTWhatsAppTemplatesStatus>> getAllWhatsappTempStatus();
        Task<BOTWhatsAppTemplatesStatus?> getStatusById(long id);
        Task<BOTWhatsAppTemplatesStatus?> getStatusByTempId(string id);
        Task<BOTWhatsAppTemplatesStatus> UpdateTemplateStatusAsync(BOTWhatsAppTemplatesStatus model);
    }
}