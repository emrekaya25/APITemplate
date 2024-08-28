using APITemplate.Entity.DTO.UserRoleDTO;
using APITemplate.Entity.Poco;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.DTOMapper.UserRoleDTOMapper
{
	public class UserRoleDTORequestMapper:Profile
	{
        public UserRoleDTORequestMapper()
        {
            CreateMap<UserRole, UserRoleDTORequest>();
            CreateMap<UserRoleDTORequest, UserRole>();
        }
    }
}
