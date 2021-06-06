using System;
using System.Collections.Generic;

// Token: 0x020009B2 RID: 2482
public static class DictionaryExtension
{
	// Token: 0x06008711 RID: 34577 RVA: 0x002B9C80 File Offset: 0x002B7E80
	public static void Union<K, V>(this Dictionary<K, V> source, Dictionary<K, V> other)
	{
		foreach (KeyValuePair<K, V> keyValuePair in other)
		{
			source[keyValuePair.Key] = keyValuePair.Value;
		}
	}

	// Token: 0x06008712 RID: 34578 RVA: 0x002B9CDC File Offset: 0x002B7EDC
	public static void Union<K, V>(this Map<K, V> source, Map<K, V> other)
	{
		foreach (KeyValuePair<K, V> keyValuePair in other)
		{
			source[keyValuePair.Key] = keyValuePair.Value;
		}
	}
}
