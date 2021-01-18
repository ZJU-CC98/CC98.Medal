using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace CC98.Medal.Data
{
	/// <summary>
	/// 定义用户拥有勋章的情况。
	/// </summary>
	[Index(nameof(UserId))]
	[Index(nameof(MedalId))]
	public class UserMedalOwnership
	{
		/// <summary>
		/// 关联的用户的标识。
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// 关联的勋章的标识。
		/// </summary>
		public int MedalId { get; set; }

		/// <summary>
		/// 关联的勋章。
		/// </summary>
		[ForeignKey(nameof(MedalId))]
		[Required]
		public Medal Medal { get; set; } = null!;

		/// <summary>
		/// 拥有结束时间。如该属性为 <c>null</c> 则表示无限期拥有。
		/// </summary>
		public DateTimeOffset? ExpireTime { get; set; }

		/// <summary>
		/// 勋章的排序顺序。
		/// </summary>
		public int SortOrder { get; set; }

	}

	/// <summary>
	/// 提供数据库相关的扩展功能。该类型为静态类型。
	/// </summary>
	public static class DataUtility
	{
		/// <summary>
		/// 根据用户的勋章拥有记录，判断当前拥有状况。
		/// </summary>
		/// <param name="item">拥有记录。</param>
		/// <returns>当前拥有状况。</returns>
		public static OwnershipState GetOwnershipState(this UserMedalOwnership? item)
		{
			if (item == null)
			{
				return OwnershipState.NotOwned;
			}

			if (item.ExpireTime == null)
			{
				return OwnershipState.OwnedPermanently;
			}

			return item.ExpireTime.Value >= DateTimeOffset.Now
				? OwnershipState.Owned
				: OwnershipState.NotOwned;
		}
	}

	/// <summary>
	/// 获取所有权的拥有状态。
	/// </summary>
	public enum OwnershipState
	{
		/// <summary>
		/// 未拥有。
		/// </summary>
		NotOwned,
		/// <summary>
		/// 已拥有。
		/// </summary>
		Owned,
		/// <summary>
		/// 已永久拥有。
		/// </summary>
		OwnedPermanently
	}
}