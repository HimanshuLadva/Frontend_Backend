using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Data.Repositories;
using CRMBackend.Database.DBRepository;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Repository
{
    public class UserPhotoRepo : Repository<UserPhotos>, IUserPhotoRepo
    {
        private readonly RMbackendContext _context;
        private readonly IAWSImageService _imageService;

        public UserPhotoRepo(RMbackendContext context, IAWSImageService imageService) : base(context)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<UserPhotoViewModel> AddPhotoAsync(int contactId, UserPhotoModel model)
        {
            string imageUrl = await _imageService.UploadFileAsync(model.Image, AWSDirectory.Photos);
            var data = new UserPhotos()
            {
                ImageUrl = imageUrl,
                ContactsId = contactId
            };
            await InsertAsync(data);
            await SaveChangesAsync();

            var result = await GetPhotoByIdAsync(data.Id, data.ContactsId);
            return result;
        }

        public async Task<List<UserPhotoViewModel>> GetAllPhotoAsync(int contactId)
        {
            var data = await GetAll().Where(x => x.ContactsId == contactId).Select(x => new UserPhotoViewModel()
            {
                Id = x.Id,
                ImageUrl = _imageService.GetImageBaseUrl() + x.ImageUrl,
                ContactsId = contactId
            }).ToListAsync();

            return data;
        }

        public async Task<UserPhotoViewModel> GetPhotoByIdAsync(int id, int contactId)
        {
            var data = await Query(x => x.ContactsId == contactId && x.Id == id).Select(x => new UserPhotoViewModel()
            {
                Id = x.Id,
                ImageUrl = _imageService.GetImageBaseUrl() + x.ImageUrl,
                ContactsId = contactId
            }).FirstOrDefaultAsync();

            return data!;
        }

        public async Task<UserPhotoViewModel> UpdatePhotoAsync(int contactId, int id, UserPhotoModel model)
        {
            var data = await Query(x => x.Id == id && x.ContactsId == contactId).FirstOrDefaultAsync();
            bool isDeletedOrNot = await _imageService.DeleteImageAsync(data!.ImageUrl!);
            string imageUrl = await _imageService.UploadFileAsync(model.Image, AWSDirectory.Photos);

            if (model != null)
            {
                data!.Id = id;
                data.ImageUrl = imageUrl;
                data.ContactsId = contactId;
            }

            Update(data!);
            await SaveChangesAsync();

            var result = await GetPhotoByIdAsync(data.Id, data.ContactsId);
            return result;
        }

        public async Task<bool> DeletePhotoAsync(int contactId, int id)
        {
            var record = await Query(x => x.Id == id && x.ContactsId == contactId).FirstOrDefaultAsync();
            bool isDeletedOrNot = await _imageService.DeleteImageAsync(record!.ImageUrl!);

            Delete(record);
            await SaveChangesAsync();

            return true;
        }
    }
}
