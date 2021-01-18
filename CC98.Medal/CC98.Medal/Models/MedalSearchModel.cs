using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CC98.Medal.Models
{
	/// <summary>
	/// 勋章搜索相关信息。
	/// </summary>
	public class MedalSearchModel
	{
		public string? Keyword { get; set; }

		public MedalSearchOwnState? OwnState { get; set; }

		public MedalSearchObtainType? ObtainType { get; set; }
	}

	public enum MedalSearchOwnState
	{
		[Display(Name = "已拥有")]
		Owned,
		[Display(Name = "未拥有")]
		NotOwned,
	}

	[Flags]
	public enum MedalSearchObtainType
	{
		[Display(Name = "无法自主获得")]
		None = 0,
		[Display(Name = "可购买")]
		Buy = 0x1,
		[Display(Name = "可申请")]
		Apply = 0x2,
		[Display(Name = "可购买或申请")]
		Both = Buy | Apply,
	}
}
