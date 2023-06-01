using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMUserRepository : SCRMIUserRepository
    {
        private readonly WebsiteCMSDbContext _context;

        public SCRMUserRepository(WebsiteCMSDbContext context)
        {
            _context = context;
        }

        public Task<SCRMUserModel> AddUserAsync(SCRMUserModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<SCRMUserModel>> GetAllUserAsync(SCRMRequestParams requestParams)
        {
            throw new NotImplementedException();
        }

        public Task<SCRMUserModel> GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<SCRMUserModel> UpdateUserAsync(string userId, SCRMUserModel model)
        {
            throw new NotImplementedException();
        }

        //public async Task<IPagedList<SCRMUserModel>> GetAllUserAsync(SCRMRequestParams requestParams)
        //{
        //    var records = new List<SCRMUserModel>();
        //    records = await _context.tblSCRMUser
        //           .Select(x => new SCRMUserModel()
        //           {
        //               Id = x.Id,
        //               Name = x.Name,
        //               Email = x.Email
        //           }).ToListAsync();

        //    if (requestParams.search != null)
        //        records = await records.Where(x => x.Name.Contains(requestParams.search)).ToListAsync();

        //    requestParams.recordCount = records.Count;

        //    var data = await SortResult(records, requestParams);
        //    return data;
        //}

        //public async Task<SCRMUserModel> GetUserByIdAsync(int userId)
        //{
        //    var record = await _context.tblSCRMUser.Where(x => x.Id == userId)
        //        .Select(x => new SCRMUserModel()
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Email = x.Email
        //        }).FirstOrDefaultAsync();
        //    return record!;
        //}

        //public async Task<SCRMUserModel> AddUserAsync(SCRMUserModel model)
        //{
        //    var data = new SCRMUser()
        //    {
        //        Name = model.Name,
        //        Email = model.Email
        //    };

        //    _context.tblSCRMUser.Add(data);
        //    await _context.SaveChangesAsync();

        //    var record = await GetUserByIdAsync(data.Id);
        //    return record;
        //}

        //public async Task<SCRMUserModel> UpdateUserAsync(int userId, SCRMUserModel model)
        //{
        //    var data = await _context.tblSCRMUser.FindAsync(userId);
        //    if (data != null)
        //    {
        //        data.Id = userId;
        //        data.Name = model.Name;
        //        data.Email = model.Email;

        //        _context.tblSCRMUser.Update(data);
        //        await _context.SaveChangesAsync();

        //        var record = await GetUserByIdAsync(data.Id);
        //        return record;
        //    }
        //    return null!;
        //}

        //public async Task DeleteUserAsync(int userId)
        //{
        //    var record = new SCRMUser()
        //    {
        //        Id = userId
        //    };
        //    _context.tblSCRMUser.Remove(record);
        //    await _context.SaveChangesAsync();
        //}

        //public static async Task<IPagedList<SCRMUserModel>> SortResult(List<SCRMUserModel> source, SCRMRequestParams requestParams)
        //{
        //    IOrderedEnumerable<SCRMUserModel> data = source.OrderBy(s => s.Name);
        //    if (requestParams.sortBy != null)
        //        if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
        //            data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
        //        else
        //            data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

        //    return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        //}
    }
}
