using APITemplate.DataAccess.Abstract;
using APITemplate.DataAccess.Abstract.DataManagement;
using APITemplate.DataAccess.Concrete.Context;
using APITemplate.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.DataAccess.Concrete.DataManagement
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly APITemplateContext _context;

		public UnitOfWork(APITemplateContext context)
		{
			_context = context;
			UserRepository = new EfUserRepository(_context);
			RoleRepository = new EfRoleRepository(_context);
			UserRoleRepository = new EfUserRoleRepository(_context);
		}

		public IUserRepository UserRepository { get; }
		public IRoleRepository RoleRepository { get; }
		public IUserRoleRepository UserRoleRepository { get; }

		public Task<int> SaveChangesAsync()
		{
			foreach (var item in _context.ChangeTracker.Entries<BaseEntity>())
			{
				if (item.State == EntityState.Added)
				{
					item.Entity.AddedTime = DateTime.Now;

					if (item.Entity.IsActive == false)
					{
						item.Entity.IsActive = true;
					}
				}

				else if (item.State == EntityState.Modified)
				{
					item.Entity.UpdatedTime = DateTime.Now;
				}
			}

			return _context.SaveChangesAsync();
		}
	}
}
