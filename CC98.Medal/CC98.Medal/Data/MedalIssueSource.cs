namespace CC98.Medal.Data
{
	/// <summary>
	/// 获取或设置获得勋章的来源。
	/// </summary>
	public enum MedalIssueSource
	{
		/// <summary>
		/// 用户自主购买。
		/// </summary>
		Buy,
		/// <summary>
		/// 用户申请并获得通过。
		/// </summary>
		Apply,
		/// <summary>
		/// 系统自动颁发/获得操作。
		/// </summary>
		System,
		/// <summary>
		/// 管理员手动操作。
		/// </summary>
		Manual,
	}
}