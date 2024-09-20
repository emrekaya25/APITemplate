using APITemplate.DataAccess.Abstract;
using APITemplate.DataAccess.Abstract.DataManagement;
using APITemplate.DataAccess.Concrete.Context;
using APITemplate.Entity.Base;
using Microsoft.AspNetCore.Http;
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
		private readonly IHttpContextAccessor _accessor;

		public UnitOfWork(APITemplateContext context, IHttpContextAccessor accessor)
		{
			_context = context;
			UserRepository = new EfUserRepository(_context);
			RoleRepository = new EfRoleRepository(_context);
			UserRoleRepository = new EfUserRoleRepository(_context);
			_accessor = accessor;
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
					item.Entity.UpdatedTime = DateTime.Now;
					item.Entity.Guid = Guid.NewGuid();
					item.Entity.AddedIPV4Address = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
					item.Entity.UpdatedIPV4Address = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

					if (item.Entity.IsActive == null)
					{
						item.Entity.IsActive = true;
					}
				}

				else if (item.State == EntityState.Modified)
				{
					item.Entity.UpdatedTime = DateTime.Now;
					item.Entity.UpdatedIPV4Address = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
				}
			}

			return _context.SaveChangesAsync();
		}
	}
}
