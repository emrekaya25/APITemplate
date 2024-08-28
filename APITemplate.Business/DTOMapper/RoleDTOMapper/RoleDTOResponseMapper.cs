using APITemplate.Entity.DTO.RoleDTO;
using APITemplate.Entity.Poco;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.DTOMapper.RoleDTOMapper
{
	public class RoleDTOResponseMapper:Profile
	{
        public RoleDTOResponseMapper()
        {
            CreateMap<Role, RoleDTOResponse>()
            .ForMember(dest => dest.UserRoles, opt =>
            {
                opt.MapFrom(src => src.UserRoles);
            }).ReverseMap();
        }
    }
}
