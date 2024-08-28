using APITemplate.DataAccess.Abstract;
using APITemplate.DataAccess.Concrete.DataManagement;
using APITemplate.Entity.Poco;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.DataAccess.Concrete
{
	public class EfUserRepository : Repository<User>, IUserRepository
	{
		public EfUserRepository(DbContext dbContext) : base(dbContext)
		{
		}
	}
}
