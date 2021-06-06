using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000909 RID: 2313
[Serializable]
public class ScriptableAssetCatalog<T> : ScriptableObject where T : BaseAssetCatalogItem, new()
{
	// Token: 0x060080D2 RID: 32978 RVA: 0x0029D997 File Offset: 0x0029BB97
	public bool TryAddAsset(string guid, string bundleName)
	{
		List<T> assets = this.m_assets;
		T t = Activator.CreateInstance<T>();
		t.guid = guid;
		t.bundleId = (string.IsNullOrEmpty(bundleName) ? -1 : this.GetOrAssignBundleId(bundleName));
		assets.Add(t);
		return true;
	}

	// Token: 0x060080D3 RID: 32979 RVA: 0x0029D9D4 File Offset: 0x0029BBD4
	protected int GetOrAssignBundleId(string bundleName)
	{
		int num = this.m_bundleNames.IndexOf(bundleName);
		if (num >= 0)
		{
			return num;
		}
		this.m_bundleNames.Add(bundleName);
		return this.m_bundleNames.Count - 1;
	}

	// Token: 0x04006994 RID: 27028
	[SerializeField]
	public List<T> m_assets = new List<T>();

	// Token: 0x04006995 RID: 27029
	[SerializeField]
	public List<string> m_bundleNames = new List<string>();
}
