using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.BLL.Services
{
    public class BusinessCategoryService : IBusinessCategoryService
    {
        private readonly IBusinessCategoryRepository _categoryRepository;

        public BusinessCategoryService(IBusinessCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<BusinessCategoryModel> GetCategoryByIdAsync(int id)
        {
            var record = await _categoryRepository.GetCategoryByIdAsync(id);
            return record!;
        }
        public async Task<BusinessCategoryModel> AddCategory(BusinessCategoryModel model)
        {
            var record = await _categoryRepository.AddCategory(model);
            return record;
        }

        public async Task<IPagedList<BusinessCategoryModel>> GetAllCategoryAsync(SCRMRequestParams requestParams)
        {
            var records = await _categoryRepository.GetAllCategoryAsync(requestParams);
            return records;
        }

        public async Task<BusinessCategoryModel> UpdateCategoryAsync(int id, string Name)
        {
            var data = await _categoryRepository.UpdateCategoryAsync(id, Name);
            return data;
        }


        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var isDeleted = await _categoryRepository.DeleteCategoryAsync(id);
            return isDeleted;
        }
    }
}
