using APITemplate.Entity.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Entity.DTO.UserRoleDTO
{
	public class UserRoleDTORequest
	{
        public int Id { get; set; }
		public Guid Guid { get; set; }
		public int UserId { get; set; }
		public int RoleId { get; set; }
	}
}
