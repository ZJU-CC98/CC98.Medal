using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CC98.Medal.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace CC98.Medal.Components
{
	/// <summary>
	/// 勋章选择界面。
	/// </summary>
	public class MedalCategorySelectorViewComponent : ViewComponent
	{
		/// <summary>
		/// 初始化 <see cref="MedalCategorySelectorViewComponent"/> 组件的新实例。
		/// </summary>
		/// <param name="dbContext">数据库上下文对象。</param>
		public MedalCategorySelectorViewComponent(CC98MedalDbContext dbContext)
		{
			DbContext = dbContext;
		}

		/// <summary>
		/// 数据库上下文实例。
		/// </summary>
		private CC98MedalDbContext DbContext { get; }

		/// <summary>
		/// 执行组件。
		/// </summary>
		/// <returns></returns>
		public async Task<IViewComponentResult> InvokeAsync(ModelExpression aspFor)
		{
			var items =
				from i in DbContext.MedalCategories
				select i;

			ViewBag.AspFor = aspFor;
			return View(await items.ToArrayAsync(HttpContext.RequestAborted));
		}
	}
}
