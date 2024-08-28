using APITemplate.Business.Abstract;
using APITemplate.Entity.DTO.UserRoleDTO;
using APITemplate.Tools.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITemplate.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserRoleController : ControllerBase
	{
		private readonly IUserRoleService _userRoleService;

		public UserRoleController(IUserRoleService userRoleService)
		{
			_userRoleService = userRoleService;
		}

		[HttpPost("/api/AddUserRole")]
		public async Task<IActionResult> AddUserRole(UserRoleDTORequest userRoleDTORequest)
		{
			var userRole = await _userRoleService.AddAsync(userRoleDTORequest);
			return Ok(ApiResponse<UserRoleDTOResponse>.SuccesWithData(userRole));
		}

		[HttpPost("/api/DeleteUserRole")]
		public async Task<IActionResult> DeleteUserRole(UserRoleDTORequest userRoleDTORequest)
		{
			await _userRoleService.DeleteAsync(userRoleDTORequest);
			return Ok(ApiResponse<UserRoleDTOResponse>.SuccesWithOutData());
		}

		[HttpPost("/api/UpdateUserRole")]
		public async Task<IActionResult> UpdateUserRole(UserRoleDTORequest userRoleDTORequest)
		{
			await _userRoleService.UpdateAsync(userRoleDTORequest);
			return Ok(ApiResponse<UserRoleDTOResponse>.SuccesWithOutData());
		}

		[HttpPost("/api/GetUserRole")]
		public async Task<IActionResult> GetUserRole(UserRoleDTORequest userRoleDTORequest)
		{
			var userRole = await _userRoleService.GetAsync(userRoleDTORequest);
			return Ok(ApiResponse<UserRoleDTOResponse>.SuccesWithData(userRole));
		}

		[HttpPost("/api/GetAllUserRoles")]
		public async Task<IActionResult> GetAllUserRoles(UserRoleDTORequest userRoleDTORequest)
		{
			var userRoles = await _userRoleService.GetAllAsync(userRoleDTORequest);
			return Ok(ApiResponse<List<UserRoleDTOResponse>>.SuccesWithData(userRoles));
		}
	}
}
