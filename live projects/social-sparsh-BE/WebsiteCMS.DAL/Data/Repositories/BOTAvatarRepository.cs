using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Exceptions;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class BOTAvatarRepository : Repository<BOTAvatar> ,IBOTAvatarRepository
    {

        public BOTAvatarRepository(WebsiteCMSDbContext context) : base(context)
        {
        }

        public Task<List<BOTAvatar>?> getAvatar()
        {
                return GetAll()!.ToListAsync()!;
        }

        public async Task<bool?> DeleteAvatarAsync(long id)
        {
                DeleteById(id);
                return await SaveChangesAsync() > 0;
        }

        public async Task<BOTAvatar> AddAvatarAsync(BOTAvatar model)
        {
                await InsertSaveAsync(model);
                return model;
        }
    }
}