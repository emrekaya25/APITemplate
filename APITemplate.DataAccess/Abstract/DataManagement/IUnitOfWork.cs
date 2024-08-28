using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.DataAccess.Abstract.DataManagement
{
	public interface IUnitOfWork
	{
		public IUserRepository UserRepository { get; }
		public IRoleRepository RoleRepository { get; }
		public IUserRoleRepository UserRoleRepository { get; }
		Task<int> SaveChangesAsync();
	}
}
