using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Database.Interfaces;
using WebsiteCMS.DAL.Database.SPHelper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Routing.Template;

namespace WebsiteCMS.DAL.Database.DBRepository
{
    /// <summary>
    ///     A generic class that is the parent of all the Repository classes.
    ///     Contains methods for performing CRUD operations.
    ///     <list type="bullet">
    ///     <listheader>Has the following fields:</listheader>
    ///     <item><see cref="bool"/> <term>disposed</term><description>Because it inherits form <see cref="IDisposable"/></description></item>
    ///     <item><see cref="DbContext"/> <term>dBContext</term><description>To access the database.</description></item>
    ///     <item><see cref="DbSet{TEntity}"/> <term>dbSet</term><description>To access the specific table.</description></item>
    ///     <item><see cref="IDbContextTransaction"/> <term>transaction</term><description>For intigrity so the operation cAn be rolled back if not properly executed.</description></item>
    ///     </list>
    /// </summary>
    public abstract class Repository<TEntity> : ProcedureManager, IDisposable where TEntity : class
    {
        private bool disposed = false;
        private DbContext dBContext;
        private readonly DbSet<TEntity> dbSet;
        private IDbContextTransaction? transaction;

        /// <summary>
        ///     Initializes A new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DbContext context) : base(context)
        {
            this.dBContext = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        ///     A method that Gets the all records of A perticular table.
        ///     <para>
        ///         Gets A <see cref="bool"/> to specify to track entities by the <see cref="DbContext.ChangeTracker"/>
        ///     </para>
        /// </summary>
        /// <param name="AsNoTracking">If true, as no tracking.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public IQueryable<TEntity> GetAll(bool AsNoTracking = false)
        {
            return GetQueryable(null, AsNoTracking);
        }

        /// <summary>
        ///     A method that Deletes the entity provided.
        ///     <para>
        ///         Gets <see cref="TEntity"/> to be deleted.
        ///     </para>
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete != null)
            {
                if (dBContext.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }

                dbSet.Remove(entityToDelete);
            }
        }

        /// <summary>
        ///     A method that Deletes the <see cref="TEntity"/> by id.
        ///     <para>
        ///         Gets <see cref="object"/> id of the <see cref="TEntity"/> to be deleted
        ///     </para>
        /// </summary>
        /// <param name="id">The id.</param>
        public virtual void DeleteById(object id)
        {
            TEntity? entity = dbSet.Find(id);

            if (entity != null)
            {
                Delete(entity);
            }
        }

        /// <summary>
        ///     A method that Deletes the range of <see cref="TEntity"/> provided.
        ///     <para>
        ///         Gets <see cref="List{T}"/> of <see cref="TEntity"/> to be Deleted
        ///     </para>
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void DeleteRange(List<TEntity> entities)
        {
            if(entities != null || entities!.Count != 0)
            {
                dbSet.RemoveRange(entities);
            }
        }

        /// <summary>
        ///     A method that Gets the <see cref="Queryable"/> from the Database of <see cref="TEntity"/>.
        ///     <para>
        ///         Gets <see cref="Expression{Func{TEntity, bool}}"/> filter to filter out the records.
        ///     </para>
        ///     <para>
        ///         Gets A <see cref="bool"/> to specify to track entities by the <see cref="DbContext.ChangeTracker"/>
        ///     </para>
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="AsNoTracking">If true, as no tracking.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? filter = null, bool AsNoTracking = false)
        {
            IQueryable<TEntity> query = AsNoTracking ? dbSet.AsNoTracking() : dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        /// <summary>
        ///     An asynchronous method that deletes the <see cref="TEntity"/> by Id.
        ///     <para>
        ///         Gets <see cref="object"/> id of the <see cref="TEntity"/> to be deleted.
        ///     </para>
        /// </summary>
        /// <param name="id">The id.</param>
        public virtual async void DeleteByIdAsync(object id)
        {
            TEntity? entity = await dbSet.FindAsync(id);

            if (entity != null)
            {
                Delete(entity);
            }
        }

        /// <summary>
        ///     An asynchronous method to get paginated response list of <see cref="TEntity"/>.
        ///     <para>
        ///         Gets <see cref="int"/> pageNumber to get the starting point of the response List.
        ///     </para>
        ///     <para>
        ///         Gets <see cref="int"/> pageSize to determine the number of records to be takken
        ///     </para>
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public IQueryable<TEntity> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking();
        }

        /// <summary>
        ///     method to query the  database and Get the desired Result.
        ///     <para>
        ///         Gets <see cref="Expression{Func{TEntity, bool}}"/> filter to filter out the records.
        ///     </para>
        ///     <para>
        ///         Gets A <see cref="bool"/> to specify to track entities by the <see cref="DbContext.ChangeTracker"/>
        ///     </para>
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="AsNoTracking">If true, as no tracking.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, bool AsNoTracking = false)
        {
            return GetQueryable(filter, AsNoTracking);
        }

        /// <summary>
        ///     A method to insert the <see cref="TEntity"/> to the table.
        ///     <para>
        ///         Gets the <see cref="TEntity"/> to be inserted.
        ///     </para>
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(TEntity entity)
        {
            if (entity != null)
            {
                dbSet.Add(entity);
            }
        }

        /// <summary>
        ///     An asynchronous method to insert the <see cref="TEntity"/> to the table.
        ///     <para>
        ///         Gets the <see cref="TEntity"/> to be inserted.
        ///     </para>
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity != null)
            {
                await dbSet.AddAsync(entity);
            }
        }

        /// <summary>
        ///     An asynchronous method to insert the <see cref="TEntity"/> and save to the table.
        ///     <para>
        ///         Gets the <see cref="TEntity"/> to be inserted.
        ///     </para>
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="Task{TResult}"/> of <see cref="TEntity"/>.</returns>
        public virtual async Task<TEntity?> InsertSaveAsync(TEntity entity)
        {
            if (entity != null)
            {
                await dbSet.AddAsync(entity);

                if (await SaveChangesAsync() > 0)
                {
                    return entity;
                }
            }

            return null;
        }

        /// <summary>
        ///     A method to insert the range of <see cref="TEntity"/> to the table.
        ///     <para>
        ///         Gets the <see cref="List{T}"/> of <see cref="TEntity"/> to be inserted.
        ///     </para>
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertRange(List<TEntity> entities)
        {
            if (entities != null)
            {
                dbSet.AddRange(entities);
            }
        }

        /// <summary>
        ///     An asynchronous method to insert the range of <see cref="TEntity"/> to the table.
        ///     <para>
        ///         Gets the <see cref="List{T}"/> of <see cref="TEntity"/> to be inserted.
        ///     </para>
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public virtual async Task InsertRangeAsync(List<TEntity> entities)
        {
            if (entities != null)
            {
                await dbSet.AddRangeAsync(entities);
            }
        }

        /// <summary>
        ///     A method to update the <see cref="TEntity"/> record.
        ///     <para>
        ///         Gets the <see cref="TEntity"/> to update.
        ///     </para>
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate != null)
            {
                dbSet.Update(entityToUpdate);
            }
        }

        /// <summary>
        ///     A method that begins the database transaction.
        /// </summary>
        public void BeginTransaction()
        {
            transaction = dBContext.Database.BeginTransaction();
        }

        /// <summary>
        ///     A method that commits the database transaction.
        /// </summary>
        public void Commit()
        {
            transaction?.Commit();
        }

        /// <summary>
        ///     A method that rollbacks thedatabase transaction.
        /// </summary>
        public void Rollback()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        /// <summary>
        ///     A method that saves the changes made into the <see cref="dbSet"/>.
        /// </summary>
        /// <returns>An <see cref="int"/> that indicates the number of rows effected.</returns>
        public int SaveChanges()
        {
            return dBContext.SaveChanges();
        }

        /// <summary>
        ///     An asynchronous method that saves the changes made into the <see cref="dbSet"/>.
        /// </summary>
        /// <returns>A <see cref="Task"/> of <see cref="int"/> that indicates the number of rows effected.</returns>
        public Task<int> SaveChangesAsync()
        {
            return dBContext.SaveChangesAsync();
        }

        /// <summary>
        ///     A method that Gets the <see cref="TEntity"/> by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A <see cref="TEntity?"/>.</returns>
        public TEntity? GetByID(object id)
        {
            return dbSet.Find(id);
        }
        
        /// <summary>
        ///     An asynchronous method that Gets the <see cref="TEntity"/> by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A <see cref="TEntity?"/>.</returns>
        public async Task<TEntity?> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        /// <summary>
        ///     A method that checks weather the <see cref="TEntity"/> with provided filter wexist in the table or not.
        ///     <para>
        ///         Gets <see cref="Expression{Func{TEntity, bool}}"/> filter to filter out the records.
        ///     </para>
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>A <see cref="bool"/> that indicates weather <see cref="TEntity"/> exists or not.</returns>
        public bool IsExists(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity?> query = GetQueryable(filter);
            return query.Any();
        }
        
        /// <summary>
        ///     A method that checks weather the <see cref="TEntity"/> with provided filter wexist in the table or not.
        ///     <para>
        ///         Gets <see cref="Expression{Func{TEntity, bool}}"/> filter to filter out the records.
        ///     </para>
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>A <see cref="Task"/> of <see cref="bool"/> that indicates weather <see cref="TEntity"/> exists or not.</returns>
        public async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity?> query = GetQueryable(filter);
            return await query.AnyAsync();
        }

        /// <summary>
        ///     Releases the unmanaged resources used by the Repository&lt;TEntity&gt; and optionally
        ///     releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     True to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dBContext.Dispose();

                    if (transaction != null)
                    {
                        transaction.Dispose();
                    }
                }
            }

            this.disposed = true;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}