using System;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class AssetLoadingHelper
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000339 RID: 825 RVA: 0x00014250 File Offset: 0x00012450
	// (remove) Token: 0x0600033A RID: 826 RVA: 0x00014288 File Offset: 0x00012488
	public event EventHandler AssetLoadingComplete;

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x0600033B RID: 827 RVA: 0x000142BD File Offset: 0x000124BD
	public int AssetsLoading
	{
		get
		{
			return this.m_AssetsLoading;
		}
	}

	// Token: 0x0600033C RID: 828 RVA: 0x000142C5 File Offset: 0x000124C5
	public bool AddAssetToLoad(int assetCount = 1)
	{
		this.m_AssetsLoading += assetCount;
		return true;
	}

	// Token: 0x0600033D RID: 829 RVA: 0x000142D8 File Offset: 0x000124D8
	public void AssetLoadCompleted()
	{
		if (this.m_AssetsLoading > 0)
		{
			this.m_AssetsLoading--;
			if (this.m_AssetsLoading == 0 && this.AssetLoadingComplete != null)
			{
				this.AssetLoadingComplete(this, EventArgs.Empty);
				return;
			}
		}
		else
		{
			Debug.LogError("AssetLoadCompleted() called when no assets left.");
		}
	}

	// Token: 0x04000253 RID: 595
	private int m_AssetsLoading;
}
