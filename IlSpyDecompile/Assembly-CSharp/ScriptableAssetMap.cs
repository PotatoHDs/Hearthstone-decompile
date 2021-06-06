using System;
using UnityEngine;

[Serializable]
public class ScriptableAssetMap : ScriptableObject
{
	[Serializable]
	public class SerializableMap : SerializableDictionary<string, string>
	{
	}

	public const string CARDS_MAP_PATH = "Assets/AssetManifest/AssetMaps/cards_map.asset";

	public SerializableMap map = new SerializableMap();
}
