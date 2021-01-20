using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CC98.Medal.Data;
using CC98.Medal.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using Sakura.AspNetCore;

namespace CC98.Medal.Controllers
{
	/// <summary>
	/// 提供勋章管理功能。
	/// </summary>
	public class MedalController : Controller
	{
		public MedalController(CC98MedalDbContext dbContext, IOperationMessageAccessor messageAccessor, IHtmlLocalizer<MedalController> localizer, IHtmlLocalizer<SharedResources> sharedLocalizer)
		{
			DbContext = dbContext;
			MessageAccessor = messageAccessor;
			Localizer = localizer;
			SharedLocalizer = sharedLocalizer;
		}

		/// <summary>
		/// 数据库上下文对象。
		/// </summary>
		private CC98MedalDbContext DbContext { get; }

		/// <summary>
		/// 消息服务对象。
		/// </summary>
		private IOperationMessageAccessor MessageAccessor { get; }

		private IHtmlLocalizer<MedalController> Localizer { get; }

		private IHtmlLocalizer<SharedResources> SharedLocalizer { get; }

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
			var medals =
				from i in DbContext.Medals
				where i.State != MedalState.Hidden
				select i;

			if (categoryId != null)
			{
				medals = from i in medals
						 where i.CategoryId == categoryId
						 select i;
			}

			// 添加所有者信息
			var data = FindMedalOwnInfo(medals);

			ViewBag.CategoryId = categoryId!;
			return View(await data.ToPagedListAsync(24, page, cancellationToken));
		}

		/// <summary>
		/// 显示我的勋章列表。
		/// </summary>
		/// <param name="page">页码。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> My(int page = 1, CancellationToken cancellationToken = default)
		{
			var myId = User.GetUserId();

			var items =
				(from i in DbContext.UserMedalOwnerships
				 where i.UserId == myId && (i.ExpireTime == null || i.ExpireTime > DateTimeOffset.Now) && i.Medal.State != MedalState.Disabled
				 orderby i.SortOrder
				 select i)
				.Include(p => p.Medal);

			return View(await items.ToPagedListAsync(20, page, cancellationToken));
		}


		/// <summary>
		/// 查询勋章相关的拥有信息。
		/// </summary>
		/// <param name="source">筛选过勋章列表。</param>
		/// <returns>包含拥有信息的勋章列表。</returns>
		private IQueryable<MedalAndOwnInfo> FindMedalOwnInfo(IQueryable<Data.Medal> source)
		{
			var userId = User.TryGetUserId();

			// 未登录，不显示拥有信息
			if (userId == null)
			{
				return from i in source
					   orderby i.Name
					   select new MedalAndOwnInfo
					   {
						   Medal = i,
						   Ownership = null
					   };
			}

			var myOwns =
				from i in DbContext.UserMedalOwnerships
				where i.UserId == userId
				select i;

			return from i in source
				   join o in myOwns on i.Id equals o.MedalId into ms
				   from m in ms.DefaultIfEmpty()
				   orderby i.Name
				   select new MedalAndOwnInfo
				   {
					   Medal = i,
					   Ownership = m
				   };
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

		/// <summary>
		/// 执行编辑操作。
		/// </summary>
		/// <param name="model">数据模型。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policies.Edit)]
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
		/// 执行删除操作。
		/// </summary>
		/// <param name="id">要删除的勋章的标识。</param>
		/// <param name="returnUrl">返回的页面地址。</param>
		/// <param name="cancellationToken">取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policies.Edit)]
		public async Task<IActionResult> Delete(int id, string returnUrl, CancellationToken cancellationToken)
		{
			var item = await DbContext.Medals.FindAsync(new object[] { id }, cancellationToken);

			if (item == null)
			{
				MessageAccessor.Add(OperationMessageLevel.Error, SharedLocalizer["操作失败"],
					Localizer["指定的勋章不存在，或已经被删除。"]);
			}
			else
			{
				DbContext.Medals.Remove(item);

				try
				{
					await DbContext.SaveChangesAsync(cancellationToken);
					MessageAccessor.Add(OperationMessageLevel.Success, SharedLocalizer["操作成功"],
						Localizer["你已经删除了勋章 <strong>{0}</strong>。", item.Name]);

				}
				catch (DbUpdateException ex)
				{
					MessageAccessor.Add(OperationMessageLevel.Error, SharedLocalizer["操作失败"], Localizer["操作中发生错误：{0}", ex.GetBaseMessage()]);
				}
			}

			return this.TryReturn(returnUrl);
		}

		/// <summary>
		/// 显示勋章管理页面。
		/// </summary>
		/// <param name="page">页码。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>视图结果。</returns>
		[HttpGet]
		[Authorize(Policies.Edit)]
		public async Task<IActionResult> Manage(int page = 1, CancellationToken cancellationToken = default)
		{
			var items =
				(from i in DbContext.Medals
				 orderby i.Name
				 select i)
				.Include(p => p.Category);

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

		/// <summary>
		/// 购买勋章操作。
		/// </summary>
		/// <param name="id">需要购买的勋章的标识。</param>
		/// <param name="buySetting">订购使用的购买设置。</param>
		/// <param name="returnUrl">购买后返回的地址。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Buy(int id, MedalBuySetting buySetting, string? returnUrl, CancellationToken cancellationToken = default)
		{
			var userId = User.GetUserId();
			var medal = await DbContext.Medals.FindAsync(new object[] { id }, cancellationToken);

			if (medal == null)
			{
				return NotFound();
			}

			if (!medal.CanBuy)
			{
				return BadRequest("这个勋章不能购买。");
			}


			// 寻找是否有匹配的购买设置。如果没有，说明参数有误或购买设置已经修改。
			if (!medal.BuySettings.Any(i => i.ExpireDays == buySetting.ExpireDays && i.Price == buySetting.Price))
			{
				return BadRequest("购买设置无效。这有可能是管理员修改了勋章的购买价格或者时间信息。请刷新页面后重试一次。");
			}

			var userInfo = await DbContext.Users.FindAsync(new object[] { userId }, cancellationToken);

			// 金额不足
			if (userInfo.Wealth < buySetting.Price)
			{
				return BadRequest("你的财富值目前不足以执行这个操作。");
			}


			// 检索是否已经拥有勋章
			var ownInfo =
				await DbContext.UserMedalOwnerships.FindAsync(new object[] { userId, medal.Id }, cancellationToken);

			// 已经拥有勋章
			if (ownInfo != null)
			{
				// 无限期拥有，不能继续购买
				if (ownInfo.ExpireTime == null)
				{
					return BadRequest("当前用户已经无限期拥有这枚勋章，不能再次购买勋章。");
				}

				// 购买无限期，则直接将有效期延长到无限
				if (buySetting.ExpireDays == null)
				{
					ownInfo.ExpireTime = null;
				}
				else
				{
					// 计算有效期的开始时间，如果勋章尚未过期，则从过期时间开始，如果已过期，则从现在开始
					var startTime =
						ownInfo.ExpireTime.Value > DateTimeOffset.Now
							? ownInfo.ExpireTime.Value
							: DateTimeOffset.Now;


					// 更新过期时间
					ownInfo.ExpireTime = startTime.AddDays(buySetting.ExpireDays.Value);
				}
			}
			// 未拥有勋章，直接插入新项目
			else
			{
				ownInfo = new UserMedalOwnership
				{
					UserId = userId,
					MedalId = id,
					ExpireTime =
						buySetting.ExpireDays == null
							? null
							: DateTimeOffset.Now.AddDays(buySetting.ExpireDays.Value)
				};

				DbContext.UserMedalOwnerships.Add(ownInfo);
			}


			// 扣除财富值
			userInfo.Wealth -= buySetting.Price;

			try
			{
				await DbContext.SaveChangesAsync(cancellationToken);

				var medalUrl = Url.Action("Detail", "Medal", new { id });

				var message =
					ownInfo.ExpireTime != null
						? Localizer["你已经购买或延长了勋章 <strong><a href=\"{1}\">{0}</a></strong> 的 {2:d} 天有效期，目前勋章有效期至 <strong>{3:D}</strong>。",
							medal.Name,
							medalUrl, buySetting.ExpireDays!,
							ownInfo.ExpireTime]
						: Localizer["你已经购买了勋章 <strong><a href=\"{1}\">{0}</a></strong> 并且获得永久有效期。", medal.Name, medalUrl];

				MessageAccessor.Add(OperationMessageLevel.Success, SharedLocalizer["操作成功"], message);

				if (!Url.IsLocalUrl(returnUrl))
				{
					returnUrl = Url.Action("Index", "Home");
				}

				return Redirect(returnUrl);

			}
			catch (DbUpdateException ex)
			{
				return BadRequest(ex.GetBaseMessage());
			}



		}
	}
}