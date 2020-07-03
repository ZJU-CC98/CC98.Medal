using System.ComponentModel.DataAnnotations;

namespace CC98.Medal.Data
{
	/// <summary>
	/// 定义勋章状态。
	/// </summary>
	public enum MedalState
	{
		/// <summary>
		/// 勋章可以正常展示并使用。
		/// </summary>
		[Display(Name= "正常")]
		Normal = 0,
		/// <summary>
		/// 勋章不在勋章列表中显示，但不影响已经获得的用户展示。
		/// </summary>
		[Display(Name="隐藏")]
		Hidden,
		/// <summary>
		/// 勋章已经被禁用。已经获得勋章的用户也无法展示勋章。
		/// </summary>
		[Display(Name = "禁用")]
		Disabled
	}
}