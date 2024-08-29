using APITemplate.Entity.DTO.RoleDTO;
using APITemplate.Entity.DTO.UserRoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Entity.DTO.LoginDTO
{
	public class LoginDTOResponse
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public List<UserRoleDTOResponse> Roles { get; set; }
		public string Token { get; set; }
	}
}
