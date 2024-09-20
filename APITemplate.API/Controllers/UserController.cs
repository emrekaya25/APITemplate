using APITemplate.Business.Abstract;
using APITemplate.Business.Validation.UserValidator;
using APITemplate.Entity.DTO.LoginDTO;
using APITemplate.Entity.DTO.UserDTO;
using APITemplate.Tools.Result;
using APITemplate.Tools.Utilities.Attributes;
using APITemplate.Tools.Utilities.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace APITemplate.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}
		[FileLogger("Kullanıcı Eklendi.")]
		[HttpPost("/api/AddUser")]
		[ValidationFilter(typeof(UserValidation))]
		public async Task<IActionResult> AddUser(UserDTORequest userDTORequest)
		{
			var user = await _userService.AddAsync(userDTORequest);
			return Ok(ApiResponse<UserDTOResponse>.SuccesWithData(user));
		}

		[AllowAnonymous]
		[FileLogger("Kullanıcı Güncellendi.")]
		[HttpPost("/api/UpdateUser")]
		[ValidationFilter(typeof(UserValidation))]
		public async Task<IActionResult> UpdateUser(UserDTORequest userDTORequest)
		{
			var user = await _userService.UpdateAsync(userDTORequest);
			return Ok(ApiResponse<UserDTOResponse>.SuccesWithData(user));
		}

		[FileLogger("Kullanıcı Silindi.")]
		[HttpPost("/api/DeleteUser")]
		public async Task<IActionResult> DeleteUser(UserDTORequest userDTORequest)
		{
			var user = await _userService.DeleteAsync(userDTORequest);
			return Ok(ApiResponse<UserDTOResponse>.SuccesWithData(user));
		}

		[AllowAnonymous]
		[FileLogger("Seçili Kullanıcı Getirildi.")]
		[HttpPost("/api/GetUser")]
		public async Task<IActionResult> GetUser(UserDTORequest userDTORequest)
		{
			var user = await _userService.GetAsync(userDTORequest);
			if (user != null)
			{
				return Ok(ApiResponse<UserDTOResponse>.SuccesWithData(user));
			}
			else
			{
				return NotFound(ApiResponse<UserDTOResponse>.SuccesNoDataFound("Veri bulunamadı.."));
			}
		}

		[FileLogger("Tüm Kullanıcılar Getirildi.")]
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
				return NotFound(ApiResponse<List<UserDTOResponse>>.SuccesNoDataFound("Veri bulunamadı.."));
			}
		}

		[AllowAnonymous]
		[FileLogger("Sisteme Giriş Yapıldı.")]
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

		[AllowAnonymous]
		[FileLogger("Şifre Değiştirildi.")]
		[HttpPost("/api/ResetPassword")]
		public async Task<IActionResult> ResetPassword(UserDTOResetPassword userDTORequest)
		{
			var user = await _userService.ResetPassword(userDTORequest);
			if (user != null)
			{
				return Ok(ApiResponse<UserDTOResponse>.SuccesWithData(user));
			}
			else
			{
				return NotFound(ApiResponse<UserDTOResponse>.SuccesNoDataFound("Şifre değiştirilemedi.."));
			}
		}
	}
}
