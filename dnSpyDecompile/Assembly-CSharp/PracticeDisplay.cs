using System;
using UnityEngine;

// Token: 0x0200061E RID: 1566
public class PracticeDisplay : MonoBehaviour
{
	// Token: 0x060057D2 RID: 22482 RVA: 0x001CB718 File Offset: 0x001C9918
	private void Awake()
	{
		PracticeDisplay.s_instance = this;
		GameObject gameObject = (GameObject)GameUtils.Instantiate(this.m_practicePickerTrayPrefab, this.m_practicePickerTrayContainer, false);
		this.m_practicePickerTray = gameObject.GetComponent<PracticePickerTrayDisplay>();
		if (UniversalInputManager.UsePhoneUI)
		{
			SceneUtils.SetLayer(this.m_practicePickerTray, GameLayer.IgnoreFullScreenEffects);
		}
		AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", delegate(AssetReference name, GameObject go, object data)
		{
			if (go == null)
			{
				Debug.LogError("Unable to load DeckPickerTray.");
				return;
			}
			this.m_deckPickerTray = go.GetComponent<DeckPickerTrayDisplay>();
			if (this.m_deckPickerTray == null)
			{
				Debug.LogError("DeckPickerTrayDisplay component not found in DeckPickerTray object.");
				return;
			}
			if (this.m_deckPickerTrayContainer != null)
			{
				GameUtils.SetParent(this.m_deckPickerTray, this.m_deckPickerTrayContainer, false);
			}
			AdventureSubScene component = base.GetComponent<AdventureSubScene>();
			if (component != null)
			{
				this.m_practicePickerTray.AddTrayLoadedListener(delegate
				{
					this.OnTrayPartLoaded();
					this.m_practicePickerTray.gameObject.SetActive(false);
				});
				this.m_deckPickerTray.AddDeckTrayLoadedListener(new AbsDeckPickerTrayDisplay.DeckTrayLoaded(this.OnTrayPartLoaded));
				if (this.m_practicePickerTray.IsLoaded() && this.m_deckPickerTray.IsLoaded())
				{
					component.SetIsLoaded(true);
				}
			}
			this.InitializeTrays();
			CheatMgr.Get().RegisterCheatHandler("replaymissions", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_replaymissions), null, null, null);
			CheatMgr.Get().RegisterCheatHandler("replaymission", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_replaymissions), null, null, null);
			NetCache.Get().RegisterScreenPractice(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		}, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x060057D3 RID: 22483 RVA: 0x001CB7A4 File Offset: 0x001C99A4
	private void OnTrayPartLoaded()
	{
		AdventureSubScene component = base.GetComponent<AdventureSubScene>();
		if (component != null)
		{
			component.SetIsLoaded(this.IsLoaded());
		}
	}

	// Token: 0x060057D4 RID: 22484 RVA: 0x001CB7D0 File Offset: 0x001C99D0
	private void OnDestroy()
	{
		NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		if (CheatMgr.Get() != null)
		{
			CheatMgr.Get().UnregisterCheatHandler("replaymissions", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_replaymissions));
			CheatMgr.Get().UnregisterCheatHandler("replaymission", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_replaymissions));
		}
		PracticeDisplay.s_instance = null;
	}

	// Token: 0x060057D5 RID: 22485 RVA: 0x001CB836 File Offset: 0x001C9A36
	public static PracticeDisplay Get()
	{
		return PracticeDisplay.s_instance;
	}

	// Token: 0x060057D6 RID: 22486 RVA: 0x001CB83D File Offset: 0x001C9A3D
	public bool IsLoaded()
	{
		return this.m_practicePickerTray.IsLoaded() && this.m_deckPickerTray.IsLoaded();
	}

	// Token: 0x060057D7 RID: 22487 RVA: 0x001CB859 File Offset: 0x001C9A59
	private bool OnProcessCheat_replaymissions(string func, string[] args, string rawArgs)
	{
		AssetLoader.Get().InstantiatePrefab("ReplayTutorialDebug.prefab:895d5f9524722b24582e50484279bba1", AssetLoadingOptions.None);
		return true;
	}

	// Token: 0x060057D8 RID: 22488 RVA: 0x001CB872 File Offset: 0x001C9A72
	public Vector3 GetPracticePickerShowPosition()
	{
		return this.m_practicePickerTrayShowPos;
	}

	// Token: 0x060057D9 RID: 22489 RVA: 0x001CB87A File Offset: 0x001C9A7A
	public Vector3 GetPracticePickerHidePosition()
	{
		return this.m_practicePickerTrayShowPos + this.m_practicePickerTrayHideOffset;
	}

	// Token: 0x060057DA RID: 22490 RVA: 0x001CB894 File Offset: 0x001C9A94
	private void OnNetCacheReady()
	{
		NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.Practice)
		{
			return;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			return;
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		Error.AddWarningLoc("GLOBAL_FEATURE_DISABLED_TITLE", "GLOBAL_FEATURE_DISABLED_MESSAGE_PRACTICE", Array.Empty<object>());
	}

	// Token: 0x060057DB RID: 22491 RVA: 0x001CB900 File Offset: 0x001C9B00
	private void InitializeTrays()
	{
		int selectedAdventure = (int)AdventureConfig.Get().GetSelectedAdventure();
		int selectedMode = (int)AdventureConfig.Get().GetSelectedMode();
		string headerText = GameUtils.GetAdventureDataRecord(selectedAdventure, selectedMode).Name;
		this.m_deckPickerTray.SetHeaderText(headerText);
		this.m_deckPickerTray.InitAssets();
		this.m_practicePickerTray.Init();
		this.m_practicePickerTrayShowPos = this.m_practicePickerTray.transform.localPosition;
		this.m_practicePickerTray.transform.localPosition = this.GetPracticePickerHidePosition();
	}

	// Token: 0x04004B59 RID: 19289
	public GameObject m_deckPickerTrayContainer;

	// Token: 0x04004B5A RID: 19290
	public GameObject m_practicePickerTrayContainer;

	// Token: 0x04004B5B RID: 19291
	public GameObject_MobileOverride m_practicePickerTrayPrefab;

	// Token: 0x04004B5C RID: 19292
	public Vector3_MobileOverride m_practicePickerTrayHideOffset;

	// Token: 0x04004B5D RID: 19293
	private static PracticeDisplay s_instance;

	// Token: 0x04004B5E RID: 19294
	private PracticePickerTrayDisplay m_practicePickerTray;

	// Token: 0x04004B5F RID: 19295
	private Vector3 m_practicePickerTrayShowPos;

	// Token: 0x04004B60 RID: 19296
	private DeckPickerTrayDisplay m_deckPickerTray;
}
