using APITemplate.Entity.DTO.UserRoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Entity.DTO.RoleDTO
{
	public class RoleDTOResponse
	{
		public int Id { get; set; }
		public Guid Guid { get; set; }
		public string Name { get; set; }
		public List<UserRoleDTOResponse> UserRoles { get; set; }
	}
}
