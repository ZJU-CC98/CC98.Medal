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
		[Display(Name = "正常", Description = "勋章系统列表中将列出勋章，用户可以正常展示该勋章。")]
		Normal = 0,
		/// <summary>
		/// 勋章不在勋章列表中显示，但不影响已经获得的用户展示。
		/// </summary>
		[Display(Name = "隐藏", Description = "勋章系统列表中不列出该勋章，但获得该勋章的用户可以展示该勋章。")]
		Hidden,
		/// <summary>
		/// 勋章已经被禁用。已经获得勋章的用户也无法展示勋章。
		/// </summary>
		[Display(Name = "禁用", Description = "该勋章不会在勋章系统列表中列出，用户也无法展示此勋章。")]
		Disabled
	}
}