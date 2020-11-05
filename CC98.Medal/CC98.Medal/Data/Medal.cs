using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

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
		[Display(Name = "勋章名称")]
		public string Name { get; set; } = null!;

		/// <summary>
		/// 获取或设置该勋章的描述。
		/// </summary>
		[Display(Name = "勋章描述")]
		public string? Description { get; set; }

		/// <summary>
		/// 获取或设置该勋章对应的图像的路径。
		/// </summary>
		[Required]
		[DataType(DataType.ImageUrl)]
		[Display(Name = "图片地址")]
		public string ImageUri { get; set; } = null!;

		/// <summary>
		/// 获取或设置单击该勋章时需要跳转的地址。
		/// </summary>
		[DataType(DataType.Url)]
		[Display(Name = "链接地址")]
		public string? LinkUri { get; set; }

		/// <summary>
		/// 获取或设置该勋章关联的类别的标识。
		/// </summary>
		[Display(Name = "勋章分类")]
		public int? CategoryId { get; set; }

		/// <summary>
		/// 获取或设置该勋章关联的类别。如果为 <c>null</c> 则未关联到任何类别。
		/// </summary>
		[ForeignKey(nameof(CategoryId))]
		[Display(Name = "勋章分类")]
		public MedalCategory? Category { get; set; }

		/// <summary>
		/// 获取或设置该勋章的状态。
		/// </summary>
		[Display(Name = "勋章状态")]
		public MedalState State { get; set; }

		/// <summary>
		/// 获取或设置一个值，指示勋章是否可以申请。
		/// </summary>
		[Display(Name = "允许申请")]
		public bool CanApply { get; set; }

		/// <summary>
		/// 获取或设置一个值，指示勋章是否可以购买。
		/// </summary>
		[Display(Name = "允许购买")]
		public bool CanBuy { get; set; }

		/// <summary>
		/// 获取或设置一个值，指示是否要隐藏勋章的拥有者。
		/// </summary>
		[Display(Name = "隐藏拥有者")]
		public bool HideOwners { get; set; }

		/// <summary>
		/// <see cref="BuySettings"/> 内部数据库存储字符串。
		/// </summary>
		[Column(nameof(BuySettings))]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string? BuySettingString { get; set; }

		/// <summary>
		/// 勋章的购买设置。
		/// </summary>
		[IgnoreDataMember]
		[XmlIgnore]
		[NotMapped]
		public MedalBuySetting[] BuySettings
		{
			get => SerializationHelper.TryDeserialize<MedalBuySetting[]>(BuySettingString) ?? Array.Empty<MedalBuySetting>();
			set => BuySettingString = SerializationHelper.TrySerialize(value);
		}
	}
}