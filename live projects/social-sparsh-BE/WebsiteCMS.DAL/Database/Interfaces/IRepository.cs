using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Database.Interfaces
{
    //public interface IRepository<TEntity> : IProcedureManager where TEntity : class
    //{
    //    IQueryable<TEntity> GetAll(bool AsNoTracking);
    //    void Delete(TEntity entityToDelete);

    //    void DeleteById(object id);

    //    void DeleteByIdAsync(object id);

    //    IQueryable<TEntity> GetPagedResponseAsync(int pageNumber, int pageSize);

    //    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, bool AsNoTracking);

    //    void Insert(TEntity entity);

    //    Task InsertAsync(TEntity entity);

    //    void InsertRange(List<TEntity> entities);

    //    Task InsertRangeAsync(List<TEntity> entities);

    //    Task<TEntity?> InsertSaveAsync(TEntity entity);

    //    void Update(TEntity entityToUpdate);

    //    void BeginTransaction();

    //    void Commit();

    //    TEntity? GetByID(object id);
    //    Task<TEntity?> GetByIDAsync(object id);

    //    bool IsExists(Expression<Func<TEntity, bool>> filter);

    //    Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> filter);

    //    void Rollback();

    //    int SaveChanges();

    //    Task<int> SaveChangesAsync();
    //}
}