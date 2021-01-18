using JetBrains.Annotations;

namespace CC98.Medal
{
	/// <summary>
	/// 定义应用程序级部署设置。该设置由配置文件提供，不支持由应用程序直接更改。
	/// </summary>
	[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.Itself | ImplicitUseTargetFlags.Members)]
	public class AppSetting
	{
		/// <summary>
		/// 文件上传设置。
		/// </summary>
		public FileUploadSetting FileUpload { get; set; } = null!;
	}
}