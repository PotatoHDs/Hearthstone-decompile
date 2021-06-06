using System;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class CreditsScene : PegasusScene
{
	// Token: 0x06001528 RID: 5416 RVA: 0x00079291 File Offset: 0x00077491
	protected override void Awake()
	{
		base.Awake();
		AssetLoader.Get().InstantiatePrefab("Credits.prefab:4ffef537c5070494eb038d15271a6ebe", new PrefabCallback<GameObject>(this.OnUIScreenLoaded), null, AssetLoadingOptions.None);
		if (InactivePlayerKicker.Get() != null)
		{
			InactivePlayerKicker.Get().SetShouldCheckForInactivity(false);
		}
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x000792CE File Offset: 0x000774CE
	public override bool IsUnloading()
	{
		return this.m_unloading;
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x000792D6 File Offset: 0x000774D6
	public override void Unload()
	{
		this.m_unloading = true;
		if (InactivePlayerKicker.Get() != null)
		{
			InactivePlayerKicker.Get().SetShouldCheckForInactivity(true);
		}
		this.m_unloading = false;
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x000792F8 File Offset: 0x000774F8
	private void OnUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("CreditsScene.OnUIScreenLoaded() - failed to load screen {0}", assetRef));
			return;
		}
	}

	// Token: 0x04000E2B RID: 3627
	private bool m_unloading;
}
