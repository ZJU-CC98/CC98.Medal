using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC98.Medal.Data
{
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
		public string Name { get; set; } = null!;

		/// <summary>
		/// 获取或设置该勋章的描述。
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// 获取或设置该勋章对应的图像的路径。
		/// </summary>
		[Required]
		[DataType(DataType.ImageUrl)]
		public string ImageUri { get; set; } = null!;

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
}