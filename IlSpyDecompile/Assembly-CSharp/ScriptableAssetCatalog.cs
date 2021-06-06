using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScriptableAssetCatalog<T> : ScriptableObject where T : BaseAssetCatalogItem, new()
{
	[SerializeField]
	public List<T> m_assets = new List<T>();

	[SerializeField]
	public List<string> m_bundleNames = new List<string>();

	public bool TryAddAsset(string guid, string bundleName)
	{
		m_assets.Add(new T
		{
			guid = guid,
			bundleId = (string.IsNullOrEmpty(bundleName) ? (-1) : GetOrAssignBundleId(bundleName))
		});
		return true;
	}

	protected int GetOrAssignBundleId(string bundleName)
	{
		int num = m_bundleNames.IndexOf(bundleName);
		if (num >= 0)
		{
			return num;
		}
		m_bundleNames.Add(bundleName);
		return m_bundleNames.Count - 1;
	}
}
[Serializable]
public class ScriptableAssetCatalog : ScriptableAssetCatalog<BaseAssetCatalogItem>
{
}
