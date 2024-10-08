﻿using APITemplate.Business.Abstract;
using APITemplate.Business.Validation.UserValidator;
using APITemplate.DataAccess.Abstract.DataManagement;
using APITemplate.Entity.DTO.LoginDTO;
using APITemplate.Entity.DTO.UserDTO;
using APITemplate.Entity.Poco;
using APITemplate.Tools.Utilities.Attributes;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.Concrete
{
	public class UserManager : IUserService
	{
		private readonly Lazy<IUnitOfWork> _uow;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		public UserManager(Lazy<IUnitOfWork> uow, IMapper mapper, IConfiguration configuration)
		{
			_uow = uow;
			_mapper = mapper;
			_configuration = configuration;
		}
		

		public async Task<UserDTOResponse> AddAsync(UserDTORequest entity)
		{
			var user = _mapper.Map<User>(entity);
            // Name'in ilk harfini büyük yapma
            user.Name = char.ToUpper(user.Name[0], CultureInfo.CurrentCulture) + user.Name.Substring(1).ToLower();
            // LastName'i Türkçe kültüre göre büyük harfe çevirme
            user.LastName = user.LastName.ToUpper(new CultureInfo("tr-TR", false));

			//şifreyi hashleme
			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _uow.Value.UserRepository.AddAsync(user);
			await _uow.Value.SaveChangesAsync();

			//default role ekleme
			//UserRole userRole = new UserRole();
			//userRole.UserId = user.Id;
			//userRole.RoleId = 2;
			//await _uow.UserRoleRepository.AddAsync(userRole);
			//await _uow.SaveChangesAsync();

			var userResponse = _mapper.Map<UserDTOResponse>(user);
			return userResponse;
		}

		public async Task<UserDTOResponse> DeleteAsync(UserDTORequest entity)
		{
			// user'a bağlı rolleri silme
			var userRoles = await _uow.Value.UserRoleRepository.GetAllAsync(x=>x.UserId == entity.Id);
			if (userRoles != null)
			{
				foreach (var userRole in userRoles)
				{
					await _uow.Value.UserRoleRepository.DeleteAsync(userRole);
				}
				await _uow.Value.SaveChangesAsync();
			}
			var user = _mapper.Map<User>(entity);
			await _uow.Value.UserRepository.DeleteAsync(user);
			await _uow.Value.SaveChangesAsync();

			var userResponse = _mapper.Map<UserDTOResponse>(user);
			return userResponse;
		}

		public async Task<List<UserDTOResponse>> GetAllAsync(UserDTORequest? entity)
		{
			//filtreleme

			var users = await _uow.Value.UserRepository.GetAllAsync(x => true, "UserRoles.Role");
			List<UserDTOResponse> userDTOResponses = new();

			if (!entity.Name.Contains("string"))
			{
				users = users.Where(x=>x.Name == entity.Name);
			}
			if(!entity.LastName.Contains("string"))
			{
				users = users.Where(x => x.LastName == entity.LastName);
			}
			if (!entity.Email.Contains("string"))
			{
				users = users.Where(x => x.Email == entity.Email);
			}
			userDTOResponses = users.Select(x => _mapper.Map<UserDTOResponse>(x)).ToList();

			return userDTOResponses;
		}

		public async Task<UserDTOResponse> GetAsync(UserDTORequest entity)
		{
			var user = await _uow.Value.UserRepository.GetAsync(x=>x.Id == entity.Id, "UserRoles.Role");
			if (user == null)
			{
				user = await _uow.Value.UserRepository.GetAsync(x=>x.Guid == entity.Guid,"UserRoles.Role");
			}
			var userResponse = _mapper.Map<UserDTOResponse>(user);
			return userResponse;
		}

		public async Task<UserDTOResponse> UpdateAsync(UserDTORequest entity)
		{
			var user = await _uow.Value.UserRepository.GetAsync(x => x.Id == entity.Id,"UserRoles.Role");
			entity.Password = user.Password;
			if (entity.Image == null) //güncellemede fotoğraf eklememişse eski fotoğraf eklenir.
			{
				entity.Image = user.Image;
			}

			if (entity.UserRoles.Count > 0) // güncellemede önceki tüm userRole bilgilerini siliyorum.
			{
				var userRoles = await _uow.Value.UserRoleRepository.GetAllAsync(x=>x.UserId == entity.Id);
				if (userRoles != null)
				{
                    foreach (var userRole in userRoles)
                    {
                        await _uow.Value.UserRoleRepository.DeleteAsync(userRole);
                    }
                    await _uow.Value.SaveChangesAsync();
				}
			}

			user = _mapper.Map(entity,user);

			await _uow.Value.UserRepository.UpdateAsync(user);
			await _uow.Value.SaveChangesAsync();

			var userResponse = _mapper.Map<UserDTOResponse>(user);
			return userResponse;
		}


		public async Task<UserDTOResponse> ResetPassword(UserDTOResetPassword entity)
		{
			var user = await _uow.Value.UserRepository.GetAsync(x=>x.Id == entity.Id);

			if (BCrypt.Net.BCrypt.Verify(entity.ConfirmPassword,user.Password))
			{
				user.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);

				await _uow.Value.UserRepository.UpdateAsync(user);
				await _uow.Value.SaveChangesAsync();
			}

			var userResponse = _mapper.Map<UserDTOResponse>(user);
			return userResponse;
		}

		//Login
		public async Task<LoginDTOResponse> LoginAsync(LoginDTORequest loginDTORequest)
		{
			var user = await _uow.Value.UserRepository.GetAsync(x => x.Email == loginDTORequest.Email, "UserRoles.Role");

			if (BCrypt.Net.BCrypt.Verify(loginDTORequest.Password,user.Password))
			{
                var userResponse = _mapper.Map<LoginDTOResponse>(user);

                List<Claim> claims = new List<Claim>()
				{
					new Claim(ClaimTypes.Name,userResponse.Name),
					new Claim(ClaimTypes.Email,userResponse.Email),
					new Claim("UserId",userResponse.Id.ToString()),
				};

				foreach (var role in userResponse.Roles)
				{
					claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
				}

				var secretKey = _configuration["JWT:Token"];
				var issuer = _configuration["JWT:Issuer"];
				var audiance = _configuration["JWT:Audiance"];

				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.UTF8.GetBytes(secretKey);
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Issuer = issuer,
					Audience = audiance,
					Subject = new ClaimsIdentity(claims),
					Expires = DateTime.Now.AddDays(1),
					NotBefore = DateTime.Now,
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
				};

				var token = tokenHandler.CreateToken(tokenDescriptor);
				userResponse.Token = tokenHandler.WriteToken(token);

                return userResponse;
            }

			else
			{
				return null;
			}

		}
    }
}
