﻿using APITemplate.Entity.DTO.RoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.Abstract
{
	public interface IRoleService:IGenericService<RoleDTORequest, RoleDTOResponse>
	{
	}
}
