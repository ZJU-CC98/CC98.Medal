namespace CC98.Medal
{
	/// <summary>
	/// 文件上传设置。
	/// </summary>
	public class FileUploadSetting
	{
		/// <summary>
		/// 是否默认压缩图片。
		/// </summary>
		public bool CompressByDefault { get; set; }

		/// <summary>
		/// 上传时默认的子文件夹。
		/// </summary>
		public string? DefaultSubPath { get; set; }

		/// <summary>
		/// 上传允许的最大文件尺寸，以字节为单位。
		/// </summary>
		public long MaxSize { get; set; }
	}
}