using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.DungeonCrawl;
using Hearthstone.UI;
using PegasusClient;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x0200003A RID: 58
[CustomEditClass]
public class AdventureDungeonCrawlDisplay : MonoBehaviour
{
	// Token: 0x0600020D RID: 525 RVA: 0x0000B9AB File Offset: 0x00009BAB
	public static AdventureDungeonCrawlDisplay Get()
	{
		return AdventureDungeonCrawlDisplay.m_instance;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000B9B2 File Offset: 0x00009BB2
	private void Awake()
	{
		AdventureDungeonCrawlDisplay.m_instance = this;
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000B9BA File Offset: 0x00009BBA
	private void Start()
	{
		CollectionManager.Get().RegisterDeckCreatedListener(new CollectionManager.DelOnDeckCreated(this.OnDeckCreated));
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		this.m_isPVPDR = SceneMgr.Get().IsInDuelsMode();
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000B9F8 File Offset: 0x00009BF8
	public void StartRun(DungeonCrawlServices services)
	{
		this.m_dungeonCrawlData = services.DungeonCrawlData;
		this.m_subsceneController = services.SubsceneController;
		this.m_assetLoadingHelper = services.AssetLoadingHelper;
		services.AssetLoadingHelper.AssetLoadingComplete += this.OnSubSceneLoaded;
		this.m_subsceneController.TransitionComplete += this.OnSubSceneTransitionComplete;
		AdventureDbId selectedAdv = this.m_dungeonCrawlData.GetSelectedAdventure();
		AdventureModeDbId selectedMode = this.m_dungeonCrawlData.GetSelectedMode();
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdv, (int)selectedMode);
		this.m_playerHeroData = new AdventureDungeonCrawlDisplay.PlayerHeroData(this.m_dungeonCrawlData);
		this.m_playerHeroData.OnHeroDataChanged += delegate()
		{
			this.m_playMat.SetPlayerHeroDbId(this.m_playerHeroData.HeroDbId);
		};
		this.m_AdventureTitle.Text = adventureDataRecord.Name;
		AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
		if (AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey <= (GameSaveKeyId)0)
		{
			Debug.LogErrorFormat("Adventure {0} Mode {1} has no GameSaveDataKey set! This mode does not work without defining GAME_SAVE_DATA_SERVER_KEY in ADVENTURE.dbi!", new object[]
			{
				selectedAdv,
				selectedMode
			});
		}
		if (AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey <= (GameSaveKeyId)0)
		{
			Debug.LogErrorFormat("Adventure {0} Mode {1} has no GameSaveDataKey set! This mode does not work without defining GAME_SAVE_DATA_CLIENT_KEY in ADVENTURE.dbi!", new object[]
			{
				selectedAdv,
				selectedMode
			});
		}
		if (AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey == AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey)
		{
			Debug.LogErrorFormat("Adventure {0} Mode {1} has an equal GameSaveDataKey for Client and Server. These keys are not allowed to be equal!", new object[]
			{
				selectedAdv,
				selectedMode
			});
		}
		this.m_bossCardBackId = adventureDataRecord.BossCardBack;
		if (this.m_bossCardBackId == 0)
		{
			this.m_bossCardBackId = 0;
		}
		List<ScenarioDbfRecord> records = GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == (int)selectedAdv && r.ModeId == (int)selectedMode, -1);
		if (records == null || records.Count < 1)
		{
			Log.Adventures.PrintError("No Scenarios found for Adventure {0} and Mode {1}!", new object[]
			{
				selectedAdv,
				selectedMode
			});
		}
		else if (records.Count == 1)
		{
			ScenarioDbfRecord scenarioDbfRecord = records[0];
			this.m_dungeonCrawlData.SetMission((ScenarioDbId)scenarioDbfRecord.ID, false);
			Log.Adventures.Print("Owns wing for this Dungeon Run? {0}", new object[]
			{
				AdventureProgressMgr.Get().OwnsWing(scenarioDbfRecord.WingId)
			});
		}
		else if (this.m_dungeonCrawlData.GetMission() == ScenarioDbId.INVALID)
		{
			Log.Adventures.Print("No selectedScenarioId currently set - this should come with the GameSaveData.", Array.Empty<object>());
		}
		else
		{
			ScenarioDbfRecord scenarioDbfRecord2 = records.Find((ScenarioDbfRecord x) => x.ID == (int)this.m_dungeonCrawlData.GetMission());
			if (scenarioDbfRecord2 == null)
			{
				Log.Adventures.PrintError("No matching Scenario for this Adventure has been set in AdventureConfig! AdventureConfig's mission: {0}", new object[]
				{
					this.m_dungeonCrawlData.GetMission()
				});
			}
			else
			{
				Log.Adventures.Print("Owns wing for this Dungeon Run? {0}", new object[]
				{
					AdventureProgressMgr.Get().OwnsWing(scenarioDbfRecord2.WingId)
				});
			}
		}
		this.m_shouldSkipHeroSelect = adventureDataRecord.DungeonCrawlSkipHeroSelect;
		this.m_mustPickShrine = adventureDataRecord.DungeonCrawlMustPickShrine;
		this.m_mustSelectChapter = adventureDataRecord.DungeonCrawlSelectChapter;
		this.m_anomalyModeCardDbId = (long)adventureDataRecord.AnomalyModeDefaultCardId;
		this.m_assetLoadingHelper.AddAssetToLoad(1);
		this.m_dungeonCrawlPlayMatReference.RegisterReadyListener<AdventureDungeonCrawlPlayMat>(new Action<AdventureDungeonCrawlPlayMat>(this.OnPlayMatReady));
		bool retireButtonSupported = adventureDataRecord.DungeonCrawlIsRetireSupported;
		this.m_assetLoadingHelper.AddAssetToLoad(1);
		Widget.EventListenerDelegate <>9__4;
		Action<object> <>9__5;
		this.m_retireButtonReference.RegisterReadyListener<Widget>(delegate(Widget w)
		{
			Widget.EventListenerDelegate listener;
			if ((listener = <>9__4) == null)
			{
				listener = (<>9__4 = delegate(string eventName)
				{
					if (eventName == "Button_Framed_Clicked")
					{
						if (!retireButtonSupported)
						{
							return;
						}
						this.m_retireButton.SetActive(false);
						AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
						popupInfo.m_headerText = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_RETIRE_CONFIRMATION_HEADER");
						popupInfo.m_text = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_RETIRE_CONFIRMATION_BODY");
						popupInfo.m_showAlertIcon = true;
						popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
						popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
						if (this.m_isPVPDR)
						{
							popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnPVPDRRetirePopupResponse);
						}
						else
						{
							popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnRetirePopupResponse);
						}
						DialogManager.Get().ShowPopup(popupInfo);
					}
				});
			}
			w.RegisterEventListener(listener);
			this.m_retireButton = w.gameObject;
			this.m_retireButton.SetActive(false);
			Action<object> listener2;
			if ((listener2 = <>9__5) == null)
			{
				listener2 = (<>9__5 = delegate(object _)
				{
					this.m_assetLoadingHelper.AssetLoadCompleted();
				});
			}
			w.RegisterDoneChangingStatesListener(listener2, null, true, true);
		});
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_dungeonCrawlDeckSelectWidgetReference.RegisterReadyListener<DungeonCrawlDeckSelect>(new Action<DungeonCrawlDeckSelect>(this.OnDungeonCrawlDeckTrayReady));
		}
		if (this.m_dungeonCrawlDeckTray != null && this.m_dungeonCrawlDeckTray.m_deckBigCard != null)
		{
			this.m_dungeonCrawlDeckTray.m_deckBigCard.OnBigCardShown += this.OnDeckTrayBigCardShown;
		}
		this.EnableBackButton(true);
		if (this.m_isPVPDR)
		{
			Navigation.PushUnique(new Navigation.NavigateBackHandler(PvPDungeonRunScene.Get().NavigateBackFromPlaymat));
		}
		else
		{
			Navigation.PushUnique(new Navigation.NavigateBackHandler(AdventureDungeonCrawlDisplay.OnNavigateBack));
		}
		this.m_BackButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackButtonPress));
		if (this.m_ShowDeckButton != null)
		{
			this.m_ShowDeckButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnShowDeckButtonPress));
		}
		this.DisableBackButtonIfInDemoMode();
		this.RequestOrLoadCachedGameSaveData();
		this.SetDungeonCrawlDisplayVisualStyle();
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000BE57 File Offset: 0x0000A057
	public void EnablePlayButton()
	{
		if (this.m_playMat != null)
		{
			this.m_playMat.PlayButton.Enable();
		}
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0000BE77 File Offset: 0x0000A077
	public void DisablePlayButton()
	{
		if (this.m_playMat != null && this.m_playMat.PlayButton.IsEnabled())
		{
			this.m_playMat.PlayButton.Disable(false);
		}
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0000BEAA File Offset: 0x0000A0AA
	public void EnableBackButton(bool enabled)
	{
		if (this.m_BackButton != null && this.m_BackButton.IsEnabled() != enabled)
		{
			this.m_BackButton.SetEnabled(enabled, false);
			this.m_BackButton.Flip(enabled, false);
		}
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
	private void OnDeckTrayBigCardShown(Actor shownActor, EntityDef entityDef)
	{
		if (shownActor == null || entityDef == null)
		{
			return;
		}
		long num = (long)GameUtils.TranslateCardIdToDbId(entityDef.GetCardId(), false);
		if (this.m_anomalyModeCardDbId != num)
		{
			return;
		}
		HighlightRender componentInChildren = shownActor.GetComponentInChildren<HighlightRender>();
		MeshRenderer meshRenderer = (componentInChildren != null) ? componentInChildren.GetComponent<MeshRenderer>() : null;
		if (meshRenderer != null && this.m_anomalyModeCardHighlightMaterial != null)
		{
			meshRenderer.SetSharedMaterial(this.m_anomalyModeCardHighlightMaterial);
			meshRenderer.enabled = true;
		}
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000BF5C File Offset: 0x0000A15C
	private void OnPlayMatPlayButtonReady(PlayButton playButton)
	{
		if (playButton == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButtonReference is null, or does not have a PlayButton component on it!", Array.Empty<object>());
			return;
		}
		playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPlayButtonPress));
		Widget componentInParent = playButton.GetComponentInParent<WidgetTemplate>();
		if (componentInParent != null)
		{
			componentInParent.RegisterDoneChangingStatesListener(delegate(object _)
			{
				this.m_assetLoadingHelper.AssetLoadCompleted();
			}, null, true, true);
			return;
		}
		Error.AddDevWarning("UI Error!", "Could not find PlayMat PlayButton WidgetTemplate!", Array.Empty<object>());
		this.m_assetLoadingHelper.AssetLoadCompleted();
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
	private void OnDungeonCrawlDeckTrayReady(DungeonCrawlDeckSelect deckSelect)
	{
		this.m_dungeonCrawlDeckSelect = deckSelect;
		if (this.m_dungeonCrawlDeckSelect == null)
		{
			Error.AddDevWarning("UI Error!", "Could not find AdventureDungeonCrawlDeckTray in the AdventureDeckSelectWidget.", Array.Empty<object>());
			return;
		}
		if (this.m_dungeonCrawlDeckSelect == null)
		{
			Error.AddDevWarning("UI Error!", "Could not find SlidingTray in the AdventureDeckSelectWidget.", Array.Empty<object>());
			return;
		}
		deckSelect.playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPlayButtonPress));
		deckSelect.heroDetails.AddHeroPowerListener(UIEventType.ROLLOVER, delegate(UIEvent e)
		{
			this.ShowBigCard(this.m_heroPowerBigCard, this.m_currentHeroPowerFullDef, this.m_HeroPowerBigCardBone);
		});
		deckSelect.heroDetails.AddHeroPowerListener(UIEventType.ROLLOUT, delegate(UIEvent e)
		{
			BigCardHelper.HideBigCard(this.m_heroPowerBigCard);
		});
		if (deckSelect.deckTray != null && deckSelect.deckTray.m_deckBigCard != null)
		{
			deckSelect.deckTray.m_deckBigCard.OnBigCardShown += this.OnDeckTrayBigCardShown;
		}
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
	private void OnPlayMatReady(AdventureDungeonCrawlPlayMat playMat)
	{
		if (playMat == null)
		{
			Error.AddDevWarning("UI Error!", "m_dungeonCrawlPlayMatReference is null, or does not have a AdventureDungeonCrawlPlayMat component on it!", Array.Empty<object>());
			return;
		}
		this.m_playMat = playMat;
		this.m_playMat.SetCardBack(this.m_bossCardBackId);
		this.m_BossPowerBone = this.m_playMat.m_BossPowerBone;
		this.m_assetLoadingHelper.AddAssetToLoad(1);
		this.m_playMat.m_PlayButtonReference.RegisterReadyListener<PlayButton>(new Action<PlayButton>(this.OnPlayMatPlayButtonReady));
		this.LoadInitialAssets();
		Widget component = playMat.GetComponent<WidgetTemplate>();
		if (component != null)
		{
			component.RegisterDoneChangingStatesListener(delegate(object _)
			{
				this.m_assetLoadingHelper.AssetLoadCompleted();
			}, null, true, true);
			return;
		}
		Error.AddDevWarning("UI Error!", "Could not find PlayMat WidgetTemplate!", Array.Empty<object>());
		this.m_assetLoadingHelper.AssetLoadCompleted();
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000C188 File Offset: 0x0000A388
	private void Update()
	{
		if (this.m_dungeonCrawlData == null || !this.m_dungeonCrawlData.IsDevMode)
		{
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.Z))
		{
			if (this.m_playMat == null)
			{
				return;
			}
			if (this.m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_BOSS_GRAVEYARD)
			{
				this.m_playMat.ShowNextBoss(this.GetPlayButtonTextForNextMission());
				return;
			}
			if (this.m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_NEXT_BOSS)
			{
				this.ShowRunEnd(this.m_defeatedBossIds, this.m_bossWhoDefeatedMeId);
			}
		}
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000C204 File Offset: 0x0000A404
	private void OnDestroy()
	{
		AdventureDungeonCrawlDisplay.m_instance = null;
		DefLoader.DisposableFullDef currentBossHeroPowerFullDef = this.m_currentBossHeroPowerFullDef;
		if (currentBossHeroPowerFullDef != null)
		{
			currentBossHeroPowerFullDef.Dispose();
		}
		DefLoader.DisposableFullDef currentHeroPowerFullDef = this.m_currentHeroPowerFullDef;
		if (currentHeroPowerFullDef != null)
		{
			currentHeroPowerFullDef.Dispose();
		}
		if (this.m_playMat != null)
		{
			this.m_playMat.HideBossHeroPowerTooltip(true);
		}
		if (this.m_dungeonCrawlDeckTray != null && this.m_dungeonCrawlDeckTray.m_deckBigCard != null)
		{
			this.m_dungeonCrawlDeckTray.m_deckBigCard.OnBigCardShown -= this.OnDeckTrayBigCardShown;
		}
		if (this.m_dungeonCrawlDeckSelect != null && this.m_dungeonCrawlDeckSelect.deckTray != null && this.m_dungeonCrawlDeckSelect.deckTray.m_deckBigCard != null)
		{
			this.m_dungeonCrawlDeckSelect.deckTray.m_deckBigCard.OnBigCardShown -= this.OnDeckTrayBigCardShown;
		}
		GameMgr gameMgr = GameMgr.Get();
		if (gameMgr == null)
		{
			return;
		}
		gameMgr.UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000C308 File Offset: 0x0000A508
	private void OnBossActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_bossActor = AdventureDungeonCrawlDisplay.OnActorLoaded(assetRef, go, this.m_playMat.m_nextBossFaceBone.gameObject, true);
		if (this.m_bossActor != null)
		{
			PegUIElement pegUIElement = this.m_bossActor.GetCollider().gameObject.AddComponent<PegUIElement>();
			pegUIElement.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
			{
				this.m_bossActor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
				this.ShowBigCard(this.m_bossPowerBigCard, this.m_currentBossHeroPowerFullDef, this.m_HeroPowerBigCardBone);
				this.m_bossHeroPowerHideCoroutine = base.StartCoroutine(this.HideBossHeroPowerTooltipAfterHover());
			});
			pegUIElement.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
			{
				this.m_bossActor.SetActorState(ActorStateType.CARD_IDLE);
				BigCardHelper.HideBigCard(this.m_bossPowerBigCard);
				if (this.m_bossHeroPowerHideCoroutine != null)
				{
					base.StopCoroutine(this.m_bossHeroPowerHideCoroutine);
				}
			});
		}
		this.m_playMat.SetBossActor(this.m_bossActor);
		this.m_assetLoadingHelper.AssetLoadCompleted();
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
	private void LoadInitialAssets()
	{
		int selectedAdventure = (int)this.m_dungeonCrawlData.GetSelectedAdventure();
		AdventureModeDbId selectedMode = this.m_dungeonCrawlData.GetSelectedMode();
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord(selectedAdventure, (int)selectedMode);
		if (adventureDataRecord == null)
		{
			Log.Adventures.PrintError("Tried to load assets but data record not found!", Array.Empty<object>());
			return;
		}
		IAssetLoader assetLoader = AssetLoader.Get();
		this.m_assetLoadingHelper.AddAssetToLoad(1);
		assetLoader.InstantiatePrefab(adventureDataRecord.DungeonCrawlBossCardPrefab, new PrefabCallback<GameObject>(this.OnBossActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		this.m_assetLoadingHelper.AddAssetToLoad(1);
		assetLoader.InstantiatePrefab("History_HeroPower_Opponent.prefab:a99d23d6e8630f94b96a8e096fffb16f", new PrefabCallback<GameObject>(this.OnBossPowerBigCardLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		this.m_assetLoadingHelper.AddAssetToLoad(1);
		assetLoader.InstantiatePrefab("Card_Dungeon_Play_Hero.prefab:183cb9cc59697844e911776ec349fe5e", new PrefabCallback<GameObject>(this.OnHeroActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		this.m_assetLoadingHelper.AddAssetToLoad(1);
		assetLoader.InstantiatePrefab("History_HeroPower.prefab:e73edf8ccea2b11429093f7a448eef53", new PrefabCallback<GameObject>(this.OnHeroPowerBigCardLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		this.m_assetLoadingHelper.AddAssetToLoad(1);
		assetLoader.InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", new PrefabCallback<GameObject>(this.OnHeroPowerActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
	private IEnumerator HideBossHeroPowerTooltipAfterHover()
	{
		float timer = 0f;
		while (timer < this.m_RolloverTimeToHideBossHeroPowerTooltip)
		{
			timer += Time.unscaledDeltaTime;
			yield return new WaitForEndOfFrame();
		}
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_HERO_POWER_TUTORIAL_PROGRESS, out num);
		if (num == 1L)
		{
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_HERO_POWER_TUTORIAL_PROGRESS, new long[]
			{
				2L
			}), null);
		}
		this.m_playMat.HideBossHeroPowerTooltip(false);
		yield break;
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000C4D3 File Offset: 0x0000A6D3
	private void OnBossPowerBigCardLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_bossPowerBigCard = AdventureDungeonCrawlDisplay.OnActorLoaded(assetRef, go, this.m_BossPowerBone, false);
		if (this.m_bossPowerBigCard != null)
		{
			this.m_bossPowerBigCard.TurnOffCollider();
		}
		this.m_assetLoadingHelper.AssetLoadCompleted();
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0000C512 File Offset: 0x0000A712
	private void OnHeroPowerBigCardLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_heroPowerBigCard = AdventureDungeonCrawlDisplay.OnActorLoaded(assetRef, go, this.m_HeroPowerBigCardBone, false);
		if (this.m_heroPowerBigCard != null)
		{
			this.m_heroPowerBigCard.TurnOffCollider();
		}
		this.m_assetLoadingHelper.AssetLoadCompleted();
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0000C554 File Offset: 0x0000A754
	private void RequestOrLoadCachedGameSaveData()
	{
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY)
		{
			GameSaveDataManager.Get().ClearLocalData(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey);
		}
		base.StartCoroutine(this.InitializeFromGameSaveDataWhenReady());
		if (!GameSaveDataManager.Get().IsDataReady(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey))
		{
			GameSaveDataManager.Get().Request(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, new GameSaveDataManager.OnRequestDataResponseDelegate(this.OnRequestGameSaveDataServerResponse));
		}
		else
		{
			this.m_hasReceivedGameSaveDataServerKeyResponse = true;
		}
		if (!GameSaveDataManager.Get().IsDataReady(AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey))
		{
			GameSaveDataManager.Get().Request(AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey, new GameSaveDataManager.OnRequestDataResponseDelegate(this.OnRequestGameSaveDataClientResponse));
			return;
		}
		this.m_hasReceivedGameSaveDataClientKeyResponse = true;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000C5F3 File Offset: 0x0000A7F3
	private void OnRequestGameSaveDataServerResponse(bool success)
	{
		if (!success)
		{
			Debug.LogError("OnRequestGameSaveDataResponse: Error requesting game save data for current adventure.");
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			return;
		}
		this.m_hasReceivedGameSaveDataServerKeyResponse = true;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000C617 File Offset: 0x0000A817
	private void OnRequestGameSaveDataClientResponse(bool success)
	{
		if (!success)
		{
			Debug.LogError("OnRequestGameSaveDataResponse: Error requesting game save data for current adventure.");
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			return;
		}
		this.m_hasReceivedGameSaveDataClientKeyResponse = true;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000C63B File Offset: 0x0000A83B
	private IEnumerator InitializeFromGameSaveDataWhenReady()
	{
		while (this.m_playMat == null || !this.m_playMat.IsReady())
		{
			Log.Adventures.Print("Waiting for Play Mat to be initialized before handling new Game Save Data.", Array.Empty<object>());
			yield return null;
		}
		while (this.m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			Log.Adventures.Print("Waiting for Play Mat to be done transitioning before handling new Game Save Data.", Array.Empty<object>());
			yield return null;
		}
		while (!this.m_hasReceivedGameSaveDataClientKeyResponse || !this.m_hasReceivedGameSaveDataServerKeyResponse)
		{
			yield return null;
		}
		DungeonCrawlUtil.MigrateDungeonCrawlSubkeys(AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey, AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey);
		while (this.m_heroActor == null || this.m_heroPowerActor == null || this.m_heroPowerBigCard == null)
		{
			yield return null;
		}
		this.InitializeFromGameSaveData();
		yield break;
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000C64C File Offset: 0x0000A84C
	private bool IsScenarioValidForAdventureAndMode(ScenarioDbId selectedScenario)
	{
		if (!AdventureUtils.IsMissionValidForAdventureMode(this.m_dungeonCrawlData.GetSelectedAdventure(), this.m_dungeonCrawlData.GetSelectedMode(), selectedScenario))
		{
			Debug.LogErrorFormat("Scenario {0} is not a part of Adventure {1} and mode {2}! Something is probably wrong.", new object[]
			{
				selectedScenario,
				this.m_dungeonCrawlData.GetSelectedAdventure(),
				this.m_dungeonCrawlData.GetSelectedMode()
			});
			return false;
		}
		return true;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000C6BC File Offset: 0x0000A8BC
	private void InitializeFromGameSaveData()
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)this.m_dungeonCrawlData.GetSelectedAdventure(), (int)this.m_dungeonCrawlData.GetSelectedMode());
		this.m_playerHeroData.DeckClass = TAG_CLASS.INVALID;
		List<long> list = null;
		List<CardWithPremiumStatus> deckCardListPremium = null;
		List<long> list2 = null;
		List<long> list3 = null;
		List<long> list4 = null;
		List<long> list5 = null;
		List<long> list6 = null;
		long num = 0L;
		long num2 = 0L;
		if (GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, out this.m_defeatedBossIds))
		{
			this.m_numBossesDefeated = this.m_defeatedBossIds.Count;
		}
		List<long> list7 = null;
		List<long> list8 = null;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, out list);
		long num3;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CLASS, out num3);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_LOST_TO, out this.m_bossWhoDefeatedMeId);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_A, out list3);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_B, out list4);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_C, out list5);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_OPTION, out list2);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SHRINE_OPTIONS, out this.m_shrineOptions);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_FIGHT, out list6);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT, out num);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_TREASURE, out num2);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_HEALTH, out this.m_nextBossHealth);
		long heroHealth;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_HEALTH, out heroHealth);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_CARDS_ADDED_TO_DECK_MAP, out this.m_cardsAddedToDeckMap);
		long num4;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_SHRINE, out num4);
		long num5;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_SCENARIO_ID, out num5);
		long num6;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, out num6);
		long num7;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_OVERRIDE, out num7);
		long selectedLoadoutTreasureDbId;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, out selectedLoadoutTreasureDbId);
		long selectedHeroPowerDbId;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, out selectedHeroPowerDbId);
		long num8;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER, out num8);
		long num9;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_ANOMALY_MODE, out num9);
		long num10;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_ANOMALY_MODE, out num10);
		long selectedDeckId;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_DECK, out selectedDeckId);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_ENCHANTMENT_INDICES, out list7);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_ENCHANTMENTS, out list8);
		long num11;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_CURRENT_ANOMALY_MODE_CARD, out num11);
		long num12;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_ANOMALY_MODE_CARD_PREVIEW, out num12);
		long num13;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE, out num13);
		long num14;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, out num14);
		long num15;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, out num15);
		this.m_playerHeroData.DeckClass = (TAG_CLASS)num3;
		this.m_selectedShrineIndex = (int)num4;
		if (list != null)
		{
			deckCardListPremium = CardWithPremiumStatus.ConvertList(list);
		}
		this.m_isRunRetired = (num15 > 0L);
		this.m_isRunActive = DungeonCrawlUtil.IsDungeonRunActive(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey);
		this.m_hasSeenLatestDungeonRunComplete = (num13 > 0L);
		bool flag = this.m_isRunActive || this.ShouldShowRunCompletedScreen();
		if (this.m_isPVPDR)
		{
			CollectionDeck duelsDeck = CollectionManager.Get().GetDuelsDeck();
			if (duelsDeck != null)
			{
				if (this.m_isRunActive || (list != null && list.Count > duelsDeck.GetTotalCardCount()))
				{
					List<CardWithPremiumStatus> cardsWithPremiumStatus = duelsDeck.GetCardsWithPremiumStatus();
					int k;
					int i;
					for (i = 0; i < deckCardListPremium.Count; i = k + 1)
					{
						int num16 = cardsWithPremiumStatus.FindIndex((CardWithPremiumStatus r) => r.cardId == deckCardListPremium[i].cardId);
						if (num16 >= 0)
						{
							deckCardListPremium[i].premium = cardsWithPremiumStatus[num16].premium;
							cardsWithPremiumStatus.RemoveAt(num16);
						}
						k = i;
					}
				}
				else if (!this.m_seedDeckCreateRequested)
				{
					this.m_realDuelSeedDeck = duelsDeck;
					deckCardListPremium = duelsDeck.GetCardsWithPremiumStatus();
					this.m_dungeonCrawlData.SelectedHeroClass = (this.m_playerHeroData.DeckClass = duelsDeck.GetClass());
				}
			}
		}
		this.m_dungeonCrawlData.SelectedLoadoutTreasureDbId = selectedLoadoutTreasureDbId;
		this.m_dungeonCrawlData.SelectedHeroPowerDbId = selectedHeroPowerDbId;
		this.m_dungeonCrawlData.SelectedDeckId = selectedDeckId;
		if ((int)num14 != 0)
		{
			this.m_dungeonCrawlData.SelectedHeroClass = (TAG_CLASS)num14;
		}
		ScenarioDbId scenarioDbId = (ScenarioDbId)num7;
		if (scenarioDbId != ScenarioDbId.INVALID && !this.IsScenarioValidForAdventureAndMode(scenarioDbId))
		{
			scenarioDbId = ScenarioDbId.INVALID;
		}
		Log.Adventures.Print("Scenario Override set to {0}!", new object[]
		{
			scenarioDbId
		});
		this.m_dungeonCrawlData.SetMissionOverride(scenarioDbId);
		ScenarioDbId scenarioDbId2 = flag ? ((ScenarioDbId)num6) : ((ScenarioDbId)num5);
		if (scenarioDbId2 != ScenarioDbId.INVALID && this.IsScenarioValidForAdventureAndMode(scenarioDbId2))
		{
			this.m_dungeonCrawlData.SetMission(scenarioDbId2, true);
		}
		bool flag2 = false;
		if (!flag)
		{
			flag2 = this.m_dungeonCrawlData.HasValidLoadoutForSelectedAdventure();
			if (!flag2)
			{
				AdventureDungeonCrawlDisplay.ResetDungeonCrawlSelections(this.m_dungeonCrawlData);
			}
		}
		this.m_playMat.m_paperControllerReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnPlayMatPaperControllerReady));
		this.m_playMat.m_paperControllerReference_phone.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnPlayMatPaperControllerReady));
		if (flag)
		{
			this.m_dungeonCrawlData.AnomalyModeActivated = (num10 > 0L);
		}
		else if (flag2)
		{
			this.m_dungeonCrawlData.AnomalyModeActivated = (num9 > 0L);
		}
		if (flag)
		{
			this.m_heroHealth = heroHealth;
		}
		else
		{
			this.m_heroHealth = 0L;
		}
		if (this.HandleDemoModeReset())
		{
			return;
		}
		long num17 = flag ? num11 : num12;
		if (num17 > 0L)
		{
			this.m_anomalyModeCardDbId = num17;
		}
		if (this.m_isRunActive && deckCardListPremium != null)
		{
			if (num != 0L)
			{
				List<long>[] array = new List<long>[]
				{
					list3,
					list4,
					list5
				};
				int num18 = (int)num - 1;
				if (num18 >= array.Length || array[num18] == null)
				{
					Log.Adventures.PrintError("Attempting to add Loot choice {0} to the deck list, but there is not corresponding list of Loot!", new object[]
					{
						num18
					});
				}
				else
				{
					List<long> list9 = array[num18];
					for (int j = 1; j < list9.Count; j++)
					{
						deckCardListPremium.Add(new CardWithPremiumStatus(list9[j], TAG_PREMIUM.NORMAL));
					}
				}
			}
			if (num2 != 0L && list2 != null)
			{
				int num19 = (int)num2 - 1;
				if (list2.Count <= num19)
				{
					Log.Adventures.PrintError("Attempting to add Treasure choice {0} to the deck list, but treasureLootOptions only has {1} options!", new object[]
					{
						num19,
						list2.Count
					});
				}
				else
				{
					deckCardListPremium.Add(new CardWithPremiumStatus(list2[num19], TAG_PREMIUM.NORMAL));
				}
			}
		}
		ScenarioDbId mission = this.m_dungeonCrawlData.GetMission();
		int num20 = 0;
		WingDbfRecord wingRecordFromMissionId = GameUtils.GetWingRecordFromMissionId((int)mission);
		this.m_numBossesInRun = this.m_dungeonCrawlData.GetAdventureBossesInRun(wingRecordFromMissionId);
		if (wingRecordFromMissionId != null)
		{
			num20 = Mathf.Max(0, GameUtils.GetSortedWingUnlockIndex(wingRecordFromMissionId));
			this.m_plotTwistCardDbId = (long)wingRecordFromMissionId.PlotTwistCardId;
		}
		int num21 = 0;
		if (list6 != null && list6.Count > num20 && !this.m_isRunRetired)
		{
			num21 = (int)list6[num20];
		}
		if (num21 != 0)
		{
			this.m_nextBossCardId = GameUtils.TranslateDbIdToCardId(num21, false);
		}
		else
		{
			this.m_nextBossCardId = GameUtils.GetMissionHeroCardId((int)mission);
		}
		if (this.m_nextBossCardId == null)
		{
			Log.Adventures.PrintWarning("AdventureDungeonCrawlDisplay.OnGameSaveDataResponse() - No cardId for boss dbId {0}!", new object[]
			{
				num21
			});
		}
		else
		{
			this.m_assetLoadingHelper.AddAssetToLoad(1);
			DefLoader.Get().LoadFullDef(this.m_nextBossCardId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnBossFullDefLoaded), null, null);
		}
		long num22 = flag ? num8 : this.m_dungeonCrawlData.SelectedHeroPowerDbId;
		if (num22 != 0L)
		{
			this.SetHeroPower(GameUtils.TranslateDbIdToCardId((int)num22, false));
		}
		if (this.m_isRunActive || this.ShouldShowRunCompletedScreen())
		{
			AdventureDungeonCrawlDisplay.s_shouldShowWelcomeBanner = false;
		}
		this.InitializePlayMat();
		if (this.ShouldShowRunCompletedScreen())
		{
			if (this.m_isPVPDR)
			{
				this.ShowDuelsEndRun();
			}
			else
			{
				this.ShowRunEnd(this.m_defeatedBossIds, this.m_bossWhoDefeatedMeId);
			}
			this.SetUpBossKillCounter(this.m_playerHeroData.DeckClass);
			this.SetUpDeckList(deckCardListPremium, flag, false, list7, list8);
			this.SetUpHeroPortrait(this.m_playerHeroData);
			if (!SceneMgr.Get().IsInDuelsMode())
			{
				this.SetUpPhoneRunCompleteScreen();
			}
		}
		else if (!this.m_isRunActive)
		{
			if (!this.m_dungeonCrawlData.HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure())
			{
				this.TryShowWelcomeBanner();
			}
			bool flag3 = !this.m_isPVPDR;
			if (this.m_mustPickShrine)
			{
				if (this.m_shrineOptions == null && this.m_dungeonCrawlData.GetSelectedAdventure() == AdventureDbId.TRL)
				{
					this.m_shrineOptions = this.GetDefaultStartingShrineOptions_TRL();
				}
				if (this.m_shrineOptions != null && this.m_selectedShrineIndex == 0)
				{
					this.SetPlaymatStateForShrineSelection(this.m_shrineOptions);
					this.SetUpHeroPortrait(this.m_playerHeroData);
					flag3 = false;
				}
				else if (this.m_shrineOptions != null)
				{
					long shrineCardId = this.m_shrineOptions[this.m_selectedShrineIndex - 1];
					this.SetUpDeckListFromShrine(shrineCardId, false);
					TAG_CLASS classFromShrine = this.GetClassFromShrine(shrineCardId);
					this.m_playerHeroData.DeckClass = classFromShrine;
					this.SetUpHeroPortrait(this.m_playerHeroData);
					if (this.m_dungeonCrawlData.SelectedHeroClass == TAG_CLASS.INVALID)
					{
						this.m_dungeonCrawlData.SelectedHeroClass = classFromShrine;
					}
				}
				this.SetUpBossKillCounter(this.m_dungeonCrawlData.SelectedHeroClass);
			}
			else
			{
				if (this.m_dungeonCrawlData.HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure() && this.m_dungeonCrawlData.SelectedHeroClass != TAG_CLASS.INVALID)
				{
					this.m_playerHeroData.DeckClass = this.m_dungeonCrawlData.SelectedHeroClass;
					this.SetUpHeroPortrait(this.m_playerHeroData);
					this.SetUpBossKillCounter(this.m_playerHeroData.DeckClass);
				}
				bool flag4 = this.m_dungeonCrawlData.SelectableLoadoutTreasuresExist();
				bool flag5 = this.m_dungeonCrawlData.SelectableHeroPowersAndDecksExist();
				if (flag4 || flag5)
				{
					if (!this.m_dungeonCrawlData.HasValidLoadoutForSelectedAdventure())
					{
						this.m_currentLoadoutState = AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.INVALID;
						this.GoToNextLoadoutState();
						flag3 = false;
						if (this.m_plotTwistCardDbId != 0L || (this.m_anomalyModeCardDbId != 0L && this.m_dungeonCrawlData.AnomalyModeActivated))
						{
							this.SetUpDeckList(null, flag, false, null, null);
						}
					}
					else if (this.m_isPVPDR)
					{
						this.SetUpDeckList(deckCardListPremium, flag, false, null, null);
						this.m_playMat.ShowPVPDRActiveRun(this.GetPlayButtonTextForNextMission());
						this.m_BackButton.SetText("GLOBAL_LEAVE");
					}
					else if ((this.m_dungeonCrawlDeck == null || this.m_dungeonCrawlDeck.GetTotalCardCount() <= 0) && this.m_dungeonCrawlData.SelectedDeckId != 0L && this.m_playerHeroData.DeckClass != TAG_CLASS.INVALID)
					{
						string text;
						string text2;
						List<long> cards = CollectionManager.Get().LoadDeckFromDBF((int)this.m_dungeonCrawlData.SelectedDeckId, out text, out text2);
						this.SetUpDeckList(CardWithPremiumStatus.ConvertList(cards), flag, false, null, null);
					}
				}
				else if (adventureDataRecord.DungeonCrawlDefaultToDeckFromUpcomingScenario)
				{
					this.SetUpDeckListFromScenario(this.m_dungeonCrawlData.GetMission(), flag);
				}
			}
			if (flag3)
			{
				this.m_playMat.SetUpDefeatedBosses(null, this.m_numBossesInRun);
				this.m_playMat.SetShouldShowBossHeroPowerTooltip(this.ShouldShowBossHeroPowerTutorial());
				this.m_assetLoadingHelper.AddAssetToLoad(1);
				this.m_playMat.SetUpCardBacks(this.m_numBossesInRun - 1, new AdventureDungeonCrawlPlayMat.AssetLoadCompletedCallback(this.m_assetLoadingHelper.AssetLoadCompleted));
				string playButtonText = "GLUE_CHOOSE";
				if (this.m_shouldSkipHeroSelect || this.m_dungeonCrawlData.HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure())
				{
					playButtonText = this.GetPlayButtonTextForNextMission();
				}
				this.m_playMat.ShowNextBoss(playButtonText);
				if (this.m_mustSelectChapter)
				{
					this.m_BackButton.SetText("GLOBAL_LEAVE");
				}
			}
			this.SetUpPhoneNewRunScreen();
		}
		else
		{
			this.SetUpBossKillCounter(this.m_playerHeroData.DeckClass);
			if (adventureDataRecord.DungeonCrawlDefaultToDeckFromUpcomingScenario && (deckCardListPremium == null || deckCardListPremium.Count == 0))
			{
				if ((list7 != null && list7.Count > 0) || (list8 != null && list8.Count > 0))
				{
					Debug.LogWarning("AdventureDungeonCrawlDisplay.InitializeFromGameSaveData() - Setting the deck list using the deck from upcoming scenario, but you have deck card enchantments! Something is probably wrong. Enchantments being ignored.");
				}
				this.SetUpDeckListFromScenario(this.m_dungeonCrawlData.GetMission(), flag);
			}
			else
			{
				this.SetUpDeckList(deckCardListPremium, flag, false, list7, list8);
			}
			this.SetUpHeroPortrait(this.m_playerHeroData);
			this.m_playMat.SetUpDefeatedBosses(this.m_defeatedBossIds, this.m_numBossesInRun);
			this.m_playMat.SetShouldShowBossHeroPowerTooltip(this.ShouldShowBossHeroPowerTutorial());
			this.m_assetLoadingHelper.AddAssetToLoad(1);
			int num23 = (this.m_defeatedBossIds == null) ? 0 : this.m_defeatedBossIds.Count;
			this.m_playMat.SetUpCardBacks(this.m_numBossesInRun - num23 - 1, new AdventureDungeonCrawlPlayMat.AssetLoadCompletedCallback(this.m_assetLoadingHelper.AssetLoadCompleted));
			this.SetPlayMatStateFromGameSaveData();
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_dungeonCrawlDeckTray.gameObject.SetActive(false);
			}
			this.m_retireButton.SetActive(adventureDataRecord.DungeonCrawlIsRetireSupported);
		}
		this.m_assetLoadingHelper.AssetLoadCompleted();
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000D3EC File Offset: 0x0000B5EC
	private void OnPlayMatPaperControllerReady(VisualController paperController)
	{
		if (paperController == null)
		{
			Debug.LogError("paperController was null in OnPlayMatPaperControllerReady!");
			this.m_assetLoadingHelper.AssetLoadCompleted();
			return;
		}
		paperController.RegisterDoneChangingStatesListener(delegate(object _)
		{
			this.m_assetLoadingHelper.AssetLoadCompleted();
		}, null, true, true);
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000D424 File Offset: 0x0000B624
	private void InitializePlayMat()
	{
		this.m_assetLoadingHelper.AddAssetToLoad(1);
		this.m_playMat.Initialize(this.m_dungeonCrawlData);
		Widget component = this.m_playMat.GetComponent<WidgetTemplate>();
		if (component != null)
		{
			component.RegisterDoneChangingStatesListener(delegate(object _)
			{
				this.m_assetLoadingHelper.AssetLoadCompleted();
			}, null, true, true);
			return;
		}
		Error.AddDevWarning("UI Error!", "Could not find PlayMat WidgetTemplate!", Array.Empty<object>());
		this.m_assetLoadingHelper.AssetLoadCompleted();
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000D499 File Offset: 0x0000B699
	private IEnumerator SetPlayMatStateFromGameSaveDataWhenReady()
	{
		while (GameSaveDataManager.Get().IsRequestPending(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey) || GameSaveDataManager.Get().IsRequestPending(AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey) || this.m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			yield return null;
		}
		this.SetPlayMatStateFromGameSaveData();
		yield break;
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
	private string GetPlayButtonTextForNextMission()
	{
		string text = "";
		if (GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.PLAY_BUTTON_TEXT_OVERRIDE, out text) && !string.IsNullOrEmpty(text))
		{
			return text;
		}
		if (this.m_isPVPDR && !this.m_isRunActive && (this.m_realDuelSeedDeck == null || !this.m_realDuelSeedDeck.IsValidForRuleset))
		{
			return "GLUE_PVPDR_BUILD_DECK";
		}
		return "GLOBAL_PLAY";
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0000D50D File Offset: 0x0000B70D
	private bool IsNextMissionASpecialEncounter()
	{
		if (!this.m_hasReceivedGameSaveDataServerKeyResponse)
		{
			Debug.LogError("GetPlayButtonTextForNextMission() - this cannot be called before we've gotten the Game Save Data Server Key response!");
			return false;
		}
		return this.m_dungeonCrawlData.GetMissionOverride() > ScenarioDbId.INVALID;
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0000D534 File Offset: 0x0000B734
	private void SetPlayMatStateFromGameSaveData()
	{
		List<long> list = null;
		List<long> list2 = null;
		List<long> list3 = null;
		List<long> list4 = null;
		long num = 0L;
		long num2 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_A, out list2);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_B, out list3);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_C, out list4);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_OPTION, out list);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT, out num);
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_TREASURE, out num2);
		bool flag = DungeonCrawlUtil.IsDungeonRunActive(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey);
		this.m_playMat.IsNextMissionASpecialEncounter = this.IsNextMissionASpecialEncounter();
		if (this.m_backButtonHighlight != null)
		{
			this.m_backButtonHighlight.ChangeState(ActorStateType.NONE);
		}
		if (this.m_isPVPDR)
		{
			if (Cheats.Get().HasCheatTreasureIds() && list != null && list.Count > 0)
			{
				List<int> list5;
				Cheats.Get().SaveDuelsCheatTreasures(out list5);
				Cheats.Get().ClearCheatTreasures();
				int num3 = Mathf.Min(list5.Count, list.Count);
				for (int i = 0; i < num3; i++)
				{
					list[i] = (long)list5[i];
				}
			}
			if (Cheats.Get().HasCheatLootIds() && num == 0L && ((list2 != null && list2.Count > 0) || (list3 != null && list3.Count > 0) || (list4 != null && list4.Count > 0)))
			{
				List<int> list6;
				List<int> list7;
				List<int> list8;
				Cheats.Get().SaveDuelsCheatLoot(out list6, out list7, out list8);
				Cheats.Get().ClearCheatLoot();
				for (int j = 0; j < list6.Count; j++)
				{
					list2[j + 1] = (long)list6[j];
				}
				for (int k = 0; k < list7.Count; k++)
				{
					list3[k + 1] = (long)list7[k];
				}
				for (int l = 0; l < list8.Count; l++)
				{
					list4[l + 1] = (long)list8[l];
				}
			}
		}
		if (flag && num2 == 0L && list != null && list.Count > 0)
		{
			this.m_playMat.ShowTreasureOptions(list);
			return;
		}
		if (flag && num == 0L && ((list2 != null && list2.Count > 0) || (list3 != null && list3.Count > 0) || (list4 != null && list4.Count > 0)))
		{
			this.m_playMat.ShowLootOptions(list2, list3, list4);
			return;
		}
		if (!flag)
		{
			this.m_playMat.SetUpDefeatedBosses(null, this.m_numBossesInRun);
			this.m_playMat.SetShouldShowBossHeroPowerTooltip(this.ShouldShowBossHeroPowerTutorial());
			this.m_playMat.SetUpCardBacks(this.m_numBossesInRun - 1, null);
		}
		if (this.m_isPVPDR)
		{
			this.m_playMat.ShowPVPDRActiveRun(this.GetPlayButtonTextForNextMission());
			return;
		}
		this.m_playMat.ShowNextBoss(this.GetPlayButtonTextForNextMission());
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0000D80C File Offset: 0x0000BA0C
	private void SetPlaymatStateForShrineSelection(List<long> shrineOptions)
	{
		if (shrineOptions == null || shrineOptions.Count == 0)
		{
			Log.Adventures.PrintError("SetPlaymatStateForShrineSelection: No shrine options found for adventure.", Array.Empty<object>());
			return;
		}
		this.SetShowDeckButtonEnabled(false);
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_DECK_SELECTION_TUTORIAL, out num);
		if (num == 0L)
		{
			this.m_playMat.ShowEmptyState();
			base.StartCoroutine(this.ShowDeckSelectionTutorialPopupWhenReady(delegate
			{
				this.m_playMat.ShowShrineOptions(shrineOptions);
			}));
			return;
		}
		this.m_playMat.ShowShrineOptions(shrineOptions);
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000D8AE File Offset: 0x0000BAAE
	private List<long> GetDefaultStartingShrineOptions_TRL()
	{
		return new List<long>
		{
			52891L,
			51920L,
			53036L
		};
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0000D8D9 File Offset: 0x0000BAD9
	private IEnumerator ShowDeckSelectionTutorialPopupWhenReady(Action popupDismissedCallback)
	{
		while (!this.m_subsceneTransitionComplete)
		{
			yield return new WaitForEndOfFrame();
		}
		while (AdventureDungeonCrawlDisplay.s_shouldShowWelcomeBanner)
		{
			yield return new WaitForEndOfFrame();
		}
		AdventureDef adventureDef = this.m_dungeonCrawlData.GetAdventureDef();
		if (adventureDef != null && !string.IsNullOrEmpty(adventureDef.m_AdventureDeckSelectionTutorialBannerPrefab))
		{
			BannerManager.Get().ShowBanner(adventureDef.m_AdventureDeckSelectionTutorialBannerPrefab, null, null, delegate()
			{
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_DECK_SELECTION_TUTORIAL, new long[]
				{
					1L
				}), null);
				popupDismissedCallback();
			}, null);
		}
		else
		{
			popupDismissedCallback();
		}
		yield break;
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
	private bool HandleDemoModeReset()
	{
		if (AdventureDungeonCrawlDisplay.IsInDemoMode() && (this.m_numBossesDefeated >= 3 || this.m_bossWhoDefeatedMeId != 0L))
		{
			this.m_isRunActive = false;
			this.m_defeatedBossIds = null;
			this.m_bossWhoDefeatedMeId = 0L;
			this.m_numBossesDefeated = 0;
			base.StartCoroutine(this.ShowDemoThankQuote());
			AdventureDungeonCrawlDisplay.s_shouldShowWelcomeBanner = false;
			return true;
		}
		return false;
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0000D948 File Offset: 0x0000BB48
	private void TryShowWelcomeBanner()
	{
		if (AdventureDungeonCrawlDisplay.s_shouldShowWelcomeBanner)
		{
			AdventureDef adventureDef = this.m_dungeonCrawlData.GetAdventureDef();
			if (adventureDef != null && !string.IsNullOrEmpty(adventureDef.m_AdventureIntroBannerPrefab))
			{
				BannerManager.Get().ShowBanner(adventureDef.m_AdventureIntroBannerPrefab, null, GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_INTRO_BANNER_BUTTON"), delegate()
				{
					AdventureDungeonCrawlDisplay.s_shouldShowWelcomeBanner = false;
				}, null);
				WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(this.m_dungeonCrawlData.GetMission());
				DungeonCrawlSubDef_VOLines.PlayVOLine(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, this.m_playerHeroData.HeroDbId, DungeonCrawlSubDef_VOLines.VOEventType.WELCOME_BANNER, 0, true);
				return;
			}
			AdventureDungeonCrawlDisplay.s_shouldShowWelcomeBanner = false;
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
	private bool ShouldShowBossHeroPowerTutorial()
	{
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_HERO_POWER_TUTORIAL_PROGRESS, out num);
		if (num != 0L)
		{
			return num == 1L;
		}
		if (this.m_numBossesDefeated >= 2)
		{
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_HERO_POWER_TUTORIAL_PROGRESS, new long[]
			{
				1L
			}), null);
			return true;
		}
		return false;
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000DA58 File Offset: 0x0000BC58
	private void ShowRunEnd(List<long> defeatedBossIds, long bossWhoDefeatedMeId)
	{
		this.m_BackButton.Flip(false, true);
		this.m_BackButton.SetEnabled(false, false);
		this.m_assetLoadingHelper.AddAssetToLoad(1);
		this.m_playMat.ShowRunEnd(defeatedBossIds, bossWhoDefeatedMeId, this.m_numBossesInRun, this.HasCompletedAdventureWithAllClasses(), this.GetRunWinsForClass(this.m_playerHeroData.DeckClass) == 1L, this.GetNumberOfClassesThatHaveCompletedAdventure(), AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey, new AdventureDungeonCrawlPlayMat.AssetLoadCompletedCallback(this.m_assetLoadingHelper.AssetLoadCompleted), new AdventureDungeonCrawlBossGraveyard.RunEndSequenceCompletedCallback(this.RunEndCompleted));
	}

	// Token: 0x06000232 RID: 562 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
	private int GetNumberOfClassesThatHaveCompletedAdventure()
	{
		int num = 0;
		foreach (TAG_CLASS tagClass in GameSaveDataManager.GetClassesFromDungeonCrawlProgressMap())
		{
			if (this.GetRunWinsForClass(tagClass) > 0L)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0000DB48 File Offset: 0x0000BD48
	private bool HasCompletedAdventureWithAllClasses()
	{
		List<int> guestHeroesForCurrentAdventure = this.m_dungeonCrawlData.GetGuestHeroesForCurrentAdventure();
		if (guestHeroesForCurrentAdventure.Count > 0)
		{
			using (List<int>.Enumerator enumerator = guestHeroesForCurrentAdventure.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int cardDbId = enumerator.Current;
					TAG_CLASS tagClassFromCardDbId = GameUtils.GetTagClassFromCardDbId(cardDbId);
					if (GameSaveDataManager.GetClassesFromDungeonCrawlProgressMap().Contains(tagClassFromCardDbId) && !this.HasCompletedAdventureWithClass(tagClassFromCardDbId))
					{
						return false;
					}
				}
				return true;
			}
		}
		foreach (TAG_CLASS tagClass in GameSaveDataManager.GetClassesFromDungeonCrawlProgressMap())
		{
			if (!this.HasCompletedAdventureWithClass(tagClass))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000DC10 File Offset: 0x0000BE10
	private bool HasCompletedAdventureWithClass(TAG_CLASS tagClass)
	{
		return this.GetRunWinsForClass(tagClass) > 0L;
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0000DC20 File Offset: 0x0000BE20
	private void RunEndCompleted()
	{
		if (this.m_BackButton == null)
		{
			return;
		}
		this.m_dungeonCrawlData.SelectedHeroClass = TAG_CLASS.INVALID;
		this.m_BackButton.Flip(true, false);
		this.m_BackButton.SetEnabled(true, false);
		if (this.m_backButtonHighlight != null)
		{
			this.m_backButtonHighlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000DC80 File Offset: 0x0000BE80
	private void SetUpBossKillCounter(TAG_CLASS deckClass)
	{
		bool shouldSkipHeroSelect = this.m_shouldSkipHeroSelect;
		long bossWins = 0L;
		long num = 0L;
		this.m_bossKillCounter.SetDungeonRunData(this.m_dungeonCrawlData);
		if (deckClass != TAG_CLASS.INVALID && !shouldSkipHeroSelect)
		{
			this.m_bossKillCounter.SetHeroClass(deckClass);
			bossWins = this.GetBossWinsForClass(deckClass);
			num = this.GetRunWinsForClass(deckClass);
		}
		else if (shouldSkipHeroSelect)
		{
			GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_ALL_CLASSES_TOTAL_BOSS_WINS, out bossWins);
			GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_ALL_CLASSES_TOTAL_RUN_WINS, out num);
		}
		this.m_bossKillCounter.SetBossWins(bossWins);
		if (num > 0L)
		{
			this.m_bossKillCounter.SetRunWins(num);
		}
		this.m_bossKillCounter.UpdateLayout();
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000DD28 File Offset: 0x0000BF28
	private long GetRunWinsForClass(TAG_CLASS tagClass)
	{
		long result = 0L;
		GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys adventureDungeonCrawlClassProgressSubkeys;
		if (GameSaveDataManager.GetProgressSubkeyForDungeonCrawlClass(tagClass, out adventureDungeonCrawlClassProgressSubkeys))
		{
			GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, adventureDungeonCrawlClassProgressSubkeys.runWins, out result);
		}
		return result;
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0000DD5C File Offset: 0x0000BF5C
	public bool IsCardLoadoutTreasure(string cardID)
	{
		if (this.m_dungeonCrawlData != null)
		{
			int num = GameUtils.TranslateCardIdToDbId(cardID, false);
			using (List<AdventureLoadoutTreasuresDbfRecord>.Enumerator enumerator = this.m_dungeonCrawlData.GetLoadoutTreasuresForClass(this.m_dungeonCrawlData.SelectedHeroClass).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CardId == num)
					{
						return true;
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
	private long GetBossWinsForClass(TAG_CLASS tagClass)
	{
		long result = 0L;
		GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys adventureDungeonCrawlClassProgressSubkeys;
		if (GameSaveDataManager.GetProgressSubkeyForDungeonCrawlClass(tagClass, out adventureDungeonCrawlClassProgressSubkeys))
		{
			GameSaveDataManager.Get().GetSubkeyValue(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, adventureDungeonCrawlClassProgressSubkeys.bossWins, out result);
		}
		return result;
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000DE0C File Offset: 0x0000C00C
	private void SetUpDeckListFromShrine(long shrineCardId, bool playDeckGlowAnimation)
	{
		List<long> list = new List<long>();
		CardTagDbfRecord shrineDeckCardTagRecord = GameDbf.CardTag.GetRecord((CardTagDbfRecord r) => r.CardId == (int)shrineCardId && r.TagId == 1099);
		foreach (DeckCardDbfRecord deckCardDbfRecord in GameDbf.DeckCard.GetRecords((DeckCardDbfRecord r) => r.DeckId == shrineDeckCardTagRecord.TagValue, -1))
		{
			list.Add((long)deckCardDbfRecord.CardId);
		}
		list.Add(shrineCardId);
		this.SetUpDeckList(CardWithPremiumStatus.ConvertList(list), false, playDeckGlowAnimation, null, null);
		this.SetShowDeckButtonEnabled(true);
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
	private void SetUpDeckListFromScenario(ScenarioDbId scenario, bool useLoadoutOfActiveRun)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)scenario);
		if (record != null)
		{
			string text;
			string text2;
			List<long> cards = CollectionManager.Get().LoadDeckFromDBF(record.Player1DeckId, out text, out text2);
			this.SetUpDeckList(CardWithPremiumStatus.ConvertList(cards), useLoadoutOfActiveRun, false, null, null);
		}
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000DF0C File Offset: 0x0000C10C
	private void SetUpDeckList(List<CardWithPremiumStatus> deckCardList, bool useLoadoutOfActiveRun, bool playGlowAnimation = false, List<long> deckCardIndices = null, List<long> deckCardEnchantments = null)
	{
		if (this.m_playerHeroData.DeckClass == TAG_CLASS.INVALID)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDisplay.SetUpDeckList() - deckClass is INVALID!", Array.Empty<object>());
			return;
		}
		if (string.IsNullOrEmpty(this.m_playerHeroData.HeroCardId))
		{
			return;
		}
		this.m_dungeonCrawlDeck = new CollectionDeck
		{
			HeroCardID = this.m_playerHeroData.HeroCardId
		};
		this.m_dungeonCrawlDeck.FormatType = FormatType.FT_WILD;
		this.m_dungeonCrawlDeck.Type = DeckType.CLIENT_ONLY_DECK;
		if (this.m_isPVPDR)
		{
			this.m_dungeonCrawlDeck.Name = GameStrings.Get("GLUE_COLLECTION_DUEL_DECKNAME");
			this.m_dungeonCrawlDeck.HeroPowerCardID = GameUtils.TranslateDbIdToCardId((int)this.m_dungeonCrawlData.SelectedHeroPowerDbId, false);
		}
		if (this.m_isPVPDR && !this.m_isRunActive)
		{
			this.m_dungeonCrawlDeck.Type = DeckType.PVPDR_DISPLAY_DECK;
			if (deckCardList != null && deckCardList.Count > this.m_dungeonCrawlDeck.GetMaxCardCount())
			{
				this.m_dungeonCrawlDeck.Type = DeckType.CLIENT_ONLY_DECK;
			}
		}
		if (this.m_anomalyModeCardDbId != 0L && this.m_dungeonCrawlData.AnomalyModeActivated)
		{
			string text = GameUtils.TranslateDbIdToCardId((int)this.m_anomalyModeCardDbId, false);
			if (text != null)
			{
				this.m_dungeonCrawlDeck.AddCard(text, TAG_PREMIUM.NORMAL, false);
			}
			else
			{
				Log.Adventures.PrintWarning("AdventureDungeonCrawlDisplay.SetUpDeckList() - No cardId for anomalyCardDbId {0}!", new object[]
				{
					this.m_anomalyModeCardDbId
				});
			}
		}
		if (this.m_plotTwistCardDbId != 0L)
		{
			string text2 = GameUtils.TranslateDbIdToCardId((int)this.m_plotTwistCardDbId, false);
			if (text2 != null)
			{
				this.m_dungeonCrawlDeck.AddCard(text2, TAG_PREMIUM.NORMAL, false);
			}
			else
			{
				Log.Adventures.PrintWarning("AdventureDungeonCrawlDisplay.SetUpDeckList() - No cardId for m_plotTwistCardDbId {0}!", new object[]
				{
					this.m_plotTwistCardDbId
				});
			}
		}
		if (!useLoadoutOfActiveRun && this.m_dungeonCrawlData.SelectedLoadoutTreasureDbId != 0L)
		{
			string text3 = GameUtils.TranslateDbIdToCardId((int)this.m_dungeonCrawlData.SelectedLoadoutTreasureDbId, false);
			if (!string.IsNullOrEmpty(text3))
			{
				CollectionDeckSlot collectionDeckSlot = this.m_dungeonCrawlDeck.FindFirstSlotByCardId(text3);
				if (collectionDeckSlot == null || collectionDeckSlot.Count == 0)
				{
					this.m_dungeonCrawlDeck.AddCard(text3, TAG_PREMIUM.NORMAL, false);
				}
			}
			else
			{
				Log.Adventures.PrintWarning("AdventureDungeonCrawlDisplay.SetUpDeckList() - No cardId for SelectedLoadoutTreasureDbId {0}!", new object[]
				{
					this.m_dungeonCrawlData.SelectedLoadoutTreasureDbId
				});
			}
		}
		if (deckCardList != null)
		{
			Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
			if (deckCardIndices != null && deckCardEnchantments != null && deckCardIndices.Count == deckCardEnchantments.Count)
			{
				for (int i = 0; i < deckCardIndices.Count; i++)
				{
					List<int> list;
					if (!dictionary.TryGetValue((int)deckCardIndices[i], out list))
					{
						list = new List<int>();
						dictionary.Add((int)deckCardIndices[i], list);
					}
					list.Add((int)deckCardEnchantments[i]);
				}
			}
			for (int j = 0; j < deckCardList.Count; j++)
			{
				long cardId = deckCardList[j].cardId;
				TAG_PREMIUM premium = deckCardList[j].premium;
				if (cardId != 0L)
				{
					string text4 = GameUtils.TranslateDbIdToCardId((int)cardId, false);
					List<int> enchantments;
					if (text4 == null)
					{
						Log.Adventures.PrintWarning("AdventureDungeonCrawlDisplay.SetUpDeckList() - No cardId for dbId {0}!", new object[]
						{
							cardId
						});
					}
					else if (dictionary.TryGetValue(j + 1, out enchantments))
					{
						this.m_dungeonCrawlDeck.AddCard_DungeonCrawlBuff(text4, premium, enchantments);
					}
					else
					{
						this.m_dungeonCrawlDeck.AddCard(text4, premium, false);
					}
				}
			}
		}
		this.m_dungeonCrawlDeckTray.SetDungeonCrawlDeck(this.m_dungeonCrawlDeck, playGlowAnimation);
		this.SetUpCardsCreatedByTreasures();
		this.SetUpPhoneNewRunScreen();
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0000E258 File Offset: 0x0000C458
	private void SetUpHeroPortrait(AdventureDungeonCrawlDisplay.PlayerHeroData playerHeroData)
	{
		if (this.m_heroActor == null)
		{
			Log.Adventures.PrintError("Unable to change hero portrait. No hero actor has been loaded.", Array.Empty<object>());
			return;
		}
		if (string.IsNullOrEmpty(playerHeroData.HeroCardId))
		{
			return;
		}
		bool flag = this.IsInDefeatScreen();
		NetCache.CardDefinition favoriteHero = CollectionManager.Get().GetFavoriteHero(playerHeroData.DeckClass);
		bool flag2 = this.m_dungeonCrawlData.GuestHeroesExistForCurrentAdventure();
		TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
		if (!flag && !flag2 && favoriteHero != null && !GameUtils.IsVanillaHero(favoriteHero.Name))
		{
			premium = TAG_PREMIUM.GOLDEN;
		}
		this.SetHero(playerHeroData.HeroCardId, premium);
		if (flag)
		{
			this.m_heroActor.GetComponent<Animation>().Play("AllyDefeat_Desat");
		}
		if (this.m_heroHealth == 0L)
		{
			CardTagDbfRecord record = GameDbf.CardTag.GetRecord((CardTagDbfRecord r) => r.CardId == playerHeroData.HeroDbId && r.TagId == 45);
			this.m_heroHealth = (long)record.TagValue;
		}
		this.SetHeroHealthVisual(this.m_heroActor, !flag);
		if (this.m_dungeonCrawlDeckSelect != null)
		{
			this.SetHeroHealthVisual(this.m_dungeonCrawlDeckSelect.heroDetails.HeroActor, !flag);
		}
	}

	// Token: 0x0600023E RID: 574 RVA: 0x0000E380 File Offset: 0x0000C580
	private void SetHero(string cardID, TAG_PREMIUM premium)
	{
		if (this.m_heroActor == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDisplay.SetHero was called but m_heroActor was null", Array.Empty<object>());
			return;
		}
		using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(cardID, null))
		{
			if (!(((fullDef != null) ? fullDef.CardDef : null) == null) && fullDef.EntityDef != null)
			{
				this.m_heroActor.SetCardDef(fullDef.DisposableCardDef);
				this.m_heroActor.SetEntityDef(fullDef.EntityDef);
				fullDef.CardDef.m_AlwaysRenderPremiumPortrait = false;
				this.m_heroActor.SetPremium(premium);
				this.m_heroActor.UpdateAllComponents();
				this.m_heroActor.Show();
				this.m_heroClassIconsControllerReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnHeroClassIconsControllerReady));
			}
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000E460 File Offset: 0x0000C660
	private void SetHeroPower(string cardID)
	{
		if (this.m_heroPowerActor == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDisplay.SetHeroPower was called but m_heroPowerActor was null.", Array.Empty<object>());
			return;
		}
		BoxCollider component = this.m_heroPowerActor.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.enabled = false;
		}
		if (string.IsNullOrEmpty(cardID))
		{
			this.m_heroPowerActor.Hide();
			return;
		}
		DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(cardID, null);
		if (((fullDef != null) ? fullDef.CardDef : null) == null || ((fullDef != null) ? fullDef.EntityDef : null) == null)
		{
			return;
		}
		DefLoader.DisposableFullDef currentHeroPowerFullDef = this.m_currentHeroPowerFullDef;
		if (currentHeroPowerFullDef != null)
		{
			currentHeroPowerFullDef.Dispose();
		}
		this.m_currentHeroPowerFullDef = fullDef;
		this.m_heroPowerActor.SetFullDef(fullDef);
		this.m_heroPowerActor.UpdateAllComponents();
		this.m_heroPowerActor.Show();
		if (component != null)
		{
			component.enabled = true;
		}
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000E53C File Offset: 0x0000C73C
	private void SetUpPhoneNewRunScreen()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		this.m_dungeonCrawlDeckTray.gameObject.SetActive(false);
		bool showDeckButtonEnabled = this.m_dungeonCrawlDeck != null && this.m_dungeonCrawlDeck.GetTotalCardCount() > 0;
		this.SetShowDeckButtonEnabled(showDeckButtonEnabled);
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000E588 File Offset: 0x0000C788
	public void SetShowDeckButtonEnabled(bool enabled)
	{
		if (!UniversalInputManager.UsePhoneUI || enabled == this.m_ShowDeckButton.IsEnabled())
		{
			return;
		}
		this.m_ShowDeckButton.SetEnabled(enabled, false);
		this.m_ShowDeckButton.Flip(enabled, false);
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
	private void SetUpPhoneRunCompleteScreen()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		this.m_ShowDeckButtonFrame.SetActive(false);
		this.m_ShowDeckNoButtonFrame.SetActive(false);
		this.m_TrayFrameDefault.SetActive(false);
		this.m_TrayFrameRunComplete.SetActive(true);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_dungeonCrawlDeckTray.gameObject.SetActive(false);
		}
		GameUtils.SetParent(this.m_AdventureTitle, this.m_AdventureTitleRunCompleteBone, false);
		this.m_PhoneDeckTray.SetActive(true);
		GameUtils.SetParent(this.m_PhoneDeckTray, this.m_DeckTrayRunCompleteBone, false);
		TraySection traySection = (TraySection)GameUtils.Instantiate(this.m_DeckListHeaderPrefab, this.m_DeckListHeaderRunCompleteBone, true);
		this.m_dungeonCrawlDeckTray.OffsetDeckBigCardByVector(this.m_DeckBigCardOffsetForRunCompleteState);
		traySection.m_deckBox.m_neverUseGoldenPortraits = this.IsInDefeatScreen();
		traySection.m_deckBox.SetHeroCardID(this.m_playerHeroData.HeroCardId);
		traySection.m_deckBox.HideBanner();
		traySection.m_deckBox.SetDeckName(this.GetClassNameFromDeckClass(this.m_playerHeroData.DeckClass));
		traySection.m_deckBox.HideRenameVisuals();
		traySection.m_deckBox.SetDeckNameAsSingleLine(true);
		if (this.IsInDefeatScreen())
		{
			traySection.m_deckBox.PlayDesaturationAnimation();
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
	private bool IsInDefeatScreen()
	{
		return this.m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_BOSS_GRAVEYARD && this.m_numBossesDefeated < this.m_numBossesInRun;
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0000E718 File Offset: 0x0000C918
	private void SetUpCardsCreatedByTreasures()
	{
		if (this.m_cardsAddedToDeckMap == null || this.m_cardsAddedToDeckMap.Count == 0 || this.m_cardsAddedToDeckMap.Count % 2 == 1)
		{
			return;
		}
		Dictionary<long, long> dictionary = new Dictionary<long, long>();
		for (int i = 0; i < this.m_cardsAddedToDeckMap.Count; i += 2)
		{
			dictionary[this.m_cardsAddedToDeckMap[i]] = this.m_cardsAddedToDeckMap[i + 1];
		}
		this.m_dungeonCrawlDeckTray.CardIdToCreatorMap = dictionary;
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000E794 File Offset: 0x0000C994
	public static bool OnNavigateBack()
	{
		if (AdventureDungeonCrawlDisplay.m_instance == null)
		{
			Debug.LogError("Trying to navigate back, but AdventureDungeonCrawlDisplay has been destroyed!");
			return false;
		}
		AdventureDungeonCrawlPlayMat playMat = AdventureDungeonCrawlDisplay.m_instance.m_playMat;
		if (playMat != null)
		{
			if (playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
			{
				return false;
			}
			playMat.HideBossHeroPowerTooltip(true);
		}
		if (UniversalInputManager.UsePhoneUI && AdventureDungeonCrawlDisplay.m_instance.m_dungeonCrawlDeckTray != null)
		{
			AdventureDungeonCrawlDisplay.m_instance.m_dungeonCrawlDeckTray.gameObject.SetActive(false);
		}
		if (playMat != null && playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_BOSS_GRAVEYARD && AdventureDungeonCrawlDisplay.m_instance.m_mustSelectChapter)
		{
			if (AdventureDungeonCrawlDisplay.m_instance.m_subsceneController != null)
			{
				AdventureDungeonCrawlDisplay.m_instance.m_subsceneController.ChangeSubScene(AdventureData.Adventuresubscene.LOCATION_SELECT, false);
			}
		}
		else if (AdventureDungeonCrawlDisplay.m_instance.m_subsceneController != null)
		{
			AdventureDungeonCrawlDisplay.m_instance.m_subsceneController.SubSceneGoBack(true);
		}
		return true;
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0000E86D File Offset: 0x0000CA6D
	private void OnBackButtonPress(UIEvent e)
	{
		this.EnableBackButton(false);
		Navigation.GoBack();
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0000E87C File Offset: 0x0000CA7C
	private void GoToHeroSelectSubscene()
	{
		bool flag = this.m_dungeonCrawlData.GuestHeroesExistForCurrentAdventure();
		this.m_playMat.PlayButton.Disable(false);
		AdventureData.Adventuresubscene subscene = flag ? AdventureData.Adventuresubscene.ADVENTURER_PICKER : AdventureData.Adventuresubscene.MISSION_DECK_PICKER;
		if (this.m_subsceneController != null)
		{
			this.m_subsceneController.ChangeSubScene(subscene, true);
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000E8C1 File Offset: 0x0000CAC1
	private void GoBackToHeroPower()
	{
		this.m_dungeonCrawlData.SelectedHeroPowerDbId = 0L;
		this.SetHeroPower(null);
		base.StartCoroutine(this.ShowHeroPowerOptionsWhenReady());
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000E8E4 File Offset: 0x0000CAE4
	private void GoBackFromHeroPower()
	{
		this.m_playMat.PlayHeroPowerOptionSelected();
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000E8F1 File Offset: 0x0000CAF1
	private void GoBackToTreasureLoadoutSelection()
	{
		this.SetUpDeckList(new List<CardWithPremiumStatus>(), false, true, null, null);
		this.m_dungeonCrawlData.SelectedLoadoutTreasureDbId = 0L;
		base.StartCoroutine(this.ShowTreasureSatchelWhenReady());
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000E91C File Offset: 0x0000CB1C
	private void GoBackFromTreasureLoadoutSelection()
	{
		this.m_playMat.PlayTreasureSatchelOptionHidden();
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000E92C File Offset: 0x0000CB2C
	private void GoBackFromDeckTemplateSelection()
	{
		this.m_dungeonCrawlData.SelectedDeckId = 0L;
		this.SetUpDeckList(new List<CardWithPremiumStatus>(), false, true, null, null);
		if (UniversalInputManager.UsePhoneUI)
		{
			if (this.m_dungeonCrawlDeckSelect == null || !this.m_dungeonCrawlDeckSelect.isReady)
			{
				Error.AddDevWarning("UI Error!", "AdventureDeckSelectWidget is not setup correctly or is not ready!", Array.Empty<object>());
				return;
			}
			this.m_dungeonCrawlDeckSelect.deckTray.SetDungeonCrawlDeck(this.m_dungeonCrawlDeck, false);
			this.m_dungeonCrawlDeckSelect.playButton.Disable(false);
		}
		else
		{
			this.m_playMat.PlayButton.Disable(false);
		}
		this.m_playMat.DeselectAllDeckOptionsWithoutId(0);
		this.m_playMat.PlayDeckOptionSelected();
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
	private static bool OnNavigateBackFromCurrentLoadoutState()
	{
		AdventureDungeonCrawlPlayMat playMat = AdventureDungeonCrawlDisplay.m_instance.m_playMat;
		if (playMat != null && playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			return false;
		}
		if (AdventureDungeonCrawlDisplay.m_instance == null)
		{
			Debug.LogError("Trying to navigate back to previous loadout selection, but AdventureDungeonCrawlDisplay has been destroyed!");
			return false;
		}
		switch (AdventureDungeonCrawlDisplay.m_instance.m_currentLoadoutState)
		{
		case AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.HEROPOWER:
			AdventureDungeonCrawlDisplay.m_instance.GoBackFromHeroPower();
			if (AdventureDungeonCrawlDisplay.m_instance.m_dungeonCrawlData.SelectableLoadoutTreasuresExist() && !AdventureDungeonCrawlDisplay.m_instance.m_isPVPDR)
			{
				AdventureDungeonCrawlDisplay.m_instance.GoBackToTreasureLoadoutSelection();
			}
			break;
		case AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.TREASURE:
			AdventureDungeonCrawlDisplay.m_instance.GoBackFromTreasureLoadoutSelection();
			if (AdventureDungeonCrawlDisplay.m_instance.m_isPVPDR)
			{
				AdventureDungeonCrawlDisplay.m_instance.GoBackToHeroPower();
			}
			break;
		case AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.DECKTEMPLATE:
			AdventureDungeonCrawlDisplay.m_instance.GoBackFromDeckTemplateSelection();
			AdventureDungeonCrawlDisplay.m_instance.GoBackToHeroPower();
			break;
		}
		return true;
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
	private void GoToNextLoadoutState()
	{
		switch (this.m_currentLoadoutState)
		{
		case AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.INVALID:
			if (this.m_dungeonCrawlData.SelectableLoadoutTreasuresExist() && !this.m_isPVPDR)
			{
				base.StartCoroutine(this.ShowTreasureSatchelWhenReady());
				return;
			}
			if (this.m_dungeonCrawlData.SelectableHeroPowersAndDecksExist())
			{
				base.StartCoroutine(this.ShowHeroPowerOptionsWhenReady());
			}
			break;
		case AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.HEROPOWER:
			if (this.m_isPVPDR)
			{
				this.SetUpDeckList(null, false, false, null, null);
				base.StartCoroutine(this.ShowTreasureSatchelWhenReady());
				return;
			}
			base.StartCoroutine(this.ShowDeckOptionsWhenReady());
			return;
		case AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.TREASURE:
			if (this.m_isPVPDR)
			{
				this.SetUpDeckList(null, false, false, null, null);
				this.LockInDuelsLoadoutSelections();
				base.StartCoroutine(this.ShowBuildDeckButtonWhenReady());
				return;
			}
			base.StartCoroutine(this.ShowHeroPowerOptionsWhenReady());
			return;
		case AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.DECKTEMPLATE:
			break;
		default:
			return;
		}
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000EB88 File Offset: 0x0000CD88
	private void LockInDuelsLoadoutSelections()
	{
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(AdventureDungeonCrawlDisplay.OnNavigateBackFromCurrentLoadoutState));
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(GuestHeroPickerTrayDisplay.OnNavigateBack));
		if (this.m_dungeonCrawlData.HasValidLoadoutForSelectedAdventure())
		{
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			this.m_dungeonCrawlData.GetSelectedAdventureDataRecord();
			list.Add(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, new long[]
			{
				(long)this.m_dungeonCrawlData.SelectedHeroClass
			}));
			list.Add(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, new long[]
			{
				this.m_dungeonCrawlData.SelectedHeroPowerDbId
			}));
			list.Add(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, new long[]
			{
				this.m_dungeonCrawlData.SelectedLoadoutTreasureDbId
			}));
			GameSaveDataManager.Get().SaveSubkeys(list, null);
			this.m_BackButton.SetText("GLOBAL_LEAVE");
		}
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000EC74 File Offset: 0x0000CE74
	private void LockInNewRunSelectionsAndTransition()
	{
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(AdventureDungeonCrawlDisplay.OnNavigateBackFromCurrentLoadoutState));
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(GuestHeroPickerTrayDisplay.OnNavigateBack));
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(AdventureLocationSelectBook.OnNavigateBack));
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(AdventureBookPageManager.NavigateToMapPage));
		if (this.m_subsceneController != null)
		{
			this.m_subsceneController.RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene.CHOOSER);
		}
		if (this.m_dungeonCrawlData.HasValidLoadoutForSelectedAdventure())
		{
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			AdventureDataDbfRecord selectedAdventureDataRecord = this.m_dungeonCrawlData.GetSelectedAdventureDataRecord();
			if (selectedAdventureDataRecord.DungeonCrawlSelectChapter)
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_SCENARIO_ID, new long[]
				{
					(long)this.m_dungeonCrawlData.GetMission()
				}));
			}
			if (selectedAdventureDataRecord.DungeonCrawlPickHeroFirst)
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, new long[]
				{
					(long)this.m_dungeonCrawlData.SelectedHeroClass
				}));
			}
			if (this.m_dungeonCrawlData.SelectableHeroPowersExist())
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, new long[]
				{
					this.m_dungeonCrawlData.SelectedHeroPowerDbId
				}));
			}
			if (!this.m_isPVPDR && this.m_dungeonCrawlData.SelectableDecksExist())
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_DECK, new long[]
				{
					this.m_dungeonCrawlData.SelectedDeckId
				}));
			}
			if (this.m_dungeonCrawlData.SelectableLoadoutTreasuresExist())
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, new long[]
				{
					this.m_dungeonCrawlData.SelectedLoadoutTreasureDbId
				}));
			}
			long num = this.m_dungeonCrawlData.AnomalyModeActivated ? 1L : 0L;
			list.Add(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_ANOMALY_MODE, new long[]
			{
				num
			}));
			GameSaveDataManager.Get().SaveSubkeys(list, null);
			this.SetShowDeckButtonEnabled(true);
			this.m_BackButton.SetText("GLOBAL_LEAVE");
			base.StartCoroutine(this.SetPlayMatStateFromGameSaveDataWhenReady());
			return;
		}
		Navigation.GoBack();
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000EE78 File Offset: 0x0000D078
	private void OnPlayButtonPress(UIEvent e)
	{
		PlayButton playButton = e.GetElement() as PlayButton;
		if (playButton != null)
		{
			playButton.Disable(false);
		}
		this.m_playMat.HideBossHeroPowerTooltip(true);
		if (this.m_dungeonCrawlData.DoesSelectedMissionRequireDeck() && !this.m_dungeonCrawlData.HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure() && !this.m_shouldSkipHeroSelect && (this.m_numBossesDefeated == 0 || !this.m_isRunActive))
		{
			this.GoToHeroSelectSubscene();
			return;
		}
		if (this.m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS && this.m_playMat.GetPlayMatOptionType() == AdventureDungeonCrawlPlayMat.OptionType.DECK)
		{
			this.m_playMat.PlayDeckOptionSelected();
			this.LockInNewRunSelectionsAndTransition();
			return;
		}
		if (!this.m_isPVPDR || (this.m_realDuelSeedDeck != null && this.m_realDuelSeedDeck.IsValidForRuleset) || this.m_isRunActive)
		{
			this.QueueForGame();
			return;
		}
		if (this.m_realDuelSeedDeck == null)
		{
			this.CreateDuelsSeedDeck();
			return;
		}
		playButton.Enable();
		this.EditCurrentDeck();
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0000EF5C File Offset: 0x0000D15C
	private void QueueForGame()
	{
		List<int> guestHeroesForCurrentAdventure = this.m_dungeonCrawlData.GetGuestHeroesForCurrentAdventure();
		bool flag = guestHeroesForCurrentAdventure.Count > 0 && (!this.m_shouldSkipHeroSelect || this.m_mustPickShrine);
		int heroCardDbId = GameUtils.GetFavoriteHeroCardDBIdFromClass(this.m_playerHeroData.DeckClass);
		if (flag)
		{
			TAG_CLASS tag_CLASS = this.m_dungeonCrawlData.SelectedHeroClass;
			if (this.m_isRunActive || this.m_mustPickShrine)
			{
				tag_CLASS = this.m_playerHeroData.DeckClass;
			}
			bool flag2 = false;
			foreach (int num in guestHeroesForCurrentAdventure)
			{
				if (GameUtils.GetTagClassFromCardDbId(num) == tag_CLASS)
				{
					flag2 = true;
					heroCardDbId = num;
					break;
				}
			}
			if (!flag2)
			{
				Debug.LogErrorFormat("Attempting to start a game with class {0}, but no guest hero found of that class!", new object[]
				{
					tag_CLASS
				});
				return;
			}
		}
		long deckid = 0L;
		bool flag3 = false;
		if (this.m_isPVPDR)
		{
			CollectionDeck duelsDeck = CollectionManager.Get().GetDuelsDeck();
			if (this.m_realDuelSeedDeck != null)
			{
				deckid = this.m_realDuelSeedDeck.ID;
			}
			else if (duelsDeck != null)
			{
				deckid = duelsDeck.ID;
			}
			else
			{
				Debug.LogError("Valid duels deck not found, canceling queue");
				flag3 = true;
			}
		}
		if (flag3)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR_DECK_ERROR_TITLE");
			popupInfo.m_text = GameStrings.Get("GLUE_PVPDR_DECK_ERROR_DESC");
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		if (this.m_isPVPDR)
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.DUELS_QUEUE
			});
			PVPDRLobbyDataModel pvpdrlobbyDataModel = PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel();
			if (pvpdrlobbyDataModel != null)
			{
				SessionRecord sessionRecord = new SessionRecord();
				sessionRecord.Wins = (uint)pvpdrlobbyDataModel.Wins;
				sessionRecord.Losses = (uint)pvpdrlobbyDataModel.Losses;
				sessionRecord.RunFinished = false;
				sessionRecord.SessionRecordType = SessionRecordType.DUELS;
				BnetPresenceMgr.Get().SetGameFieldBlob(22U, sessionRecord);
			}
		}
		GameMgr.Get().FindGameWithHero(this.m_dungeonCrawlData.GameType, FormatType.FT_WILD, (int)this.m_dungeonCrawlData.GetMissionToPlay(), 0, heroCardDbId, deckid);
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000F170 File Offset: 0x0000D370
	private void OnShowDeckButtonPress(UIEvent e)
	{
		this.ShowMobileDeckTray(this.m_dungeonCrawlDeckTray.gameObject.GetComponent<SlidingTray>());
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000F188 File Offset: 0x0000D388
	protected void OnSubSceneLoaded(object sender, EventArgs args)
	{
		this.m_playMat.OnSubSceneLoaded();
		this.m_playMat.SetRewardOptionSelectedCallback(new AdventureDungeonCrawlPlayMat.RewardOptionSelectedCallback(this.OnRewardOptionSelected));
		this.m_playMat.SetTreasureSatchelOptionSelectedCallback(new AdventureDungeonCrawlTreasureOption.TreasureSelectedOptionCallback(this.OnTreasureSatchelOptionSelected));
		this.m_playMat.SetHeroPowerOptionCallback(new AdventureDungeonCrawlHeroPowerOption.HeroPowerSelectedOptionCallback(this.OnHeroPowerOptionSelected), new AdventureDungeonCrawlHeroPowerOption.HeroPowerHoverOptionCallback(this.OnHeroPowerOptionRollover), new AdventureDungeonCrawlHeroPowerOption.HeroPowerHoverOptionCallback(this.OnHeroPowerOptionRollout));
		this.m_playMat.SetDeckOptionSelectedCallback(new AdventureDungeonCrawlDeckOption.DeckOptionSelectedCallback(this.OnDeckOptionSelected));
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0000F214 File Offset: 0x0000D414
	protected void OnSubSceneTransitionComplete(object sender, EventArgs args)
	{
		this.m_subsceneTransitionComplete = true;
		if (this.m_dungeonCrawlDeckTray != null)
		{
			this.m_dungeonCrawlDeckTray.gameObject.SetActive(true);
		}
		if (this.m_playMat != null)
		{
			this.m_playMat.OnSubSceneTransitionComplete();
		}
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0000F260 File Offset: 0x0000D460
	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("AdventureDungeonCrawlDisplay.OnHeroActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		this.m_heroActor = go.GetComponent<Actor>();
		if (this.m_heroActor == null)
		{
			Debug.LogWarning(string.Format("AdventureDungeonCrawlDisplay.OnHeroActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		this.m_heroActor.transform.parent = this.m_socketHeroBone.transform;
		this.m_heroActor.transform.localPosition = Vector3.zero;
		this.m_heroActor.transform.localScale = Vector3.one;
		this.m_heroActor.Hide();
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000F304 File Offset: 0x0000D504
	private void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("AdventureDungeonCrawlDisplay.OnHeroPowerActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		this.m_heroPowerActor = go.GetComponent<Actor>();
		if (this.m_heroPowerActor == null)
		{
			Debug.LogWarning(string.Format("AdventureDungeonCrawlDisplay.OnHeroPowerActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		this.m_heroPowerActor.transform.parent = this.m_heroPowerBone;
		this.m_heroPowerActor.transform.localPosition = Vector3.zero;
		this.m_heroPowerActor.transform.localRotation = Quaternion.identity;
		this.m_heroPowerActor.transform.localScale = Vector3.one;
		this.m_heroPowerActor.Hide();
		this.m_heroPowerActor.SetUnlit();
		PegUIElement pegUIElement = go.AddComponent<PegUIElement>();
		go.AddComponent<BoxCollider>().enabled = false;
		pegUIElement.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
		{
			this.ShowBigCard(this.m_heroPowerBigCard, this.m_currentHeroPowerFullDef, this.m_HeroPowerBigCardBone);
		});
		pegUIElement.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
		{
			BigCardHelper.HideBigCard(this.m_heroPowerBigCard);
		});
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0000F3FC File Offset: 0x0000D5FC
	private void SetHeroHealthVisual(Actor actor, bool show)
	{
		if (actor == null)
		{
			Log.Adventures.PrintError("SetHeroHealthVisual: actor provided is null!", Array.Empty<object>());
			return;
		}
		actor.GetHealthObject().gameObject.SetActive(show);
		if (show)
		{
			actor.GetHealthText().Text = Convert.ToString(this.m_heroHealth);
			actor.GetHealthText().AmbientLightBlend = 0f;
		}
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0000F461 File Offset: 0x0000D661
	private IEnumerator ShowTreasureSatchelWhenReady()
	{
		TAG_CLASS heroClass = this.m_dungeonCrawlData.SelectedHeroClass;
		while (this.m_playMat == null || this.m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			yield return null;
		}
		List<AdventureLoadoutTreasuresDbfRecord> loadoutTreasuresForClass = this.m_dungeonCrawlData.GetLoadoutTreasuresForClass(heroClass);
		this.m_playMat.ShowTreasureSatchel(loadoutTreasuresForClass, AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey);
		this.m_currentLoadoutState = AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.TREASURE;
		this.EnableBackButton(true);
		if (this.m_isPVPDR)
		{
			Navigation.PushUnique(new Navigation.NavigateBackHandler(AdventureDungeonCrawlDisplay.OnNavigateBackFromCurrentLoadoutState));
		}
		yield break;
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000F470 File Offset: 0x0000D670
	private IEnumerator ShowHeroPowerOptionsWhenReady()
	{
		TAG_CLASS heroPowerClass = this.m_dungeonCrawlData.SelectedHeroClass;
		while (this.m_playMat == null || this.m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			yield return null;
		}
		List<AdventureHeroPowerDbfRecord> heroPowersForClass = this.m_dungeonCrawlData.GetHeroPowersForClass(heroPowerClass);
		this.m_playMat.ShowHeroPowers(heroPowersForClass, AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey);
		this.m_currentLoadoutState = AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.HEROPOWER;
		this.EnableBackButton(true);
		if (!this.m_isPVPDR && AdventureDungeonCrawlDisplay.m_instance.m_dungeonCrawlData.SelectableLoadoutTreasuresExist())
		{
			Navigation.PushUnique(new Navigation.NavigateBackHandler(AdventureDungeonCrawlDisplay.OnNavigateBackFromCurrentLoadoutState));
		}
		yield break;
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000F47F File Offset: 0x0000D67F
	private IEnumerator ShowDeckOptionsWhenReady()
	{
		while (this.m_playMat == null || this.m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			yield return null;
		}
		List<AdventureDeckDbfRecord> decksForClass = this.m_dungeonCrawlData.GetDecksForClass(this.m_playerHeroData.DeckClass);
		this.m_playMat.ShowDecks(decksForClass, AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, AdventureDungeonCrawlDisplay.m_gameSaveDataClientKey);
		this.m_currentLoadoutState = AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.DECKTEMPLATE;
		this.EnableBackButton(true);
		Navigation.PushUnique(new Navigation.NavigateBackHandler(AdventureDungeonCrawlDisplay.OnNavigateBackFromCurrentLoadoutState));
		yield break;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000F48E File Offset: 0x0000D68E
	private IEnumerator ShowBuildDeckButtonWhenReady()
	{
		while (this.m_playMat == null || this.m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			yield return null;
		}
		this.m_currentLoadoutState = AdventureDungeonCrawlDisplay.DungeonRunLoadoutState.LOADOUTCOMPLETE;
		this.m_playMat.ShowPVPDRActiveRun(this.GetPlayButtonTextForNextMission());
		this.m_playMat.PlayButton.SetText(this.GetPlayButtonTextForNextMission());
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_dungeonCrawlDeckSelect.deckTray.SetDungeonCrawlDeck(this.m_dungeonCrawlDeck, false);
			using (DefLoader.DisposableCardDef disposableCardDef = this.m_heroActor.ShareDisposableCardDef())
			{
				this.m_dungeonCrawlDeckSelect.heroDetails.UpdateHeroInfo(this.m_heroActor.GetEntityDef(), disposableCardDef);
			}
			using (DefLoader.DisposableCardDef disposableCardDef2 = this.m_heroPowerActor.ShareDisposableCardDef())
			{
				this.m_dungeonCrawlDeckSelect.heroDetails.UpdateHeroPowerInfo(this.m_heroPowerActor.GetEntityDef(), disposableCardDef2);
				yield break;
			}
		}
		yield break;
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
	private void OnBossFullDefLoaded(string cardId, DefLoader.DisposableFullDef def, object userData)
	{
		try
		{
			if (def == null)
			{
				Log.Adventures.PrintError("Unable to load {0} hero def for Dungeon Crawl boss.", new object[]
				{
					cardId
				});
				this.m_assetLoadingHelper.AssetLoadCompleted();
			}
			else
			{
				string text = null;
				string cardId2 = def.EntityDef.GetCardId();
				if (GameUtils.IsModeHeroic(this.m_dungeonCrawlData.GetSelectedMode()))
				{
					int cardTagValue = GameUtils.GetCardTagValue(cardId2, GAME_TAG.HEROIC_HERO_POWER);
					if (cardTagValue != 0)
					{
						text = GameUtils.TranslateDbIdToCardId(cardTagValue, false);
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = GameUtils.GetHeroPowerCardIdFromHero(cardId2);
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.m_assetLoadingHelper.AddAssetToLoad(1);
					DefLoader.Get().LoadFullDef(text, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnBossPowerFullDefLoaded), null, null);
				}
				EntityDef entityDef = def.EntityDef;
				if (entityDef != null && this.m_nextBossHealth != 0L && !this.m_isRunRetired)
				{
					entityDef = entityDef.Clone();
					entityDef.SetTag<long>(GAME_TAG.HEALTH, this.m_nextBossHealth);
				}
				if (this.IsNextMissionASpecialEncounter() && this.m_bossActor != null && this.m_bossActor.GetHealthObject() != null)
				{
					this.m_bossActor.GetHealthObject().Hide();
				}
				this.m_playMat.SetBossFullDef(def.DisposableCardDef, entityDef);
				this.m_assetLoadingHelper.AssetLoadCompleted();
			}
		}
		finally
		{
			if (def != null)
			{
				((IDisposable)def).Dispose();
			}
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0000F600 File Offset: 0x0000D800
	private void OnBossPowerFullDefLoaded(string cardId, DefLoader.DisposableFullDef def, object userData)
	{
		if (def == null)
		{
			Debug.LogError(string.Format("Unable to load {0} hero power def for Dungeon Crawl boss.", cardId), base.gameObject);
			this.m_assetLoadingHelper.AssetLoadCompleted();
			return;
		}
		DefLoader.DisposableFullDef currentBossHeroPowerFullDef = this.m_currentBossHeroPowerFullDef;
		if (currentBossHeroPowerFullDef != null)
		{
			currentBossHeroPowerFullDef.Dispose();
		}
		this.m_currentBossHeroPowerFullDef = def;
		this.m_assetLoadingHelper.AssetLoadCompleted();
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000F658 File Offset: 0x0000D858
	private void OnTreasureSatchelOptionSelected(long treasureLoadoutDbId)
	{
		this.m_dungeonCrawlData.SelectedLoadoutTreasureDbId = treasureLoadoutDbId;
		AdventureConfig.Get().SelectedLoadoutTreasureDbId = treasureLoadoutDbId;
		if (this.m_dungeonCrawlData.SelectableHeroPowersAndDecksExist())
		{
			List<AdventureLoadoutTreasuresDbfRecord> loadoutTreasuresForClass = this.m_dungeonCrawlData.GetLoadoutTreasuresForClass(this.m_dungeonCrawlData.SelectedHeroClass);
			AdventureDungeonCrawlRewardOption.OptionData optionData = new AdventureDungeonCrawlRewardOption.OptionData(AdventureDungeonCrawlPlayMat.OptionType.TREASURE_SATCHEL, new List<long>
			{
				treasureLoadoutDbId
			}, loadoutTreasuresForClass.FindIndex((AdventureLoadoutTreasuresDbfRecord r) => (long)r.CardId == treasureLoadoutDbId));
			this.OnRewardOptionSelected(optionData);
			this.m_playMat.PlayTreasureSatchelOptionSelected();
			this.GoToNextLoadoutState();
			return;
		}
		Debug.LogWarning("AdventureDungeonCrawlDisplay.OnTreasureLoadoutOptionSelected: Selecting a Treasure Loadout but no Hero Power or Deck is not supported!");
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000F708 File Offset: 0x0000D908
	private void OnHeroPowerOptionSelected(long heroPowerDbId, bool isLocked)
	{
		if (isLocked)
		{
			return;
		}
		this.m_dungeonCrawlData.SelectedHeroPowerDbId = heroPowerDbId;
		AdventureConfig.Get().SelectedHeroPowerDbId = heroPowerDbId;
		this.SetHeroPower(GameUtils.TranslateDbIdToCardId((int)heroPowerDbId, false));
		if (this.m_HeroPowerPortraitPlayMaker != null && !string.IsNullOrEmpty(this.m_HeroPowerPotraitIntroStateName))
		{
			this.m_HeroPowerPortraitPlayMaker.SendEvent(this.m_HeroPowerPotraitIntroStateName);
		}
		this.m_playMat.PlayHeroPowerOptionSelected();
		this.GoToNextLoadoutState();
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0000F77C File Offset: 0x0000D97C
	private void OnHeroPowerOptionRollover(long heroPowerDbId, GameObject bone)
	{
		GameUtils.SetParent(this.m_heroPowerBigCard, bone, false);
		using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(GameUtils.TranslateDbIdToCardId((int)heroPowerDbId, false), null))
		{
			this.ShowBigCard(this.m_heroPowerBigCard, fullDef, this.m_HeroPowerBigCardBone);
		}
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000F7DC File Offset: 0x0000D9DC
	private void OnHeroPowerOptionRollout(long heroPowerDbId, GameObject bone)
	{
		BigCardHelper.HideBigCard(this.m_heroPowerBigCard);
		GameUtils.SetParent(this.m_heroPowerBigCard, this.m_HeroPowerBigCardBone, false);
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000F7FC File Offset: 0x0000D9FC
	private void OnDeckOptionSelected(int deckId, List<long> deckContent, bool deckIsLocked)
	{
		this.m_playMat.DeselectAllDeckOptionsWithoutId(deckId);
		this.m_dungeonCrawlData.SelectedDeckId = (long)deckId;
		this.SetUpDeckList(CardWithPremiumStatus.ConvertList(deckContent), false, true, null, null);
		if (UniversalInputManager.UsePhoneUI)
		{
			if (this.m_dungeonCrawlDeckSelect == null || !this.m_dungeonCrawlDeckSelect.isReady)
			{
				Error.AddDevWarning("UI Error!", "AdventureDeckSelectWidget is not setup correctly or is not ready!", Array.Empty<object>());
				return;
			}
			this.m_dungeonCrawlDeckSelect.deckTray.SetDungeonCrawlDeck(this.m_dungeonCrawlDeck, false);
			using (DefLoader.DisposableCardDef disposableCardDef = this.m_heroActor.ShareDisposableCardDef())
			{
				this.m_dungeonCrawlDeckSelect.heroDetails.UpdateHeroInfo(this.m_heroActor.GetEntityDef(), disposableCardDef);
			}
			using (DefLoader.DisposableCardDef disposableCardDef2 = this.m_heroPowerActor.ShareDisposableCardDef())
			{
				this.m_dungeonCrawlDeckSelect.heroDetails.UpdateHeroPowerInfo(this.m_heroPowerActor.GetEntityDef(), disposableCardDef2);
			}
			if (deckIsLocked)
			{
				this.m_dungeonCrawlDeckSelect.playButton.Disable(false);
			}
			else
			{
				this.m_dungeonCrawlDeckSelect.playButton.Enable();
			}
			this.ShowMobileDeckTray(this.m_dungeonCrawlDeckSelect.slidingTray);
		}
		else if (deckIsLocked)
		{
			this.m_playMat.PlayButton.Disable(false);
		}
		else
		{
			this.m_playMat.PlayButton.Enable();
		}
		this.GoToNextLoadoutState();
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0000F974 File Offset: 0x0000DB74
	private void OnRewardOptionSelected(AdventureDungeonCrawlRewardOption.OptionData optionData)
	{
		if (!GameSaveDataManager.Get().IsDataReady(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey))
		{
			Log.Adventures.PrintError("Attempting to make a selection, but no data is ready yet!", Array.Empty<object>());
			return;
		}
		if (this.m_playMat.GetPlayMatState() != AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS)
		{
			Log.Adventures.PrintError("Attempting to choose a reward, but the Play Mat is not currently in the 'SHOWING_OPTIONS' state!", Array.Empty<object>());
			return;
		}
		GameSaveKeySubkeyId gameSaveKeySubkeyId = GameSaveKeySubkeyId.INVALID;
		switch (optionData.optionType)
		{
		case AdventureDungeonCrawlPlayMat.OptionType.LOOT:
			gameSaveKeySubkeyId = GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT;
			break;
		case AdventureDungeonCrawlPlayMat.OptionType.TREASURE:
			gameSaveKeySubkeyId = GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_TREASURE;
			break;
		case AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE:
			gameSaveKeySubkeyId = GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_SHRINE;
			break;
		}
		this.m_dungeonCrawlDeckTray.gameObject.SetActive(true);
		Action onCompleteCallback = null;
		if (optionData.optionType == AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE)
		{
			if (this.m_shrineOptions == null || this.m_shrineOptions.Count == 0)
			{
				Log.Adventures.PrintError("OnRewardOptionSelected: Player selected a shrine, but there are no shrine options!", Array.Empty<object>());
				return;
			}
			long shrineCardId = this.m_shrineOptions[optionData.index];
			this.m_playerHeroData.DeckClass = this.GetClassFromShrine(shrineCardId);
			this.SetUpDeckList(new List<CardWithPremiumStatus>(), false, false, null, null);
			onCompleteCallback = delegate()
			{
				this.SetUpDeckListFromShrine(shrineCardId, true);
				this.ChangeHeroPortrait(this.m_playerHeroData.HeroCardId, TAG_PREMIUM.NORMAL);
			};
		}
		for (int i = (gameSaveKeySubkeyId == GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT) ? 1 : 0; i < optionData.options.Count; i++)
		{
			string text = GameUtils.TranslateDbIdToCardId((int)optionData.options[i], true);
			if (!string.IsNullOrEmpty(text))
			{
				Actor animateFromActor = null;
				if (!UniversalInputManager.UsePhoneUI)
				{
					animateFromActor = this.m_playMat.GetActorToAnimateFrom(text, optionData.index);
				}
				this.m_dungeonCrawlDeckTray.AddCard(text, animateFromActor, onCompleteCallback);
			}
		}
		TooltipPanelManager.Get().HideKeywordHelp();
		if (optionData.optionType != AdventureDungeonCrawlPlayMat.OptionType.TREASURE_SATCHEL)
		{
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			list.Add(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, gameSaveKeySubkeyId, new long[]
			{
				(long)(optionData.index + 1)
			}));
			GameSaveDataManager.Get().SaveSubkeys(list, null);
			this.m_playMat.PlayRewardOptionSelected(optionData);
			base.StartCoroutine(this.SetPlayMatStateFromGameSaveDataWhenReady());
		}
		this.PlayRewardSelectVO(optionData);
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000FB7C File Offset: 0x0000DD7C
	private void PlayRewardSelectVO(AdventureDungeonCrawlRewardOption.OptionData optionData)
	{
		if (optionData.optionType != AdventureDungeonCrawlPlayMat.OptionType.TREASURE && optionData.optionType != AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE)
		{
			return;
		}
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(this.m_dungeonCrawlData.GetMission());
		if (DungeonCrawlSubDef_VOLines.GetNextValidEventType(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, this.m_playerHeroData.HeroDbId, DungeonCrawlSubDef_VOLines.OFFER_LOOT_PACKS_EVENTS, 0) != DungeonCrawlSubDef_VOLines.VOEventType.INVALID)
		{
			return;
		}
		int treasureDatabaseID = AdventureDungeonCrawlRewardOption.GetTreasureDatabaseID(optionData);
		if (treasureDatabaseID == 47251 && !Options.Get().GetBool(Option.HAS_JUST_SEEN_LOOT_NO_TAKE_CANDLE_VO))
		{
			return;
		}
		DungeonCrawlSubDef_VOLines.PlayVOLine(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, this.m_playerHeroData.HeroDbId, DungeonCrawlSubDef_VOLines.VOEventType.TAKE_TREASURE_GENERAL, treasureDatabaseID, true);
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000FC18 File Offset: 0x0000DE18
	private bool ShouldShowRunCompletedScreen()
	{
		if (this.m_isPVPDR)
		{
			PVPDRLobbyDataModel pvpdrlobbyDataModel = PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel();
			return !pvpdrlobbyDataModel.HasSession || !pvpdrlobbyDataModel.IsSessionActive;
		}
		return (this.m_defeatedBossIds != null || this.m_bossWhoDefeatedMeId != 0L) && (!this.m_isRunActive && !this.m_isRunRetired) && !this.m_hasSeenLatestDungeonRunComplete;
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000FC7C File Offset: 0x0000DE7C
	private void ShowMobileDeckTray(SlidingTray tray)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (tray == null)
		{
			Debug.LogError("ToggleMobileDeckTray: Could not find SlidingTray on Dungeon Crawl Deck Tray.");
			return;
		}
		this.m_playMat.HideBossHeroPowerTooltip(true);
		SlidingTray.TrayToggledListener trayListener = null;
		trayListener = delegate(bool shown)
		{
			this.OnMobileDeckTrayToggled(tray, shown, trayListener);
		};
		tray.RegisterTrayToggleListener(trayListener);
		tray.ToggleTraySlider(true, null, true);
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0000FD07 File Offset: 0x0000DF07
	private void OnMobileDeckTrayToggled(SlidingTray tray, bool shown, SlidingTray.TrayToggledListener trayListener)
	{
		if (!shown)
		{
			tray.UnregisterTrayToggleListener(trayListener);
			this.m_playMat.ShowBossHeroPowerTooltip();
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000FD20 File Offset: 0x0000DF20
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		switch (eventData.m_state)
		{
		case FindGameState.CLIENT_CANCELED:
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_CANCELED:
			this.HandleGameStartupFailure();
			break;
		case FindGameState.SERVER_GAME_STARTED:
			if (this.m_subsceneController != null)
			{
				this.m_subsceneController.RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene.ADVENTURER_PICKER);
				this.m_subsceneController.RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene.MISSION_DECK_PICKER);
			}
			break;
		}
		return false;
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0000FD8F File Offset: 0x0000DF8F
	private void HandleGameStartupFailure()
	{
		if (SceneMgr.Get().IsInDuelsMode())
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.DUELS_IDLE
			});
		}
		this.EnablePlayButton();
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0000FDBE File Offset: 0x0000DFBE
	private IEnumerator ShowDemoThankQuote()
	{
		string thankQuote = Vars.Key("Demo.DungeonThankQuote").GetStr("");
		float @float = Vars.Key("Demo.DungeonThankQuoteDelaySeconds").GetFloat(1f);
		float blockSeconds = Vars.Key("Demo.DungeonThankQuoteDurationSeconds").GetFloat(5f);
		BannerPopup thankBanner = null;
		yield return new WaitForSeconds(@float);
		BannerManager.Get().ShowBanner("NewPopUp_LOOT.prefab:c1f1a158f539ad3428175ebcd948f138", null, thankQuote, delegate()
		{
			AdventureDungeonCrawlDisplay.s_shouldShowWelcomeBanner = true;
			this.TryShowWelcomeBanner();
		}, delegate(BannerPopup popup)
		{
			thankBanner = popup;
			GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(thankBanner.gameObject.layer), "BannerInputBlocker", thankBanner.transform);
			gameObject.transform.localPosition = new Vector3(0f, 100f, 0f);
			gameObject.layer = 17;
		});
		while (thankBanner == null)
		{
			yield return null;
		}
		yield return new WaitForSeconds(blockSeconds);
		thankBanner.Close();
		yield break;
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0000FDCD File Offset: 0x0000DFCD
	private static bool IsInDemoMode()
	{
		return DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2017_ADVENTURE;
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0000FDDD File Offset: 0x0000DFDD
	private void DisableBackButtonIfInDemoMode()
	{
		if (AdventureDungeonCrawlDisplay.IsInDemoMode())
		{
			this.m_BackButton.SetEnabled(false, false);
			this.m_BackButton.Flip(false, false);
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0000FE00 File Offset: 0x0000E000
	private void SetDungeonCrawlDisplayVisualStyle()
	{
		DungeonRunVisualStyle visualStyle = this.m_dungeonCrawlData.VisualStyle;
		foreach (AdventureDungeonCrawlDisplay.DungeonCrawlDisplayStyleOverride dungeonCrawlDisplayStyleOverride in this.m_DungeonCrawlDisplayStyle)
		{
			if (dungeonCrawlDisplayStyleOverride.VisualStyle == visualStyle)
			{
				MeshRenderer component = this.m_dungeonCrawlTray.GetComponent<MeshRenderer>();
				if (component != null && dungeonCrawlDisplayStyleOverride.DungeonCrawlTrayMaterial != null)
				{
					component.SetMaterial(dungeonCrawlDisplayStyleOverride.DungeonCrawlTrayMaterial);
				}
				if (!UniversalInputManager.UsePhoneUI || !(this.m_ViewDeckTrayMesh != null))
				{
					break;
				}
				MeshRenderer component2 = this.m_ViewDeckTrayMesh.GetComponent<MeshRenderer>();
				if (component2 != null && dungeonCrawlDisplayStyleOverride.PhoneDeckTrayMaterial != null)
				{
					component2.SetMaterial(dungeonCrawlDisplayStyleOverride.PhoneDeckTrayMaterial);
					break;
				}
				break;
			}
		}
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0000FEE4 File Offset: 0x0000E0E4
	private string GetClassNameFromDeckClass(TAG_CLASS deckClass)
	{
		List<int> guestHeroesForCurrentAdventure = this.m_dungeonCrawlData.GetGuestHeroesForCurrentAdventure();
		if (guestHeroesForCurrentAdventure.Count == 0)
		{
			return GameStrings.GetClassName(deckClass);
		}
		using (List<int>.Enumerator enumerator = guestHeroesForCurrentAdventure.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int guestHeroCardDbId = enumerator.Current;
				if (GameUtils.GetTagClassFromCardDbId(guestHeroCardDbId) == deckClass)
				{
					return GameDbf.GuestHero.GetRecord((GuestHeroDbfRecord r) => r.CardId == guestHeroCardDbId).Name;
				}
			}
		}
		return string.Empty;
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0000FF8C File Offset: 0x0000E18C
	private TAG_CLASS GetClassFromShrine(long shrineCardId)
	{
		return GameUtils.GetTagClassFromCardDbId((int)shrineCardId);
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0000FF98 File Offset: 0x0000E198
	private void ChangeHeroPortrait(string newHeroCardId, TAG_PREMIUM premium)
	{
		if (this.m_heroActor == null)
		{
			Log.Adventures.PrintError(string.Format("Unable to change hero portrait to cardId={0}. No actor has been loaded.", newHeroCardId), Array.Empty<object>());
			return;
		}
		DefLoader.Get().LoadFullDef(newHeroCardId, delegate(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
		{
			try
			{
				this.m_heroActor.SetFullDef(fullDef);
				this.m_heroActor.SetPremium(premium);
				this.m_heroActor.GetComponent<PlayMakerFSM>().SendEvent(fullDef.EntityDef.GetClass().ToString());
			}
			finally
			{
				if (fullDef != null)
				{
					((IDisposable)fullDef).Dispose();
				}
			}
		}, null, null);
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0000FFFC File Offset: 0x0000E1FC
	public static Actor OnActorLoaded(string actorName, GameObject actorObject, GameObject container, bool withRotation = false)
	{
		Actor component = actorObject.GetComponent<Actor>();
		if (component != null)
		{
			if (container != null)
			{
				GameUtils.SetParent(component, container, withRotation);
				SceneUtils.SetLayer(component, container.layer);
			}
			component.SetUnlit();
			component.Hide();
		}
		else
		{
			Debug.LogWarning(string.Format("ERROR actor \"{0}\" has no Actor component", actorName));
		}
		return component;
	}

	// Token: 0x06000273 RID: 627 RVA: 0x00010058 File Offset: 0x0000E258
	private void ShowBigCard(Actor heroPowerBigCard, DefLoader.DisposableFullDef heroPowerFullDef, GameObject bone)
	{
		Vector3? origin = null;
		if (this.m_heroPowerActor != null)
		{
			origin = new Vector3?(this.m_heroPowerActor.gameObject.transform.position);
		}
		BigCardHelper.ShowBigCard(heroPowerBigCard, heroPowerFullDef, bone, this.m_BigCardScale, origin);
	}

	// Token: 0x06000274 RID: 628 RVA: 0x000100A8 File Offset: 0x0000E2A8
	private void OnHeroClassIconsControllerReady(Widget widget)
	{
		if (widget == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - widget was null!");
			return;
		}
		if (this.m_heroActor == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - m_heroActor was null!");
			return;
		}
		HeroClassIconsDataModel heroClassIconsDataModel = new HeroClassIconsDataModel();
		EntityDef entityDef = this.m_heroActor.GetEntityDef();
		if (entityDef == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - m_heroActor did not contain an entity def!");
			return;
		}
		heroClassIconsDataModel.Classes.Clear();
		foreach (TAG_CLASS item in entityDef.GetClasses(null))
		{
			heroClassIconsDataModel.Classes.Add(item);
		}
		widget.BindDataModel(heroClassIconsDataModel, false);
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0001015C File Offset: 0x0000E35C
	private static void ResetDungeonCrawlSelections(IDungeonCrawlData data)
	{
		data.SelectedLoadoutTreasureDbId = 0L;
		data.SelectedHeroPowerDbId = 0L;
		data.SelectedDeckId = 0L;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x00010178 File Offset: 0x0000E378
	private void OnRetirePopupResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			this.m_retireButton.SetActive(true);
			return;
		}
		Navigation.GoBack();
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, new long[]
		{
			1L
		}), delegate(bool dataWrittenSuccessfully)
		{
			this.HandleRetireSuccessOrFail(dataWrittenSuccessfully);
		});
	}

	// Token: 0x06000277 RID: 631 RVA: 0x000101D0 File Offset: 0x0000E3D0
	private void HandleRetireSuccessOrFail(bool success)
	{
		if (success)
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_RETIRE_CONFIRMATION_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_RETIRE_FAILURE_BODY");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
		this.m_retireButton.SetActive(true);
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0001022C File Offset: 0x0000E42C
	public void CreateDuelsSeedDeck()
	{
		if (!this.m_seedDeckCreateRequested)
		{
			CollectionDeckTray.Get().GetDecksContent().CreateNewDeckFromUserSelection(this.m_dungeonCrawlData.SelectedHeroClass, this.GetDuelsHeroCardIDFromClass(this.m_dungeonCrawlData.SelectedHeroClass), "PVPDR DECK", DeckSourceType.DECK_SOURCE_TYPE_NORMAL, null);
			this.m_seedDeckCreateRequested = true;
		}
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0001027C File Offset: 0x0000E47C
	private void OnDeckCreated(long deckID)
	{
		if (this.m_seedDeckCreateRequested)
		{
			CollectionDeck deck = CollectionManager.Get().GetDeck(deckID);
			if (deck != null && deckID == deck.ID && this.m_isPVPDR)
			{
				this.m_realDuelSeedDeck = deck;
				Network.Get().SendPVPDRSessionInfoRequest();
				this.m_playMat.PlayButton.Enable();
				this.EditCurrentDeck();
				this.m_seedDeckCreateRequested = false;
			}
		}
	}

	// Token: 0x0600027A RID: 634 RVA: 0x000102E0 File Offset: 0x0000E4E0
	public bool BackFromDeckEdit(CollectionDeck deck)
	{
		this.SyncDeckList();
		this.SaveDuelsDeckList();
		base.StartCoroutine(this.EnablePlayButtonWhenDeckChangesAreSaved(20f));
		this.m_BackButton.SetText("GLOBAL_LEAVE");
		this.m_dungeonCrawlDeckTray.m_cardsContent.UpdateCardList();
		if (this.m_realDuelSeedDeck.IsValidForRuleset)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR_LOCK_IN");
			popupInfo.m_text = GameStrings.Get("GLUE_PVPDR_LOCK_IN_DESC");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					this.LockInNewRunSelectionsAndTransition();
					Network.Get().SendPVPDRSessionInfoRequest();
					CollectionDeckTray.Get().OnConfirmBackOutOfDeckContentsDuel();
					Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnExitCollection));
					PvPDungeonRunScene.Get().ShowDungeonCrawlDisplay(delegate(object e)
					{
						if (UniversalInputManager.UsePhoneUI)
						{
							this.m_dungeonCrawlDeckTray.gameObject.SetActive(true);
						}
					});
				}
			};
			DialogManager.Get().ShowPopup(popupInfo);
			return false;
		}
		return true;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00010388 File Offset: 0x0000E588
	public string GetDuelsHeroCardIDFromClass(TAG_CLASS heroClass)
	{
		string result = string.Empty;
		if (this.m_dungeonCrawlData != null)
		{
			foreach (AdventureGuestHeroesDbfRecord adventureGuestHeroesDbfRecord in AdventureUtils.GetSortedGuestHeroRecordsForAdventures(this.m_dungeonCrawlData.GetSelectedAdventure()))
			{
				int cardIdFromGuestHeroDbId = GameUtils.GetCardIdFromGuestHeroDbId(adventureGuestHeroesDbfRecord.GuestHeroId);
				if (GameUtils.GetTagClassFromCardDbId(cardIdFromGuestHeroDbId) == heroClass)
				{
					result = GameUtils.TranslateDbIdToCardId(cardIdFromGuestHeroDbId, false);
				}
			}
		}
		return result;
	}

	// Token: 0x0600027C RID: 636 RVA: 0x00010408 File Offset: 0x0000E608
	private IEnumerator EnablePlayButtonWhenDeckChangesAreSaved(float timeout)
	{
		bool didTimeout = false;
		while (this.m_realDuelSeedDeck.IsSavingChanges())
		{
			this.DisablePlayButton();
			timeout -= Time.deltaTime;
			if (timeout <= 0f)
			{
				didTimeout = true;
				break;
			}
			yield return null;
		}
		if (didTimeout)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR_DECK_ERROR_TITLE");
			popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_GENERIC_ERROR");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = delegate(AlertPopup.Response poopupResponse, object userData)
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			};
			DialogManager.Get().ShowPopup(popupInfo);
		}
		else
		{
			this.EnablePlayButton();
		}
		yield break;
	}

	// Token: 0x0600027D RID: 637 RVA: 0x00010420 File Offset: 0x0000E620
	private void EditCurrentDeck()
	{
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnExitCollection));
		if (this.m_dungeonCrawlDeck != null)
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_dungeonCrawlDeckTray.gameObject.SetActive(false);
			}
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			collectionManagerDisplay.EnableInput(true);
			CollectionDeckTray.Get().EnterDeckEditForPVPDR(this.m_dungeonCrawlDeck);
			PvPDungeonRunScene.Get().HideDungeonCrawlDisplay();
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.DUELS_BUILDING_DECK
			});
			collectionManagerDisplay.CheckClipboardAndPromptPlayerToPaste();
		}
	}

	// Token: 0x0600027E RID: 638 RVA: 0x000104B5 File Offset: 0x0000E6B5
	public bool GetDuelsDeckIsValid()
	{
		return this.m_realDuelSeedDeck != null && this.m_realDuelSeedDeck.IsValidForRuleset;
	}

	// Token: 0x0600027F RID: 639 RVA: 0x000104CC File Offset: 0x0000E6CC
	public void SyncDeckList()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (this.m_realDuelSeedDeck != null && editedDeck != null)
		{
			this.m_realDuelSeedDeck.RemoveAllCards();
			foreach (CollectionDeckSlot collectionDeckSlot in editedDeck.GetSlots())
			{
				this.m_realDuelSeedDeck.AddCard(collectionDeckSlot.CardID, collectionDeckSlot.PreferredPremium, false);
			}
			if (editedDeck.CardBackID != this.m_realDuelSeedDeck.CardBackID)
			{
				this.m_realDuelSeedDeck.CardBackID = editedDeck.CardBackID;
				this.m_realDuelSeedDeck.CardBackOverridden = true;
			}
		}
	}

	// Token: 0x06000280 RID: 640 RVA: 0x00010588 File Offset: 0x0000E788
	public void SaveDuelsDeckList()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (this.m_realDuelSeedDeck != null && editedDeck != null)
		{
			CollectionManager.Get().SetEditedDeck(this.m_realDuelSeedDeck, null);
			CollectionDeckTray.SaveCurrentDeck();
			CollectionManager.Get().SetEditedDeck(editedDeck, null);
		}
	}

	// Token: 0x06000281 RID: 641 RVA: 0x000105CD File Offset: 0x0000E7CD
	private bool OnExitCollection()
	{
		if (CollectionDeckTray.Get().OnBackOutOfDeckContents())
		{
			(CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay).EnableInput(false);
			PvPDungeonRunScene.Get().ShowDungeonCrawlDisplay(delegate(object e)
			{
				if (UniversalInputManager.UsePhoneUI)
				{
					this.m_dungeonCrawlDeckTray.gameObject.SetActive(true);
				}
			});
			return true;
		}
		return false;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x00010609 File Offset: 0x0000E809
	private void OnPVPDRRetirePopupResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			this.m_retireButton.SetActive(true);
			return;
		}
		Network.Get().RegisterNetHandler(PVPDRRetireResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRRetireResponse), null);
		Network.Get().SendPVPDRRetireRequest();
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00010648 File Offset: 0x0000E848
	private void OnPVPDRRetireResponse()
	{
		GameSaveDataManager.Get().Request(AdventureDungeonCrawlDisplay.m_gameSaveDataServerKey, null);
		Network.Get().SendPVPDRSessionInfoRequest();
		Network.Get().RemoveNetHandler(PVPDRRetireResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRRetireResponse));
		PVPDRRetireResponse pvpdrretireResponse = Network.Get().GetPVPDRRetireResponse();
		bool flag = pvpdrretireResponse != null && pvpdrretireResponse.ErrorCode == ErrorCode.ERROR_OK;
		this.HandleRetireSuccessOrFail(flag);
		if (!flag)
		{
			return;
		}
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().RecentWin = false;
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().RecentLoss = false;
		if (PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsPaidEntry)
		{
			this.ShowDuelsEndRun();
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			Navigation.GoBack();
		}
		this.EndDuelsSession(0L);
	}

	// Token: 0x06000284 RID: 644 RVA: 0x00010708 File Offset: 0x0000E908
	private void ShowDuelsEndRun()
	{
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.DUELS_REWARD
		});
		this.m_playMat.ShowPVPDRReward();
		this.m_BackButton.SetEnabled(false, false);
		this.m_BackButton.Flip(false, false);
		Navigation.PushBlockBackingOut();
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0001075A File Offset: 0x0000E95A
	public void EndDuelsSession(long noticeId = 0L)
	{
		this.m_rewardNoticeId = noticeId;
		Network.Get().RegisterNetHandler(PVPDRSessionEndResponse.PacketID.ID, new Network.NetHandler(this.OnSessionEndResponse), null);
		Network.Get().SendPVPDRSessionEndRequest();
	}

	// Token: 0x06000286 RID: 646 RVA: 0x00010790 File Offset: 0x0000E990
	private void OnSessionEndResponse()
	{
		Network.Get().RemoveNetHandler(PVPDRSessionEndResponse.PacketID.ID, new Network.NetHandler(this.OnSessionEndResponse));
		PVPDRSessionEndResponse pvpdrsessionEndResponse = Network.Get().GetPVPDRSessionEndResponse();
		if (pvpdrsessionEndResponse.ErrorCode != ErrorCode.ERROR_OK)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR");
			popupInfo.m_text = GameStrings.Get("GLUE_PVPDR_SESSION_END_ERROR");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = delegate(AlertPopup.Response poopupResponse, object userData)
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			};
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		if (this.m_rewardNoticeId > 0L)
		{
			Network.Get().AckNotice(this.m_rewardNoticeId);
			this.m_rewardNoticeId = 0L;
		}
		DuelsConfig.Get().SetRecentEnd(true);
		int lastRatingChange = pvpdrsessionEndResponse.NewRating - PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().Rating;
		int lastRatingChange2 = pvpdrsessionEndResponse.NewPaidRating - PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().PaidRating;
		Network.Get().RequestPVPDRStatsInfo();
		DuelsConfig.Get().LastRunWins = PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().Wins;
		if (!PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsPaidEntry)
		{
			PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().LastPlayedMode = 1;
			PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().LastRatingChange = lastRatingChange;
			base.StartCoroutine(this.ShowRatingChangePopupWhenReady(delegate
			{
				this.OnSessionEndComplete();
			}));
			return;
		}
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().LastPlayedMode = 2;
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().LastRatingChange = lastRatingChange2;
		this.OnSessionEndComplete();
	}

	// Token: 0x06000287 RID: 647 RVA: 0x00010925 File Offset: 0x0000EB25
	private IEnumerator ShowRatingChangePopupWhenReady(Action callback)
	{
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsRatingNotice = true;
		while (this.m_playMat == null || !this.m_playMat.IsReady() || this.m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE || !Navigation.CanGoBack)
		{
			Log.Adventures.Print("Waiting for Play Mat to be initialized before showing rating change popup", Array.Empty<object>());
			yield return null;
		}
		int lastRatingChange = PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().LastRatingChange;
		int lastRunWins = DuelsConfig.Get().LastRunWins;
		string key = "GLUE_PVPDR_END_OF_RUN_TIER_0_WIN";
		string text = string.Concat(lastRatingChange);
		if (lastRatingChange >= 0)
		{
			text = "+" + lastRatingChange;
		}
		if (lastRunWins >= 12)
		{
			key = "GLUE_PVPDR_END_OF_RUN_TIER_MAX_WIN";
		}
		else if (lastRunWins >= 6)
		{
			key = "GLUE_PVPDR_END_OF_RUN_TIER_2_WIN";
		}
		else if (lastRunWins >= 1)
		{
			key = "GLUE_PVPDR_END_OF_RUN_TIER_1_WIN";
		}
		PvPDungeonRunScene.ShowDuelsMessagePopup(GameStrings.Format("GLUE_PVPDR_END_OF_RUN_HEADER", new object[]
		{
			lastRunWins
		}), GameStrings.Get(key), GameStrings.Format("GLUE_PVPDR_RATING_CHANGE", new object[]
		{
			text
		}), callback);
		yield break;
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0001093B File Offset: 0x0000EB3B
	private void OnSessionEndComplete()
	{
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().HasSession = false;
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsSessionActive = false;
		Navigation.PopBlockBackingOut();
		Navigation.GoBack();
	}

	// Token: 0x04000176 RID: 374
	[CustomEditField(Sections = "UI")]
	public UberText m_AdventureTitle;

	// Token: 0x04000177 RID: 375
	[CustomEditField(Sections = "UI")]
	public UIBButton m_BackButton;

	// Token: 0x04000178 RID: 376
	[CustomEditField(Sections = "UI")]
	public AdventureDungeonCrawlDeckTray m_dungeonCrawlDeckTray;

	// Token: 0x04000179 RID: 377
	[CustomEditField(Sections = "UI")]
	public AsyncReference m_dungeonCrawlDeckSelectWidgetReference;

	// Token: 0x0400017A RID: 378
	[CustomEditField(Sections = "UI")]
	public AsyncReference m_dungeonCrawlPlayMatReference;

	// Token: 0x0400017B RID: 379
	[CustomEditField(Sections = "UI")]
	public AsyncReference m_heroClassIconsControllerReference;

	// Token: 0x0400017C RID: 380
	[CustomEditField(Sections = "UI")]
	public GameObject m_dungeonCrawlTray;

	// Token: 0x0400017D RID: 381
	[CustomEditField(Sections = "UI")]
	public DungeonCrawlBossKillCounter m_bossKillCounter;

	// Token: 0x0400017E RID: 382
	[CustomEditField(Sections = "UI")]
	public HighlightState m_backButtonHighlight;

	// Token: 0x0400017F RID: 383
	[CustomEditField(Sections = "UI")]
	public float m_RolloverTimeToHideBossHeroPowerTooltip = 0.35f;

	// Token: 0x04000180 RID: 384
	[CustomEditField(Sections = "UI")]
	public Material m_anomalyModeCardHighlightMaterial;

	// Token: 0x04000181 RID: 385
	[CustomEditField(Sections = "UI")]
	public float m_BigCardScale = 1f;

	// Token: 0x04000182 RID: 386
	[CustomEditField(Sections = "UI")]
	public AsyncReference m_retireButtonReference;

	// Token: 0x04000183 RID: 387
	[CustomEditField(Sections = "Animation")]
	public PlayMakerFSM m_HeroPowerPortraitPlayMaker;

	// Token: 0x04000184 RID: 388
	[CustomEditField(Sections = "Animation")]
	public string m_HeroPowerPotraitIntroStateName;

	// Token: 0x04000185 RID: 389
	[CustomEditField(Sections = "Bones")]
	public Transform m_socketHeroBone;

	// Token: 0x04000186 RID: 390
	[CustomEditField(Sections = "Bones")]
	public Transform m_heroPowerBone;

	// Token: 0x04000187 RID: 391
	[CustomEditField(Sections = "Bones")]
	public GameObject m_BossPowerBone;

	// Token: 0x04000188 RID: 392
	[CustomEditField(Sections = "Bones")]
	public GameObject m_HeroPowerBigCardBone;

	// Token: 0x04000189 RID: 393
	[CustomEditField(Sections = "Styles")]
	public List<AdventureDungeonCrawlDisplay.DungeonCrawlDisplayStyleOverride> m_DungeonCrawlDisplayStyle;

	// Token: 0x0400018A RID: 394
	[CustomEditField(Sections = "Phone")]
	public UIBButton m_ShowDeckButton;

	// Token: 0x0400018B RID: 395
	[CustomEditField(Sections = "Phone")]
	public GameObject m_ShowDeckButtonFrame;

	// Token: 0x0400018C RID: 396
	[CustomEditField(Sections = "Phone")]
	public GameObject m_ShowDeckNoButtonFrame;

	// Token: 0x0400018D RID: 397
	[CustomEditField(Sections = "Phone")]
	public GameObject m_PhoneDeckTray;

	// Token: 0x0400018E RID: 398
	[CustomEditField(Sections = "Phone")]
	public GameObject m_DeckTrayRunCompleteBone;

	// Token: 0x0400018F RID: 399
	[CustomEditField(Sections = "Phone")]
	public GameObject m_DeckListHeaderRunCompleteBone;

	// Token: 0x04000190 RID: 400
	[CustomEditField(Sections = "Phone")]
	public TraySection m_DeckListHeaderPrefab;

	// Token: 0x04000191 RID: 401
	[CustomEditField(Sections = "Phone")]
	public GameObject m_TrayFrameDefault;

	// Token: 0x04000192 RID: 402
	[CustomEditField(Sections = "Phone")]
	public GameObject m_TrayFrameRunComplete;

	// Token: 0x04000193 RID: 403
	[CustomEditField(Sections = "Phone")]
	public GameObject m_AdventureTitleRunCompleteBone;

	// Token: 0x04000194 RID: 404
	[CustomEditField(Sections = "Phone")]
	public Vector3 m_DeckBigCardOffsetForRunCompleteState;

	// Token: 0x04000195 RID: 405
	[CustomEditField(Sections = "Phone")]
	public GameObject m_ViewDeckTrayMesh;

	// Token: 0x04000196 RID: 406
	public AdventureDungeonCrawlPlayMat m_playMat;

	// Token: 0x04000197 RID: 407
	public static bool s_shouldShowWelcomeBanner = true;

	// Token: 0x04000198 RID: 408
	private bool m_subsceneTransitionComplete;

	// Token: 0x04000199 RID: 409
	private CollectionDeck m_dungeonCrawlDeck;

	// Token: 0x0400019A RID: 410
	private DungeonCrawlDeckSelect m_dungeonCrawlDeckSelect;

	// Token: 0x0400019B RID: 411
	private Actor m_heroActor;

	// Token: 0x0400019C RID: 412
	private AdventureDungeonCrawlDisplay.PlayerHeroData m_playerHeroData;

	// Token: 0x0400019D RID: 413
	private int m_numBossesDefeated;

	// Token: 0x0400019E RID: 414
	private List<long> m_defeatedBossIds;

	// Token: 0x0400019F RID: 415
	private long m_bossWhoDefeatedMeId;

	// Token: 0x040001A0 RID: 416
	private long m_nextBossHealth;

	// Token: 0x040001A1 RID: 417
	private string m_nextBossCardId;

	// Token: 0x040001A2 RID: 418
	private long m_heroHealth;

	// Token: 0x040001A3 RID: 419
	private bool m_isRunActive;

	// Token: 0x040001A4 RID: 420
	private bool m_isRunRetired;

	// Token: 0x040001A5 RID: 421
	private int m_selectedShrineIndex;

	// Token: 0x040001A6 RID: 422
	private List<long> m_cardsAddedToDeckMap = new List<long>();

	// Token: 0x040001A7 RID: 423
	private bool m_hasSeenLatestDungeonRunComplete;

	// Token: 0x040001A8 RID: 424
	private List<long> m_shrineOptions;

	// Token: 0x040001A9 RID: 425
	private long m_anomalyModeCardDbId;

	// Token: 0x040001AA RID: 426
	private long m_plotTwistCardDbId;

	// Token: 0x040001AB RID: 427
	private static GameSaveKeyId m_gameSaveDataServerKey;

	// Token: 0x040001AC RID: 428
	private static GameSaveKeyId m_gameSaveDataClientKey;

	// Token: 0x040001AD RID: 429
	private bool m_hasReceivedGameSaveDataServerKeyResponse;

	// Token: 0x040001AE RID: 430
	private bool m_hasReceivedGameSaveDataClientKeyResponse;

	// Token: 0x040001AF RID: 431
	private int m_numBossesInRun;

	// Token: 0x040001B0 RID: 432
	private int m_bossCardBackId;

	// Token: 0x040001B1 RID: 433
	private bool m_shouldSkipHeroSelect;

	// Token: 0x040001B2 RID: 434
	private bool m_mustPickShrine;

	// Token: 0x040001B3 RID: 435
	private bool m_mustSelectChapter;

	// Token: 0x040001B4 RID: 436
	private Coroutine m_bossHeroPowerHideCoroutine;

	// Token: 0x040001B5 RID: 437
	private IDungeonCrawlData m_dungeonCrawlData;

	// Token: 0x040001B6 RID: 438
	private ISubsceneController m_subsceneController;

	// Token: 0x040001B7 RID: 439
	private AssetLoadingHelper m_assetLoadingHelper;

	// Token: 0x040001B8 RID: 440
	private Actor m_bossActor;

	// Token: 0x040001B9 RID: 441
	private Actor m_bossPowerBigCard;

	// Token: 0x040001BA RID: 442
	private Actor m_heroPowerActor;

	// Token: 0x040001BB RID: 443
	private DefLoader.DisposableFullDef m_currentBossHeroPowerFullDef;

	// Token: 0x040001BC RID: 444
	private Actor m_heroPowerBigCard;

	// Token: 0x040001BD RID: 445
	private DefLoader.DisposableFullDef m_currentHeroPowerFullDef;

	// Token: 0x040001BE RID: 446
	private GameObject m_retireButton;

	// Token: 0x040001BF RID: 447
	private AdventureDungeonCrawlDisplay.DungeonRunLoadoutState m_currentLoadoutState;

	// Token: 0x040001C0 RID: 448
	private static AdventureDungeonCrawlDisplay m_instance;

	// Token: 0x040001C1 RID: 449
	private bool m_isPVPDR;

	// Token: 0x040001C2 RID: 450
	private CollectionDeck m_realDuelSeedDeck;

	// Token: 0x040001C3 RID: 451
	private bool m_seedDeckCreateRequested;

	// Token: 0x040001C4 RID: 452
	private long m_rewardNoticeId;

	// Token: 0x020012AC RID: 4780
	[Serializable]
	public class DungeonCrawlDisplayStyleOverride
	{
		// Token: 0x0400A43B RID: 42043
		public DungeonRunVisualStyle VisualStyle;

		// Token: 0x0400A43C RID: 42044
		public Material DungeonCrawlTrayMaterial;

		// Token: 0x0400A43D RID: 42045
		public Material PhoneDeckTrayMaterial;
	}

	// Token: 0x020012AD RID: 4781
	public class PlayerHeroData
	{
		// Token: 0x140000DA RID: 218
		// (add) Token: 0x0600D4F3 RID: 54515 RVA: 0x003E5FC4 File Offset: 0x003E41C4
		// (remove) Token: 0x0600D4F4 RID: 54516 RVA: 0x003E5FFC File Offset: 0x003E41FC
		public event AdventureDungeonCrawlDisplay.PlayerHeroData.DataChangedEventHandler OnHeroDataChanged;

		// Token: 0x0600D4F5 RID: 54517 RVA: 0x003E6031 File Offset: 0x003E4231
		public PlayerHeroData(IDungeonCrawlData dungeonCrawlData)
		{
			this.m_dungeonCrawlData = dungeonCrawlData;
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x0600D4F6 RID: 54518 RVA: 0x003E6040 File Offset: 0x003E4240
		// (set) Token: 0x0600D4F7 RID: 54519 RVA: 0x003E6048 File Offset: 0x003E4248
		public TAG_CLASS DeckClass
		{
			get
			{
				return this._deckClass;
			}
			set
			{
				this._deckClass = value;
				if (value == TAG_CLASS.INVALID)
				{
					this.HeroCardId = null;
					this.HeroDbId = 0;
				}
				else
				{
					this.HeroCardId = AdventureUtils.GetHeroCardIdFromClassForDungeonCrawl(this.m_dungeonCrawlData, value);
					this.HeroDbId = GameUtils.TranslateCardIdToDbId(this.HeroCardId, false);
				}
				if (this.OnHeroDataChanged != null)
				{
					this.OnHeroDataChanged();
				}
			}
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x0600D4F8 RID: 54520 RVA: 0x003E60A6 File Offset: 0x003E42A6
		// (set) Token: 0x0600D4F9 RID: 54521 RVA: 0x003E60AE File Offset: 0x003E42AE
		public int HeroDbId { get; private set; }

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x0600D4FA RID: 54522 RVA: 0x003E60B7 File Offset: 0x003E42B7
		// (set) Token: 0x0600D4FB RID: 54523 RVA: 0x003E60BF File Offset: 0x003E42BF
		public string HeroCardId { get; private set; }

		// Token: 0x0400A43F RID: 42047
		private TAG_CLASS _deckClass;

		// Token: 0x0400A442 RID: 42050
		private IDungeonCrawlData m_dungeonCrawlData;

		// Token: 0x02002975 RID: 10613
		// (Invoke) Token: 0x06013EE9 RID: 81641
		public delegate void DataChangedEventHandler();
	}

	// Token: 0x020012AE RID: 4782
	private enum DungeonRunLoadoutState
	{
		// Token: 0x0400A444 RID: 42052
		INVALID,
		// Token: 0x0400A445 RID: 42053
		HEROPOWER,
		// Token: 0x0400A446 RID: 42054
		TREASURE,
		// Token: 0x0400A447 RID: 42055
		DECKTEMPLATE,
		// Token: 0x0400A448 RID: 42056
		LOADOUTCOMPLETE
	}
}
