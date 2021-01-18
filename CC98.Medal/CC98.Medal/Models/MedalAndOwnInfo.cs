using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CC98.Medal.Data;

namespace CC98.Medal.Models
{
	/// <summary>
	/// 勋章和某个用户的拥有信息。
	/// </summary>
	public record MedalAndOwnInfo
	{
		/// <summary>
		/// 勋章信息。
		/// </summary>
		public Data.Medal Medal { get; init; } = null!;
		/// <summary>
		/// 用户所有信息。
		/// </summary>
		public UserMedalOwnership? Ownership { get; init; }
	}
}
