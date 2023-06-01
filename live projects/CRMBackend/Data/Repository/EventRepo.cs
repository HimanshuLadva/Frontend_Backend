using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Database.DBRepository;
using CRMBackend.Models;
using CRMBackend.Utility;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Repository
{
    public class EventRepo : Repository<Events>, IEventRepo
    {
        private readonly RMbackendContext _context;
        private readonly IBaseRepo _baseRepo;

        public EventRepo(RMbackendContext context, IBaseRepo baseRepo) : base(context)
        {
            _context = context;
            _baseRepo = baseRepo;
        }

        public async Task<EventModel> AddEventAsync(EventModel model)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = new Events()
            {
                Name = model.Name,
                EventDate = model.EventDate,
                ClientId = user.ClientId,
            };
            await InsertAsync(data);
            await SaveChangesAsync();
            var result = await GetEventByIdAsync(data.Id);
            return result;
        }

        public async Task<List<EventModel>> GetAllEventAsync()
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var data = await Query(x => x.ClientId == user.ClientId).Select(x => new EventModel()
            {
                Id = x.Id,
                Name = x.Name,
                EventDate = x.EventDate,
                IsActive = x.IsActive,
                ClientId = x.ClientId,
            }).ToListAsync();

            return data;
        }

        public async Task<EventModel> GetEventByIdAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var data = await Query(x => x.ClientId == user.ClientId && x.Id == id).Select(x => new EventModel()
            {
                Id = x.Id,
                Name = x.Name,
                EventDate = x.EventDate,
                IsActive = x.IsActive,
                ClientId = x.ClientId,
            }).FirstOrDefaultAsync();

            return data!;
        }

        public async Task<EventModel> UpdateEventAsync(int id, EventModel model)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = await Query(x => x.Id == id && x.ClientId == user.ClientId).FirstOrDefaultAsync();

            if (model != null)
            {
                data!.Id = id;
                data.Name = model.Name;
                data.EventDate = model.EventDate;
                data.ClientId = data.ClientId;
            }

            Update(data!);
            await SaveChangesAsync();
            var result = await GetEventByIdAsync(data!.Id);
            return result;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = new Events()
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
