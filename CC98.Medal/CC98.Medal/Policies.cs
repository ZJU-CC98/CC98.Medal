using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC98.Medal
{
	/// <summary>
	/// 定义应用程序中使用的策略。
	/// </summary>
	public static class Policies
	{
		/// <summary>
		/// 管理勋章系统。
		/// </summary>
		public const string Admin = nameof(Admin);

		/// <summary>
		/// 颁发或者收回勋章。
		/// </summary>
		public const string Issue = nameof(Issue);

		/// <summary>
		/// 审核勋章申请。
		/// </summary>
		public const string Review = nameof(Review);

		/// <summary>
		/// 编辑勋章列表。
		/// </summary>
		public const string Edit = nameof(Edit);

		/// <summary>
		/// 定义系统中使用的角色。
		/// </summary>
		public static class Roles
		{
			/// <summary>
			/// 通用系统管理员。
			/// </summary>
			public const string GeneralAdministrators = "Administrators";

			/// <summary>
			/// 勋章系统管理员。
			/// </summary>
			public const string MedalAdministrators = "Medal Administrators";

			/// <summary>
			/// 勋章系统操作员。
			/// </summary>
			public const string MedalOperators = "Medal Operators";

			/// <summary>
			/// 勋章系统审核员。
			/// </summary>
			public const string MedalReviewers = "Medal Reviewers";

			/// <summary>
			/// 勋章系统编辑员。
			/// </summary>
			public const string MedalEditors = "Medal Editors";
		}
	}
}
