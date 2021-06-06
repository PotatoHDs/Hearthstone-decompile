using System.Collections.Generic;
using System.Linq;

namespace Hearthstone.Progression
{
	public static class Miscellaneous
	{
		public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
		{
			return source.Select((T item, int index) => (item, index));
		}
	}
}
