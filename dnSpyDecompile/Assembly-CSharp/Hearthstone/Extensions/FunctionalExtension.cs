using System;
using System.Collections.Generic;
using Hearthstone.Util;

namespace Hearthstone.Extensions
{
	// Token: 0x0200107A RID: 4218
	public static class FunctionalExtension
	{
		// Token: 0x0600B63A RID: 46650 RVA: 0x0037E724 File Offset: 0x0037C924
		public static void Accumulate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
		{
			FunctionalUtil.Accumulate<TSource, TAccumulate>(source, seed, func);
		}

		// Token: 0x0600B63B RID: 46651 RVA: 0x0037E72E File Offset: 0x0037C92E
		public static void Zip<T1, T2>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2, Action<T1, T2> action)
		{
			FunctionalUtil.Zip<T1, T2>(collection1, collection2, action);
		}

		// Token: 0x0600B63C RID: 46652 RVA: 0x0037E738 File Offset: 0x0037C938
		public static void Zip<T1, T2, T3>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3, Action<T1, T2, T3> action)
		{
			FunctionalUtil.Zip<T1, T2, T3>(collection1, collection2, collection3, action);
		}

		// Token: 0x0600B63D RID: 46653 RVA: 0x0037E743 File Offset: 0x0037C943
		public static void Zip<T1, T2, T3, T4>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2, IEnumerable<T3> collection3, IEnumerable<T4> collection4, Action<T1, T2, T3, T4> action)
		{
			FunctionalUtil.Zip<T1, T2, T3, T4>(collection1, collection2, collection3, collection4, action);
		}
	}
}
