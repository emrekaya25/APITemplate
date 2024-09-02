using APITemplate.Entity.DTO.UserRoleDTO;
using APITemplate.Entity.Poco;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.Validation.UserRoleValidator
{
	public class UserRoleValidation:AbstractValidator<UserRoleDTORequest>
	{
        public UserRoleValidation()
        {
            RuleFor(x=>x.UserId).NotEmpty().WithMessage("Kullanıcı bilgisi girilmeli !");
            RuleFor(x=>x.RoleId).NotEmpty().WithMessage("Rol bilgisi girilmeli !");
        }
    }
}
