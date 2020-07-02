using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CC98.Medal.Data;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CC98.Medal.Models
{
	/// <summary>
	/// 显示勋章列表时 JavaScript 代码需要使用的数据结构。
	/// </summary>
	public class MedalCategoryTreeViewModel
	{
		/// <summary>
		/// 显示的文字。
		/// </summary>
		[JsonPropertyName("text")]
		public string Text { get; set; } = null!;

		/// <summary>
		/// 节点 ID。
		/// </summary>
		[JsonPropertyName("id")]
		public string? Id { get; set; }


		/// <summary>
		/// 链接地址。
		/// </summary>
		[JsonPropertyName("href")]
		public string? LinkUri { get; set; }

		/// <summary>
		/// CSS 类名称。
		/// </summary>
		[JsonPropertyName("class")]
		public string? CssClass { get; set; }

		/// <summary>
		/// 图标的 CSS 类名称。
		/// </summary>
		[JsonPropertyName("icon")]
		public string? IconClass { get; set; }

		/// <summary>
		/// 下层节点的集合。
		/// </summary>
		[JsonPropertyName("nodes")]
		public MedalCategoryTreeViewModel[] Children { get; set; } = null!;
	}
}
