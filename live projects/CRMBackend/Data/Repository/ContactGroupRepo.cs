using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Database.DBRepository;

namespace CRMBackend.Data.Repository
{
    public class ContactGroupRepo : Repository<ContactGroups>, IContactGroupRepo
    {
        private readonly RMbackendContext? _context;

        public ContactGroupRepo(RMbackendContext context) : base(context)
        {
        }
        public async Task<bool> AssignGroupToContact(int GroupId, string ContactCollection)
        {
            string[] contacts = ContactCollection.Split(',').Select(x => x.Trim()).ToArray();

            foreach (var contact in contacts)
            {
                await InsertAsync(new ContactGroups()
                {
                    GroupId = GroupId,
                    ContactId = Convert.ToInt32(contact)
                });
            }
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> AssignContactToGroup(int ContactId, string GroupCollection)
        {
            string[] events = GroupCollection.Split(',').Select(x => x.Trim()).ToArray();

            foreach (var evts in events)
            {
                await InsertAsync(new ContactGroups()
                {
                    GroupId = Convert.ToInt32(evts),
                    ContactId = ContactId
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
