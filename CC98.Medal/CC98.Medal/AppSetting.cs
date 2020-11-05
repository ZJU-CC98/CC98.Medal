﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CC98.Medal
{
	/// <summary>
	/// 定义应用程序级设置。
	/// </summary>
	public class AppSetting
	{
		/// <summary>
		/// 系统公告消息。支持 HTML。
		/// </summary>
		[DataType(DataType.Html)]
		public string? Announcement { get; set; }

		/// <summary>
		/// 获取 <see cref="AppSetting"/> 对象的默认值。
		/// </summary>
		public static AppSetting Default => new AppSetting();
	}
}
