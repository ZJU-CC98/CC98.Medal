using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CC98.Medal
{
	/// <summary>
	/// 提供对于数据序列化和反序列化的帮助方法。该类型为静态类型。
	/// </summary>
	public static class SerializationHelper
	{
		/// <summary>
		/// 尝试序列化一个值对象。如果无法进行序列化，则返回给定的默认值。
		/// </summary>
		/// <typeparam name="T">要序列化的值的类型。</typeparam>
		/// <param name="value">要序列化的值。</param>
		/// <param name="defaultValue">如果序列化失败，返回的默认值。</param>
		/// <returns>如果序列化成功，则返回序列化后的文字；否则，返回 <paramref name="defaultValue"/> 的值。</returns>
		public static string? TrySerialize<T>(T value, string? defaultValue = default)
		{
			try
			{
				return JsonSerializer.Serialize(value);
			}
			catch (JsonException)
			{
				return defaultValue;
			}
		}

		/// <summary>
		/// 尝试从文本中反序列化一个值对象。如果无法进行反序列化，则返回给定的默认值。
		/// </summary>
		/// <typeparam name="T">要反序列化的值的类型。</typeparam>
		/// <param name="value">要反序列化的文本。</param>
		/// <param name="defaultValue">如果反序列化失败，返回的默认值。</param>
		/// <returns>如果反序列化成功，则返回反序列化后的值；否则，返回 <paramref name="defaultValue"/> 的值。</returns>
		[CanBeNull]
		[return: MaybeNull]
		public static T TryDeserialize<T>(string? value, [CanBeNull]T defaultValue = default)
		{
			try
			{
				return JsonSerializer.Deserialize<T>(value ?? string.Empty);
			}
			catch (JsonException)
			{
				return defaultValue!;
			}
		}
	}
}
