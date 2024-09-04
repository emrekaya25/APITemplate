using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; // Newtonsoft.Json kullanarak JSON dönüşümü
using Serilog;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APITemplate.Tools.Utilities.Logging
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class FileLogger : Attribute, IAsyncActionFilter
	{
		private readonly string _actionMessage;

		public FileLogger(string actionMessage)
		{
			_actionMessage = actionMessage;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var userId = context.HttpContext.User.FindFirst("UserId")?.Value;
			var userName = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
			var actionTime = DateTime.UtcNow;

			// Metod adını almak için ActionDescriptor'ı kullan
			var actionDescriptor = context.ActionDescriptor;
			var actionMethodName = GetActionMethodName(actionDescriptor);

			// Loglama öncesi işlemler
			Log.Information("User ID: {UserId}, User Name: {UserName}, Action: {Action}, Message: {Message}, Time: {Time}",
				userId, userName, actionMethodName, _actionMessage, actionTime);

			// Aksiyon metodunu çağır
			var executedContext = await next();

			// Dönen verileri loglama
			var result = executedContext.Result;
			Log.Information("Content: {Content}", GetResultContent(result));
		}

		public async Task OnActionExecutedAsync(ActionExecutedContext context)
		{
			// İsteğin tamamlanmasından sonra yapılacak işlemler

			// Dönen sonucu logla
			var result = context.Result;
			Log.Information("Content: {Content}", GetResultContent(result));
		}

		// Action name'i APITemplate.API.Controllers.RoleController.GetAllRoles (APITemplate.API) bu şekilden GetAllRoles şekline getirme.
		private string GetActionMethodName(ActionDescriptor actionDescriptor)
		{
			if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
			{
				return controllerActionDescriptor.MethodInfo.Name;
			}
			return "Unknown Action";
		}

		// Result içeriğini ayrıştırma
		private string GetResultContent(IActionResult result)
		{
			if (result is OkObjectResult okResult)
			{
				return JsonConvert.SerializeObject(okResult.Value);
			}
			else if (result is ContentResult contentResult)
			{
				return contentResult.Content;
			}
			else if (result is JsonResult jsonResult)
			{
				return JsonConvert.SerializeObject(jsonResult.Value);
			}
			return "Unknown Result";
		}
	}
}
