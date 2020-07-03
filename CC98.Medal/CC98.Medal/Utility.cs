using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
	}
}
