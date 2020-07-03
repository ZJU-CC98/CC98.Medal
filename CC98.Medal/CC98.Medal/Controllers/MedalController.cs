using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CC98.Medal.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

		/// <summary>
		/// 显示创建勋章页面。
		/// </summary>
		/// <returns>操作结果。</returns>
		[HttpGet]
		[Authorize(Policies.Edit)]
		public IActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// 执行创建勋章操作。
		/// </summary>
		/// <param name="model">数据模型。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policies.Edit)]
		public async Task<IActionResult> Create(Data.Medal model, CancellationToken cancellationToken)
		{
			if (ModelState.IsValid)
			{
				// ReSharper disable once MethodHasAsyncOverloadWithCancellation
				DbContext.Medals.Add(model);

				try
				{
					await DbContext.SaveChangesAsync(cancellationToken);
					return RedirectToAction("Manage", "Medal");
				}
				catch (DbUpdateException ex)
				{
					ModelState.AddModelError(string.Empty, ex.GetBaseMessage());
				}

			}

			return View(model);
		}

		/// <summary>
		/// 显示编辑勋章页面。
		/// </summary>
		/// <param name="id">要编辑的勋章标识。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpGet]
		[Authorize(Policies.Edit)]
		public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
		{
			var item = await DbContext.Medals.FindAsync(new object[] { id }, cancellationToken);

			if (item == null)
			{
				return NotFound();
			}

			return View(item);
		}

		public async Task<IActionResult> Edit(Data.Medal model, CancellationToken cancellationToken)
		{
			if (ModelState.IsValid)
			{
				DbContext.Medals.Update(model);

				try
				{
					await DbContext.SaveChangesAsync(cancellationToken);
					return RedirectToAction("Manage", "Medal");
				}
				catch (DbUpdateException ex)
				{
					ModelState.AddModelError(string.Empty, ex.GetBaseMessage());
				}

			}

			return View(model);
		}

		/// <summary>
		/// 显示勋章管理页面。
		/// </summary>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>视图结果。</returns>
		[HttpGet]
		[Authorize(Policies.Edit)]
		public async Task<IActionResult> Manage(int page = 1, CancellationToken cancellationToken = default)
		{
			var items =
				from i in DbContext.Medals
				orderby i.Name
				select i;

			return View(await items.ToPagedListAsync(20, page, cancellationToken));
		}

		/// <summary>
		/// 显示勋章详情页面。
		/// </summary>
		/// <param name="id">勋章标识。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>视图结果。</returns>
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Detail(int id, CancellationToken cancellationToken = default)
		{
			var item =
				await (from i in DbContext.Medals
					   where i.Id == id
					   select i).SingleOrDefaultAsync(cancellationToken);

			if (item == null || item.State != MedalState.Normal)
			{
				return NotFound();
			}

			return View(item);
		}
	}
}