using APITemplate.Business.Abstract;
using APITemplate.Business.Validation.RoleValidator;
using APITemplate.DataAccess.Abstract.DataManagement;
using APITemplate.Entity.DTO.RoleDTO;
using APITemplate.Entity.Poco;
using APITemplate.Tools.Utilities.Attributes;
using APITemplate.Tools.Utilities.Logging;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.Business.Concrete
{
	public class RoleManager : IRoleService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public RoleManager(IMapper mapper, IUnitOfWork uow)
		{
			_mapper = mapper;
			_uow = uow;
		}
		public async Task<RoleDTOResponse> AddAsync(RoleDTORequest entity)
		{
			var role = _mapper.Map<Role>(entity);
			await _uow.RoleRepository.AddAsync(role);
			await _uow.SaveChangesAsync();

			var roleDTOResponse = _mapper.Map<RoleDTOResponse>(role);
			return roleDTOResponse;
		}

		public async Task<RoleDTOResponse> DeleteAsync(RoleDTORequest entity)
		{
			var role = _mapper.Map<Role>(entity);
			await _uow.RoleRepository.DeleteAsync(role);
			await _uow.SaveChangesAsync();

			var roleResponse = _mapper.Map<RoleDTOResponse>(role);
			return roleResponse;
		}

		public async Task<List<RoleDTOResponse>> GetAllAsync(RoleDTORequest entity)
		{
			var roles = await _uow.RoleRepository.GetAllAsync(x => true,"UserRoles.User");
			List<RoleDTOResponse> roleDTOResponses = new();

			if (!entity.Name.Contains("string"))
			{
				roles = roles.Where(x => x.Name == entity.Name);
			}
			roleDTOResponses = roles.Select(x => _mapper.Map<RoleDTOResponse>(x)).ToList();

			return roleDTOResponses;
		}

		public async Task<RoleDTOResponse> GetAsync(RoleDTORequest entity)
		{
			var role = await _uow.RoleRepository.GetAsync(x => x.Id == entity.Id,"UserRoles.User");
			if (role == null)
			{
				role = await _uow.RoleRepository.GetAsync(x=>x.Guid == entity.Guid,"UserRoles.User");
			}
			var roleResponse = _mapper.Map<RoleDTOResponse>(role);
			return roleResponse;
		}

		public async Task<RoleDTOResponse> UpdateAsync(RoleDTORequest entity)
		{
			var role = await _uow.RoleRepository.GetAsync(x => x.Id == entity.Id);
			role = _mapper.Map(entity, role);
			await _uow.RoleRepository.UpdateAsync(role);
			await _uow.SaveChangesAsync();

			var roleResponse = _mapper.Map<RoleDTOResponse>(role);
			return roleResponse;
		}
	}
}
