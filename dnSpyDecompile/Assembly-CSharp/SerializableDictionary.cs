using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000015 RID: 21
[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	// Token: 0x0600008D RID: 141 RVA: 0x00003878 File Offset: 0x00001A78
	public void OnBeforeSerialize()
	{
		this.keys.Clear();
		this.values.Clear();
		foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
		{
			this.keys.Add(keyValuePair.Key);
			this.values.Add(keyValuePair.Value);
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x000038FC File Offset: 0x00001AFC
	public void OnAfterDeserialize()
	{
		base.Clear();
		if (this.keys.Count != this.values.Count)
		{
			throw new Exception(string.Format("There are {0} keys and {1} values after deserialization. Make sure that both kay and value types are serializable", this.keys.Count, this.values.Count));
		}
		for (int i = 0; i < this.keys.Count; i++)
		{
			base.Add(this.keys[i], this.values[i]);
		}
	}

	// Token: 0x0400005D RID: 93
	[SerializeField]
	private List<TKey> keys = new List<TKey>();

	// Token: 0x0400005E RID: 94
	[SerializeField]
	private List<TValue> values = new List<TValue>();
}
