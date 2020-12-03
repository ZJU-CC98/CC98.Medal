using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CC98.Medal.Models
{
	public class EnumSelectorViewModel
	{
		/// <summary>
		/// 列表数据。
		/// </summary>
		public IEnumerable<EnumSelectorItem> Items { get; set; } = null!;

		/// <summary>
		/// 关联的后端模型表达式。
		/// </summary>
		public ModelExpression AspFor { get; set; } = null!;
	}

	/// <summary>
	/// 枚举选择器中的单个选项的信息。
	/// </summary>
	public class EnumSelectorItem
	{
		/// <summary>
		/// 选项的实际值。
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// 选项的名称。
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 选项的描述。
		/// </summary>
		public string? Description { get; set; }

		public EnumSelectorItem()
		{
			Name = null!;
			Value = null!;
		}

		public EnumSelectorItem(string value, string name, string? description)
		{
			Value = value;
			Name = name;
			Description = description;
		}
	}
}
