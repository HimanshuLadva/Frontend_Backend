using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Database.DBRepository;
using CRMBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Repository
{
    public class UserEmailRepo : Repository<UserEmails>, IUserEmailRepo
    {
        private readonly RMbackendContext _context;
        private readonly IBaseRepo _baseRepo;

        public UserEmailRepo(RMbackendContext context, IBaseRepo baseRepo) : base(context)
        {
            _context = context;
            _baseRepo = baseRepo;
        }

        public async Task<UserEmailModel> AddEmailAsync(UserEmailModel model)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = new UserEmails()
            {
                Subject = model.Subject,
                Body = model.Body,
                ClientId = user.ClientId,
                Recipients = new List<RecipientsEmails>()
            };

            string[] recipients = model.RecipientCollection!.Split(',').Select(x => x.Trim()).ToArray();

            foreach (var recipient in recipients)
            {
                data.Recipients.Add(new RecipientsEmails()
                {
                    UserEmailId = data.Id,
                    Email = recipient
                });
            }

            await InsertAsync(data);
            await SaveChangesAsync();

            var result = await GetEmailByIdAsync(data.Id);
            return result;
        }

        public async Task<List<UserEmailModel>> GetAllEmailAsync()
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var data = await GetAll().Where(x => x.ClientId == user.ClientId).Select(x => new UserEmailModel()
            {
                Id = x.Id,
                Subject = x.Subject,
                Body = x.Body,
                ClientId = x.ClientId,
                Recipients = x.Recipients!.Select(y => new RecipientsEmailModel()
                {
                    Id = y.Id,
                    UserEmailId = y.UserEmailId,
                    Email = y.Email
                }).ToList(),
            }).ToListAsync();

            return data;
        }

        public async Task<UserEmailModel> GetEmailByIdAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var data = await Query(x => x.ClientId == user.ClientId && x.Id == id).Select(x => new UserEmailModel()
            {
                Id = x.Id,
                Subject = x.Subject,
                Body = x.Body,
                ClientId = x.ClientId,
                Recipients = x.Recipients!.Select(y => new RecipientsEmailModel()
                {
                    Id = y.Id,
                    UserEmailId = y.UserEmailId,
                    Email = y.Email
                }).ToList(),
            }).FirstOrDefaultAsync();

            return data!;
        }

        public async Task<bool> DeleteEmailAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = new UserEmails()
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
