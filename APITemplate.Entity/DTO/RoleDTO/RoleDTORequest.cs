using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Entity.DTO.RoleDTO
{
	public class RoleDTORequest
	{
        public int Id { get; set; }
		public Guid Guid { get; set; }
		public string Name { get; set; }
	}
}
