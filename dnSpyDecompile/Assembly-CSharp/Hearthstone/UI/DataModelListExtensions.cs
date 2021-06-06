using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearthstone.UI
{
	// Token: 0x02000FE7 RID: 4071
	public static class DataModelListExtensions
	{
		// Token: 0x0600B149 RID: 45385 RVA: 0x0036B6DC File Offset: 0x003698DC
		public static DataModelList<T> ToDataModelList<T>(this IEnumerable<T> source) where T : class
		{
			return source.Aggregate(new DataModelList<T>(), delegate(DataModelList<T> acc, T element)
			{
				acc.Add(element);
				return acc;
			});
		}
	}
}
