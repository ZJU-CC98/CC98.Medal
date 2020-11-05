using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC98.Medal.Data
{
	/// <summary>
	/// 定义勋章的类别。
	/// </summary>
	// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
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
		public virtual IList<MedalCategory> Children { get; set; } = new Collection<MedalCategory>();

		/// <summary>
		/// 获取或设置该类别包含的勋章的集合。
		/// </summary>
		[InverseProperty(nameof(Medal.Category))]
		public virtual IList<Medal> Medals { get; set; } = new Collection<Medal>();

		/// <summary>
		/// 获取或设置勋章分类的名称。
		/// </summary>
		[StringLength(100)]
		[Required]
		public string Name { get; set; } = null!;

		/// <summary>
		/// 获取或设置勋章分类的描述。
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// 获取或设置勋章分类的图标。
		/// </summary>
		[DataType(DataType.ImageUrl)]
		public string? IconUri { get; set; }

		/// <summary>
		/// 排序顺序。更小的数字排序在更前面。
		/// </summary>
		public int SortOrder { get; set; }
	}
}