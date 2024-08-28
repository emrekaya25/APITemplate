using APITemplate.Entity.DTO.UserDTO;
using APITemplate.Entity.Poco;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.DTOMapper.UserDTOMapper
{
	public class UserDTOResponseMapper:Profile
	{
        public UserDTOResponseMapper()
        {
            CreateMap<User, UserDTOResponse>().
                ForMember(dest => dest.UserRoles, opt =>
                {
                    opt.MapFrom(src=>src.UserRoles);
                });
        }
    }
}
