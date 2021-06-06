using System.Collections.Generic;

public static class DictionaryExtension
{
	public static void Union<K, V>(this Dictionary<K, V> source, Dictionary<K, V> other)
	{
		foreach (KeyValuePair<K, V> item in other)
		{
			source[item.Key] = item.Value;
		}
	}

	public static void Union<K, V>(this Map<K, V> source, Map<K, V> other)
	{
		foreach (KeyValuePair<K, V> item in other)
		{
			source[item.Key] = item.Value;
		}
	}
}
