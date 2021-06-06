using System;
using System.Collections;
using Assets;
using PegasusShared;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002E5 RID: 741
[CustomEditClass]
public class FiresideGatheringDisplay : MonoBehaviour
{
	// Token: 0x060026B1 RID: 9905 RVA: 0x000C205C File Offset: 0x000C025C
	private void Awake()
	{
		FiresideGatheringDisplay.s_instance = this;
		if (FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.NONE || FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.MAIN_SCREEN)
		{
			FiresideGatheringManager.Get().CurrentFiresideGatheringMode = FiresideGatheringManager.FiresideGatheringMode.MAIN_SCREEN;
		}
		else
		{
			FormatType @enum = Options.Get().GetEnum<FormatType>(Option.FORMAT_TYPE);
			this.m_AccordionMenuTray.GoToSpecifiedModeAutomatically(FiresideGatheringManager.Get().CurrentFiresideGatheringMode, @enum);
		}
		GameObject gameObject = (GameObject)GameUtils.Instantiate(this.m_OpponentPickerTrayPrefab, this.m_OpponentPickerTrayContainer, false);
		this.m_opponentPickerTray = gameObject.GetComponent<FiresideGatheringOpponentPickerTrayDisplay>();
		if (UniversalInputManager.UsePhoneUI)
		{
			SceneUtils.SetLayer(this.m_opponentPickerTray, GameLayer.IgnoreFullScreenEffects);
		}
		this.m_opponentPickerTray.Init();
		this.m_opponentPickerTray.gameObject.SetActive(false);
		this.m_opponentPickerTrayShowPos = this.m_opponentPickerTray.transform.localPosition;
		this.m_opponentPickerTray.transform.localPosition = this.GetOpponentPickerHidePosition();
		this.m_trayShowPos = Vector3.zero;
		this.m_TrayContainer.transform.localPosition = this.GetTrayHidePosition();
		bool flag = FiresideGatheringManager.Get().ShowSmallSignIfNeeded(this.m_TavernSignContainer.transform);
		Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		if (!flag)
		{
			FiresideGatheringManager.Get().OnSignClosed += this.OnTavernSignAnimationComplete;
		}
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_TavernBrawl);
		this.SetFiresideGatheringPresenceStatus();
	}

	// Token: 0x060026B2 RID: 9906 RVA: 0x000C21BC File Offset: 0x000C03BC
	private void OnDestroy()
	{
		FiresideGatheringDisplay.s_instance = null;
		FiresideGatheringManager.Get().OnSignClosed -= this.OnTavernSignAnimationComplete;
		if (Box.Get() != null)
		{
			Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		}
	}

	// Token: 0x060026B3 RID: 9907 RVA: 0x000C2209 File Offset: 0x000C0409
	public static FiresideGatheringDisplay Get()
	{
		return FiresideGatheringDisplay.s_instance;
	}

	// Token: 0x060026B4 RID: 9908 RVA: 0x000C2210 File Offset: 0x000C0410
	public Vector3 GetOpponentPickerShowPosition()
	{
		return this.m_opponentPickerTrayShowPos;
	}

	// Token: 0x060026B5 RID: 9909 RVA: 0x000C2218 File Offset: 0x000C0418
	public Vector3 GetOpponentPickerHidePosition()
	{
		return this.m_opponentPickerTrayShowPos + this.m_OpponentPickerTrayHideOffset;
	}

	// Token: 0x060026B6 RID: 9910 RVA: 0x000C222B File Offset: 0x000C042B
	public Vector3 GetTrayShowPosition()
	{
		return this.m_trayShowPos;
	}

	// Token: 0x060026B7 RID: 9911 RVA: 0x000C2233 File Offset: 0x000C0433
	public Vector3 GetTrayHidePosition()
	{
		return this.m_trayShowPos + this.m_TrayHideOffset;
	}

	// Token: 0x060026B8 RID: 9912 RVA: 0x000C2246 File Offset: 0x000C0446
	public void ShowDeckPickerTray()
	{
		AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", delegate(AssetReference name, GameObject go, object data)
		{
			if (go == null)
			{
				Debug.LogError("Unable to load DeckPickerTray.");
				return;
			}
			this.m_deckPickerTray = go.GetComponent<DeckPickerTrayDisplay>();
			if (this.m_TrayContainer != null)
			{
				GameUtils.SetParent(this.m_deckPickerTray, this.m_TrayContainer, false);
			}
			this.m_deckPickerTray.InitAssets();
			this.m_deckPickerTray.SetHeaderText(GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_TITLE"));
			this.m_deckPickerTray.transform.localPosition = this.m_DeckPickerTrayPosition;
			Navigation.RemoveHandler(new Navigation.NavigateBackHandler(DeckPickerTrayDisplay.OnNavigateBack));
			base.StartCoroutine(this.ShowDeckPickerTrayWhenReady());
		}, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x060026B9 RID: 9913 RVA: 0x000C227E File Offset: 0x000C047E
	public void HideDeckPickerTray()
	{
		this.HideTray(delegate
		{
			if (this.m_deckPickerTray != null && this.m_deckPickerTray.gameObject != null)
			{
				UnityEngine.Object.Destroy(this.m_deckPickerTray.gameObject);
			}
		});
	}

	// Token: 0x060026BA RID: 9914 RVA: 0x000C2292 File Offset: 0x000C0492
	public void ShowTavernBrawlTray()
	{
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnTavernBrawlSceneLoaded));
		SceneManager.LoadSceneAsync("TavernBrawl", LoadSceneMode.Additive);
	}

	// Token: 0x060026BB RID: 9915 RVA: 0x000C22B6 File Offset: 0x000C04B6
	public void HideTavernBrawlTray()
	{
		this.HideTray(delegate
		{
			if (CollectionManager.Get().GetCollectibleDisplay() != null)
			{
				CollectionManager.Get().GetCollectibleDisplay().Unload();
				UnityEngine.Object.Destroy(CollectionManager.Get().GetCollectibleDisplay().gameObject);
			}
			if (TavernBrawlDisplay.Get() != null)
			{
				TavernBrawlDisplay.Get().Unload();
				UnityEngine.Object.Destroy(TavernBrawlDisplay.Get().gameObject);
			}
		});
	}

	// Token: 0x060026BC RID: 9916 RVA: 0x000C22DD File Offset: 0x000C04DD
	public void ShowOpponentPickerTray(Action onTrayHiddenListener)
	{
		FiresideGatheringOpponentPickerTrayDisplay.Get().RegisterTrayHiddenListener(onTrayHiddenListener);
		FiresideGatheringOpponentPickerTrayDisplay.Get().Show();
	}

	// Token: 0x060026BD RID: 9917 RVA: 0x000C22F4 File Offset: 0x000C04F4
	private void ShowTray()
	{
		iTween.Stop(this.m_TrayContainer);
		this.m_TrayContainer.SetActive(true);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.GetTrayShowPosition(),
			"isLocal",
			true,
			"time",
			this.m_trayAnimationTime,
			"easetype",
			this.m_trayInEaseType,
			"oncomplete",
			new Action<object>(delegate(object e)
			{
				this.HideFiresideGatheringDisplayTray(true);
			}),
			"delay",
			0.001f
		});
		iTween.MoveTo(this.m_TrayContainer, args);
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880");
	}

	// Token: 0x060026BE RID: 9918 RVA: 0x000C23C8 File Offset: 0x000C05C8
	public void HideTray(Action onComplete)
	{
		this.HideFiresideGatheringDisplayTray(false);
		iTween.Stop(this.m_TrayContainer);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.GetTrayHidePosition(),
			"isLocal",
			true,
			"time",
			this.m_trayAnimationTime,
			"easetype",
			this.m_trayOutEaseType,
			"oncomplete",
			new Action<object>(delegate(object e)
			{
				onComplete();
			}),
			"delay",
			0.001f
		});
		iTween.MoveTo(this.m_TrayContainer, args);
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf");
		this.SetFiresideGatheringPresenceStatus();
	}

	// Token: 0x060026BF RID: 9919 RVA: 0x000C24A9 File Offset: 0x000C06A9
	private void HideFiresideGatheringDisplayTray(bool hidden)
	{
		this.m_FiresideGatheringDisplayTray.transform.localPosition = (hidden ? this.m_firesideGatheringDisplayTrayHidePosition : Vector3.zero);
	}

	// Token: 0x060026C0 RID: 9920 RVA: 0x000C24CB File Offset: 0x000C06CB
	private IEnumerator ShowDeckPickerTrayWhenReady()
	{
		while (!NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheDecks>())
		{
			yield return null;
		}
		while (!CollectionManager.Get().AreAllDeckContentsReady())
		{
			yield return null;
		}
		this.ShowTray();
		yield break;
	}

	// Token: 0x060026C1 RID: 9921 RVA: 0x000C24DA File Offset: 0x000C06DA
	private void OnTavernBrawlSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object callbackData)
	{
		if (scene.GetType() != typeof(TavernBrawlScene))
		{
			return;
		}
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnTavernBrawlSceneLoaded));
		base.StartCoroutine(this.WaitThenShowTavernBrawlTray());
	}

	// Token: 0x060026C2 RID: 9922 RVA: 0x000C2518 File Offset: 0x000C0718
	private IEnumerator WaitThenShowTavernBrawlTray()
	{
		while (TavernBrawlDisplay.Get() == null)
		{
			yield return null;
		}
		if (this.m_TrayContainer != null)
		{
			GameUtils.SetParent(TavernBrawlDisplay.Get(), this.m_TrayContainer, false);
		}
		TavernBrawlDisplay.Get().transform.localPosition = this.m_TavernBrawlTrayPosition;
		if (TavernBrawlManager.Get().CurrentMission() != null && TavernBrawlManager.Get().CurrentMission().canEditDeck)
		{
			while (CollectionManager.Get().GetCollectibleDisplay() == null)
			{
				yield return null;
			}
			if (this.m_TrayContainer != null)
			{
				GameUtils.SetParent(CollectionManager.Get().GetCollectibleDisplay(), this.m_TrayContainer, false);
			}
			CollectionManager.Get().GetCollectibleDisplay().transform.localPosition = this.m_CollectionManagerTrayPosition;
		}
		this.ShowTray();
		yield break;
	}

	// Token: 0x060026C3 RID: 9923 RVA: 0x000C2528 File Offset: 0x000C0728
	private void OnTavernSignAnimationComplete()
	{
		if (FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.NONE)
		{
			return;
		}
		NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_LOBBY_ARRIVE"), this.m_LobbyArriveAudio, 0f, null, false);
	}

	// Token: 0x060026C4 RID: 9924 RVA: 0x000C2579 File Offset: 0x000C0779
	private void SetFiresideGatheringPresenceStatus()
	{
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.HUB
		});
	}

	// Token: 0x060026C5 RID: 9925 RVA: 0x000C2595 File Offset: 0x000C0795
	private void OnBoxTransitionFinished(object userdata)
	{
		FiresideGatheringManager.Get().EnableTransitionInputBlocker(false);
	}

	// Token: 0x040015F6 RID: 5622
	[CustomEditField(Sections = "Main Tray Fields")]
	public GameObject m_FiresideGatheringDisplayTray;

	// Token: 0x040015F7 RID: 5623
	[CustomEditField(Sections = "Main Tray Fields")]
	public GameObject m_TrayContainer;

	// Token: 0x040015F8 RID: 5624
	[CustomEditField(Sections = "Main Tray Fields")]
	public GameObject m_TavernSignContainer;

	// Token: 0x040015F9 RID: 5625
	[CustomEditField(Sections = "Main Tray Fields")]
	public Vector3 m_TrayHideOffset;

	// Token: 0x040015FA RID: 5626
	[CustomEditField(Sections = "Main Tray Fields")]
	public FiresideGatheringAccordionMenuTray m_AccordionMenuTray;

	// Token: 0x040015FB RID: 5627
	[CustomEditField(Sections = "Main Tray Fields", T = EditType.SOUND_PREFAB)]
	public string m_LobbyArriveAudio = "VO_Innkeeper_Male_Dwarf_FSG_LobbyArrive_01.prefab:5071efa83205af742a82b7456b7a6060";

	// Token: 0x040015FC RID: 5628
	[CustomEditField(Sections = "Sub Tray Fields")]
	public Vector3_MobileOverride m_TavernBrawlTrayPosition;

	// Token: 0x040015FD RID: 5629
	[CustomEditField(Sections = "Sub Tray Fields")]
	public Vector3_MobileOverride m_CollectionManagerTrayPosition;

	// Token: 0x040015FE RID: 5630
	[CustomEditField(Sections = "Sub Tray Fields")]
	public Vector3_MobileOverride m_DeckPickerTrayPosition;

	// Token: 0x040015FF RID: 5631
	[CustomEditField(Sections = "Main Tray Fields", T = EditType.GAME_OBJECT)]
	public string m_TavernBrawlDisplayPrefab;

	// Token: 0x04001600 RID: 5632
	[CustomEditField(Sections = "Opponent Picker Fields")]
	public GameObject m_OpponentPickerTrayContainer;

	// Token: 0x04001601 RID: 5633
	[CustomEditField(Sections = "Opponent Picker Fields")]
	public GameObject_MobileOverride m_OpponentPickerTrayPrefab;

	// Token: 0x04001602 RID: 5634
	[CustomEditField(Sections = "Opponent Picker Fields")]
	public Vector3 m_OpponentPickerTrayHideOffset;

	// Token: 0x04001603 RID: 5635
	[CustomEditField(Sections = "Animation Settings")]
	public float m_trayAnimationTime = 0.5f;

	// Token: 0x04001604 RID: 5636
	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayInEaseType = iTween.EaseType.easeOutBounce;

	// Token: 0x04001605 RID: 5637
	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayOutEaseType = iTween.EaseType.easeOutCubic;

	// Token: 0x04001606 RID: 5638
	private static FiresideGatheringDisplay s_instance;

	// Token: 0x04001607 RID: 5639
	private FiresideGatheringOpponentPickerTrayDisplay m_opponentPickerTray;

	// Token: 0x04001608 RID: 5640
	private Vector3 m_opponentPickerTrayShowPos;

	// Token: 0x04001609 RID: 5641
	private DeckPickerTrayDisplay m_deckPickerTray;

	// Token: 0x0400160A RID: 5642
	private Vector3 m_trayShowPos;

	// Token: 0x0400160B RID: 5643
	private readonly Vector3 m_firesideGatheringDisplayTrayHidePosition = new Vector3(0f, 0f, -5000f);
}
