using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using CC98.Authentication.OpenIdConnect;
using CC98.Medal.Data;

using IdentityModel;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CC98.Medal
{
	/// <summary>
	/// Ӧ�ó�����������͡�
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// ��ʼ�� <see cref="Startup"/> ���͵���ʵ����
		/// </summary>
		/// <param name="configuration">Ӧ�ó������á�</param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Ӧ�ó���������Ϣ��
		/// </summary>
		private IConfiguration Configuration { get; }

		/// <summary>
		/// ����Ӧ�ó������
		/// </summary>
		/// <param name="services"></param>
		[UsedImplicitly]
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<CC98MedalDbContext>(options =>
				{
					options.UseSqlServer(Configuration.GetConnectionString("CC98-Medal"));
				});

			services.AddControllersWithViews()
				.AddRazorRuntimeCompilation()
				.AddMvcLocalization(options => options.ResourcesPath = "Resources")
				.AddDataAnnotationsLocalization()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder);

			services.AddAuthentication(IdentityConstants.ApplicationScheme)
				.AddCookie(IdentityConstants.ApplicationScheme, options =>
				{
					options.LoginPath = "/Account/LogOn";
					options.LogoutPath = "/Account/LogOff";
					options.AccessDeniedPath = "/Home/AccessDenied";

					options.Cookie.HttpOnly = true;
					options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
					options.Cookie.SameSite = SameSiteMode.Lax;
				})
				.AddCookie(IdentityConstants.ExternalScheme)
				.AddCC98(CC98Defaults.AuthenticationScheme, options =>
				{
					options.ClientId = Configuration["Authentication:CC98:ClientId"];
					options.ClientSecret = Configuration["Authentication:CC98:ClientSecret"];
					options.Authority = Configuration["Authentication:CC98:Authority"];
					options.CallbackPath = Configuration["Authentication:CC98:CallbackPath"];

					options.ResponseType = OidcConstants.ResponseTypes.CodeIdTokenToken;
					options.SignInScheme = IdentityConstants.ExternalScheme;
				});

			// ���Զ���
			services.AddAuthorization(options =>
			{
				options.AddPolicy(Policies.Admin, builder => builder.RequireRole(Policies.Roles.GeneralAdministrators, Policies.Roles.MedalAdministrators));

				options.AddPolicy(Policies.Issue,
					builder => builder.RequireRole(Policies.Roles.GeneralAdministrators,
						Policies.Roles.MedalAdministrators, Policies.Roles.MedalOperators));

				options.AddPolicy(Policies.Review, builder => builder.RequireRole(Policies.Roles.GeneralAdministrators, Policies.Roles.MedalAdministrators, Policies.Roles.MedalOperators, Policies.Roles.MedalReviewers));

				options.AddPolicy(Policies.Edit, builder => builder.RequireRole(Policies.Roles.GeneralAdministrators, Policies.Roles.MedalAdministrators, Policies.Roles.MedalOperators, Policies.Roles.MedalEditors));
			});

			services.AddExternalSignInManager();
		}

		/// <summary>
		/// ����Ӧ�ó���ִ�����̡�
		/// </summary>
		/// <param name="app">Ӧ�ó������</param>
		/// <param name="env">����������Ϣ��</param>
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

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseRequestLocalization(options =>
			{
				options.DefaultRequestCulture = new RequestCulture("zh-CN", "zh-CN");

				options.AddSupportedCultures("zh-CN-Hans", "zh-CN", "zh-Hans", "zh");
				options.AddSupportedUICultures("zh-CN-Hans", "zh-CN", "zh-Hans", "zh");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
