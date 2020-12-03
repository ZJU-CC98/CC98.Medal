using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CC98.Medal.Data;
using Microsoft.AspNetCore.Authorization;
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
		public CategoryController(CC98MedalDbContext dbContext)
		{
			DbContext = dbContext;
		}

		/// <summary>
		/// 数据库上下文对象。
		/// </summary>
		private CC98MedalDbContext DbContext { get; }

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
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpGet]
		public async Task<IActionResult> Detail(int id, CancellationToken cancellationToken = default)
		{
			var item =
				await (from i in DbContext.MedalCategories
					   where i.Id == id
					   select i)
					.Include(p => p.Parent)
					.SingleOrDefaultAsync(cancellationToken);

			if (item == null)
			{
				return NotFound();
			}

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
