using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Sakura.AspNetCore.Mvc;

namespace CC98.Medal.Services
{
	public class HtmlContentSerializer : IObjectSerializer
	{

		private static JsonSerializerOptions Options { get; } = CreateSerializerOptions();
		private static JsonSerializerOptions CreateSerializerOptions()
		{
			var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
			options.Converters.Add(new HtmlContentConverter());

			return options;
		}


		/// <inheritdoc />
		public string Serialize(object obj)
		{
			var info = new SerializedObjectInfo
			{
				TypeName = obj.GetType().AssemblyQualifiedName,
				Value = JsonSerializer.Serialize(obj, Options)
			};

			return JsonSerializer.Serialize(info, Options);
		}

		/// <inheritdoc />
		public object Deserialize(string obj)
		{
			var info = JsonSerializer.Deserialize<SerializedObjectInfo>(obj, Options)!;
			return JsonSerializer.Deserialize(info.Value, Type.GetType(info.TypeName)!, Options)!;
		}
	}
}
