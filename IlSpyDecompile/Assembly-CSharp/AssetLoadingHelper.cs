using System;
using UnityEngine;

public class AssetLoadingHelper
{
	private int m_AssetsLoading;

	public int AssetsLoading => m_AssetsLoading;

	public event EventHandler AssetLoadingComplete;

	public bool AddAssetToLoad(int assetCount = 1)
	{
		m_AssetsLoading += assetCount;
		return true;
	}

	public void AssetLoadCompleted()
	{
		if (m_AssetsLoading > 0)
		{
			m_AssetsLoading--;
			if (m_AssetsLoading == 0 && this.AssetLoadingComplete != null)
			{
				this.AssetLoadingComplete(this, EventArgs.Empty);
			}
		}
		else
		{
			Debug.LogError("AssetLoadCompleted() called when no assets left.");
		}
	}
}
