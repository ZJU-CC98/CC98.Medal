using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CC98.Medal.TagHelpers
{
	/// <summary>
	/// 当控件关联到验证错误时，将控件转换为错误样式。
	/// </summary>
	[HtmlTargetElement(Attributes = ValidationForAttributeName)]
	public class ErrorInputTagHelper : TagHelper
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="output"></param>
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var key = ValidationFor.Name;
			var keyModelState = ViewContext.ModelState[key];

			// ReSharper disable once ConditionIsAlwaysTrueOrFalse
			var hasError = keyModelState != null && keyModelState.Errors.Any();

			if (hasError)
			{
				output.AddClass(ErrorClass, HtmlEncoder.Default);
			}
		}

		/// <summary>
		/// 定义 <see cref="ValidationFor"/> 属性绑定的 HTML 属性名称。该字段为常量。
		/// </summary>
		public const string ValidationForAttributeName = "asp-validation-style-for";

		/// <summary>
		/// 执行上下文对应的数据上下文对象。
		/// </summary>
		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; } = null!;

		/// <summary>
		/// 控件关联到的数据表达式。
		/// </summary>
		[HtmlAttributeName(ValidationForAttributeName)]
		public ModelExpression ValidationFor { get; set; } = null!;



		/// <summary>
		/// 定义 <see cref="ErrorClass"/> 属性绑定的 HTML 属性名称。该字段为常量。
		/// </summary>
		public const string ErrorClassAttributeName = "asp-error-class";

		/// <summary>
		/// 当对应的数据发生错误时，要为当前 HTML 元素添加的错误类名称。该属性值默认为 “error”。
		/// </summary>
		[HtmlAttributeName(ErrorClassAttributeName)]
		public string ErrorClass { get; set; } = "error";
	}
}
