using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sakura.AspNetCore.Mvc;
using Sakura.AspNetCore.Mvc.Internal;

namespace CC98.Medal.TagHelpers
{
	public class SemanticUIPagerHtmlGenerator : IPagerHtmlGenerator
	{
		/// <inheritdoc />
		public IHtmlContent GeneratePager(PagerRenderingList list, PagerGenerationContext context)
		{
			var container = new TagBuilder("div");
			container.AddCssClass(" ui pagination menu");

			foreach (var item in list.Items)
			{
				container.InnerHtml.AppendHtml(GenerateItemHtml(item));
			}

			return container;
		}

		private IHtmlContent GenerateItemHtml(PagerRenderingItem item)
		{
			// A tag
			var tag = new TagBuilder("a");

			// default class
			tag.AddCssClass("item");

			// active/disabled class
			switch (item.State)
			{
				case PagerRenderingItemState.Active:
					tag.AddCssClass("active");
					break;
				case PagerRenderingItemState.Disabled:
					tag.AddCssClass("disabled");
					break;
			}

			// link
			if (!string.IsNullOrEmpty(item.Link))
			{
				tag.Attributes.Add("href", item.Link);
			}

			// inner content
			tag.InnerHtml.SetHtmlContent(item.Content);

			return tag;
		}
	}
}
