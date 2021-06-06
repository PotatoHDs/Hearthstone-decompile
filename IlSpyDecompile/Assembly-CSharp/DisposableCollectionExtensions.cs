using System;
using System.Collections.Generic;

public static class DisposableCollectionExtensions
{
	public static void SetOrReplaceDisposable<K, V>(this Map<K, V> map, K key, V newValue) where V : IDisposable
	{
		if (map != null)
		{
			if (map.TryGetValue(key, out var value))
			{
				value?.Dispose();
			}
			map[key] = newValue;
		}
	}

	public static void DisposeValuesAndClear<K, V>(this Map<K, V> map) where V : IDisposable
	{
		if (map == null)
		{
			return;
		}
		foreach (V value in map.Values)
		{
			value?.Dispose();
		}
		map.Clear();
	}

	public static void DisposeValuesAndClear<V>(this IList<V> list) where V : IDisposable
	{
		if (list == null)
		{
			return;
		}
		foreach (V item in list)
		{
			item?.Dispose();
		}
		list.Clear();
	}

	public static void DisposeAndRemoveAt<V>(this IList<V> list, int index) where V : IDisposable
	{
		if (list == null || list.Count <= index)
		{
			return;
		}
		V val = list[index];
		ref V reference = ref val;
		V val2 = default(V);
		if (val2 == null)
		{
			val2 = reference;
			reference = ref val2;
			if (val2 == null)
			{
				goto IL_0044;
			}
		}
		reference.Dispose();
		goto IL_0044;
		IL_0044:
		list.RemoveAt(index);
	}
}
