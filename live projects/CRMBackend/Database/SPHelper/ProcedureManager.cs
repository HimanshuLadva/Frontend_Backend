using CRMBackend.Database.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Dapper;

namespace CRMBackend.Database.SPHelper
{
    public abstract class ProcedureManager : IProcedureManager
    {
        private readonly DbContext dbContext;
        private readonly IDbConnection connection;

        public ProcedureManager(DbContext _dBContext)
        {
            dbContext = _dBContext;
            connection = new SqlConnection(dbContext.Database.GetConnectionString());
        }

        public TResultSet MapToSpMultipleResultSet<TResultSet>(string storeProcedure, object parameters, int? timeOut = 30) where TResultSet : MultipleResultSet, new()
        {
            var result = new TResultSet();

            try
            {
                using (var multi = connection.QueryMultiple(storeProcedure, parameters, commandTimeout: timeOut, commandType: CommandType.StoredProcedure))
                {
                    if (multi != null)
                    {
                        foreach (var tprop in typeof(TResultSet).GetProperties())
                        {
                            var list = result.CreateListType(tprop);
                            var innerType = result.GetInnerType(tprop);

                            multi.Read(innerType).ToList().ForEach(f => list.Add(f));

                            if (list != null)
                            {
                                tprop.SetValue(result, list, null);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                //logger.Error(Ex, Ex.Message);
            }

            return result;
        }

        public int ExecStoreProcedure(string storeProcedure, object parameters, int? timeOut = 30)
        {
            int Result = -1;

            try
            {
                Result = connection.Execute(sql: storeProcedure, param: parameters, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);
            }
            catch (Exception Ex)
            {
                //logger.Error(Ex, Ex.Message);
            }

            return Result;
        }

        public Task<int>? ExecStoreProcedureAsync<TResult>(string storeProcedure, object parameters, int? timeOut = 30)
        {
            Task<int>? objResult = default;

            try
            {
                objResult = connection.ExecuteAsync(sql: storeProcedure, param: parameters, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);
            }
            catch (Exception Ex)
            {
                //logger.Error(Ex, Ex.Message);
            }

            return objResult;
        }

        public IEnumerable<TResult>? ExecStoreProcedureList<TResult>(string storeProcedure, object parameters, int? timeOut = 30)
        {
            List<TResult>? objResult = default;

            try
            {
                return connection.Query<TResult>(sql: storeProcedure, param: parameters, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);
            }
            catch (Exception Ex)
            {
                //logger.Error(Ex, Ex.Message);
            }

            return objResult;
        }

        public Task<IEnumerable<TResult>>? ExecStoreProcedureListAsync<TResult>(string storeProcedure, object parameters, int? timeOut = 30)
        {
            Task<IEnumerable<TResult>>? objResult = default;

            try
            {
                return connection.QueryAsync<TResult>(sql: storeProcedure, param: parameters, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);
            }
            catch (Exception Ex)
            {
                //logger.Error(Ex, Ex.Message);
            }

            return objResult;
        }
    }
}
