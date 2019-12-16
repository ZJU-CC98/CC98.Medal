using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
		public virtual DbSet<Medal> Medals { get; set; }

		/// <summary>
		/// 获取或设置系统中包含的勋章类别的集合。
		/// </summary>
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public virtual DbSet<MedalCategory> MedalCategories { get; set; }
	}

	/// <summary>
	/// 表示勋章的购买设置。
	/// </summary>
	public class MedalOrderSetting
	{
		/// <summary>
		/// 获取或设置购买需要的价格。
		/// </summary>
		[Range(0, int.MaxValue)]
		public int Price { get; set; }

		/// <summary>
		/// 获取或设置购买的有效时间。设定为 <c>null</c> 则永久有效。
		/// </summary>
		[Range(0, int.MaxValue)]
		public int? ExpireDays { get; set; }
	}

	/// <summary>
	/// 表示一个勋章。
	/// </summary>
	public class Medal
	{
		/// <summary>
		/// 获取或设置该勋章的标识。
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// 获取或设置该勋章的名称。
		/// </summary>
		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		/// <summary>
		/// 获取或设置该勋章的描述。
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// 获取或设置该勋章对应的图像的路径。
		/// </summary>
		[Required]
		[DataType(DataType.ImageUrl)]
		public string ImageUri { get; set; }

		/// <summary>
		/// 获取或设置单击该勋章时需要跳转的地址。
		/// </summary>
		[DataType(DataType.Url)]
		public string? LinkUri { get; set; }

		/// <summary>
		/// 获取或设置该勋章关联的类别的标识。
		/// </summary>
		public int? CategoryId { get; set; }

		/// <summary>
		/// 获取或设置该勋章关联的类别。如果为 <c>null</c> 则未关联到任何类别。
		/// </summary>
		[ForeignKey(nameof(CategoryId))]
		public MedalCategory? Category { get; set; }
	}

	/// <summary>
	/// 定义勋章的类别。
	/// </summary>
	public class MedalCategory
	{
		/// <summary>
		/// 获取或设置该类别的标识。
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// 获取或设置该类别的上级类别的标识。
		/// </summary>
		public int? ParentId { get; set; }

		/// <summary>
		/// 获取或设置该类别的上级类别。
		/// </summary>
		[ForeignKey(nameof(ParentId))]
		public MedalCategory? Parent { get; set; }

		/// <summary>
		/// 获取或设置该类别包含的下级类别的集合。
		/// </summary>
		[InverseProperty(nameof(Parent))]
		public IList<MedalCategory> Children { get; set; } = new Collection<MedalCategory>();

		/// <summary>
		/// 获取或设置该类别包含的勋章的集合。
		/// </summary>
		[InverseProperty(nameof(Medal.Category))]
		public IList<Medal> Medals { get; set; } = new Collection<Medal>();
	}
}
