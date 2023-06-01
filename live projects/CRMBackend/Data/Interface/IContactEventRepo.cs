namespace CRMBackend.Data.Interface
{
    public interface IContactEventRepo
    {
        Task<bool> AssignContactToEvent(int ContactId, string EventCollection);
        Task<bool> AssignEventToContact(int EventId, string ContactCollection);
    }
}
