using System;
using UnityEngine;

// Token: 0x0200085A RID: 2138
[Serializable]
public class ScriptableAssetMap : ScriptableObject
{
	// Token: 0x04005BD1 RID: 23505
	public const string CARDS_MAP_PATH = "Assets/AssetManifest/AssetMaps/cards_map.asset";

	// Token: 0x04005BD2 RID: 23506
	public ScriptableAssetMap.SerializableMap map = new ScriptableAssetMap.SerializableMap();

	// Token: 0x02002457 RID: 9303
	[Serializable]
	public class SerializableMap : SerializableDictionary<string, string>
	{
	}
}
