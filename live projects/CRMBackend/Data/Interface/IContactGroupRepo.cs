namespace CRMBackend.Data.Interface
{
    public interface IContactGroupRepo
    {
        Task<bool> AssignContactToGroup(int ContactId, string EventCollection);
        Task<bool> AssignGroupToContact(int EventId, string ContactCollection);
    }
}
