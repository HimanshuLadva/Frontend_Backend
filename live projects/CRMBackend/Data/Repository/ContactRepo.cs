using AutoMapper;
using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Database.DBRepository;
using CRMBackend.Models;
using CRMBackend.Utility;
using Microsoft.EntityFrameworkCore;

namespace CRMBackend.Data.Repository
{
    public class ContactRepo : Repository<Contacts>, IContactRepo
    {
        private readonly RMbackendContext _context;
        private readonly IBaseRepo _baseRepo;
        private readonly IMapper _mapper;

        public ContactRepo(RMbackendContext context,
                           IBaseRepo baseRepo,
                           IMapper mapper) : base(context)
        {
            _context = context;
            _baseRepo = baseRepo;
            _mapper = mapper;
        }

        public async Task<ContactModel> AddContactAsync(ContactModel model)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            //var data = _mapper.Map<Contacts>(model);
            //data.ClientId = user.ClientId;
            var data = new Contacts()
            {
                CompanyName = model.CompanyName,
                CompanyAddress = model.CompanyAddress,
                StateId = model.StateId,
                DistrictId = model.DistrictId,
                CityId = model.CityId,
                PinCode = model.PinCode,
                CompanyPhoneNo1 = model.CompanyPhoneNo1,
                CompanyPhoneNo2 = model.CompanyPhoneNo2,
                CompanyWebsite = model.CompanyWebsite,
                CompanyEmail = model.CompanyEmail,
                ImportantDate = model.ImportantDate,
                ImportantDateDesc = model.ImportantDateDesc,
                PersonName = model.PersonName,
                PersonDesignation = model.PersonDesignation,
                PersonBirthday = model.PersonBirthday,
                PersonMobile = model.PersonMobile,
                PersonAddress = model.PersonAddress,
                PersonStateId = model.PersonStateId,
                PersonDistrictId = model.PersonDistrictId,
                PersonCityId = model.PersonCityId,
                PersonPinCode = model.PersonPinCode,
                PersonPhoneNo1 = model.PersonPhoneNo1,
                PersonPhoneNo2 = model.PersonPhoneNo2,
                PersonEmail = model.PersonEmail,
                Person2Name = model.Person2Name,
                Person2Designation = model.Person2Designation,
                Person2Birthday = model.PersonBirthday,
                Person2Mobile = model.PersonMobile,
                ClientId = user.ClientId,

                /*public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public int StateId { get; set; }
        public States? State { get; set; }
        public int DistrictId { get; set; }
        public Districts? District { get; set; }
        public int CityId { get; set; }
        public Cities? City { get; set; }
        public string? PinCode { get; set; }
        public string? CompanyPhoneNo1 { get; set; }
        public string? CompanyPhoneNo2 { get; set; }
        public string? CompanyWebsite { get; set; }
        public string? CompanyEmail { get; set; }

        //important dates
        public DateTime? ImportantDate { get; set; }
        public string? ImportantDateDesc { get; set; }

        //person detail 
        public string? PersonName { get; set; }
        public string? PersonDesignation { get; set; }
        public DateTime? PersonBirthday { get; set; }
        public string? PersonMobile { get; set; }
        public string? PersonAddress { get; set; }
        public int PersonStateId { get; set; }
        public int PersonDistrictId { get; set; }
        public int PersonCityId { get; set; }
        public string? PersonPinCode { get; set; }
        public string? PersonPhoneNo1 { get; set; }
        public string? PersonPhoneNo2 { get; set; }
        public string? PersonEmail { get; set; }
        public string? Person2Name { get; set; }
        public string? Person2Designation { get; set; }
        public DateTime? Person2Birthday { get; set; }
        public string? Person2Mobile { get; set; }

        // references
        public int ClientId { get; set; }*/
            };

            try
            {
                await InsertAsync(data);
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

            var result = await GetContactByIdAsync(data.Id);
            return result;
        }

        public async Task<List<ContactModel>> GetAllContactAsync()
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var records = await Query(x => x.ClientId == user.ClientId).ToListAsync();
            var data = _mapper.Map<List<ContactModel>>(records);

            return data;
        }

        public async Task<ContactModel> GetContactByIdAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();

            var records = await Query(x => x.ClientId == user.ClientId && x.Id == id).FirstOrDefaultAsync();
            var data = _mapper.Map<ContactModel>(records);

            return data;
        }
        public async Task<List<Events>> ContactWiseEvents(int contactId)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var record = await Query(x => x.ClientId == user.ClientId && x.Id == contactId).Include(x => x.ContactEvent!).ThenInclude(x => x.Events).Where(x => x.Id == contactId).FirstOrDefaultAsync();
            //var records = await _context.ContactEvents.Where(x => x.ContactsId == contactId).ToListAsync();

            return record!.ContactEvent!.Select(x => x.Events).ToList();
        }

        public async Task<ContactModel> UpdateContactAsync(int id, ContactModel model)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var record = await Query(x => x.Id == id && x.ClientId == user.ClientId).FirstOrDefaultAsync();

            //if (model.Image != null)
            //{
            //    await _photoRepo.AddPhotoAsync(model.ContactsId, new UserPhotoModel()
            //    {
            //        Image = model.Image,
            //        ContactsId = model.ContactsId,
            //    });
            //}
            //if (model.Description != null)
            //{
            //    await _noteRepo.AddNoteAsync(model.ContactsId, new UserNoteModel()
            //    {
            //        Description = model.Description,
            //        ContactsId = model.ContactsId,
            //    });
            //}
            //if (model.Document != null)
            //{
            //    await _documentRepo.AddDocumentAsync(model.ContactsId, new UserDocumentModel()
            //    {
            //        Document = model.Document,
            //        ContactsId = model.ContactsId,
            //    });
            //}

            var data = _mapper.Map<Contacts>(model);
            data.Id = record!.Id;
            data.ClientId = record!.ClientId;

            Update(data!);
            await SaveChangesAsync();
            var result = await GetContactByIdAsync(data.Id);
            return result;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var user = await _baseRepo.GetCurrentUserAsync();
            var data = new Contacts()
            {
                Id = id,
                ClientId = user.ClientId,
            };

            Delete(data);
            await SaveChangesAsync();

            return true;
        }
    }
}
