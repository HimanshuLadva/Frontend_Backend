using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBOTVisitorRepository
    {
        Task<BOTVisitor?> GetBotVisitorByUUID(Guid id);
        Task<BOTVisitor?> UpdateVisitorAsync(BOTVisitor model);
        Task<List<BOTVisitor>> GetAllVisitor();
        Task<BOTVisitor> AddVisitorAsync(BOTVisitor model);
        Task<BOTVisitor?> GetBotVisitorById(long id);
    }
}