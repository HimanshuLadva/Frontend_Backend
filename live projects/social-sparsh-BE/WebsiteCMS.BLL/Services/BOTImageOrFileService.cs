using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Services
{
    public class BOTImageOrFileService : IBOTImageOrFileService
    {
        private readonly IBOTImageOrFileRepository _repository;

        public BOTImageOrFileService(IBOTImageOrFileRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> UpdateImageOrFile(BOTImageOrFileModel model)
        {
            var imageOrFile = await _repository.UpdateImageOrFile(model);
            return imageOrFile;
        }

        public async Task DeleteImageOrFile(string FrontendId)
        {
            await _repository.DeleteImageOrFile(FrontendId);
        }

        public async Task<BOTImageOrFile> GetImageOrFileByFrontendId(string FrontendId)
        {
            var record = await _repository.GetImageOrFileByFrontendId(FrontendId);
            return record!;
        }
    }
}
