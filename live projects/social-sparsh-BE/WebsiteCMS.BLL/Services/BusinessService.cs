using Microsoft.AspNetCore.Mvc;
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
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.BLL.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;

        public BusinessService(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }
        public async Task<BusinessModel> AddBusiness(BusinessModel model)
        {
            var result = await _businessRepository.AddBusiness(model);
            return result;
        }

        public async Task<IPagedList<BusinessModel>> GetAllBusinessDetail(SCRMRequestParams requestParams)
        {
            var records = await _businessRepository.GetAllBusinessDetail(requestParams);
            return records;
        }

        public async Task<BusinessModel> GetBusinessDetailByIdAsync(int id)
        {
            var record = await _businessRepository.GetBusinessDetailByIdAsync(id);
            return record!;
        }

        public async Task<BusinessModel> UpdateBusinessDetail(int id, BusinessModel model)
        {
            var record = await _businessRepository.UpdateBusinessDetail(id, model);
            return record!;
        }

        public async Task<bool> DeleteBusinessDetailAsync(int id)
        {
            bool isDeleted = await _businessRepository.DeleteBusinessDetailAsync(id);
            return isDeleted;
        }
    }
}
