using APITemplate.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Entity.Poco
{
	public class User:BaseEntity
	{
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Image {  get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
