using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Database.DBRepository;
using CRMBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Repository
{
    public class GroupRepo : Repository<Groups>, IGroupRepo
    {
        private readonly RMbackendContext _context;
        private readonly IBaseRepo _baseRepo;

        public GroupRepo(RMbackendContext context, IBaseRepo baseRepo) : base(context)
        {
            _context = context;
            _baseRepo = baseRepo;
        }

        public async Task<GroupViewModel> AddGroupAsync(GroupModel model)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = new Groups()
            {
                Name = model.Name,
                ClientId = user.ClientId,
            };
            await InsertAsync(data);
            await SaveChangesAsync();

            var result = await GetGroupByIdAsync(data.Id);
            return result;
        }

        public async Task<List<GroupViewModel>> GetAllGroupAsync()
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var data = GetAll().Where(x => x.ClientId == user.ClientId).Select(x => new GroupViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                ClientId = x.ClientId,
            }).ToList();

            return data;
        }

        public async Task<GroupViewModel> GetGroupByIdAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var data = await Query(x => x.ClientId == user.ClientId && x.Id == id).Select(x => new GroupViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                ClientId = x.ClientId,
            }).FirstOrDefaultAsync();

            return data!;
        }

        public async Task<GroupViewModel> UpdateGroupAsync(int id, GroupModel model)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = await Query(x => x.Id == id && x.ClientId == user.ClientId).FirstOrDefaultAsync();

            if (model != null)
            {
                data!.Id = id;
                data.Name = model.Name;
                data.ClientId = user.ClientId;
            }

            Update(data!);
            await SaveChangesAsync();
            var result = await GetGroupByIdAsync(data!.Id);
            return result;
        }

        public async Task<bool> DeleteGroupAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = new Groups()
            {
                Id = id,
                ClientId = user.ClientId,
            };

            Delete(data);
            await SaveChangesAsync();

            return true;
        }
    }
}
