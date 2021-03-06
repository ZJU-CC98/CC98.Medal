﻿using System;
using System.Linq;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace CC98.Medal.Data
{
	/// <summary>
	/// 勋章中心数据库上下文对象。
	/// </summary>
	public class CC98MedalDbContext : DbContext
	{
		/// <summary>
		/// 初始化 <see cref="CC98MedalDbContext"/> 对象的新实例。
		/// </summary>
		/// <param name="options">创建数据库上下文时使用的选项。</param>
		[UsedImplicitly]
		public CC98MedalDbContext(DbContextOptions<CC98MedalDbContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// 获取或设置系统中包含的勋章的集合。
		/// </summary>
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public virtual DbSet<Medal> Medals { get; set; } = null!;

		/// <summary>
		/// 获取或设置系统中包含的用户的集合。
		/// </summary>
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public virtual DbSet<User> Users { get; set; } = null!;

		/// <summary>
		/// 获取或设置系统中包含的勋章类别的集合。
		/// </summary>
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public virtual DbSet<MedalCategory> MedalCategories { get; set; } = null!;

		/// <summary>
		/// 获取或设置系统中包含的勋章颁发记录的集合。
		/// </summary>
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public virtual DbSet<MedalIssueRecord> MedalIssueRecords { get; set; } = null!;

		/// <summary>
		/// 获取或设置系统中包含的用户勋章拥有情况的集合。
		/// </summary>
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public virtual DbSet<UserMedalOwnership> UserMedalOwnerships { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// 复合主键
			modelBuilder.Entity<UserMedalOwnership>().HasKey(i => new { i.UserId, i.MedalId });

			// 忽略 User 表构建
			modelBuilder.Entity<User>().ToTable("Users", t => t.ExcludeFromMigrations());
		}
	}
}
