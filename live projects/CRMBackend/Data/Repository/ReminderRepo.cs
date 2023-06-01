using AutoMapper;
using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Database.DBRepository;
using CRMBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Repository
{
    public class ReminderRepo : Repository<Reminders>, IReminderRepo
    {
        private readonly RMbackendContext _context;
        private readonly IBaseRepo _baseRepo;
        private readonly IMapper _mapper;

        public ReminderRepo(RMbackendContext context, IBaseRepo baseRepo, IMapper mapper) : base(context)
        {
            _context = context;
            _baseRepo = baseRepo;
            _mapper = mapper;
        }

        public async Task<ReminderModel> AddReminderAsync(ReminderModel model)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = _mapper.Map<Reminders>(model);
            data.ClientId = user.ClientId;

            await InsertAsync(data);
            await SaveChangesAsync();

            var result = await GetReminderByIdAsync(data.Id);
            return result;
        }

        public async Task<List<ReminderModel>> GetAllReminderAsync()
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var records = await GetAll().Where(x => x.ClientId == user.ClientId).ToListAsync();
            var data = _mapper.Map<List<ReminderModel>>(records);

            return data;
        }

        public async Task<ReminderModel> GetReminderByIdAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var records = await Query(x => x.ClientId == user.ClientId && x.Id == id).FirstOrDefaultAsync();
            var data = _mapper.Map<ReminderModel>(records);

            return data;
        }

        public async Task<ReminderModel> UpdateReminderAsync(int id, ReminderModel model)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = await Query(x => x.Id == id && x.ClientId == user.ClientId).FirstOrDefaultAsync();

            if (model != null)
            {
                data!.Id = data.Id;
                data.ClientId = user.ClientId;
                data.Date = model.Date;
                data.Description = model.Description;
                data.Title = model.Title;
            }

            Update(data!);
            await SaveChangesAsync();
            var result = await GetReminderByIdAsync(data!.Id);
            return result;
        }

        public async Task<bool> DeleteReminderAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = new Reminders()
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
