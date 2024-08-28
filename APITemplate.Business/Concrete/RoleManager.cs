using APITemplate.Business.Abstract;
using APITemplate.DataAccess.Abstract.DataManagement;
using APITemplate.Entity.DTO.RoleDTO;
using APITemplate.Entity.Poco;
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

		public async Task DeleteAsync(RoleDTORequest entity)
		{
			var role = _mapper.Map<Role>(entity);
			await _uow.RoleRepository.DeleteAsync(role);
			await _uow.SaveChangesAsync();
		}

		public async Task<List<RoleDTOResponse>> GetAllAsync(RoleDTORequest entity)
		{
			var roles = await _uow.RoleRepository.GetAllAsync(x=>true);

			List<RoleDTOResponse> roleDTOResponses = new();
			foreach (var role in roles)
			{
				roleDTOResponses.Add(_mapper.Map<RoleDTOResponse>(role));
			}
			return roleDTOResponses;
		}

		public async Task<RoleDTOResponse> GetAsync(RoleDTORequest entity)
		{
			var role = await _uow.RoleRepository.GetAsync(x=>x.Id == entity.Id);
			var roleResponse = _mapper.Map<RoleDTOResponse>(role);
			return roleResponse;
		}

		public async Task UpdateAsync(RoleDTORequest entity)
		{
			var role = await _uow.RoleRepository.GetAsync(x=>x.Id == entity.Id);
			role = _mapper.Map(entity,role);
			await _uow.RoleRepository.UpdateAsync(role);
			await _uow.SaveChangesAsync();
		}
	}
}
