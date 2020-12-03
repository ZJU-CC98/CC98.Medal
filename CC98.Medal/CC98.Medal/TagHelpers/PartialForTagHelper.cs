using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;

namespace CC98.Medal.TagHelpers
{
	// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
	[HtmlTargetElement("partial", Attributes = "for", TagStructure = TagStructure.WithoutEndTag)]
	public class PartialForTagHelper : TagHelper
	{
		[HtmlAttributeName("for")]
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public ModelExpression For { get; set; } = null!;

		[ViewContext]
		[HtmlAttributeNotBound]
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public ViewContext ViewContext { get; set; } = null!;


		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			ViewContext.ViewBag.ModelName = For.Name;
		}

		/// <summary>
		/// 重写执行顺序以便于在默认标签之前运行。
		/// </summary>
		public override int Order => base.Order - 1;
	}
}
