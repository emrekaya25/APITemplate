using APITemplate.Business.Abstract;
using APITemplate.Entity.DTO.LoginDTO;
using APITemplate.Entity.DTO.UserDTO;
using APITemplate.Tools.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITemplate.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("/api/AddUser")]
		public async Task<IActionResult> AddUser(UserDTORequest userDTORequest)
		{
			var user = await _userService.AddAsync(userDTORequest);
			return Ok(ApiResponse<UserDTOResponse>.SuccesWithData(user));
		}

		[HttpPost("/api/UpdateUser")]
		public async Task<IActionResult> UpdateUser(UserDTORequest userDTORequest)
		{
			await _userService.UpdateAsync(userDTORequest);
			return Ok(ApiResponse<UserDTOResponse>.SuccesWithOutData());
		}

		[HttpPost("/api/DeleteUser")]
		public async Task<IActionResult> DeleteUser(UserDTORequest userDTORequest)
		{
			await _userService.DeleteAsync(userDTORequest);
			return Ok(ApiResponse<UserDTOResponse>.SuccesWithOutData());
		}

		[HttpPost("/api/GetUser")]
		public async Task<IActionResult> GetUser(UserDTORequest userDTORequest)
		{
			var user = await _userService.GetAsync(userDTORequest);
			return Ok(ApiResponse<UserDTOResponse>.SuccesWithData(user));
		}

		[HttpPost("/api/GetAllUsers")]
		public async Task<IActionResult> GetAllUsers(UserDTORequest userDTORequest)
		{
			var users = await _userService.GetAllAsync(userDTORequest);
			if (users != null)
			{
				return Ok(ApiResponse<List<UserDTOResponse>>.SuccesWithData(users));
			}
			else
			{
				return NotFound(ApiResponse<List<UserDTOResponse>>.SuccesNoDataFound("Bir sorun oluştu.."));
			}
		}

		[HttpPost("/api/Login")]
		public async Task<IActionResult> Login(LoginDTORequest loginDTORequest)
		{
			var user = await _userService.LoginAsync(loginDTORequest);
			if (user != null)
			{
				return Ok(ApiResponse<LoginDTOResponse>.SuccesWithData(user));
			}
			else
			{
				return NotFound(ApiResponse<LoginDTOResponse>.SuccesNoDataFound("Kullanıcı adı veya şifre yanlış."));
			}
		}
	}
}
