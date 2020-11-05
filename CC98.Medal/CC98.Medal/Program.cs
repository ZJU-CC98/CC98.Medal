using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CC98.Medal
{
	/// <summary>
	/// 应用程序的入口类型。
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// 应用程序的入口方法。
		/// </summary>
		/// <param name="args">应用程序的启动参数。</param>
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		/// <summary>
		/// 创建应用程序宿主。
		/// </summary>
		/// <param name="args">应用程序的启动参数。</param>
		/// <returns>表示程序宿主的 <see cref="IHostBuilder"/> 对象。</returns>
		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
