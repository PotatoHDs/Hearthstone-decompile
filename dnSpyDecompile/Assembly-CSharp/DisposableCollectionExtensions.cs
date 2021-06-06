using System;
using System.Collections.Generic;

// Token: 0x020009B5 RID: 2485
public static class DisposableCollectionExtensions
{
	// Token: 0x06008718 RID: 34584 RVA: 0x002B9E70 File Offset: 0x002B8070
	public static void SetOrReplaceDisposable<K, V>(this Map<K, V> map, K key, V newValue) where V : IDisposable
	{
		if (map != null)
		{
			V v;
			if (map.TryGetValue(key, out v) && v != null)
			{
				v.Dispose();
			}
			map[key] = newValue;
		}
	}

	// Token: 0x06008719 RID: 34585 RVA: 0x002B9EA8 File Offset: 0x002B80A8
	public static void DisposeValuesAndClear<K, V>(this Map<K, V> map) where V : IDisposable
	{
		if (map != null)
		{
			foreach (V v in map.Values)
			{
				if (v != null)
				{
					v.Dispose();
				}
			}
			map.Clear();
		}
	}

	// Token: 0x0600871A RID: 34586 RVA: 0x002B9F14 File Offset: 0x002B8114
	public static void DisposeValuesAndClear<V>(this IList<V> list) where V : IDisposable
	{
		if (list != null)
		{
			foreach (V v in list)
			{
				if (v != null)
				{
					v.Dispose();
				}
			}
			list.Clear();
		}
	}

	// Token: 0x0600871B RID: 34587 RVA: 0x002B9F74 File Offset: 0x002B8174
	public static void DisposeAndRemoveAt<V>(this IList<V> list, int index) where V : IDisposable
	{
		if (list != null && list.Count > index)
		{
			V v = list[index];
			ref V ptr = ref v;
			V v2 = default(V);
			if (v2 == null)
			{
				v2 = v;
				ptr = ref v2;
				if (v2 == null)
				{
					goto IL_44;
				}
			}
			ptr.Dispose();
			IL_44:
			list.RemoveAt(index);
		}
	}
}
