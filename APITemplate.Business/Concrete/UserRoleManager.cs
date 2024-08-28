using APITemplate.Business.Abstract;
using APITemplate.DataAccess.Abstract.DataManagement;
using APITemplate.Entity.DTO.UserRoleDTO;
using APITemplate.Entity.Poco;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.Concrete
{
	public class UserRoleManager : IUserRoleService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public UserRoleManager(IMapper mapper, IUnitOfWork uow)
		{
			_mapper = mapper;
			_uow = uow;
		}

		public async Task<UserRoleDTOResponse> AddAsync(UserRoleDTORequest entity)
		{
			var userRole = _mapper.Map<UserRole>(entity);
			await _uow.UserRoleRepository.AddAsync(userRole);
			await _uow.SaveChangesAsync();

			var userRoleResponse = _mapper.Map<UserRoleDTOResponse>(userRole);
			return userRoleResponse;
		}

		public async Task DeleteAsync(UserRoleDTORequest entity)
		{
			var userRole = _mapper.Map<UserRole>(entity);
			await _uow.UserRoleRepository.DeleteAsync(userRole);
			await _uow.SaveChangesAsync();
		}

		public async Task<List<UserRoleDTOResponse>> GetAllAsync(UserRoleDTORequest entity)
		{
			var userRoles = await _uow.UserRoleRepository.GetAllAsync(x=>true);
			List<UserRoleDTOResponse> userRoleDTOResponses = new();
			foreach (var userRole in userRoles)
			{
				userRoleDTOResponses.Add(_mapper.Map<UserRoleDTOResponse>(userRole));
			}
			return userRoleDTOResponses;
		}

		public async Task<UserRoleDTOResponse> GetAsync(UserRoleDTORequest entity)
		{
			var userRole = await _uow.UserRoleRepository.GetAsync(x=>x.Id == entity.Id);
			var userRoleResponse = _mapper.Map<UserRoleDTOResponse>(userRole);
			return userRoleResponse;
		}

		public async Task UpdateAsync(UserRoleDTORequest entity)
		{
			var userRole = await _uow.UserRoleRepository.GetAsync(x=>x.Id == entity.Id);
			userRole = _mapper.Map(entity,userRole);

			await _uow.UserRoleRepository.UpdateAsync(userRole);
			await _uow.SaveChangesAsync();
		}
	}
}
