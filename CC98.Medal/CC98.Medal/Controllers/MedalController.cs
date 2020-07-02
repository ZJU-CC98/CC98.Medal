using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CC98.Medal.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Sakura.AspNetCore;

namespace CC98.Medal.Controllers
{
	/// <summary>
	/// 提供勋章管理功能。
	/// </summary>
	public class MedalController : Controller
	{
		public MedalController(CC98MedalDbContext dbContext)
		{
			DbContext = dbContext;
		}

		/// <summary>
		/// 数据库上下文对象。
		/// </summary>
		private CC98MedalDbContext DbContext { get; }

		/// <summary>
		/// 显示勋章列表。
		/// </summary>
		/// <param name="categoryId">如果指定该参数，则只显示给定分类的勋章。</param>
		/// <param name="page">在数量超过一页时显示的页码。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index(int? categoryId = null, int page = 1, CancellationToken cancellationToken = default)
		{
			var items = from i in DbContext.Medals
						orderby i.Name
						select i;

			ViewBag.CategoryId = categoryId!;
			return View(await items.ToPagedListAsync(24, page, cancellationToken));
		}

		[HttpGet]
		[Authorize(Policies.Edit)]
		public IActionResult Create()
		{
			return View();
		}
	}
}