using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Html;

namespace CC98.Medal.Services
{
	/// <summary>
	/// 提供 <see cref="IHtmlContent"/> 的序列化支持。
	/// </summary>
	public class HtmlContentConverter : JsonConverter<IHtmlContent>
	{
		/// <inheritdoc />
		public override IHtmlContent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var str = reader.GetString();
			return new HtmlString(str);
		}

		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, IHtmlContent value, JsonSerializerOptions options)
		{
			var sw = new StringWriter();
			value.WriteTo(sw, HtmlEncoder.Default);

			writer.WriteStringValue(sw.ToString());
			;
		}
	}
}