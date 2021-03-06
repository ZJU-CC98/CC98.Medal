﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CC98.Medal.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Sakura.AspNetCore;

namespace CC98.Medal.Controllers
{
	/// <summary>
	/// 提供对勋章类别的操作。
	/// </summary>
	[Authorize(Policies.Edit)]
	public class CategoryController : Controller
	{
		/// <summary>
		/// 初始化 <see cref="CategoryController"/> 的新实例。
		/// </summary>
		/// <param name="dbContext">数据库上下文对象。</param>
		/// <param name="messageAccessor">消息访问器对象。</param>
		public CategoryController(CC98MedalDbContext dbContext, IOperationMessageAccessor messageAccessor, IHtmlLocalizer<SharedResources> sharedLocalizer, IHtmlLocalizer<CategoryController> localizer)
		{
			DbContext = dbContext;
			MessageAccessor = messageAccessor;
			SharedLocalizer = sharedLocalizer;
			Localizer = localizer;
		}

		/// <summary>
		/// 数据库上下文对象。
		/// </summary>
		private CC98MedalDbContext DbContext { get; }

		/// <summary>
		/// 消息访问器对象。
		/// </summary>
		private IOperationMessageAccessor MessageAccessor { get; }

		/// <summary>
		/// 共享本地化资源。
		/// </summary>
		public IHtmlLocalizer<SharedResources> SharedLocalizer { get; }

		/// <summary>
		/// 本地化资源。
		/// </summary>
		public IHtmlLocalizer<CategoryController> Localizer { get; }

		/// <summary>
		/// 列出所有顶级分类。
		/// </summary>
		/// <param name="page">页码。</param>
		/// <returns>操作结果。</returns>
		[HttpGet]
		public async Task<IActionResult> Index(int page = 1)
		{
			var items =
				from i in DbContext.MedalCategories
				where i.ParentId == null
				orderby i.SortOrder
				select i;

			return View(await items.ToPagedListAsync(20, page));
		}

		/// <summary>
		/// 列出单个分类的详细信息。
		/// </summary>
		/// <param name="id">分类的标识。</param>
		/// <param name="medalPage">类别相关勋章的标识。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpGet]
		public async Task<IActionResult> Detail(int id, int medalPage = 1, CancellationToken cancellationToken = default)
		{
			var item =
				await (from i in DbContext.MedalCategories
					   where i.Id == id
					   select i)
					.Include(p => p.Parent)
					.Include(p => p.Children)
					.SingleOrDefaultAsync(cancellationToken);

			if (item == null)
			{
				return NotFound();
			}

			var medals = from i in DbContext.Medals
						 where i.CategoryId == id
						 orderby i.Name
						 select i;

			ViewBag.Medals = await medals.ToPagedListAsync(10, medalPage, cancellationToken);

			return View(item);
		}

		/// <summary>
		/// 显示创建页面。
		/// </summary>
		/// <returns>操作结果。</returns>
		[HttpGet]
		public IActionResult Create(int? parentId = null)
		{
			var model = new MedalCategory
			{
				ParentId = parentId
			};

			return View(model);
		}

		/// <summary>
		/// 显示创建页面。
		/// </summary>
		/// <returns>操作结果。</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(MedalCategory model, CancellationToken cancellationToken = default)
		{
			if (ModelState.IsValid)
			{
				DbContext.MedalCategories.Add(model);

				try
				{
					await DbContext.SaveChangesAsync(cancellationToken);
					var url = Url.Action("Detail", "Category", new { id = model.Id });

					MessageAccessor.Add(OperationMessageLevel.Success, SharedLocalizer["操作成功"],
						Localizer["你已经添加了勋章类别 <a href=\"{1}\">{0}</a>。", model.Name, url]);
				}
				catch (DbUpdateException ex)
				{
					ModelState.AddModelError(string.Empty, ex.GetBaseMessage());
				}
			}

			return View(model);
		}

		/// <summary>
		/// 编辑勋章分类。
		/// </summary>
		/// <param name="id">要编辑的项目。</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
		{
			var item =

				await (from i in DbContext.MedalCategories
					   where i.Id == id
					   select i).Include(p => p.Parent)
				.SingleOrDefaultAsync(cancellationToken);

			if (item == null)
			{
				return NotFound();
			}

			return View(item);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(MedalCategory model, CancellationToken cancellationToken = default)
		{
			// 输出距离。
			await DataCheckAsync(model, cancellationToken);

			if (ModelState.IsValid)
			{
				DbContext.MedalCategories.Update(model);

				try
				{
					await DbContext.SaveChangesAsync(cancellationToken);
					return RedirectToAction("Index", "Category");
				}
				catch (DbUpdateException ex)
				{
					ModelState.AddModelError(string.Empty, ex.GetBaseMessage());
				}
			}

			return View(model);
		}

		/// <summary>
		/// 执行删除操作。
		/// </summary>
		/// <param name="id">要删除的分类标识。</param>
		/// <param name="returnUrl">返回地址。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id, string returnUrl, CancellationToken cancellationToken = default)
		{
			var item = await DbContext.MedalCategories.FindAsync(new object[] { id }, cancellationToken);

			if (item == null)
			{
				MessageAccessor.Add(OperationMessageLevel.Error, SharedLocalizer["操作失败"], Localizer["指定的分类不存在或已经被删除。"]);
			}
			else
			{
				DbContext.MedalCategories.Remove(item);

				try
				{
					await DbContext.SaveChangesAsync(cancellationToken);
					MessageAccessor.Add(OperationMessageLevel.Success, SharedLocalizer["操作成功"],
						Localizer["你已经删除了勋章分类 <strong>{0}</strong>。", item.Name]);
				}
				catch (DbUpdateException ex)
				{
					MessageAccessor.Add(OperationMessageLevel.Error, SharedLocalizer["操作失败"],
						Localizer["操作过程中发生错误：{0}", ex.GetBaseMessage()]);
				}
			}

			return this.TryReturn(returnUrl);
		}

		/// <summary>
		/// 对数据执行额外检查。
		/// </summary>
		/// <param name="category">要检查的数据。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>表示一步操作的任务。</returns>
		private async Task DataCheckAsync(MedalCategory category, CancellationToken cancellationToken)
		{
			// 检查上级关系
			if (await CheckCategoryCircleCoreAsync(category, cancellationToken))
			{
				ModelState.AddModelError(nameof(category.ParentId), "无法使用该分类作为上级分类，否则上下级关系链中将产生循环。");
			}
		}

		/// <summary>
		/// 检查勋章分类是否存在循环现象。
		/// </summary>
		/// <returns>表示异步操作的任务。</returns>
		private async Task<bool> CheckCategoryCircleCoreAsync(MedalCategory category, CancellationToken cancellationToken = default)
		{
			// 附加当前对象
			DbContext.MedalCategories.Attach(category);

			// 加载所有勋章
			await DbContext.MedalCategories.LoadAsync(cancellationToken);

			var current = category.Parent;

			while (current != null)
			{
				// 如果至少有一个上级是自己，则表示出现循环问题。
				if (current == category)
				{
					return true;
				}

				current = current.Parent;
			}

			return false;
		}
	}
}
