using Sakura.AspNetCore;
using Sakura.AspNetCore.Mvc;

namespace CC98.Medal.TagHelpers
{
	/// <summary>
	/// 提供将 <see cref="OperationMessageLevel"/> 映射到 SemanticUI 样式的方法。
	/// </summary>
	public class SemanticUIOperationMessageLevelClassMapper : IOperationMessageLevelClassMapper
	{
		public string MapLevel(OperationMessageLevel value, MessageListStyle listStyle)
		{
			return value switch
			{
				OperationMessageLevel.Success => "success",
				OperationMessageLevel.Warning => "warning",
				OperationMessageLevel.Error or OperationMessageLevel.Critical => "error",
				OperationMessageLevel.Info => "info",
				_ => ""
			};
		}
	}
}