using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Database.DBRepository;

namespace CRMBackend.Data.Repository
{
    public class ContactEventRepo : Repository<ContactEvents>, IContactEventRepo
    {
        private readonly RMbackendContext? _context;

        public ContactEventRepo(RMbackendContext context) : base(context)
        {
        }
        public async Task<bool> AssignEventToContact(int EventId, string ContactCollection)
        {
            string[] contacts = ContactCollection.Split(',').Select(x => x.Trim()).ToArray();

            foreach (var contact in contacts)
            {
                await InsertAsync(new ContactEvents()
                {
                    EventsId = EventId,
                    ContactsId = Convert.ToInt32(contact)
                });
            }
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> AssignContactToEvent(int ContactId, string EventCollection)
        {
            string[] events = EventCollection.Split(',').Select(x => x.Trim()).ToArray();

            foreach (var evts in events)
            {
                await InsertAsync(new ContactEvents()
                {
                    EventsId = Convert.ToInt32(evts),
                    ContactsId = ContactId
                });
            }
            try
            {
                await SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
