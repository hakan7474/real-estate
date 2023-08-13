using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstate.Core.GenericRepositories;

namespace RealEstate.Core.GenericUnitOfWork;

public sealed class UnitOfWork<TContext> : IUnitOfWork, IDisposable
	where TContext : DbContext
	{
		#region fields

		private readonly TContext _dbContext;
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<UnitOfWork<TContext>> _logger;

		private readonly Dictionary<Type, object> _genericRepositories;

		private bool _disposed;

		#endregion

		public UnitOfWork(TContext dbContext,
			ILoggerFactory loggerFactory,
			ILogger<UnitOfWork<TContext>> logger)
		{
			_dbContext = dbContext;
			_loggerFactory = loggerFactory;
			_logger = logger;
			_genericRepositories = new Dictionary<Type, object>();
		}

		public int SaveChanges()
		{
			return _dbContext.SaveChanges();
		}

		public async Task<int> SaveChangesAsync()
		{
            return await _dbContext.SaveChangesAsync();
		}

		#region Repository Operations
		/// <summary>
		/// Gets the specified custom repository, which should be from type <see cref="IGenericRepository"/>
		/// </summary>
		/// <typeparam name="T">The interface of the custom repository/></typeparam>
		/// <returns></returns>
		public T Repository<T>() where T : IBaseRepository
		{
			if (!typeof(T).IsInterface)
			{
				throw new ArgumentException("Generic type should be an interface.");
			}

			T repository;
			var type = typeof(T);

			if (_genericRepositories.ContainsKey(type))
			{
				repository = (T)_genericRepositories[type];
			}
			else
			{
				var repositoryType = AppDomain.CurrentDomain.GetAssemblies()
					.SelectMany(x => x.GetTypes()).FirstOrDefault(x => typeof(T).IsAssignableFrom(x) && !x.IsAbstract);

				if (repositoryType == null)
				{
					throw new NotImplementedException($"'{nameof(T)}' repository not found!");
				}

				var repositoryConstructor = repositoryType.GetConstructors().First(constructor => constructor.IsPublic);
				var loggerGenericArgumentType = repositoryConstructor.GetParameters().First(parameter => parameter.ParameterType.IsAssignableTo(typeof(ILogger)))
					.ParameterType.GenericTypeArguments.First();

				Type loggerGenericType = (typeof(Logger<>)).MakeGenericType(loggerGenericArgumentType);

				ILogger logger = (ILogger)Activator.CreateInstance(loggerGenericType, _loggerFactory);

				repository = (T)repositoryConstructor.Invoke(new object[] { _dbContext, logger });

				_genericRepositories.Add(type, repository);
			}

			return repository;
		}
		#endregion

		#region Transaction Operations.

		/// <summary>
		/// Creates a new transaction to which all retrived repostiores from UOW will write, Will throw an exception if there is an already created transaction.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <exception cref="InvalidOperationException()"/>
		/// <returns></returns>
		public async Task BeginWritingAsync(CancellationToken cancellationToken)
		{
			if (_dbContext.Database.CurrentTransaction is null)
			{
				await _dbContext.Database.BeginTransactionAsync(cancellationToken);
			}
			else
			{
				throw new InvalidOperationException("There is open writing session already, please discard it beffore starting new session.");
			}
		}
		
		public bool HasTransaction()
		{
			return _dbContext.Database.CurrentTransaction != null;
		}

		/// <summary>
		/// Saves all changes made to the current transaction and writes them to the database with clearing the current transaction, Will throw an exception if there is an already created transaction.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <exception cref="InvalidOperationException()"/>
		/// <returns>A <see cref="Task"/> that represents the asynchronous save operation.</returns>
		public async Task CommitWritingsAsync(CancellationToken cancellationToken)
		{
			await using var transaction = _dbContext.Database.CurrentTransaction;

			if (transaction is null)
			{
				throw new InvalidOperationException("Can't save changes when the transaction isn't started first");
			}

			try
			{
				await transaction.CommitAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "UOW: Exception happend while saving transaction {0}, rolling back everything.", transaction.TransactionId);
				await transaction.RollbackAsync(cancellationToken);
				throw;
			}
		}

		/// <summary>
		/// Discards all the changes in the current transaction and allows for new transaction creation, Will throw an exception if there isn't a created transaction.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <exception cref="InvalidOperationException()"/>
		/// <returns></returns>
		public async Task DiscardWritingsAsync(CancellationToken cancellationToken)
		{
			if (_dbContext.Database.CurrentTransaction is not null)
			{
				await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
			}
		}
		#endregion;

		#region Disposables
		public void Dispose()
		{
			if (!_disposed)
			{
				_genericRepositories.Clear();
				_dbContext.Dispose();
				_disposed = true;
			}

			GC.SuppressFinalize(this);
		}

		public async ValueTask DisposeAsync()
		{
			if (!_disposed)
			{
				_genericRepositories.Clear();
				await _dbContext.DisposeAsync().ConfigureAwait(false);
				_disposed = true;
			}

			GC.SuppressFinalize(this);
		}

		~UnitOfWork()
		{
			Dispose();
		}
		#endregion
	}

