using Microsoft.EntityFrameworkCore.Query;
using RealEstate.Core.Domain.Entities;
using System.Linq.Expressions;

namespace RealEstate.Core.GenericRepositories
{
    public interface IRepository<TEntity> : IBaseRepository where TEntity : class, IEntityBase
    {
        /// <summary>
        /// To add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Added entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// To addAsync entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// To addRange multiple entities at once
        /// </summary>
        /// <param name="entities">Added entities</param>
        /// <returns></returns>
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        ///  To addRangeAsync multiple entities at once
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Updated entity</returns>
        TEntity Update(TEntity entity);
        /// <summary>
        ///  Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity DbUpdate(TEntity entity);
        /// <summary>
        /// Update list of entities at once
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Updated entity list</returns>
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="entity"></param>
        void DeleteRemove(TEntity entity);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> Delete(object id);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>True if action succeed</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Delete multiple entity
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>True if action succeed</returns>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        /// <summary>
        ///  Delete multiple entity
        /// </summary>
        /// <param name="entities"></param>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Queriable dbSet
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Executes sql command
        /// </summary>
        /// <param name="interpolatedQueryString"></param>
        /// <returns></returns>
        Task<int> Execute(FormattableString interpolatedQueryString);

        Task<bool> AnyAsync(Func<TEntity, bool> search = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets single entity 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="ignoreQueryFilter"></param>
        /// <param name="includes"></param>
        /// <returns>Filtered entity</returns>
        TEntity Get(Expression<Func<TEntity, bool>> expression, bool ignoreQueryFilter = false, params Expression<Func<TEntity, object>>[] includes);


        /// <summary>
        /// Gets single entity 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="ignoreQueryFilter"></param>
        /// <param name="includes"></param>
        /// <returns>Filtered entity</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, bool ignoreQueryFilter = false, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Gets single entity 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trackChanges"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(object id, bool trackChanges = true, CancellationToken cancellationToken = default,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);


        /// <summary>
        /// Gets single entity 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="trackChanges"></param>
        /// <param name="ignoreQueryFilter"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Func<TEntity, bool> search = null, CancellationToken cancellationToken = default,
            bool trackChanges = false, bool ignoreQueryFilter = false);

        /// <summary>
        ///  Gets single entity 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="orderBy"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="trackChanges"></param>
        /// <param name="ignoreQueryFilter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> search = null,
	        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
	        CancellationToken cancellationToken = default, bool trackChanges = false, bool ignoreQueryFilter = false,
	        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

		/// <summary>
		/// Gets single entity 
		/// </summary>
		/// <param name="search"></param>
		/// <param name="ignoreQueryFilter"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TEntity> LastOrDefaultAsync(Func<TEntity, bool> search = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// GetList entity
        /// </summary>
        /// <param name="search"></param>
        /// <param name="orderBy"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="trackChanges"></param>
        /// <param name="ignoreQueryFilter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> search = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, CancellationToken cancellationToken = default, bool trackChanges = false, bool ignoreQueryFilter = false,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        /// <summary>
        /// List paged response entity
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="orderBy"></param>
        /// <param name="searchQuery"></param>
        /// <param name="ignoreQueryFilter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int page, int size, string orderBy = "",
            Func<TEntity, bool> searchQuery = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// List entity
        /// </summary>
        /// <param name="ignoreQueryFilter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyList<TEntity>> ListAllAsync(bool ignoreQueryFilter = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// get count entity
        /// </summary>
        /// <param name="search"></param>
        /// <param name="ignoreQueryFilter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(Func<TEntity, bool> search = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get count entity
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="ignoreQueryFilter"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(Expression<Func<TEntity, bool>> expression = null, bool ignoreQueryFilter = false);

        /// <summary>
        /// filters and returns list of entities
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="ignoreQueryFilter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int skip = 0, int take = 0, bool ignoreQueryFilter = false, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// filters and returns list of entities
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="ignoreQueryFilter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int skip = 0, int take = 0, bool ignoreQueryFilter = false, params Expression<Func<TEntity, object>>[] includes);
        
        /// <summary>
        /// Save entity
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}