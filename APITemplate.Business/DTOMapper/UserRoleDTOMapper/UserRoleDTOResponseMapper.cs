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
	public class UserRoleDTOResponseMapper:Profile
	{
        public UserRoleDTOResponseMapper()
        {
            CreateMap<UserRole, UserRoleDTOResponse>().
                ForMember(dest => dest.UserName, opt =>
                {
                    opt.MapFrom(src=>src.User.Name);
                }).
                ForMember(dest => dest.RoleName, opt =>
                {
                    opt.MapFrom(src=>src.Role.Name);
                }).ReverseMap();
        }
    }
}
