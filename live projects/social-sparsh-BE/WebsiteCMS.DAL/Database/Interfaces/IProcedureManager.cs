using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Database.SPHelper;

namespace WebsiteCMS.DAL.Database.Interfaces
{
    public interface IProcedureManager
    {
        int ExecStoreProcedure(string storeProcedure, object parameters, int? timeOut = 30);

        Task<int>? ExecStoreProcedureAsync<TResult>(string storeProcedure, object parameters, int? timeOut = 30);

        IEnumerable<TResult>? ExecStoreProcedureList<TResult>(string storeProcedure, object parameters, int? timeOut = 30);

        Task<IEnumerable<TResult>>? ExecStoreProcedureListAsync<TResult>(string storeProcedure, object parameters, int? timeOut = 30);

        TResultSet MapToSpMultipleResultSet<TResultSet>(string storeProcedure, object parameters, int? timeOut = 30) where TResultSet : MultipleResultSet, new();
    }
}
