using System;
using bgs;
using UnityEngine;

// Token: 0x02000609 RID: 1545
public class UnityJsonSerializer : IJsonSerializer
{
	// Token: 0x0600568A RID: 22154 RVA: 0x001C5E51 File Offset: 0x001C4051
	public T Deserialize<T>(string json)
	{
		return JsonUtility.FromJson<T>(json);
	}

	// Token: 0x0600568B RID: 22155 RVA: 0x001C5E59 File Offset: 0x001C4059
	public string Serialize(object obj)
	{
		return JsonUtility.ToJson(obj);
	}
}
