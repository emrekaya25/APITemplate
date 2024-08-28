using APITemplate.Entity.DTO.LoginDTO;
using APITemplate.Entity.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.Abstract
{
	public interface IUserService:IGenericService<UserDTORequest,UserDTOResponse>
	{
		public Task<LoginDTOResponse> LoginAsync(LoginDTORequest loginDTORequest);
	}
}
