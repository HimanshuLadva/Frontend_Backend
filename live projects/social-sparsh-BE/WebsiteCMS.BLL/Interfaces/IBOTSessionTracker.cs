using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.BLL.Interfaces
{
    public interface IBOTSessionTracker
    {
        Task<BOTVisitor> GetVisitorSession(Guid sessionId);
    }
}