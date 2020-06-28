using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC98.Medal.Data
{
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