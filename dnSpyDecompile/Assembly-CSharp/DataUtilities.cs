using System;
using System.Collections.Generic;

// Token: 0x020009AE RID: 2478
public static class DataUtilities
{
	// Token: 0x060086FD RID: 34557 RVA: 0x002B9814 File Offset: 0x002B7A14
	public static Dictionary<V, K> Reverse<K, V>(this Dictionary<K, V> source)
	{
		Dictionary<V, K> dictionary = new Dictionary<V, K>();
		foreach (K k in source.Keys)
		{
			dictionary[source[k]] = k;
		}
		return dictionary;
	}
}
