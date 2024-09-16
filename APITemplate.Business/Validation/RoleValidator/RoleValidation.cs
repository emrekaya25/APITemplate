using APITemplate.Entity.DTO.RoleDTO;
using APITemplate.Entity.Poco;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.Validation.RoleValidator
{
	public class RoleValidation:AbstractValidator<RoleDTORequest>
	{
        public RoleValidation()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Rol ismi boş olamaz !");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Rol ismi iki karakterden büyük olmalıdır !");
        }
    }
}
