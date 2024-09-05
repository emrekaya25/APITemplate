using APITemplate.Business.Abstract;
using APITemplate.Business.Validation.UserRoleValidator;
using APITemplate.Entity.DTO.UserRoleDTO;
using APITemplate.Tools.Result;
using APITemplate.Tools.Utilities.Attributes;
using APITemplate.Tools.Utilities.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITemplate.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class UserRoleController : ControllerBase
	{
		private readonly IUserRoleService _userRoleService;

		public UserRoleController(IUserRoleService userRoleService)
		{
			_userRoleService = userRoleService;
		}
		[FileLogger("Kullanıcı-Rol Eklendi.")]
		[HttpPost("/api/AddUserRole")]
		[ValidationFilter(typeof(UserRoleValidation))]
		public async Task<IActionResult> AddUserRole(UserRoleDTORequest userRoleDTORequest)
		{
			var userRole = await _userRoleService.AddAsync(userRoleDTORequest);
			return Ok(ApiResponse<UserRoleDTOResponse>.SuccesWithData(userRole));
		}

		[FileLogger("Kullanıcı-Rol Silindi.")]
		[HttpPost("/api/DeleteUserRole")]
		public async Task<IActionResult> DeleteUserRole(UserRoleDTORequest userRoleDTORequest)
		{
			var userRole = await _userRoleService.DeleteAsync(userRoleDTORequest);
			return Ok(ApiResponse<UserRoleDTOResponse>.SuccesWithData(userRole));
		}

		[FileLogger("Kullanıcı-Rol Güncellendi.")]
		[HttpPost("/api/UpdateUserRole")]
		[ValidationFilter(typeof(UserRoleValidation))]
		public async Task<IActionResult> UpdateUserRole(UserRoleDTORequest userRoleDTORequest)
		{
			var userRole = await _userRoleService.UpdateAsync(userRoleDTORequest);
			return Ok(ApiResponse<UserRoleDTOResponse>.SuccesWithData(userRole));
		}

		[FileLogger("Seçili Kullanıcı-Rol Getirildi.")]
		[HttpPost("/api/GetUserRole")]
		public async Task<IActionResult> GetUserRole(UserRoleDTORequest userRoleDTORequest)
		{
			var userRole = await _userRoleService.GetAsync(userRoleDTORequest);
			return Ok(ApiResponse<UserRoleDTOResponse>.SuccesWithData(userRole));
		}

		[FileLogger("Tüm Kullanıcı-Roller Getirildi.")]
		[HttpPost("/api/GetAllUserRoles")]
		public async Task<IActionResult> GetAllUserRoles(UserRoleDTORequest userRoleDTORequest)
		{
			var userRoles = await _userRoleService.GetAllAsync(userRoleDTORequest);
			return Ok(ApiResponse<List<UserRoleDTOResponse>>.SuccesWithData(userRoles));
		}
	}
}
