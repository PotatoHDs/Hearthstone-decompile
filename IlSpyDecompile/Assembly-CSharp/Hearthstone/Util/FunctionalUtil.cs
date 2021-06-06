using System;
using System.Collections.Generic;

namespace Hearthstone.Util
{
	public static class FunctionalUtil
	{
		public static void Accumulate<TSource, TAccumulate>(IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
		{
			TAccumulate arg = seed;
			foreach (TSource item in source)
			{
				arg = func(arg, item);
			}
		}

		public static void Zip<T1, T2>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, Action<T1, T2> action)
		{
			using IEnumerator<T1> enumerator = collection1.GetEnumerator();
			using IEnumerator<T2> enumerator2 = collection2.GetEnumerator();
			while (enumerator.MoveNext() && enumerator2.MoveNext())
			{
				action(enumerator.Current, enumerator2.Current);
			}
		}

		public static void Zip<T1, T2, T3>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3, Action<T1, T2, T3> action)
		{
			using IEnumerator<T1> enumerator = collection1.GetEnumerator();
			using IEnumerator<T2> enumerator2 = collection2.GetEnumerator();
			using IEnumerator<T3> enumerator3 = collection3.GetEnumerator();
			while (enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext())
			{
				action(enumerator.Current, enumerator2.Current, enumerator3.Current);
			}
		}

		public static void Zip<T1, T2, T3, T4>(IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3, IEnumerable<T4> collection4, Action<T1, T2, T3, T4> action)
		{
			using IEnumerator<T1> enumerator = collection1.GetEnumerator();
			using IEnumerator<T2> enumerator2 = collection2.GetEnumerator();
			using IEnumerator<T3> enumerator3 = collection3.GetEnumerator();
			using IEnumerator<T4> enumerator4 = collection4.GetEnumerator();
			while (enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() && enumerator4.MoveNext())
			{
				action(enumerator.Current, enumerator2.Current, enumerator3.Current, enumerator4.Current);
			}
		}
	}
}
