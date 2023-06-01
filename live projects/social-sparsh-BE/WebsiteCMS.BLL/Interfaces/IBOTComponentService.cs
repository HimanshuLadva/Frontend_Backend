using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IBOTComponentService
    {
        Task<BOTComponentModel> AddComponentAsync(BOTComponentModel model);
        Task<BOTComponentModel> EditComponentAsync(BOTComponentModel model);
        Task<List<BOTComponentModel>?> GetAllComponentsAsync();
    }
}