using APITemplate.Business.Abstract;
using APITemplate.Business.Validation.RoleValidator;
using APITemplate.Entity.DTO.RoleDTO;
using APITemplate.Tools.Result;
using APITemplate.Tools.Utilities.Attributes;
using APITemplate.Tools.Utilities.Logging;
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

		[FileLogger("Rol Eklendi.")]
		[HttpPost("/api/AddRole")]
		[ValidationFilter(typeof(RoleValidation))]
		public async Task<IActionResult> AddRole(RoleDTORequest roleDTORequest)
		{
			var role = await _roleService.AddAsync(roleDTORequest);
			return Ok(ApiResponse<RoleDTOResponse>.SuccesWithData(role));
		}

		[FileLogger("Rol Silindi.")]
		[HttpPost("/api/DeleteRole")]
		public async Task<IActionResult> DeleteRole(RoleDTORequest roleDTORequest)
		{
			var role = await _roleService.DeleteAsync(roleDTORequest);
			return Ok(ApiResponse<RoleDTOResponse>.SuccesWithData(role));
		}

		[FileLogger("Rol Güncellendi.")]
		[HttpPost("/api/UpdateRole")]
		[ValidationFilter(typeof(RoleValidation))]
		public async Task<IActionResult> UpdateRole(RoleDTORequest roleDTORequest)
		{
			var role = await _roleService.UpdateAsync(roleDTORequest);
			return Ok(ApiResponse<RoleDTOResponse>.SuccesWithData(role));
		}

		[FileLogger("Seçili Rol Getirildi.")]
		[HttpPost("/api/GetRole")]
		public async Task<IActionResult> GetRole(RoleDTORequest roleDTORequest)
		{
			var role = await _roleService.GetAsync(roleDTORequest);
			return Ok(ApiResponse<RoleDTOResponse>.SuccesWithData(role));
		}

		[FileLogger("Tüm Roller Getirildi.")]
		[HttpPost("/api/GetAllRoles")]
		public async Task<IActionResult> GetAllRoles(RoleDTORequest roleDTORequest)
		{
			var roles = await _roleService.GetAllAsync(roleDTORequest);
			return Ok(ApiResponse<List<RoleDTOResponse>>.SuccesWithData(roles));
		}
	}
}
