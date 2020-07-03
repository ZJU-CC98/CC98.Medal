using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC98.Medal.Data
{
	/// <summary>
	/// 定义勋章的颁发信息。
	/// </summary>
	public class MedalIssueRecord
	{
		/// <summary>
		/// 获取或设置该对象的标识。
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// 本次操作关联到的用户的标识。
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// 本次操作关联到的勋章的标识。
		/// </summary>
		public int MedalId { get; set; }

		/// <summary>
		/// 本次操作关联到的勋章。
		/// </summary>
		[ForeignKey(nameof(MedalId))]
		public Medal Medal { get; set; } = null!;

		/// <summary>
		/// 本次操作的时间。
		/// </summary>
		public DateTimeOffset Time { get; set; }
	}
}