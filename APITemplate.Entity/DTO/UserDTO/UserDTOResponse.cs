using APITemplate.Entity.DTO.UserRoleDTO;
using APITemplate.Entity.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Entity.DTO.UserDTO
{
	public class UserDTOResponse
	{
		public int Id { get; set; }
		public Guid Guid { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string? Image { get; set; }
		public string Password { get; set; }
		public List<UserRoleDTOResponse> UserRoles { get; set; }
	}
}
