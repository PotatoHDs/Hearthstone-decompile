using System;
using System.Collections;
using PegasusUtil;
using UnityEngine;

// Token: 0x0200006E RID: 110
[CustomEditClass]
public class BaconScene : PegasusScene
{
	// Token: 0x06000634 RID: 1588 RVA: 0x00024744 File Offset: 0x00022944
	private void Start()
	{
		Network.Get().RegisterNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, new Network.NetHandler(this.OnBaconRatingInfo), null);
		Network.Get().RequestBaconRatingInfo();
		Network.Get().RegisterNetHandler(BattlegroundsPremiumStatusResponse.PacketID.ID, new Network.NetHandler(this.OnBaconPremiumStatus), null);
		Network.Get().RequestBattlegroundsPremiumStatus();
		GameSaveDataManager.Get().Request(GameSaveKeyId.BACON, new GameSaveDataManager.OnRequestDataResponseDelegate(this.OnGameSaveDataReceived));
		base.StartCoroutine(this.NotifySceneLoadedWhenReady());
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x00019DD3 File Offset: 0x00017FD3
	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool IsUnloading()
	{
		return false;
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x000247D4 File Offset: 0x000229D4
	public override void Unload()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			BnetBar bnetBar = BnetBar.Get();
			if (bnetBar != null)
			{
				bnetBar.ToggleActive(true);
			}
		}
		if (this.m_baconDisplayRoot != null)
		{
			UnityEngine.Object.Destroy(this.m_baconDisplayRoot.gameObject);
		}
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x00024821 File Offset: 0x00022A21
	private void OnScreenPrefabLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_screenPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError(string.Format("BaconScene.OnScreenLoaded() - failed to load screen {0}", assetRef));
			return;
		}
		this.m_baconDisplayRoot = go;
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0002484B File Offset: 0x00022A4B
	private void OnBaconRatingInfo()
	{
		Network.Get().RemoveNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, new Network.NetHandler(this.OnBaconRatingInfo));
		this.m_ratingInfoReceived = true;
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x00024875 File Offset: 0x00022A75
	private void OnBaconPremiumStatus()
	{
		Network.Get().RemoveNetHandler(BattlegroundsPremiumStatusResponse.PacketID.ID, new Network.NetHandler(this.OnBaconPremiumStatus));
		this.m_premiumInfoReceived = true;
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0002489F File Offset: 0x00022A9F
	private void OnGameSaveDataReceived(bool success)
	{
		this.m_gameSaveDataReceived = true;
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x000248A8 File Offset: 0x00022AA8
	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!this.m_ratingInfoReceived)
		{
			yield return null;
		}
		while (!this.m_premiumInfoReceived)
		{
			yield return null;
		}
		while (!this.m_gameSaveDataReceived)
		{
			yield return null;
		}
		AssetLoader.Get().InstantiatePrefab(this.m_screenPrefab, new PrefabCallback<GameObject>(this.OnScreenPrefabLoaded), null, AssetLoadingOptions.None);
		while (!this.m_screenPrefabLoaded)
		{
			yield return null;
		}
		while (this.m_baconDisplayRoot == null)
		{
			yield return null;
		}
		while (this.m_baconDisplayRoot.GetComponentInChildren<BaconDisplay>() == null)
		{
			yield return null;
		}
		this.m_baconDisplay = this.m_baconDisplayRoot.GetComponentInChildren<BaconDisplay>();
		while (!this.m_baconDisplay.IsFinishedLoading)
		{
			yield return null;
		}
		SceneMgr.Get().NotifySceneLoaded();
		yield break;
	}

	// Token: 0x04000452 RID: 1106
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_screenPrefab;

	// Token: 0x04000453 RID: 1107
	private bool m_screenPrefabLoaded;

	// Token: 0x04000454 RID: 1108
	private bool m_ratingInfoReceived;

	// Token: 0x04000455 RID: 1109
	private bool m_premiumInfoReceived;

	// Token: 0x04000456 RID: 1110
	private bool m_gameSaveDataReceived;

	// Token: 0x04000457 RID: 1111
	private GameObject m_baconDisplayRoot;

	// Token: 0x04000458 RID: 1112
	private BaconDisplay m_baconDisplay;
}
