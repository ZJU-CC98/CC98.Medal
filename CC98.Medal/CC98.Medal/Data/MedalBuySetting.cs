using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CC98.Medal.Data
{
	/// <summary>
	/// 表示勋章的购买设置。
	/// </summary>
	public class MedalBuySetting
	{
		/// <summary>
		/// 获取或设置购买需要的价格。
		/// </summary>
		[Range(0, int.MaxValue)]
		public int Price { get; set; }

		/// <summary>
		/// 获取或设置购买的有效时间。设定为 <c>null</c> 则永久有效。
		/// </summary>
		[Range(0, int.MaxValue)]
		public int? ExpireDays { get; set; }
	}
}