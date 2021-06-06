using bgs;
using UnityEngine;

public class UnityJsonSerializer : IJsonSerializer
{
	public T Deserialize<T>(string json)
	{
		return JsonUtility.FromJson<T>(json);
	}

	public string Serialize(object obj)
	{
		return JsonUtility.ToJson(obj);
	}
}
