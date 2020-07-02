using System;
using System.Threading.Tasks;

using CC98.Authentication.OpenIdConnect;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Sakura.AspNetCore.Authentication;

namespace CC98.Medal.Controllers
{
	/// <summary>
	/// 提供账户操作。
	/// </summary>
	public class AccountController : Controller
	{
		public AccountController(ExternalSignInManager externalSignInManager)
		{
			ExternalSignInManager = externalSignInManager;
		}

		/// <summary>
		/// 外部登录服务对象。
		/// </summary>
		private ExternalSignInManager ExternalSignInManager { get; }


		/// <summary>
		/// 请求登录。
		/// </summary>
		/// <param name="returnUrl">登录后要返回的 URL 地址。</param>
		/// <returns>操作结果。</returns>
		[HttpGet]
		[AllowAnonymous]
		public IActionResult LogOn(string returnUrl)
		{
			var properties = new AuthenticationProperties
			{
				RedirectUri = Url.Action("LogOnCallback", "Account", new { returnUrl })
			};

			return Challenge(properties, CC98Defaults.AuthenticationScheme);
		}

		/// <summary>
		/// 登录后执行的回调操作。
		/// </summary>
		/// <param name="returnUrl">登录后的返回地址。</param>
		/// <returns>操作结果。</returns>
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> LogOnCallback(string returnUrl)
		{
			var principal = await ExternalSignInManager.SignInFromExternalCookieAsync();

			if (principal?.Identity == null)
			{
				ViewBag.Error = "登录失败";
				return RedirectToAction("Index", "Home");
			}

			// 检测是否外部 URL
			if (!Url.IsLocalUrl(returnUrl))
			{
				returnUrl = Url.Action("Index", "Home");
			}

			return Redirect(returnUrl);
		}

		/// <summary>
		/// 执行注销操作。
		/// </summary>
		/// <returns>操作结果。</returns>
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogOff()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
