using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Entity.DTO.UserRoleDTO
{
	public class UserRoleDTOResponse
	{
		public int Id { get; set; }
		public Guid Guid { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
		public int RoleId { get; set; }
		public string RoleName { get; set; }
	}
}
