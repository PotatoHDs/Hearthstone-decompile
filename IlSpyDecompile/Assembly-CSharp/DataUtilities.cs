using System.Collections.Generic;

public static class DataUtilities
{
	public static Dictionary<V, K> Reverse<K, V>(this Dictionary<K, V> source)
	{
		Dictionary<V, K> dictionary = new Dictionary<V, K>();
		foreach (K key in source.Keys)
		{
			dictionary[source[key]] = key;
		}
		return dictionary;
	}
}
