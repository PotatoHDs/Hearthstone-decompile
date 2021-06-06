using System.Collections.Generic;

public static class ListUtils
{
	public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> x)
	{
		foreach (IEnumerable<T> item in x)
		{
			foreach (T item2 in item)
			{
				yield return item2;
			}
		}
	}
}
