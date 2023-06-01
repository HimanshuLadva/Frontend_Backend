using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Repositories;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Services
{
    public class SCRMSubCategoryService : ISCRMSubCategoryServcie
    {
        private readonly SCRMISubCategoryRepository _subCategoryRepository;

        public SCRMSubCategoryService(SCRMISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }
        public async Task<List<SCRMSubCategroyWiseTemplateModel>> GetSubCategoryWiseTemplateListAsync(int id)
        {
            var lstSubCategoryModel = new List<SCRMSubCategroyWiseTemplateModel>();
            lstSubCategoryModel = await _subCategoryRepository.GetSubCategoryWiseTemplateListAsync(id);

            return lstSubCategoryModel;
        }

        public async Task<SCRMCategoryWiseSubCategory> GetCategoryWiseSubCategory(int categoryId)
        {
            SCRMCategoryWiseSubCategory records = new SCRMCategoryWiseSubCategory();


            records = await _subCategoryRepository.GetCategoryWiseSubCategory(categoryId);

            return records;
        }
    }
}
