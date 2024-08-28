using APITemplate.Business.Abstract;
using APITemplate.DataAccess.Abstract.DataManagement;
using APITemplate.Entity.DTO.LoginDTO;
using APITemplate.Entity.DTO.UserDTO;
using APITemplate.Entity.Poco;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.Concrete
{
	public class UserManager : IUserService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		public UserManager(IUnitOfWork uow, IMapper mapper)
		{
			_uow = uow;
			_mapper = mapper;
		}

		public async Task<UserDTOResponse> AddAsync(UserDTORequest entity)
		{
			var user = _mapper.Map<User>(entity);
			await _uow.UserRepository.AddAsync(user);
			await _uow.SaveChangesAsync();

			//default role ekleme
			UserRole userRole = new UserRole();
			userRole.UserId = user.Id;
			userRole.RoleId = 2;
			await _uow.UserRoleRepository.AddAsync(userRole);
			await _uow.SaveChangesAsync();

			var userResponse = _mapper.Map<UserDTOResponse>(user);
			return userResponse;
		}

		public async Task DeleteAsync(UserDTORequest entity)
		{
			var userRoles = await _uow.UserRoleRepository.GetAllAsync(x=>x.UserId == entity.Id);
			if (userRoles != null)
			{
				foreach (var userRole in userRoles)
				{
					await _uow.UserRoleRepository.DeleteAsync(userRole);
				}
				await _uow.SaveChangesAsync();
			}
			var user = _mapper.Map<User>(entity);
			await _uow.UserRepository.DeleteAsync(user);
			await _uow.SaveChangesAsync();
		}

		public async Task<List<UserDTOResponse>> GetAllAsync(UserDTORequest entity)
		{
			var users = await _uow.UserRepository.GetAllAsync(x=>true,"UserRoles.Role");
			List<UserDTOResponse> userDTOResponses = new();
			foreach (var user in users)
			{
				userDTOResponses.Add(_mapper.Map<UserDTOResponse>(user));
			}
			return userDTOResponses;
		}

		public async Task<UserDTOResponse> GetAsync(UserDTORequest entity)
		{
			var user = await _uow.UserRepository.GetAsync(x=>x.Id == entity.Id, "UserRoles.Role");
			var userResponse = _mapper.Map<UserDTOResponse>(user);
			return userResponse;
		}

		public async Task<LoginDTOResponse> LoginAsync(LoginDTORequest loginDTORequest)
		{
			var user = await _uow.UserRepository.GetAsync(x=>x.Email == loginDTORequest.Email && x.Password == loginDTORequest.Password);
			if (user != null)
			{
				var userResponse = _mapper.Map<LoginDTOResponse>(user);
				return userResponse;
			}
			else
			{
				throw new Exception("Giriş bilgileri yanlış!");
			}
		}

		public async Task UpdateAsync(UserDTORequest entity)
		{
			var user = await _uow.UserRepository.GetAsync(x=>x.Id == entity.Id);
			user = _mapper.Map(entity,user);

			await _uow.UserRepository.UpdateAsync(user);
			await _uow.SaveChangesAsync();
		}
	}
}
