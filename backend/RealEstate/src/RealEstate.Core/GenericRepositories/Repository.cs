using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using RealEstate.Core.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;

namespace RealEstate.Core.GenericRepositories
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
			where TEntity : class, IEntityBase
            where TContext : DbContext
	{
		public readonly TContext _dbContext;
		private readonly ILogger<TEntity> _logger;

		public Repository(TContext dbContext,
			ILogger<TEntity> logger)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(TContext));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public virtual TEntity Add(TEntity entity)
		{
			_dbContext.Set<TEntity>().Add(entity);
			return entity;
		}

		public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			var entities = await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
			return entities.Entity;
		}

		public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
		{
			_dbContext.Set<TEntity>().AddRange(entities);
			return entities;
		}

		public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken)
		{
			await _dbContext.Set<TEntity>().AddRangeAsync(entity, cancellationToken);
			return entity;
		}

		public virtual TEntity Update(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			return entity;
		}

		public virtual TEntity DbUpdate(TEntity entity)
		{
			_dbContext.Update(entity);
			return entity;
		}

		public virtual IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
		{
			_dbContext.Set<TEntity>().UpdateRange(entities);
			return entities;
		}

		public void DeleteRemove(TEntity entity)
		{
			_dbContext.Set<TEntity>().Remove(entity);
		}
		public virtual async Task<TEntity> Delete(object id)
		{
			var entity = await GetByIdAsync(id, false);
			return entity is not null ? Delete(entity) : null;
		}

		public virtual TEntity Delete(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Deleted;
			return entity;
		}

		public virtual IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
		{
			_dbContext.Set<TEntity>().RemoveRange(entities);
			return entities;
		}

		public virtual void DeleteRange(IEnumerable<TEntity> entities)
		{
			_dbContext.Set<TEntity>().RemoveRange(entities);
		}

		public virtual IQueryable<TEntity> Query()
		{
			return _dbContext.Set<TEntity>();
		}

		public virtual Task<int> Execute(FormattableString interpolatedQueryString)
		{
			return _dbContext.Database.ExecuteSqlInterpolatedAsync(interpolatedQueryString);
		}

		public virtual async Task<bool> AnyAsync(Func<TEntity, bool> search = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
		{
			if (search.IsNull())
				return ignoreQueryFilter ? await _dbContext.Set<TEntity>().AsNoTracking().IgnoreQueryFilters().AnyAsync(cancellationToken) : await _dbContext.Set<TEntity>().AsNoTracking().AnyAsync(cancellationToken);

			return await SafeAnyAsync(_dbContext.Set<TEntity>().AsNoTracking().Where(search).AsQueryable(), ignoreQueryFilter, cancellationToken);
		}

		public virtual TEntity Get(Expression<Func<TEntity, bool>> expression, bool ignoreQueryFilter = false, params Expression<Func<TEntity, object>>[] includes)
		{
			IQueryable<TEntity> query = _dbContext.Set<TEntity>();

			if (ignoreQueryFilter)
			{
				query = query.IgnoreQueryFilters();
			}

			foreach (Expression<Func<TEntity, object>> include in includes)
			{
				query = query.Include(include);
			}
			return query.AsNoTracking().AsQueryable().FirstOrDefault(expression);
		}

		public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, bool ignoreQueryFilter = false, params Expression<Func<TEntity, object>>[] includes)
		{
			IQueryable<TEntity> query = _dbContext.Set<TEntity>();

			if (ignoreQueryFilter)
			{
				query = query.IgnoreQueryFilters();
			}

			foreach (Expression<Func<TEntity, object>> include in includes)
			{
				query = query.Include(include);
			}
			return await query.AsNoTracking().AsQueryable().FirstOrDefaultAsync(expression);
		}

		public virtual async Task<TEntity> GetByIdAsync(object id, bool trackChanges = true, CancellationToken cancellationToken = default, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
		{
			TEntity result;

			if (includes is not null)
			{
				IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsNoTracking();

				var parameter = Expression.Parameter(typeof(TEntity), "x");
				var property = Expression.Property(parameter, "id");
				var equal = Expression.Equal(property, Expression.Constant(id));
				var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);

				query = includes(query);

				result = await query.AsSplitQuery().FirstOrDefaultAsync(lambda, cancellationToken);
			}
			else
			{
				result = await _dbContext.Set<TEntity>().FindAsync(new[] { id }, cancellationToken);
			}

			if (!result.IsNull() && !trackChanges)
			{
				_dbContext.Entry(result).State = EntityState.Detached;
			}

			return result;
		}

		public virtual async Task<TEntity> FirstOrDefaultAsync(Func<TEntity, bool> search = null, CancellationToken cancellationToken = default, bool trackChanges = false, bool ignoreQueryFilter = false)
		{
			if (trackChanges)
			{
				if (search.IsNull())
					return ignoreQueryFilter ?
						await _dbContext.Set<TEntity>().IgnoreQueryFilters().FirstOrDefaultAsync(cancellationToken) :
						await _dbContext.Set<TEntity>().FirstOrDefaultAsync(cancellationToken);

				return await SafeFirstOrDefaultAsync(_dbContext.Set<TEntity>().Where(search).AsQueryable(), ignoreQueryFilter, cancellationToken);
			}
			else
			{
				if (search.IsNull())
					return ignoreQueryFilter ?
						await _dbContext.Set<TEntity>().AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync(cancellationToken) :
						await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(cancellationToken);
				return await SafeFirstOrDefaultAsync(_dbContext.Set<TEntity>().AsNoTracking().Where(search).AsQueryable(), ignoreQueryFilter, cancellationToken);
			}
		}

		public virtual async Task<TEntity> LastOrDefaultAsync(Func<TEntity, bool> search = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
		{
			if (search.IsNull())
				return ignoreQueryFilter ?
					await _dbContext.Set<TEntity>().AsNoTracking().IgnoreQueryFilters().LastOrDefaultAsync(cancellationToken) :
					await _dbContext.Set<TEntity>().AsNoTracking().LastOrDefaultAsync(cancellationToken);

			return await SafeLastOrDefaultAsync(_dbContext.Set<TEntity>().AsNoTracking().Where(search).AsQueryable(), ignoreQueryFilter, cancellationToken);
		}

		public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> search = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, CancellationToken cancellationToken = default, bool trackChanges = false, bool ignoreQueryFilter = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
		{
			TEntity results;
			IQueryable<TEntity> query = _dbContext.Set<TEntity>();

			if (includes is not null)
			{
				query = includes(query);
			}

			if (!trackChanges)
			{
				query = query.AsNoTracking();
			}

			if (ignoreQueryFilter)
			{
				query = query.IgnoreQueryFilters();
			}

			if (!orderBy.IsNull())
			{
				query = orderBy(query);
			}

			if (includes is not null)
			{
				query = query.AsSplitQuery();
			}

			//var sql = query.ToQueryString();
			if (search.IsNull())
			{
				results = await query.FirstOrDefaultAsync(cancellationToken);
			}
			else
			{
				results = await query.FirstOrDefaultAsync(search, cancellationToken);
			}

			return results;
		}

		public virtual async Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> search = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, CancellationToken cancellationToken = default, bool trackChanges = false, bool ignoreQueryFilter = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
		{
			List<TEntity> results;
			IQueryable<TEntity> query = _dbContext.Set<TEntity>();

			if (includes is not null)
			{
				query = includes(query);
			}

			if (!trackChanges)
			{
				query = query.AsNoTracking();
			}

			if (ignoreQueryFilter)
			{
				query = query.IgnoreQueryFilters();
			}

			if (!orderBy.IsNull())
			{
				query = orderBy(query);
			}

			if (includes is not null)
			{
				query = query.AsSplitQuery();
			}

			//var sql = query.ToQueryString();
			if (search.IsNull())
			{
				results = await query.ToListAsync(cancellationToken);
			}
			else
			{
				results = await query.Where(search).ToListAsync(cancellationToken);
			}

			return results;
		}

		/// <summary>
		/// Get paged entity
		/// </summary>
		/// <param name="page"></param>
		/// <param name="size"></param>
		/// <param name="orderBy">Example: FirstName desc, LastName asc, Birthdate desc</param>
		/// <param name="searchQuery"></param>
		/// <param name="ignoreQueryFilter"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int page, int size, string orderBy = "", Func<TEntity, bool> searchQuery = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
		{
			if (searchQuery.IsNull())
				return await GetPagedReponseAsync(page, size, orderBy, ignoreQueryFilter, cancellationToken);
			else
				return await GetFilterPagedReponseAsync(page, size, orderBy, searchQuery, ignoreQueryFilter, cancellationToken);
		}

		public virtual async Task<IReadOnlyList<TEntity>> ListAllAsync(bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
		{
			return ignoreQueryFilter ?
				 await _dbContext.Set<TEntity>().AsNoTracking().IgnoreQueryFilters().ToListAsync(cancellationToken) :
				 await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
		}

		public virtual async Task<int> CountAsync(Func<TEntity, bool> search = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
		{
			if (search.IsNull())
				return ignoreQueryFilter ?
					await _dbContext.Set<TEntity>().AsNoTracking().IgnoreQueryFilters().CountAsync(cancellationToken) :
					await _dbContext.Set<TEntity>().AsNoTracking().CountAsync(cancellationToken);

			return await SafeCountAsync(_dbContext.Set<TEntity>().AsNoTracking().Where(search).AsQueryable(), ignoreQueryFilter, cancellationToken);
		}

		public virtual async Task<long> GetCountAsync(Expression<Func<TEntity, bool>> expression = null, bool ignoreQueryFilter = false)
		{
			var query = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();

			if (ignoreQueryFilter)
			{
				query = query.IgnoreQueryFilters();
			}

			if (expression == null)
			{
				return await _dbContext.Set<TEntity>().LongCountAsync();
			}

			return await query.LongCountAsync(expression);
		}

		public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int skip = 0, int take = 0, bool ignoreQueryFilter = false, params Expression<Func<TEntity, object>>[] includes)
		{
			IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();

			if (ignoreQueryFilter)
			{
				query = query.IgnoreQueryFilters();
			}

			foreach (Expression<Func<TEntity, object>> include in includes)
			{
				query = query.Include(include);
			}

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			if (take != 0)
			{
				query = query.Skip(skip).Take(take);
			}

			return query.ToList();
		}

		public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int skip = 0, int take = 0, bool ignoreQueryFilter = false, params Expression<Func<TEntity, object>>[] includes)
		{
			IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();

			if (ignoreQueryFilter)
			{
				query = query.IgnoreQueryFilters();
			}

			foreach (Expression<Func<TEntity, object>> include in includes)
			{
				query = query.Include(include);
			}

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			if (take != 0)
			{
				query = query.Skip(skip).Take(take);
			}

			return await query.ToListAsync();
		}
		
		public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			int result;
			try
			{
				result = await _dbContext.SaveChangesAsync(cancellationToken);
			}
			catch (DbUpdateException ex)
			{
				Guid logId = Guid.NewGuid();
				_logger.LogError(ex, $"DbUpdateException - ErrorId: {logId} Could not save changes on database for entity: {typeof(TEntity).Name}!", logId);
#if DEBUG
				throw;
#endif
				throw new Exception($"{HttpStatusCode.InternalServerError} Refer to this Error Id: {logId} for further investigation with system administrator.");
			}
			catch (Exception ex)
			{
				Guid logId = Guid.NewGuid();
				_logger.LogError(ex, $"Exception - ErrorId: {logId} Could not save changes on database for entity: {typeof(TEntity).Name}!", logId);
#if DEBUG
				throw;
#endif
				throw new Exception($"{HttpStatusCode.InternalServerError} Refer to this Error Id: {logId} for further investigation with system administrator.");
			}
			if (result < 1)
				_logger.LogError("Changes could not be saved on database! Expand this log message to include further details...");

			return result;
		}

        #region Private Methods

		protected async Task<int> SafeCountAsync(IQueryable<TEntity> source, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
		{
			if (source.IsNull())
				throw new ArgumentNullException(typeof(TEntity).Name);
			if (ignoreQueryFilter)
				source = source.IgnoreQueryFilters();
			if (source is not IAsyncEnumerable<TEntity>)
				return await Task.FromResult(source.Count());
			return await source.CountAsync(cancellationToken);
		}

		protected async Task<List<TEntity>> SafeListAsync(IQueryable<TEntity> source, CancellationToken cancellationToken = default)
		{
			if (source.IsNull())
				throw new ArgumentNullException(typeof(TEntity).Name);
			if (source is not IAsyncEnumerable<TEntity>)
				return await Task.FromResult(source.ToList());
			return await source.ToListAsync(cancellationToken);
		}

		protected async Task<bool> SafeAnyAsync(IQueryable<TEntity> source, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
		{
			if (source.IsNull())
				throw new ArgumentNullException(typeof(TEntity).Name);
			if (ignoreQueryFilter)
				source = source.IgnoreQueryFilters();
			if (source is not IAsyncEnumerable<TEntity>)
				return await Task.FromResult(source.Any());
			return await source.AnyAsync(cancellationToken);
		}

		protected async Task<TEntity> SafeFirstOrDefaultAsync(IQueryable<TEntity> source, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
		{
			if (source.IsNull())
				throw new ArgumentNullException(typeof(TEntity).Name);
			if (ignoreQueryFilter)
				source = source.IgnoreQueryFilters();
			if (source is not IAsyncEnumerable<TEntity>)
				return await Task.FromResult(source.FirstOrDefault());
			return await source.FirstOrDefaultAsync(cancellationToken);
		}

		protected async Task<TEntity> SafeLastOrDefaultAsync(IQueryable<TEntity> source, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
		{
			if (source.IsNull())
				throw new ArgumentNullException(typeof(TEntity).Name);
			if (ignoreQueryFilter)
				source = source.IgnoreQueryFilters();
			if (source is not IAsyncEnumerable<TEntity>)
				return await Task.FromResult(source.LastOrDefault());
			return await source.LastOrDefaultAsync(cancellationToken);
		}

		protected async Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int page, int size, string orderBy, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
		{
			if (orderBy.IsNullOrWhiteSpace()) //&& ObjectExtensions.IsOrderByValid<TEntity>(orderBy, ",")
				return ignoreQueryFilter ?
					await _dbContext.Set<TEntity>().AsNoTracking().IgnoreQueryFilters().OrderBy(orderBy).Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken) :
					await _dbContext.Set<TEntity>().AsNoTracking().OrderBy(orderBy).Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);
			return ignoreQueryFilter ?
				await _dbContext.Set<TEntity>().IgnoreQueryFilters().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync(cancellationToken) :
				await _dbContext.Set<TEntity>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync(cancellationToken);
		}

		protected async Task<IReadOnlyList<TEntity>> GetFilterPagedReponseAsync(int page, int size, string orderBy, Func<TEntity, bool> searchQuery, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
		{
			IQueryable<TEntity> entityQuery = _dbContext.Set<TEntity>().AsNoTracking().Where(searchQuery).AsQueryable();
			if (ignoreQueryFilter)
			{
				entityQuery = entityQuery.IgnoreQueryFilters();
			}

			if (orderBy.IsNullOrWhiteSpace())//&& ObjectExtensions.IsOrderByValid<TEntity>(orderBy, ",")
				entityQuery = entityQuery.OrderBy(orderBy).Skip((page - 1) * size).Take(size);
			else
				entityQuery = entityQuery.Skip((page - 1) * size).Take(size);
			return await SafeListAsync(entityQuery, cancellationToken);
		}
		#endregion
	}
}