using System.Collections.Generic;
using System.Linq;

namespace Hearthstone.UI
{
	public static class DataModelListExtensions
	{
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
