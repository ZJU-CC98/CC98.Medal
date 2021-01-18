using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CC98.Services.Web;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CC98.Medal.Controllers
{
	/// <summary>
	/// 提供文件上传服务。
	/// </summary>
	public class FileController : Controller
	{
		/// <summary>
		/// 初始化 <see cref="FileController"/> 类型的新实例。
		/// </summary>
		/// <param name="fileUploadWebService"><see cref="Services.Web.FileUploadWebService"/> 类型的服务对象。</param>
		/// <param name="appSettingOptions">提供 <see cref="AppSetting"/> 数据的服务对象。</param>
		public FileController(FileUploadWebService fileUploadWebService, IOptions<AppSetting> appSettingOptions)
		{
			FileUploadWebService = fileUploadWebService;
			AppSetting = appSettingOptions.Value;
		}

		/// <summary>
		/// 文件上传服务。
		/// </summary>
		private FileUploadWebService FileUploadWebService { get; }
		
		private AppSetting AppSetting { get; }

		/// <summary>
		/// 上传给定的文件。
		/// </summary>
		/// <param name="file">要上传的文件。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpPost]
		[Authorize(Policies.Edit)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken = default)
		{
			if(file.Length > AppSetting.FileUpload.MaxSize)
			{
				return BadRequest("文件尺寸超过设定的最大限制。");
			}	
			
			try
			{
				var result = await FileUploadWebService.UploadAsync(new[] { file }, cancellationToken);
				return Ok(result.FirstOrDefault());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.GetBaseMessage());
			}

		}
	}
}
