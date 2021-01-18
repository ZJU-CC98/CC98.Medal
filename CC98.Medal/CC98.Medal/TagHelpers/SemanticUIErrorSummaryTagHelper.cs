using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace CC98.Medal.TagHelpers
{
	/// <summary>
	/// 提供 Semantic UI 风格的错误摘要。 
	/// </summary>
	[HtmlTargetElement("error-summary", TagStructure = TagStructure.WithoutEndTag)]
	[UsedImplicitly]
	public class SemanticUIErrorSummaryTagHelper : TagHelper
	{

		[ViewContext]
		[HtmlAttributeNotBound]
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public ViewContext ViewContext { get; set; } = null!;

		/// <summary>
		/// 错误验证模式。
		/// </summary>
		[HtmlAttributeName("mode")]
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public ValidationSummary Mode { get; set; } = ValidationSummary.ModelOnly;

		/// <summary>
		/// 错误验证消息的标题。
		/// </summary>
		[HtmlAttributeName("title")]
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public string? Title { get; set; }

		/// <inheritdoc />
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			base.Process(context, output);

			var errors = GetModelErrors().ToArray();

			// 没有错误
			if (errors.Length == 0)
			{
				output.SuppressOutput();
			}

			// 去掉标签
			output.TagName = null;
			output.Content.SetHtmlContent(GenerateErrorMessage(Title, errors));
		}


		/// <summary>
		/// 获取当前关联的所有模型错误消息。
		/// </summary>
		/// <returns>模型错误消息。</returns>
		private IEnumerable<string> GetModelErrors()
		{
			IEnumerable<ModelStateEntry> GetEntries()
			{
				switch (Mode)
				{
					case ValidationSummary.All:
						return ViewContext.ModelState.Values
							.SelectCascade(i => i.Children ?? Enumerable.Empty<ModelStateEntry>());
					case ValidationSummary.ModelOnly:
						return new[] { ViewContext.ModelState[string.Empty] };
					default:
						return Enumerable.Empty<ModelStateEntry>();
				}
			}

			return GetEntries()
				// ReSharper disable once ConditionIsAlwaysTrueOrFalse
				.Where(i => i != null)
				.SelectMany(i => i.Errors, (entry, error) => error.ErrorMessage);
		}


		/// <summary>
		/// 生成错误消息 HTML 界面。
		/// </summary>
		/// <param name="title">错误标题。</param>
		/// <param name="errors">错误内容列表。</param>
		/// <returns>表示错误消息的 HTML 界面。</returns>
		private static IHtmlContent GenerateErrorMessage(string? title, IEnumerable<string> errors)
		{
			var container = new TagBuilder("div");
			container.AddCssClass("ui error message");

			if (!string.IsNullOrEmpty(title))
			{
				var header = new TagBuilder("div");
				header.AddCssClass("header");
				header.InnerHtml.SetContent(title);

				container.InnerHtml.AppendHtml(header);
			}

			var list = new TagBuilder("ul");
			list.AddCssClass("list");

			foreach (var error in errors)
			{
				var item = new TagBuilder("li");
				item.InnerHtml.SetContent(error);

				list.InnerHtml.AppendHtml(item);
			}

			container.InnerHtml.AppendHtml(list);

			return container;
		}
	}
}
