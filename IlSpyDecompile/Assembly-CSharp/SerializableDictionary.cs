using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	[SerializeField]
	private List<TKey> keys = new List<TKey>();

	[SerializeField]
	private List<TValue> values = new List<TValue>();

	public void OnBeforeSerialize()
	{
		keys.Clear();
		values.Clear();
		using Enumerator enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			KeyValuePair<TKey, TValue> current = enumerator.Current;
			keys.Add(current.Key);
			values.Add(current.Value);
		}
	}

	public void OnAfterDeserialize()
	{
		Clear();
		if (keys.Count != values.Count)
		{
			throw new Exception($"There are {keys.Count} keys and {values.Count} values after deserialization. Make sure that both kay and value types are serializable");
		}
		for (int i = 0; i < keys.Count; i++)
		{
			Add(keys[i], values[i]);
		}
	}
}
