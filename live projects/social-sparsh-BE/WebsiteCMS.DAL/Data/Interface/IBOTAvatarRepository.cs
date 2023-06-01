using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBOTAvatarRepository
    {
        Task<BOTAvatar> AddAvatarAsync(BOTAvatar model);
        Task<bool?> DeleteAvatarAsync(long id);
        Task<List<BOTAvatar>?> getAvatar();
    }
}