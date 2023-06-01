using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBOTComponentRepository
    {
        Task<BOTComponent> AddComponentAsync(BOTComponent model);
        Task<BOTComponent> EditComponentAsync(BOTComponent model);
        Task<List<BOTComponent>?> GetAllComponentsAsync();
        Task<BOTComponent?> GetComponentByLabelAsync(string Label);
        Task<BOTComponent?> GetComponentByIdAsync(long id);
        Task<List<BOTComponent>> GetMessageLinkImageFile();
    }
}