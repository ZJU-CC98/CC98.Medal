using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC98.Medal
{
	public static class LinqExtensions
	{
		/// <summary>
		/// 层叠选择元素及其所有后代元素的内容。
		/// </summary>
		/// <typeparam name="TSource">元素的类型。</typeparam>
		/// <param name="source">要选择的源序列。</param>
		/// <param name="childrenSelector">从每个元素获得其子元素的方法。</param>
		/// <returns>将每个元素及其所有后代元素组合形成的序列。</returns>
		public static IEnumerable<TSource> SelectCascade<TSource>(this IEnumerable<TSource> source,
			Func<TSource, IEnumerable<TSource>> childrenSelector)
		{
			// ReSharper disable once PossibleMultipleEnumeration
			return source.Concat(source.SelectMany(childrenSelector).SelectCascade(childrenSelector));
		}

		/// <summary>
		/// 层叠选择元素及其所有后代元素的内容。
		/// </summary>
		/// <typeparam name="TSource">元素的类型。</typeparam>
		/// <typeparam name="TResult">结果的类型。</typeparam>
		/// <param name="source">要选择的源序列。</param>
		/// <param name="childrenSelector">从每个元素获得其子元素的方法。</param>
		/// <param name="resultSelector">从每个源选择结果的方法。</param>
		/// <returns>将每个元素及其所有后代元素组合形成的序列。</returns>
		public static IEnumerable<TResult> SelectCascade<TSource, TResult>(this IEnumerable<TSource> source,
			Func<TSource, IEnumerable<TSource>> childrenSelector, Func<TSource, TResult> resultSelector)
		{
			return source.SelectCascade(childrenSelector).Select(resultSelector);
		}
	}
}
