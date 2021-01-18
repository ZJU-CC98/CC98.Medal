using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

using Sakura.AspNetCore;
using Sakura.AspNetCore.Mvc;

namespace CC98.Medal.TagHelpers
{
	/// <summary>
	/// 提供 Semantic UI 风格的消息提示。
	/// </summary>
	public class SemanticUIOperationMessageUIGenerator : IOperationMessageHtmlGenerator
	{
		public SemanticUIOperationMessageUIGenerator(IOperationMessageLevelClassMapper levelClassMapper)
		{
			LevelClassMapper = levelClassMapper;
		}

		/// <summary>
		/// 提供消息等级到样式的映射程序。
		/// </summary>
		private IOperationMessageLevelClassMapper LevelClassMapper { get; }

		/// <inheritdoc />
		public IHtmlContent GenerateList(IEnumerable<OperationMessage> messages, MessageListStyle listStyle, bool useTwoLineMode)
		{
			var root = new TagBuilder("div");

			foreach (var item in messages)
			{
				root.InnerHtml.AppendHtml(GenerateMessageItemCore(item, listStyle));
			}

			return root;
		}

		/// <summary>
		/// 为单个消息生成消息框样式。
		/// </summary>
		/// <param name="message">消息对象。</param>
		/// <param name="listStyle">列表样式。</param>
		/// <returns>表示消息内容的 <see cref="IHtmlContent"/> 对象。</returns>
		private IHtmlContent GenerateMessageItemCore(OperationMessage message, MessageListStyle listStyle)
		{
			var tag = new TagBuilder("div");
			tag.AddCssClass("ui message");
			tag.AddCssClass(LevelClassMapper.MapLevel(message.Level, listStyle));

			var header = new TagBuilder("div");
			header.AddCssClass("header");
			header.InnerHtml.SetHtmlContent(message.Title);

			var content = new TagBuilder("p");
			content.InnerHtml.SetHtmlContent(message.Description);


			// 添加图标
			if (listStyle == MessageListStyle.AlertDialogClosable)
			{
				var icon = new TagBuilder("i");
				icon.AddCssClass("close icon");
				tag.InnerHtml.AppendHtml(icon);
			}

			tag.InnerHtml.AppendHtml(header);
			tag.InnerHtml.AppendHtml(content);


			return tag;
		}
	}
}
