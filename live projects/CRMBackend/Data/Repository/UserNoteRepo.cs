using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Data.Repositories;
using CRMBackend.Database.DBRepository;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Repository
{
    public class UserNoteRepo : Repository<UserNotes>, IUserNoteRepo
    {
        private readonly RMbackendContext _context;

        public UserNoteRepo(RMbackendContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserNoteModel> AddNoteAsync(int contactId, UserNoteModel model)
        {
            var data = new UserNotes()
            {
                Description = model.Description,
                ContactsId = contactId
            };
            await InsertAsync(data);
            await SaveChangesAsync();

            var result = await GetNoteByIdAsync(data.Id, data.ContactsId);
            return result;
        }

        public async Task<List<UserNoteModel>> GetAllNoteAsync(int contactId)
        {
            var data = await GetAll().Where(x => x.ContactsId == contactId).Select(x => new UserNoteModel()
            {
                Id = x.Id,
                Description = x.Description,
                ContactsId = contactId
            }).ToListAsync();

            return data;
        }

        public async Task<UserNoteModel> GetNoteByIdAsync(int id, int contactId)
        {
            var data = await Query(x => x.ContactsId == contactId && x.Id == id).Select(x => new UserNoteModel()
            {
                Id = x.Id,
                Description = x.Description,
                ContactsId = contactId
            }).FirstOrDefaultAsync();

            return data!;
        }

        public async Task<UserNoteModel> UpdateNoteAsync(int contactId, int id, UserNoteModel model)
        {
            var data = await Query(x => x.Id == id && x.ContactsId == contactId).FirstOrDefaultAsync();

            if (model != null)
            {
                data!.Id = id;
                data.Description = model.Description;
                data.ContactsId = contactId;
            }

            Update(data!);
            await SaveChangesAsync();

            var result = await GetNoteByIdAsync(data.Id, data.ContactsId);
            return result;
        }

        public async Task<bool> DeleteNoteAsync(int contactId, int id)
        {
            var record = await Query(x => x.Id == id && x.ContactsId == contactId).FirstOrDefaultAsync();

            Delete(record!);
            await SaveChangesAsync();

            return true;
        }
    }
}
