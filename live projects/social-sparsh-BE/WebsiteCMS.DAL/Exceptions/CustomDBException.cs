using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Exceptions
{
    public class CustomDBException : Exception
    {
        private readonly WebsiteCMSDbContext _Context;
        private readonly string _Message;
        public CustomDBException(Exception exception, WebsiteCMSDbContext dbContext)
            : base(exception.Message)
        {
            _Context = dbContext;
            _Message = exception.Message;
        }

        public async Task LogException(ControllerContext context)
        {
            //_DBContext.ChangeTracker.Clear();
            DetachAllEntities();
            var log = new BOTAPILogs
            {
                ExceptionMessage = _Message,
                MethodName = context.RouteData.Values["action"]!.ToString()!
            };
            _Context.tblBOTAPILogs.Add(log);
            await _Context.SaveChangesAsync();
        }

        public void DetachAllEntities()
        {

            var changedEntriesCopy = this._Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
    }
}