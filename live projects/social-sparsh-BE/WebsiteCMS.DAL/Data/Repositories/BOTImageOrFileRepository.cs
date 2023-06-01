using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class BOTImageOrFileRepository : Repository<BOTImageOrFile>, IBOTImageOrFileRepository
    {
        private readonly WebsiteCMSDbContext _context;
        private readonly IAWSImageService _imageRepository;

        public BOTImageOrFileRepository(WebsiteCMSDbContext context, IAWSImageService imageRepository) : base(context)
        {
            _context = context;
            _imageRepository = imageRepository;
        }

        public async Task<bool> UpdateImageOrFile(BOTImageOrFileModel model)
        {
            string imageOrFilePath = null!;

            var dataModel = await Query(x => x.FrontendId == model.FrontendId, true).FirstOrDefaultAsync();

            if (model.ImageOrFilePath!.FileName.EndsWith(".png") || model.ImageOrFilePath.FileName.EndsWith(".jpg") || model.ImageOrFilePath!.FileName.EndsWith(".pdf"))
            {
                await using var memoryStream = new MemoryStream();
                await model.ImageOrFilePath!.CopyToAsync(memoryStream);
                string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(model.ImageOrFilePath!.FileName));
                string imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.BOTImageOrFile);
                imageOrFilePath = imageUrl;
            }

            if (imageOrFilePath != null)
            {
                if (dataModel != null)
                {
                    bool isDeleted = await _imageRepository.DeleteImageAsync(dataModel.ImageOrFilePath);
                    dataModel.FrontendId = model.FrontendId;
                    dataModel.ImageOrFilePath = imageOrFilePath;
                    await AddImageorFile(dataModel);
                }
                else
                {
                    BOTImageOrFile bOTImageOrFile = new BOTImageOrFile();
                    bOTImageOrFile.FrontendId = model.FrontendId;
                    bOTImageOrFile.ImageOrFilePath = imageOrFilePath;
                    await AddImageorFile(bOTImageOrFile);
                }
                return true;
            }
            return false;
        }

        public async Task DeleteImageOrFile(string FrontendId)
        {
            var record = Query(x => x.FrontendId == FrontendId, true).FirstOrDefault();
            bool isDeleted = await _imageRepository.DeleteImageAsync(record!.ImageOrFilePath!);
            Delete(record);
            await SaveChangesAsync();
        }

        public async Task<BOTImageOrFile> GetImageOrFileByFrontendId(string FrontendId)
        {
            var record = await Query(x => x.FrontendId == FrontendId, true).Select(x => new BOTImageOrFile()
            {
                Id = x.Id,
                FrontendId = x.FrontendId,
                ImageOrFilePath = x.ImageOrFilePath
            }).FirstOrDefaultAsync();

            if (record != null)
            {
                return record;
            }
            return null!;
        }

        public async Task AddImageorFile(BOTImageOrFile model)
        {
            var isInDataBase = await Query(x => x.FrontendId == model.FrontendId, true).FirstOrDefaultAsync();
            if (isInDataBase != null)
            {
                model.Id = isInDataBase.Id;
                Update(model);
            }
            else
            {
                await InsertAsync(model);
            }
            await SaveChangesAsync();
        }

        public async Task DeleteOtherFrontendId()
        {
            var questions = await _context.tblBOTQuestion.ToListAsync();
            var allImageOrFile = GetAll(true);
            foreach (var item in allImageOrFile)
            {
                var isInQuestion = questions!.Where(x => x.FrontendId == item.FrontendId).FirstOrDefault();
                if (isInQuestion == null)
                {
                    Delete(item);
                }
            }
            await SaveChangesAsync();
        }

    }
}
