using APITemplate.Business.Abstract;
using APITemplate.Entity.DTO.RoleDTO;
using APITemplate.Tools.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITemplate.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;

		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		[HttpPost("/api/AddRole")]
		public async Task<IActionResult> AddRole(RoleDTORequest roleDTORequest)
		{
			var role = await _roleService.AddAsync(roleDTORequest);
			return Ok(ApiResponse<RoleDTOResponse>.SuccesWithData(role));
		}

		[HttpPost("/api/DeleteRole")]
		public async Task<IActionResult> DeleteRole(RoleDTORequest roleDTORequest)
		{
			await _roleService.DeleteAsync(roleDTORequest);
			return Ok(ApiResponse<RoleDTOResponse>.SuccesWithOutData());
		}

		[HttpPost("/api/UpdateRole")]
		public async Task<IActionResult> UpdateRole(RoleDTORequest roleDTORequest)
		{
			await _roleService.UpdateAsync(roleDTORequest);
			return Ok(ApiResponse<RoleDTOResponse>.SuccesWithOutData());
		}

		[HttpPost("/api/GetRole")]
		public async Task<IActionResult> GetRole(RoleDTORequest roleDTORequest)
		{
			var role = await _roleService.GetAsync(roleDTORequest);
			return Ok(ApiResponse<RoleDTOResponse>.SuccesWithData(role));
		}

		[HttpPost("/api/GetAllRoles")]
		public async Task<IActionResult> GetAllRoles(RoleDTORequest roleDTORequest)
		{
			var roles = await _roleService.GetAllAsync(roleDTORequest);
			return Ok(ApiResponse<List<RoleDTOResponse>>.SuccesWithData(roles));
		}
	}
}
