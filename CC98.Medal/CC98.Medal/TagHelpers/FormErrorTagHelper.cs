using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CC98.Medal.TagHelpers
{
	[HtmlTargetElement("form")]
	public class FormErrorTagHelper : TagHelper
	{
		[ViewContext]
		[HtmlAttributeNotBound]
		[UsedImplicitly(ImplicitUseKindFlags.Assign)]
		public ViewContext ViewContext { get; set; } = null!;

		/// <inheritdoc />
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			base.Process(context, output);

			// Add error class
			if (!ViewContext.ModelState.IsValid)
			{
				output.AddClass("error", HtmlEncoder.Default);
			}
		}
	}
}
