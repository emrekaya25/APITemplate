using APITemplate.Entity.DTO.UserDTO;
using APITemplate.Entity.Poco;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.Validation.UserValidator
{
	public class UserValidation:AbstractValidator<UserDTORequest>
	{
        public UserValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim boş olamaz !");
            RuleFor(x=>x.Name).MinimumLength(2).WithMessage("İsim iki karakterden fazla olmalıdır !");
            RuleFor(x=>x.Email).NotEmpty().WithMessage("Email boş olamaz !");
            RuleFor(x=>x.Password).NotEmpty().WithMessage("Sifre boş olamaz !");
        }
    }
}
