using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Services
{
    public class SCRMCategoryService : ISCRMCategoryService
    {
        private readonly SCRMICategoryRepository _categoryRepository;

        public SCRMCategoryService(SCRMICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<SCRMCategroyWiseTemplateModel>> GetCategoryWiseTemplateListAsync(int id)
        {
            var lstCategoryModel = new List<SCRMCategroyWiseTemplateModel>();

            lstCategoryModel = await _categoryRepository.GetCategoryWiseTemplateListAsync(id);

            return lstCategoryModel;
        }
    }
}
