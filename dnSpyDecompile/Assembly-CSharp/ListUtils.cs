using System;
using System.Collections.Generic;

// Token: 0x020009BE RID: 2494
public static class ListUtils
{
	// Token: 0x06008835 RID: 34869 RVA: 0x002BDF5C File Offset: 0x002BC15C
	public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> x)
	{
		foreach (IEnumerable<T> enumerable in x)
		{
			foreach (T t in enumerable)
			{
				yield return t;
			}
			IEnumerator<T> enumerator2 = null;
		}
		IEnumerator<IEnumerable<T>> enumerator = null;
		yield break;
		yield break;
	}
}
