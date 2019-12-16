using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CC98.Medal
{
	/// <summary>
	/// 应用程序的启动类型。
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// 初始化 <see cref="Startup"/> 类型的新实例。
		/// </summary>
		/// <param name="configuration">应用程序配置。</param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// 应用程序配置信息。
		/// </summary>
		private IConfiguration Configuration { get; }

		/// <summary>
		/// 配置应用程序服务。
		/// </summary>
		/// <param name="services"></param>
		[UsedImplicitly]
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
		}

		/// <summary>
		/// 配置应用程序执行流程。
		/// </summary>
		/// <param name="app">应用程序对象。</param>
		/// <param name="env">宿主环境信息。</param>
		[UsedImplicitly]
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
