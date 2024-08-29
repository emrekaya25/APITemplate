using APITemplate.Entity.DTO.LoginDTO;
using APITemplate.Entity.Poco;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.DTOMapper.LoginDTOMapper
{
	public class LoginDTOResponseMapper:Profile
	{
        public LoginDTOResponseMapper()
        {
            CreateMap<User, LoginDTOResponse>().
                ForMember(dest => dest.Roles, opt =>
                {
                    opt.MapFrom(src=>src.UserRoles);
                }).ReverseMap();
        }
    }
}
