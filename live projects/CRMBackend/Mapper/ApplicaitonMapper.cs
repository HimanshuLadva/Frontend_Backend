using AutoMapper;
using CRMBackend.Data.Models;
using CRMBackend.Models;

namespace CRMBackend.Mapper
{
    public class ApplicaitonMapper : Profile
    {
        public ApplicaitonMapper()
        {
            CreateMap<Contacts, ContactModel>().ReverseMap();
            CreateMap<Reminders, ReminderModel>().ReverseMap();
        }
    }
}
