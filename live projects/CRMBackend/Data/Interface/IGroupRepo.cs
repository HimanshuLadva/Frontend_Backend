using CRMBackend.Models;

namespace CRMBackend.Data.Interface
{
    public interface IGroupRepo
    {
        Task<GroupViewModel> AddGroupAsync(GroupModel model);
        Task<bool> DeleteGroupAsync(int id);
        Task<List<GroupViewModel>> GetAllGroupAsync();
        Task<GroupViewModel> GetGroupByIdAsync(int id);
        Task<GroupViewModel> UpdateGroupAsync(int id, GroupModel model);
    }
}
