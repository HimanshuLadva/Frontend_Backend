using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Database.DBRepository;
using CRMBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Repository
{
    public class UserSMSRepo : Repository<UserSMSs>, IUserSMSRepo
    {
        private readonly RMbackendContext _context;
        private readonly IBaseRepo _baseRepo;

        public UserSMSRepo(RMbackendContext context, IBaseRepo baseRepo) : base(context)
        {
            _context = context;
            _baseRepo = baseRepo;
        }

        public async Task<UserSMSModel> AddSMSAsync(UserSMSModel model)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = new UserSMSs()
            {
                Message = model.Message,
                ClientId = user.ClientId,
                Recipients = new List<RecipientsPhoneNos>()
            };

            string[] recipients = model.RecipientCollection!.Split(',').Select(x => x.Trim()).ToArray();

            foreach (var recipient in recipients)
            {
                data.Recipients.Add(new RecipientsPhoneNos()
                {
                    UserSMSId = data.Id,
                    PhoneNo = recipient
                });
            }

            await InsertAsync(data);
            await SaveChangesAsync();

            var result = await GetSMSByIdAsync(data.Id);
            return result;
        }

        public async Task<List<UserSMSModel>> GetAllSMSAsync()
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var data = await GetAll().Where(x => x.ClientId == user.ClientId).Select(x => new UserSMSModel()
            {
                Id = x.Id,
                Message = x.Message,
                ClientId = x.ClientId,
                Recipients = x.Recipients!.Select(y => new RecipientsPhoneNoModel()
                {
                    Id = y.Id,
                    UserSMSId = y.UserSMSId,
                    PhoneNo = y.PhoneNo
                }).ToList(),
            }).ToListAsync();

            return data;
        }

        public async Task<UserSMSModel> GetSMSByIdAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var data = await Query(x => x.ClientId == user.ClientId && x.Id == id).Select(x => new UserSMSModel()
            {
                Id = x.Id,
                Message = x.Message,
                ClientId = x.ClientId,
                Recipients = x.Recipients!.Select(y => new RecipientsPhoneNoModel()
                {
                    Id = y.Id,
                    UserSMSId = y.UserSMSId,
                    PhoneNo = y.PhoneNo
                }).ToList(),
            }).FirstOrDefaultAsync();

            return data!;
        }

        public async Task<bool> DeleteSMSAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = new UserSMSs()
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
