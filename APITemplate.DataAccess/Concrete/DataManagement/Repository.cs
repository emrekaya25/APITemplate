using APITemplate.DataAccess.Abstract.DataManagement;
using APITemplate.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.DataAccess.Concrete.DataManagement
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly DbContext _dbContext;
		private readonly DbSet<T> _dbSet;

		public Repository(DbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<T>();
		}

		public async Task<EntityEntry<T>> AddAsync(T entity)
		{
			return await _dbSet.AddAsync(entity);
		}

		public async Task DeleteAsync(T entity)
		{
			await Task.Run( () => _dbSet.Remove(entity));
		}

		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> Filter = null, params string[] IncludeProperties)
		{
			IQueryable<T> query = _dbSet;

			if (Filter != null)
			{
				query = query.Where(Filter);
			}
			if (IncludeProperties.Length > 0 )
			{
				foreach (string includeProperty in IncludeProperties)
				{
					query = query.Include(includeProperty);
				}
			}

			return await Task.Run( () => query );
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> Filter, params string[] IncludeProperties)
		{
			IQueryable<T> query = _dbSet;

			if(IncludeProperties.Length > 0)
			{
				foreach (string includeProperty in IncludeProperties)
				{
					query = query.Include(includeProperty);
				}
			}

			return await query.SingleOrDefaultAsync(Filter);
		}

		public async Task UpdateAsync(T entity)
		{
			await Task.Run(()=>_dbContext.Update(entity));
		}
	}
}
