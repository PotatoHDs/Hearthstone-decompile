using System;
using System.Collections.Generic;
using Hearthstone.Util;

namespace Hearthstone.Extensions
{
	public static class FunctionalExtension
	{
		public static void Accumulate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
		{
			FunctionalUtil.Accumulate(source, seed, func);
		}

		public static void Zip<T1, T2>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2, Action<T1, T2> action)
		{
			FunctionalUtil.Zip(collection1, collection2, action);
		}

		public static void Zip<T1, T2, T3>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3, Action<T1, T2, T3> action)
		{
			FunctionalUtil.Zip(collection1, collection2, collection3, action);
		}

		public static void Zip<T1, T2, T3, T4>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3, IEnumerable<T4> collection4, Action<T1, T2, T3, T4> action)
		{
			FunctionalUtil.Zip(collection1, collection2, collection3, collection4, action);
		}
	}
}
