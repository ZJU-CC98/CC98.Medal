using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CC98.Medal.Data;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CC98.Medal.Components
{
	/// <summary>
	/// 显示勋章列表的组件。
	/// </summary>
	public class MedalCategoryListViewComponent : ViewComponent
	{
		/// <summary>
		/// 初始化 <see cref="MedalCategoryListViewComponent"/> 的新实例。
		/// </summary>
		/// <param name="dbContext"><see cref="CC98MedalDbContext"/> 服务实例。</param>
		public MedalCategoryListViewComponent(CC98MedalDbContext dbContext)
		{
			DbContext = dbContext;
		}

		/// <summary>
		/// 数据库上下文对象。
		/// </summary>
		private CC98MedalDbContext DbContext { get; }

		[UsedImplicitly]
		public async Task<IViewComponentResult> InvokeAsync(int? selectedCategoryId = null, Func<MedalCategory, string>? linkGenerator = null)
		{
			var items =
				from i in DbContext.MedalCategories
				select i;

			// 参数传递
			ViewBag.LinkGenerator = linkGenerator!;
			ViewBag.SelectedCategoryId = selectedCategoryId!;

			return View(await items.ToArrayAsync(HttpContext.RequestAborted));
		}
	}
}
