using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CC98.Medal.Data;

using Microsoft.AspNetCore.Http;
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
		/// <param name="aspFor">该组件绑定到的视图模型表达式。</param>
		/// <param name="showNoCategoryOption">是否显示“无分类”表达式。</param>
		/// <returns>操作结果。</returns>
		public async Task<IViewComponentResult> InvokeAsync(ModelExpression aspFor, bool showNoCategoryOption = true)
		{
			// 加载所有项目
			var allItems = await DbContext.MedalCategories.ToArrayAsync(HttpContext.RequestAborted);

			var topItems =
				from i in allItems
				where i.ParentId == null
				orderby i.SortOrder
				select i;

			ViewBag.AspFor = aspFor;
			ViewBag.ShowNoCategoryOption = showNoCategoryOption;
			return View(topItems.ToArray());
		}
	}
}
