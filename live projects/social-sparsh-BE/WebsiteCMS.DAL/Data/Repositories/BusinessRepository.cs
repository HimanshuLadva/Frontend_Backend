using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Dml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using WebsiteCMS.DAL.Utility;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class BusinessRepository : Repository<BusinessInfo>, IBusinessRepository
    {
        private readonly WebsiteCMSDbContext _context;

        public BusinessRepository(WebsiteCMSDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<BusinessModel> AddBusiness(BusinessModel model)
        {
            var businessInfoCategoryTBL = _context.tblBusinessInfoCategorys;
            var businessServiceAreaTBL = _context.tblBusinessServiceArea;
            var phoneNos = _context.tblBusinessPhoneNos;

            string[] businessCategories = model.BusinessCategoryColleciton.Split(',').Select(x => x.Trim()).ToArray();
            string[] businessServiceArea = model.businessServiceAreaCollecitons.Split(',').Select(x => x.Trim()).ToArray();
            string[] UserphoneNos = model.UserPhoneCollection.Split(',').Select(x => x.Trim()).ToArray();

            var businessInfo = new BusinessInfo
            {
                BusinessName = model.BusinessName,
                BusinessCategoryList = new List<BusinessInfoCategories>(),
                Description = model.Description,
                TypeOfService = model.TypeOfService,
                OpeningDate = model.OpeningDate,
                businessServiceAreas = new List<BusinessServiceArea>(),
                LocationInfo = new BusinessLocationInfo(),
                ContactInfo = new BusinessContactInfo(),
            };

            businessInfo.LocationInfo = new BusinessLocationInfo
            {
                Country = _context.tblCountry.FirstOrDefaultAsync(x => x.Id == model.CountryId).Result!.Name,
                State = _context.tblState.FirstOrDefaultAsync(x => x.Id == model.StateId).Result!.Name,
                City = _context.tblCity.FirstOrDefaultAsync(x => x.Id == model.CityId).Result!.Name,
                PinCode = model.PinCode,
                BusinessInfoId = businessInfo.Id,
                StreetAddressLine1 = model.StreetAddressLine1,
                StreetAddressLine2 = model.StreetAddressLine2,
                StreetAddressLine3 = model.StreetAddressLine3,
            };

            businessInfo.ContactInfo = new BusinessContactInfo
            {
                UserPhoneNos = new List<BusinessPhoneNos>(),
                Email = model.Email,
                Website = model.Website,
                BusinessInfoId = businessInfo.Id
            };

            foreach (var category in businessCategories)
            {
                businessInfo.BusinessCategoryList.Add(new BusinessInfoCategories()
                {
                    BusinessInfoId = businessInfo.Id,
                    BusinessCategoryId = Convert.ToInt32(category)
                });
            }

            foreach (var area in businessServiceArea)
            {
                businessInfo.businessServiceAreas.Add(new BusinessServiceArea()
                {
                    Name = area,
                    BusinessInfoId = model.Id
                });
            }

            foreach (var phoneNumbers in UserphoneNos)
            {
                businessInfo.ContactInfo.UserPhoneNos.Add(new BusinessPhoneNos()
                {
                    ContactInfoId = businessInfo.ContactInfo.Id,
                    PhoneNumber = phoneNumbers
                });
            }

            //_context.tblBusinessInfo.Add(businessInfo);
            Insert(businessInfo);
            //_context.tblBusinessContactInfo.Add(contactInfo);
            //_context.tblBusinessLocationInfo.Add(locationInfo);
            await SaveChangesAsync();
            model.Id = businessInfo.Id;

            return model;
        }

        public async Task<IPagedList<BusinessModel>> GetAllBusinessDetail(SCRMRequestParams requestParams)
        {
            var records = new List<BusinessModel>();
            records = await GetAll().Select(x => new BusinessModel()
            {
                Id = x.Id,
                BusinessName = x.BusinessName,
                BusinessCategoryList = x.BusinessCategoryList!.Select(t => new BusinessInfoCategoriesModel()
                {
                    Id = t.Id,
                    BusinessInfoId = t.BusinessInfoId,
                    BusinessCategoryId = Convert.ToInt32(t.BusinessCategoryId)
                }).ToList(),
                Description = x.Description,
                TypeOfService = x.TypeOfService,
                OpeningDate = x.OpeningDate,
                businessServiceAreas = x.businessServiceAreas!.Select(y => new BusinessServiceAreaModel()
                {
                    Id = y.Id,
                    Name = y.Name,
                    BusinessInfoId = y.BusinessInfoId
                }).ToList(),
                UserPhoneNos = x.ContactInfo.UserPhoneNos!.Select(z => new BusinessPhoneNosModel()
                {
                    Id = z.Id,
                    ContactInfoId = z.ContactInfoId,
                    PhoneNumber = z.PhoneNumber
                }).ToList(),
                Email = x.ContactInfo.Email,
                Website = x.ContactInfo.Website,
                Country = x.LocationInfo.Country,
                State = x.LocationInfo.State,
                City = x.LocationInfo.City,
                PinCode = x.LocationInfo.PinCode,
                StreetAddressLine1 = x.LocationInfo.StreetAddressLine1,
                StreetAddressLine2 = x.LocationInfo.StreetAddressLine2,
                StreetAddressLine3 = x.LocationInfo.StreetAddressLine3
            }).ToListAsync();

            if (requestParams.search != null)
                records = await records.Where(x => x.BusinessName!.Contains(requestParams.search)).ToListAsync();


            requestParams.recordCount = records.Count;
            var data = await SortResult(records, requestParams);
            return data;
        }

        public async Task<BusinessModel> UpdateBusinessDetail(int id, BusinessModel model)
        {
            var businessCategoryTBL = _context.tblBusinessInfoCategorys;
            var businessServiceAreaTBL = _context.tblBusinessServiceArea;
            var contactTBL = _context.tblBusinessContactInfo;
            var phoneNosTBL = _context.tblBusinessPhoneNos;

            string[] businessCategories = model.BusinessCategoryColleciton.Split(',').Select(x => x.Trim()).ToArray();
            string[] businessServiceArea = model.businessServiceAreaCollecitons.Split(',').Select(x => x.Trim()).ToArray();
            string[] UserphoneNos = model.UserPhoneCollection.Split(',').Select(x => x.Trim()).ToArray();

            var data = await Query(x => x.Id == id).Include(x => x.ContactInfo).ThenInclude(x => x.UserPhoneNos).IncludeEntities(x => x.BusinessCategoryList!).IncludeEntities(x => x.LocationInfo).IncludeEntities(x => x.businessServiceAreas!).FirstAsync();
            var contact = data!.ContactInfo;

            if (data != null)
            {
                data.BusinessName = model.BusinessName;
                data.BusinessCategoryList = new List<BusinessInfoCategories>();
                data.Description = model.Description;
                data.TypeOfService = model.TypeOfService;
                data.OpeningDate = model.OpeningDate;
                data.businessServiceAreas = new List<BusinessServiceArea>();
                data.LocationInfo = new BusinessLocationInfo();
                data.ContactInfo = new BusinessContactInfo
                {
                    UserPhoneNos = new List<BusinessPhoneNos>(),
                    Email = model.Email,
                    Website = model.Website,
                    BusinessInfoId = data.Id
                };

                var exitsCategorys = businessCategoryTBL.Where(x => x.BusinessInfoId == id);
                if (exitsCategorys != null)
                {
                    foreach (var category in exitsCategorys)
                    {
                        businessCategoryTBL.Remove(category);
                    }
                }

                foreach (var category in businessCategories)
                {
                    data.BusinessCategoryList.Add(new BusinessInfoCategories()
                    {
                        BusinessInfoId = data.Id,
                        BusinessCategoryId = Convert.ToInt32(category)
                    });
                }

                var exitsServiceAreas = businessServiceAreaTBL.Where(x => x.BusinessInfoId == id);
                if (exitsServiceAreas != null)
                {
                    foreach (var area in exitsServiceAreas)
                    {
                        businessServiceAreaTBL.Remove(area);
                    }
                }
                foreach (var area in businessServiceArea)
                {
                    data.businessServiceAreas.Add(new BusinessServiceArea()
                    {
                        Name = area,
                        BusinessInfoId = model.Id
                    });
                }

                var exitsPhoneNos = phoneNosTBL.Where(x => x.ContactInfoId == contact.Id);
                if (exitsPhoneNos != null)
                {
                    foreach (var phoneNumber in exitsPhoneNos)
                    {
                        phoneNosTBL.Remove(phoneNumber);
                    }
                }

                foreach (var phoneNumbers in UserphoneNos)
                {
                    data!.ContactInfo!.UserPhoneNos!.Add(new BusinessPhoneNos()
                    {
                        ContactInfoId = data.ContactInfo.Id,
                        PhoneNumber = phoneNumbers
                    });
                }
                Update(data);
                await SaveChangesAsync();
                var record = await GetBusinessDetailByIdAsync(data.Id);
                return record;

            }
            return null!;
        }

        public async Task<BusinessModel> GetBusinessDetailByIdAsync(int id)
        {
            var record = await Query(x => x.Id == id).Select(x => new BusinessModel()
            {
                Id = x.Id,
                BusinessName = x.BusinessName,
                BusinessCategoryList = x!.BusinessCategoryList!.Select(t => new BusinessInfoCategoriesModel()
                {
                    Id = t.Id,
                    BusinessInfoId = t.BusinessInfoId,
                    BusinessCategoryId = Convert.ToInt32(t.BusinessCategoryId)
                }).ToList(),
                Description = x.Description,
                TypeOfService = x.TypeOfService,
                OpeningDate = x.OpeningDate,
                businessServiceAreas = x.businessServiceAreas!.Select(y => new BusinessServiceAreaModel()
                {
                    Id = y.Id,
                    Name = y.Name,
                    BusinessInfoId = y.BusinessInfoId
                }).ToList(),
                UserPhoneNos = x.ContactInfo.UserPhoneNos!.Select(z => new BusinessPhoneNosModel()
                {
                    Id = z.Id,
                    ContactInfoId = z.ContactInfoId,
                    PhoneNumber = z.PhoneNumber
                }).ToList(),
                Email = x.ContactInfo.Email,
                Website = x.ContactInfo.Website,
                Country = x.LocationInfo.Country,
                State = x.LocationInfo.State,
                City = x.LocationInfo.City,
                PinCode = x.LocationInfo.PinCode,
                StreetAddressLine1 = x.LocationInfo.StreetAddressLine1,
                StreetAddressLine2 = x.LocationInfo.StreetAddressLine2,
                StreetAddressLine3 = x.LocationInfo.StreetAddressLine3
            }).FirstOrDefaultAsync();
            return record!;
        }

        public async Task<bool> DeleteBusinessDetailAsync(int id)
        {
            var record = new BusinessInfo()
            {
                Id = id
            };
            if (record == null)
            {
                return false;
            }
            Delete(record);
            await SaveChangesAsync();
            return true;
        }
        public static async Task<IPagedList<BusinessModel>> SortResult(List<BusinessModel> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<BusinessModel> data = source.OrderBy(s => s.BusinessName);
            if (requestParams.sortBy != null)
                if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }
    }
}
