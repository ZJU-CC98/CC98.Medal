using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CC98.Medal
{
	/// <summary>
	/// 为用户访问提供辅助方法。该类型为静态类型。
	/// </summary>
	public static class UserUtility
	{
		/// <summary>
		/// 尝试获取用户的标识。如果标识不存在，则返回 <c>null</c>。
		/// </summary>
		/// <param name="principal">用户主体对象。</param>
		/// <returns>该对象中包含的用户标识。</returns>
		public static int? TryGetUserId(this ClaimsPrincipal principal)
		{
			var idString = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			return
				idString != null
					? int.Parse(idString, NumberStyles.Integer, CultureInfo.InvariantCulture)
					: null;
		}

		/// <summary>
		/// 尝试获取用户的标识。如果标识不存在，则引发 <see cref="InvalidOperationException"/> 类型的异常。
		/// </summary>
		/// <param name="principal">用户主体对象。</param>
		/// <returns>该对象中包含的用户标识。</returns>
		/// <exception cref="InvalidOperationException">无法获取用户标识信息。这可能是用户未登录引起的。</exception>
		public static int GetUserId(this ClaimsPrincipal principal)
			=> principal.TryGetUserId() ?? throw new InvalidOperationException("当前用户无标识信息，可能是用户未登录。");
	}
}
