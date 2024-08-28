using APITemplate.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Entity.Poco
{
	public class Role:BaseEntity
	{
        public string Name { get; set; }
		public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
