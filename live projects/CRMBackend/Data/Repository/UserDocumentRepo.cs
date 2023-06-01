using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Data.Repositories;
using CRMBackend.Database.DBRepository;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Repository
{
    public class UserDocumentRepo : Repository<UserDocuments>, IUserDocumentRepo
    {
        private readonly RMbackendContext _context;
        private readonly IAWSImageService _imageService;

        public UserDocumentRepo(RMbackendContext context, IAWSImageService imageService) : base(context)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<UserDocumentViewModel> AddDocumentAsync(int contactId, UserDocumentModel model)
        {
            string documentUrl = await _imageService.UploadFileAsync(model.Document, AWSDirectory.Documents);
            var data = new UserDocuments()
            {
                DocumentUrl = documentUrl,
                ContactsId = contactId
            };
            await InsertAsync(data);
            await SaveChangesAsync();

            var result = await GetDocumentByIdAsync(data.Id, data.ContactsId);
            return result;
        }

        public async Task<List<UserDocumentViewModel>> GetAllDocumentAsync(int contactId)
        {
            var data = await GetAll().Where(x => x.ContactsId == contactId).Select(x => new UserDocumentViewModel()
            {
                Id = x.Id,
                DocumentUrl = _imageService.GetImageBaseUrl() + x.DocumentUrl,
                ContactsId = contactId
            }).ToListAsync();

            return data;
        }

        public async Task<UserDocumentViewModel> GetDocumentByIdAsync(int id, int contactId)
        {
            var data = await Query(x => x.ContactsId == contactId && x.Id == id).Select(x => new UserDocumentViewModel()
            {
                Id = x.Id,
                DocumentUrl = _imageService.GetImageBaseUrl() + x.DocumentUrl,
                ContactsId = contactId
            }).FirstOrDefaultAsync();

            return data!;
        }

        public async Task<UserDocumentViewModel> UpdateDocumentAsync(int contactId, int id, UserDocumentModel model)
        {
            var data = await Query(x => x.Id == id && x.ContactsId == contactId).FirstOrDefaultAsync();
            bool isDeletedOrNot = await _imageService.DeleteImageAsync(data!.DocumentUrl!);
            string documentUrl = await _imageService.UploadFileAsync(model.Document, AWSDirectory.Documents);

            if (model != null)
            {
                data!.Id = id;
                data.DocumentUrl = documentUrl;
                data.ContactsId = contactId;
            }

            Update(data!);
            await SaveChangesAsync();
            var result = await GetDocumentByIdAsync(data.Id, data.ContactsId);
            return result;
        }

        public async Task<bool> DeleteDocumentAsync(int contactId, int id)
        {
            var record = await Query(x => x.Id == id && x.ContactsId == contactId).FirstOrDefaultAsync();
            bool isDeletedOrNot = await _imageService.DeleteImageAsync(record!.DocumentUrl!);

            Delete(record);
            await SaveChangesAsync();

            return true;
        }
    }
}
