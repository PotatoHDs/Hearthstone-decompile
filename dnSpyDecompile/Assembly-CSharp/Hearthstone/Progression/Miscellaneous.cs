using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hearthstone.Progression
{
	// Token: 0x02001100 RID: 4352
	public static class Miscellaneous
	{
		// Token: 0x0600BE9E RID: 48798 RVA: 0x003A171F File Offset: 0x0039F91F
		[return: TupleElementNames(new string[]
		{
			"item",
			"index"
		})]
		public static IEnumerable<ValueTuple<T, int>> WithIndex<T>(this IEnumerable<T> source)
		{
			return source.Select((T item, int index) => new ValueTuple<T, int>(item, index));
		}
	}
}
