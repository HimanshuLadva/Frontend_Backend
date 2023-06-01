using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class BOTComponentRepository : Repository<BOTComponent>, IBOTComponentRepository
    {
        public BOTComponentRepository(WebsiteCMSDbContext context) : base(context)
        {
        }

        public async Task<BOTComponent> AddComponentAsync(BOTComponent model)
        {
            await InsertSaveAsync(model);
            return model;
        }

        public async Task<BOTComponent> EditComponentAsync(BOTComponent model)
        {
            if (model != null)
            {
                Update(model);
                await SaveChangesAsync();
            }

            return model!;
        }

        public Task<List<BOTComponent>?> GetAllComponentsAsync()
        {
            return GetAll()!.ToListAsync()!;
        }

        public async Task<BOTComponent?> GetComponentByLabelAsync(string Label)
        {
            return await Query(x => x.Label.ToLower() == Label).FirstOrDefaultAsync();
        }

        public async Task<BOTComponent?> GetComponentByIdAsync(long id)
        {
            return await Query(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<BOTComponent>> GetMessageLinkImageFile()
        {
            return await Query(x=> x.Label == "Message" || x.Label == "Image" || x.Label == "File" || x.Label == "Link").ToListAsync();
        }
 
    }
}
