using System;
using System.Collections;
using PegasusShared;
using UnityEngine;

// Token: 0x020002F1 RID: 753
[CustomEditClass]
public class FiresideGatheringScene : PegasusScene
{
	// Token: 0x0600281E RID: 10270 RVA: 0x000CA090 File Offset: 0x000C8290
	private void Start()
	{
		AssetLoader.Get().InstantiatePrefab(this.m_FiresideGatheringPrefab, new PrefabCallback<GameObject>(this.OnFiresideGatheringPrefabLoaded), null, AssetLoadingOptions.None);
		TavernBrawlManager.Get().CurrentBrawlType = BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING;
		if (TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING))
		{
			TavernBrawlManager.Get().EnsureAllDataReady(new TavernBrawlManager.CallbackEnsureServerDataReady(this.OnFiresideBrawlServerDataReady));
			return;
		}
		this.OnFiresideBrawlServerDataReady();
	}

	// Token: 0x0600281F RID: 10271 RVA: 0x00019DD3 File Offset: 0x00017FD3
	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x06002820 RID: 10272 RVA: 0x000CA0FB File Offset: 0x000C82FB
	public override bool IsUnloading()
	{
		return this.m_unloading;
	}

	// Token: 0x06002821 RID: 10273 RVA: 0x000CA103 File Offset: 0x000C8303
	public override void Unload()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			BnetBar.Get().ToggleActive(true);
		}
		this.m_unloading = true;
		this.m_unloading = false;
	}

	// Token: 0x06002822 RID: 10274 RVA: 0x000CA12A File Offset: 0x000C832A
	public void OnFiresideGatheringPrefabLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_firesideGatheringPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError(string.Format("TavernBrawlScene.OnTavernBrawlLoaded() - failed to load screen {0}", assetRef));
			return;
		}
	}

	// Token: 0x06002823 RID: 10275 RVA: 0x000CA150 File Offset: 0x000C8350
	private void OnFiresideBrawlServerDataReady()
	{
		base.StartCoroutine(this.NotifySceneLoadedWhenReady());
		TavernBrawlManager.Get().CurrentBrawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL;
		if (TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			TavernBrawlManager.Get().EnsureAllDataReady(new TavernBrawlManager.CallbackEnsureServerDataReady(this.OnTavernBrawlServerDataReady));
		}
		else
		{
			this.OnTavernBrawlServerDataReady();
		}
		CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded(new CollectionManager.DelOnAllDeckContents(this.OnCollectionDataReady));
	}

	// Token: 0x06002824 RID: 10276 RVA: 0x000CA1B7 File Offset: 0x000C83B7
	private void OnTavernBrawlServerDataReady()
	{
		this.m_tavernBrawlLoaded = true;
		this.OnAllPresenceDataLoaded();
	}

	// Token: 0x06002825 RID: 10277 RVA: 0x000CA1C6 File Offset: 0x000C83C6
	private void OnCollectionDataReady()
	{
		this.m_collectionLoaded = true;
		this.OnAllPresenceDataLoaded();
	}

	// Token: 0x06002826 RID: 10278 RVA: 0x000CA1D5 File Offset: 0x000C83D5
	private void OnAllPresenceDataLoaded()
	{
		if (this.m_collectionLoaded && this.m_tavernBrawlLoaded)
		{
			FiresideGatheringManager.Get().UpdateDeckValidity();
		}
	}

	// Token: 0x06002827 RID: 10279 RVA: 0x000CA1F1 File Offset: 0x000C83F1
	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!this.m_firesideGatheringPrefabLoaded)
		{
			yield return 0;
		}
		SceneMgr.Get().NotifySceneLoaded();
		yield break;
	}

	// Token: 0x040016C8 RID: 5832
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_FiresideGatheringPrefab;

	// Token: 0x040016C9 RID: 5833
	private bool m_unloading;

	// Token: 0x040016CA RID: 5834
	private bool m_collectionLoaded;

	// Token: 0x040016CB RID: 5835
	private bool m_tavernBrawlLoaded;

	// Token: 0x040016CC RID: 5836
	private bool m_firesideGatheringPrefabLoaded;
}
