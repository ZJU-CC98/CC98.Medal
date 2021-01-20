using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace CC98.Medal
{
	/// <summary>
	/// 提供辅助方法。该类型为静态类型。
	/// </summary>
	public static class Utility
	{
		/// <summary>
		/// 获取异常的根源消息。
		/// </summary>
		/// <param name="ex">异常对象。</param>
		/// <returns>描述异常根源的消息。</returns>
		public static string GetBaseMessage(this Exception ex) => ex.GetBaseException().Message;

		/// <summary>
		/// 将一个字符串重复若干次。
		/// </summary>
		/// <param name="source">要重复的字符串。</param>
		/// <param name="count">要重复的次数。</param>
		/// <returns>将 <paramref name="source"/>重复 <paramref name="count"/> 次后产生的新字符串。</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> 为 <c>null</c>。</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> 为负数。</exception>
		public static string Repeat(this string source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), source, "重复次数不能为负数。");
			}

			var sb = new StringBuilder(source.Length * count);

			for (var i = 0; i < count; i++)
			{
				sb.Append(source);
			}

			return sb.ToString();
		}

		/// <summary>
		/// 尝试返回给定的地址。如果地址非法，则返回主页。
		/// </summary>
		/// <param name="controller">控制器对象。</param>
		/// <param name="returnUrl">返回地址。</param>
		/// <returns>操作结果。</returns>
		public static IActionResult TryReturn(this ControllerBase controller, string returnUrl)
		{
			if (!controller.Url.IsLocalUrl(returnUrl))
			{
				returnUrl = controller.Url.Action("Index", "Home");
			}

			return controller.Redirect(returnUrl);
		}
	}
}
