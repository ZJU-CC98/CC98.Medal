using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CC98.Medal.Data;
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

		public async Task<IActionResult> Index(int page = 1, CancellationToken cancellationToken = default)
		{
			var items = from i in DbContext.Medals
						orderby i.Name
						select i;

			return View(await items.ToPagedListAsync(24, page, cancellationToken));
		}
	}
}