using APITemplate.Entity.DTO.UserRoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.Abstract
{
	public interface IUserRoleService:IGenericService<UserRoleDTORequest, UserRoleDTOResponse>
	{
	}
}
