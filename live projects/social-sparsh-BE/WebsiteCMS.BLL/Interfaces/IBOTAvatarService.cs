using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IBOTAvatarService
    {
        Task<BOTAvatarModel> AddAvatarAsync(BOTAvatarModel model);
        Task<bool?> DeleteAvatar(long id);
        Task<List<BOTAvatarModel>?> getAvatar();
    }
}