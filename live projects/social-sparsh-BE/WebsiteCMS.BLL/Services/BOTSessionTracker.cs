using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.BLL.Services
{
    public class BOTSessionTracker : IBOTSessionTracker
    {
        private readonly IBOTVisitorRepository _visitor;
        public BOTSessionTracker(IBOTVisitorRepository visitor)
        {
            _visitor = visitor;
        }

        private Dictionary<Guid, BOTVisitor>? DictTraker = null;

        public async Task<BOTVisitor> GetVisitorSession(Guid sessionId)
        {
            DictTraker = new Dictionary<Guid, BOTVisitor>();
            //Populate DictTracker
            var visitors = await _visitor.GetAllVisitor();
            foreach (var item in visitors)
            {
                DictTraker.Add(item.VisitorUUId, item);
            }

            if (!DictTraker.ContainsKey(sessionId))
            {
                //GET FROM S
                var visitor = new BOTVisitor
                {
                    VisitorUUId = sessionId,
                    //Platform = string.Empty
                };
                var temp = await _visitor.AddVisitorAsync(visitor);
                //ADD VISITOR TO DICTIONARY
                DictTraker.Add(sessionId, visitor!);
            }
            var Visitor = DictTraker[sessionId];
            return Visitor;
        }
    }
}
