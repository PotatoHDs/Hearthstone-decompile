using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using PegasusShared;
using UnityEngine;

// Token: 0x02000044 RID: 68
[CustomEditClass]
public class AdventureMissionDisplay : AdventureSubSceneDisplay
{
	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000375 RID: 885 RVA: 0x000159A4 File Offset: 0x00013BA4
	// (set) Token: 0x06000376 RID: 886 RVA: 0x000159AC File Offset: 0x00013BAC
	[CustomEditField(Sections = "Boss Layout Settings")]
	public Vector3 BossWingOffset
	{
		get
		{
			return this.m_BossWingOffset;
		}
		set
		{
			this.m_BossWingOffset = value;
			this.UpdateWingPositions();
		}
	}

	// Token: 0x06000377 RID: 887 RVA: 0x000159BB File Offset: 0x00013BBB
	public static AdventureMissionDisplay Get()
	{
		return AdventureMissionDisplay.s_instance;
	}

	// Token: 0x06000378 RID: 888 RVA: 0x000159C4 File Offset: 0x00013BC4
	private void Awake()
	{
		AdventureMissionDisplay.s_instance = this;
		this.m_mainMusic = MusicManager.Get().GetCurrentPlaylist();
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId selectedAdventure = adventureConfig.GetSelectedAdventure();
		AdventureModeDbId selectedMode = adventureConfig.GetSelectedMode();
		string text = GameUtils.GetAdventureDataRecord((int)selectedAdventure, (int)selectedMode).Name;
		this.m_AdventureTitle.Text = text;
		List<AdventureMissionDisplay.WingCreateParams> list = this.BuildWingCreateParamsList();
		this.m_WingsToGiveBigChest.Clear();
		AdventureDef adventureDef = AdventureScene.Get().GetAdventureDef(selectedAdventure);
		AdventureSubDef subDef = adventureDef.GetSubDef(selectedMode);
		if (!string.IsNullOrEmpty(adventureDef.m_WingBottomBorderPrefab))
		{
			this.m_BossWingBorder = AssetLoader.Get().InstantiatePrefab(adventureDef.m_WingBottomBorderPrefab, AssetLoadingOptions.None);
			GameUtils.SetParent(this.m_BossWingBorder, this.m_BossWingContainer, false);
		}
		base.AddAssetToLoad(3);
		foreach (AdventureMissionDisplay.WingCreateParams wingCreateParams in list)
		{
			base.AddAssetToLoad(wingCreateParams.m_BossCreateParams.Count * 2);
		}
		this.m_TotalBosses = 0;
		this.m_TotalBossesDefeated = 0;
		if (!string.IsNullOrEmpty(adventureDef.m_ProgressDisplayPrefab))
		{
			this.m_progressDisplay = GameUtils.LoadGameObjectWithComponent<AdventureWingProgressDisplay>(adventureDef.m_ProgressDisplayPrefab);
			if (this.m_progressDisplay != null && this.m_BossWingContainer != null)
			{
				GameUtils.SetParent(this.m_progressDisplay, this.m_BossWingContainer, false);
			}
		}
		foreach (AdventureMissionDisplay.WingCreateParams wingCreateParams2 in list)
		{
			WingDbId wingId = wingCreateParams2.m_WingDef.GetWingId();
			AdventureWingDef wingDef = wingCreateParams2.m_WingDef;
			AdventureWing wing = GameUtils.LoadGameObjectWithComponent<AdventureWing>(wingDef.m_WingPrefab);
			if (!(wing == null))
			{
				if (this.m_BossWingContainer != null)
				{
					GameUtils.SetParent(wing, this.m_BossWingContainer, false);
				}
				wing.Initialize(wingDef);
				wing.SetBigChestRewards(wingId);
				wing.AddBossSelectedListener(delegate(AdventureBossCoin c, ScenarioDbId m)
				{
					this.OnBossSelected(c, m, true);
				});
				wing.AddOpenPlateStartListener(new AdventureWing.OpenPlateStart(this.OnStartUnlockPlate));
				wing.AddOpenPlateEndListener(new AdventureWing.OpenPlateEnd(this.OnEndUnlockPlate));
				wing.AddTryPurchaseWingListener(delegate
				{
					this.ShowAdventureStore(wing);
				});
				wing.AddShowRewardsListener(delegate(List<RewardData> r, Vector3 o)
				{
					this.m_RewardsDisplay.ShowRewards(r, o, null);
				});
				wing.AddHideRewardsListener(delegate(List<RewardData> r)
				{
					this.m_RewardsDisplay.HideRewards();
				});
				List<int> wingScenarios = new List<int>();
				int num = 0;
				foreach (AdventureMissionDisplay.BossCreateParams bossCreateParams in wingCreateParams2.m_BossCreateParams)
				{
					bool enabled = AdventureConfig.IsMissionAvailable((int)bossCreateParams.m_MissionId) || wingCreateParams2.m_WingDef.CoinsStartFaceUp;
					AdventureBossCoin coin = wing.CreateBoss(wingDef.m_CoinPrefab, wingDef.m_RewardsPrefab, bossCreateParams.m_MissionId, enabled);
					AdventureConfig.Get().LoadBossDef(bossCreateParams.m_MissionId, delegate(AdventureBossDef bossDef, bool y)
					{
						if (bossDef != null)
						{
							coin.SetPortraitMaterial(bossDef);
						}
						this.AssetLoadCompleted();
					});
					if (AdventureConfig.Get().GetLastSelectedMission() == bossCreateParams.m_MissionId)
					{
						base.StartCoroutine(this.RememberLastBossSelection(coin, bossCreateParams.m_MissionId));
					}
					if (AdventureProgressMgr.Get().HasDefeatedScenario((int)bossCreateParams.m_MissionId))
					{
						num++;
						this.m_TotalBossesDefeated++;
					}
					this.m_TotalBosses++;
					DefLoader.Get().LoadFullDef(bossCreateParams.m_CardDefId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroFullDefLoaded), bossCreateParams.m_MissionId, null);
					wingScenarios.Add((int)bossCreateParams.m_MissionId);
				}
				bool flag = adventureConfig.GetWingBossesDefeated(selectedAdventure, selectedMode, wingId, num) != num;
				if (flag)
				{
					this.m_BossJustDefeated = true;
				}
				if (wing.m_BigChest != null)
				{
					bool flag2 = num == wingCreateParams2.m_BossCreateParams.Count;
					if (!wing.HasBigChestRewards())
					{
						wing.HideBigChest();
					}
					else if (flag2)
					{
						if (flag)
						{
							this.m_WingsToGiveBigChest.Add(wing);
						}
						else
						{
							wing.BigChestStayOpen();
						}
					}
				}
				if (this.m_progressDisplay != null)
				{
					bool linearComplete = AdventureProgressMgr.Get().IsWingComplete(selectedAdventure, AdventureModeDbId.LINEAR, wingId);
					this.m_progressDisplay.UpdateProgress(wingCreateParams2.m_WingDef.GetWingId(), linearComplete);
				}
				adventureConfig.UpdateWingBossesDefeated(selectedAdventure, selectedMode, wingId, num);
				wing.AddShowRewardsPreviewListeners(delegate
				{
					this.ShowRewardsPreview(wing, wingScenarios.ToArray(), wing.GetBigChestRewards(), wing.GetWingName());
				});
				wing.UpdateRewardsPreviewCover();
				wing.RandomizeBackground();
				wing.SetBringToFocusCallback(new AdventureWing.BringToFocusCallback(this.BatchBringWingToFocus));
				this.m_BossWings.Add(wing);
				if (AdventureScene.Get().IsDevMode)
				{
					wing.InitializeDevMode();
				}
			}
		}
		AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", new PrefabCallback<GameObject>(this.OnHeroActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", new PrefabCallback<GameObject>(this.OnHeroPowerActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("History_HeroPower_Opponent.prefab:a99d23d6e8630f94b96a8e096fffb16f", new PrefabCallback<GameObject>(this.OnHeroPowerBigCardLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		if (this.m_BossPowerHoverArea != null)
		{
			this.m_BossPowerHoverArea.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
			{
				base.ShowBossPowerBigCard();
			});
			this.m_BossPowerHoverArea.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
			{
				base.HideBossPowerBigCard();
			});
		}
		this.m_BackButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackButtonPress));
		this.m_ChooseButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.ChangeToDeckPicker();
		});
		this.UpdateWingPositions();
		this.m_ChooseButton.Disable(false);
		StoreManager.Get().RegisterStoreShownListener(new Action(this.OnStoreShown));
		StoreManager.Get().RegisterStoreHiddenListener(new Action(this.OnStoreHidden));
		AdventureProgressMgr.Get().RegisterProgressUpdatedListener(new AdventureProgressMgr.AdventureProgressUpdatedCallback(this.OnAdventureProgressUpdate));
		if (this.m_WatermarkIcon != null && !string.IsNullOrEmpty(subDef.m_WatermarkTexture))
		{
			AssetLoader.Get().LoadAsset<Texture>(ref this.m_watermarkTexture, subDef.m_WatermarkTexture, AssetLoadingOptions.None);
			if (this.m_watermarkTexture != null)
			{
				this.m_WatermarkIcon.GetMaterial().mainTexture = this.m_watermarkTexture;
			}
			else
			{
				Debug.LogWarning(string.Format("Adventure Watermark texture is null: {0}", subDef.m_WatermarkTexture));
			}
		}
		else
		{
			Debug.LogWarning(string.Format("Adventure Watermark texture is null: m_WatermarkIcon: {0},  advSubDef.m_WatermarkTexture: {1}", this.m_WatermarkIcon, subDef.m_WatermarkTexture));
		}
		this.m_BackButton.gameObject.SetActive(true);
		this.m_PreviewPane.AddHideListener(new AdventureRewardsPreview.OnHide(this.OnHideRewardsPreview));
		AdventureDataDbfRecord selectedAdventureDataRecord = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		if (selectedAdventureDataRecord.GameSaveDataServerKey != 0)
		{
			base.AddAssetToLoad(1);
			GameSaveDataManager.Get().Request((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataServerKey, new GameSaveDataManager.OnRequestDataResponseDelegate(this.OnGameSaveDataReceived));
		}
	}

	// Token: 0x06000379 RID: 889 RVA: 0x0001619C File Offset: 0x0001439C
	private void Start()
	{
		Navigation.PushUnique(new Navigation.NavigateBackHandler(AdventureMissionDisplay.OnNavigateBack));
		AdventureWing adventureWing = null;
		foreach (AdventureWing adventureWing2 in this.m_BossWings)
		{
			if (adventureWing == null || adventureWing2.GetWingDef().GetUnlockOrder() < adventureWing.GetWingDef().GetUnlockOrder())
			{
				adventureWing = adventureWing2;
			}
		}
		if (this.m_ScrollBar != null)
		{
			this.m_ScrollBar.UpdateScroll();
			if (adventureWing != null && adventureWing != this.m_BossWings[0])
			{
				adventureWing.BringToFocus();
			}
			this.m_ScrollBar.LoadScroll(AdventureConfig.Get().GetSelectedAdventureAndModeString(), false);
		}
		AdventureConfig.Get().OnAdventureSceneUnloadEvent += this.OnAdventureSceneUnloaded;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00016288 File Offset: 0x00014488
	protected override void OnDestroy()
	{
		AdventureProgressMgr adventureProgressMgr = AdventureProgressMgr.Get();
		if (adventureProgressMgr != null)
		{
			adventureProgressMgr.RemoveProgressUpdatedListener(new AdventureProgressMgr.AdventureProgressUpdatedCallback(this.OnAdventureProgressUpdate));
		}
		StoreManager storeManager = StoreManager.Get();
		if (storeManager != null)
		{
			storeManager.RemoveStoreHiddenListener(new Action(this.OnStoreHidden));
		}
		StoreManager storeManager2 = StoreManager.Get();
		if (storeManager2 != null)
		{
			storeManager2.RemoveStoreShownListener(new Action(this.OnStoreShown));
		}
		if (AdventureConfig.Get() != null)
		{
			AdventureConfig.Get().OnAdventureSceneUnloadEvent -= this.OnAdventureSceneUnloaded;
		}
		this.SaveScrollbarValue();
		AdventureMissionDisplay.s_instance = null;
		this.m_BossPortraitDefCache.DisposeValuesAndClear<ScenarioDbId, DefLoader.DisposableFullDef>();
		this.m_BossPowerDefCache.DisposeValuesAndClear<ScenarioDbId, DefLoader.DisposableFullDef>();
		AssetHandle.SafeDispose<Texture>(ref this.m_watermarkTexture);
		base.OnDestroy();
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00016340 File Offset: 0x00014540
	private void OnAdventureSceneUnloaded()
	{
		this.SaveScrollbarValue();
	}

	// Token: 0x0600037C RID: 892 RVA: 0x00016348 File Offset: 0x00014548
	private void Update()
	{
		if (!AdventureScene.Get().IsDevMode)
		{
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.Z))
		{
			base.StartCoroutine(this.AnimateAdventureCompleteCheckmarksAndPopups(true));
		}
		if (InputCollection.GetKeyDown(KeyCode.X))
		{
			this.ShowAdventureCompletePopup();
		}
		if (InputCollection.GetKeyDown(KeyCode.C))
		{
			this.Cheat_OpenNextWing();
		}
		if (InputCollection.GetKeyDown(KeyCode.V))
		{
			this.Cheat_OpenNextChest();
		}
	}

	// Token: 0x0600037D RID: 893 RVA: 0x000163A6 File Offset: 0x000145A6
	public bool IsDisabledSelection()
	{
		return this.m_DisableSelectionCount > 0;
	}

	// Token: 0x0600037E RID: 894 RVA: 0x000163B1 File Offset: 0x000145B1
	public void AddProgressStepCompletedListener(AdventureMissionDisplay.ProgressStepCompletedCallback callback)
	{
		this.m_ProgressStepCompletedListeners.Add(callback);
	}

	// Token: 0x0600037F RID: 895 RVA: 0x000163C0 File Offset: 0x000145C0
	private void FireProgressStepCompletedListeners(AdventureMissionDisplay.ProgressStep progress)
	{
		AdventureMissionDisplay.ProgressStepCompletedCallback[] array = this.m_ProgressStepCompletedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](progress);
		}
	}

	// Token: 0x06000380 RID: 896 RVA: 0x000163F0 File Offset: 0x000145F0
	private void UpdateWingPositions()
	{
		float num = 0f;
		if (this.m_progressDisplay != null)
		{
			this.m_progressDisplay.transform.localPosition = this.m_BossWingOffset;
			TransformUtil.SetLocalPosZ(this.m_progressDisplay, this.m_BossWingOffset.z - num);
			num += this.HeightForScrollableItem(this.m_progressDisplay.gameObject);
		}
		foreach (AdventureWing adventureWing in this.m_BossWings)
		{
			adventureWing.transform.localPosition = this.m_BossWingOffset;
			TransformUtil.SetLocalPosZ(adventureWing, this.m_BossWingOffset.z - num);
			num += this.HeightForScrollableItem(adventureWing.gameObject);
		}
		if (this.m_BossWingBorder != null)
		{
			this.m_BossWingBorder.transform.localPosition = this.m_BossWingOffset;
			TransformUtil.SetLocalPosZ(this.m_BossWingBorder, this.m_BossWingOffset.z - num);
		}
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00016504 File Offset: 0x00014704
	private float HeightForScrollableItem(GameObject go)
	{
		UIBScrollableItem component = go.GetComponent<UIBScrollableItem>();
		if (component == null)
		{
			Log.All.PrintError("No UIBScrollableItem component on the GameObject {0}!", new object[]
			{
				go
			});
			return 0f;
		}
		return component.m_size.z;
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0001654C File Offset: 0x0001474C
	private void OnHeroFullDefLoaded(string cardId, DefLoader.DisposableFullDef def, object userData)
	{
		if (def == null)
		{
			Debug.LogError(string.Format("Unable to load {0} hero def for Adventure boss.", cardId), base.gameObject);
			base.AssetLoadCompleted();
			return;
		}
		ScenarioDbId scenarioDbId = (ScenarioDbId)userData;
		this.m_BossPortraitDefCache.SetOrReplaceDisposable(scenarioDbId, def);
		string missionHeroPowerCardId = GameUtils.GetMissionHeroPowerCardId((int)scenarioDbId);
		if (!string.IsNullOrEmpty(missionHeroPowerCardId))
		{
			base.AddAssetToLoad(1);
			DefLoader.Get().LoadFullDef(missionHeroPowerCardId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroPowerFullDefLoaded), scenarioDbId, null);
		}
		base.AssetLoadCompleted();
	}

	// Token: 0x06000383 RID: 899 RVA: 0x000165C8 File Offset: 0x000147C8
	private void OnHeroPowerFullDefLoaded(string cardId, DefLoader.DisposableFullDef def, object userData)
	{
		if (def == null)
		{
			Debug.LogError(string.Format("Unable to load {0} hero power def for Adventure boss.", cardId), base.gameObject);
			base.AssetLoadCompleted();
			return;
		}
		ScenarioDbId key = (ScenarioDbId)userData;
		this.m_BossPowerDefCache.SetOrReplaceDisposable(key, def);
		base.AssetLoadCompleted();
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00016610 File Offset: 0x00014810
	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_BossActor = AdventureSubSceneDisplay.OnActorLoaded(assetRef, go, this.m_BossPortraitContainer, false);
		if (this.m_BossActor != null && this.m_BossActor.GetHealthObject() != null)
		{
			this.m_BossActor.GetHealthObject().Hide();
		}
		base.AssetLoadCompleted();
	}

	// Token: 0x06000385 RID: 901 RVA: 0x0001666D File Offset: 0x0001486D
	private void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_HeroPowerActor = AdventureSubSceneDisplay.OnActorLoaded(assetRef, go, this.m_BossPowerContainer, false);
		base.AssetLoadCompleted();
	}

	// Token: 0x06000386 RID: 902 RVA: 0x00016690 File Offset: 0x00014890
	private void OnHeroPowerBigCardLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_BossPowerBigCard = AdventureSubSceneDisplay.OnActorLoaded(assetRef, go, (this.m_HeroPowerActor == null) ? null : this.m_HeroPowerActor.gameObject, false);
		if (this.m_BossPowerBigCard != null)
		{
			this.m_BossPowerBigCard.TurnOffCollider();
		}
		base.AssetLoadCompleted();
	}

	// Token: 0x06000387 RID: 903 RVA: 0x000166EB File Offset: 0x000148EB
	private void OnGameSaveDataReceived(bool success)
	{
		base.AssetLoadCompleted();
		this.UpdateCoinsWithGameSaveData();
	}

	// Token: 0x06000388 RID: 904 RVA: 0x000166FC File Offset: 0x000148FC
	private void UpdateCoinsWithGameSaveData()
	{
		foreach (AdventureWing adventureWing in this.m_BossWings)
		{
			adventureWing.UpdateAllBossCoinChests();
		}
	}

	// Token: 0x06000389 RID: 905 RVA: 0x0001674C File Offset: 0x0001494C
	protected override void OnSubSceneLoaded()
	{
		if (this.m_BossPowerBigCard != null && this.m_HeroPowerActor != null && this.m_BossPowerBigCard.transform.parent != this.m_HeroPowerActor.transform)
		{
			GameUtils.SetParent(this.m_BossPowerBigCard, this.m_HeroPowerActor.gameObject, false);
		}
		base.OnSubSceneLoaded();
	}

	// Token: 0x0600038A RID: 906 RVA: 0x000167B4 File Offset: 0x000149B4
	protected override void OnSubSceneTransitionComplete()
	{
		base.OnSubSceneTransitionComplete();
		this.TryShowWelcomeBanner(delegate
		{
			base.StartCoroutine(this.UpdateAndAnimateProgress(this.m_BossWings, true, false));
		});
	}

	// Token: 0x0600038B RID: 907 RVA: 0x00004EA7 File Offset: 0x000030A7
	private static bool OnNavigateBack()
	{
		AdventureConfig.Get().SubSceneGoBack(true);
		return true;
	}

	// Token: 0x0600038C RID: 908 RVA: 0x000167D0 File Offset: 0x000149D0
	private void OnBackButtonPress(UIEvent e)
	{
		foreach (AdventureWing adventureWing in this.m_BossWings)
		{
			adventureWing.NavigateBackCleanup();
		}
		Navigation.GoBack();
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00016828 File Offset: 0x00014A28
	private void OnZeroCostTransactionStoreExit(bool authorizationBackButtonPressed, object userData)
	{
		if (!authorizationBackButtonPressed)
		{
			return;
		}
		this.OnBackButtonPress(null);
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00016838 File Offset: 0x00014A38
	private void OnBossSelected(AdventureBossCoin coin, ScenarioDbId mission, bool playerSelected)
	{
		if (this.IsDisabledSelection())
		{
			return;
		}
		if (this.m_SelectedCoin != null)
		{
			this.m_SelectedCoin.Select(false);
		}
		this.m_SelectedCoin = coin;
		this.m_SelectedCoin.Select(true);
		if (this.m_ChooseButton != null)
		{
			if (!this.m_ChooseButton.IsEnabled())
			{
				this.m_ChooseButton.Enable();
			}
			string text = GameStrings.Get(AdventureConfig.DoesMissionRequireDeck(mission) ? "GLUE_CHOOSE" : "GLOBAL_PLAY");
			this.m_ChooseButton.SetText(text);
		}
		this.ShowBossFrame(mission);
		AdventureConfig.Get().SetMission(mission, playerSelected);
		AdventureBossDef bossDef = AdventureConfig.Get().GetBossDef(mission);
		if (bossDef.m_MissionMusic != MusicPlaylistType.Invalid && !MusicManager.Get().StartPlaylist(bossDef.m_MissionMusic))
		{
			this.ResumeMainMusic();
		}
		if (playerSelected && bossDef.m_IntroLinePlayTime == AdventureBossDef.IntroLinePlayTime.MissionSelect)
		{
			AdventureUtils.PlayMissionQuote(bossDef, this.DetermineCharacterQuotePos(coin.gameObject));
		}
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00016924 File Offset: 0x00014B24
	private Vector3 DetermineCharacterQuotePos(GameObject coin)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			return NotificationManager.PHONE_CHARACTER_POS;
		}
		Bounds boundsOfChildren = TransformUtil.GetBoundsOfChildren(coin);
		Vector3 center = boundsOfChildren.center;
		center.y -= boundsOfChildren.extents.y;
		Camera camera = Box.Get().GetCamera();
		ref Vector3 ptr = camera.WorldToScreenPoint(center);
		float num = 0.4f * (float)camera.pixelHeight;
		if (ptr.y < num)
		{
			return NotificationManager.ALT_ADVENTURE_SCREEN_POS;
		}
		return NotificationManager.DEFAULT_CHARACTER_POS;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x0001699C File Offset: 0x00014B9C
	private void ShowBossFrame(ScenarioDbId mission)
	{
		AdventureMissionDisplay.BossInfo bossInfo;
		if (this.m_BossInfoCache.TryGetValue(mission, out bossInfo))
		{
			this.m_BossTitle.Text = bossInfo.m_Title;
			if (this.m_BossDescription != null)
			{
				this.m_BossDescription.Text = bossInfo.m_Description;
			}
		}
		DefLoader.DisposableFullDef disposableFullDef;
		if (this.m_BossPortraitDefCache.TryGetValue(mission, out disposableFullDef))
		{
			this.m_BossActor.SetPremium(TAG_PREMIUM.NORMAL);
			this.m_BossActor.SetEntityDef(disposableFullDef.EntityDef);
			this.m_BossActor.SetCardDef(disposableFullDef.DisposableCardDef);
			this.m_BossActor.UpdateAllComponents();
			this.m_BossActor.SetUnlit();
			this.m_BossActor.Show();
		}
		if (this.m_BossPowerDefCache.TryGetValue(mission, out disposableFullDef))
		{
			this.m_HeroPowerActor.SetPremium(TAG_PREMIUM.NORMAL);
			this.m_HeroPowerActor.SetEntityDef(disposableFullDef.EntityDef);
			this.m_HeroPowerActor.SetCardDef(disposableFullDef.DisposableCardDef);
			this.m_HeroPowerActor.UpdateAllComponents();
			this.m_HeroPowerActor.SetUnlit();
			this.m_HeroPowerActor.Show();
			DefLoader.DisposableFullDef currentBossHeroPowerFullDef = this.m_CurrentBossHeroPowerFullDef;
			if (currentBossHeroPowerFullDef != null)
			{
				currentBossHeroPowerFullDef.Dispose();
			}
			this.m_CurrentBossHeroPowerFullDef = ((disposableFullDef != null) ? disposableFullDef.Share() : null);
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)mission);
			if (this.m_BossPowerContainer != null && !this.m_BossPowerContainer.activeSelf && !record.HideBossHeroPowerInUi)
			{
				this.m_BossPowerContainer.SetActive(true);
				return;
			}
		}
		else if (this.m_BossPowerContainer != null)
		{
			this.m_BossPowerContainer.SetActive(false);
		}
	}

	// Token: 0x06000391 RID: 913 RVA: 0x00016B24 File Offset: 0x00014D24
	private void UnselectBoss()
	{
		if (this.m_BossTitle != null)
		{
			this.m_BossTitle.Text = string.Empty;
		}
		if (this.m_BossDescription != null)
		{
			this.m_BossDescription.Text = string.Empty;
		}
		this.m_BossActor.Hide();
		if (this.m_BossPowerContainer != null)
		{
			this.m_BossPowerContainer.SetActive(false);
		}
		if (this.m_SelectedCoin != null)
		{
			this.m_SelectedCoin.Select(false);
		}
		this.m_SelectedCoin = null;
		AdventureConfig.Get().SetMission(ScenarioDbId.INVALID, true);
		if (this.m_ChooseButton.IsEnabled())
		{
			this.m_ChooseButton.Disable(false);
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00016BD8 File Offset: 0x00014DD8
	private void ChangeToDeckPicker()
	{
		ScenarioDbId mission = AdventureConfig.Get().GetMission();
		AdventureBossDef bossDef = AdventureConfig.Get().GetBossDef(mission);
		if (bossDef != null && bossDef.m_IntroLinePlayTime == AdventureBossDef.IntroLinePlayTime.MissionStart)
		{
			AdventureUtils.PlayMissionQuote(bossDef, this.DetermineCharacterQuotePos(this.m_ChooseButton.gameObject));
		}
		if (AdventureConfig.Get().DoesSelectedMissionRequireDeck())
		{
			this.m_ChooseButton.Disable(false);
			this.DisableSelection(true);
			AdventureConfig.Get().ChangeSubScene(AdventureData.Adventuresubscene.MISSION_DECK_PICKER, true);
			return;
		}
		if (this.m_advMissionDisplayTray != null)
		{
			this.m_advMissionDisplayTray.EnableRewardsChest(false);
		}
		GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, (int)AdventureConfig.Get().GetMission(), 0, 0L, null, null, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00016C91 File Offset: 0x00014E91
	public void GetExternalUILock()
	{
		this.m_waitingOnExternalLock = true;
		this.DisableSelection(true);
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00016CA1 File Offset: 0x00014EA1
	public void ReleaseExternalUILock()
	{
		this.m_waitingOnExternalLock = false;
		this.DisableSelection(false);
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00016CB4 File Offset: 0x00014EB4
	private void DisableSelection(bool yes)
	{
		if (this.m_ClickBlocker == null)
		{
			return;
		}
		this.m_DisableSelectionCount += (yes ? 1 : -1);
		bool flag = this.IsDisabledSelection();
		if (this.m_ClickBlocker.gameObject.activeSelf != flag)
		{
			this.m_ClickBlocker.gameObject.SetActive(flag);
			this.m_ScrollBar.Enable(!flag);
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00016D20 File Offset: 0x00014F20
	private void TryShowWelcomeBanner(BannerManager.DelOnCloseBanner OnCloseBanner)
	{
		bool flag = true;
		foreach (AdventureWing adventureWing in this.m_BossWings)
		{
			int num;
			AdventureProgressMgr.Get().GetWingAck((int)adventureWing.GetWingId(), out num);
			if (num > 0)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			AdventureDef adventureDef = AdventureScene.Get().GetAdventureDef(AdventureConfig.Get().GetSelectedAdventure());
			if (adventureDef != null && !string.IsNullOrEmpty(adventureDef.m_AdventureEntryQuotePrefab) && !string.IsNullOrEmpty(adventureDef.m_AdventureEntryQuoteVOLine))
			{
				string legacyAssetName = new AssetReference(adventureDef.m_AdventureEntryQuoteVOLine).GetLegacyAssetName();
				NotificationManager.Get().CreateCharacterQuote(adventureDef.m_AdventureEntryQuotePrefab, GameStrings.Get(legacyAssetName), adventureDef.m_AdventureEntryQuoteVOLine, true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
			}
			if (adventureDef != null && !string.IsNullOrEmpty(adventureDef.m_AdventureIntroBannerPrefab))
			{
				BannerManager.Get().ShowBanner(adventureDef.m_AdventureIntroBannerPrefab, null, null, OnCloseBanner, null);
				return;
			}
		}
		OnCloseBanner();
	}

	// Token: 0x06000397 RID: 919 RVA: 0x00016E3C File Offset: 0x0001503C
	private IEnumerator UpdateAndAnimateProgress(List<AdventureWing> wings, bool scrollToCoin, bool forceCoinAnimation = false)
	{
		yield return base.StartCoroutine(this.UpdateAndAnimateWingCoinsAndChests(wings, scrollToCoin, forceCoinAnimation));
		this.FireProgressStepCompletedListeners(AdventureMissionDisplay.ProgressStep.WING_COINS_AND_CHESTS_UPDATED);
		while (this.m_waitingOnExternalLock)
		{
			yield return null;
		}
		yield return base.StartCoroutine(this.AnimateWingCompleteBigChests());
		yield return base.StartCoroutine(this.AnimateProgressDisplay());
		yield return base.StartCoroutine(this.AnimateAdventureCompleteCheckmarksAndPopups(false));
		this.CheckForWingUnlocks();
		yield break;
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00016E60 File Offset: 0x00015060
	public bool HasWingJustAckedRequiredProgress(int wingId, int requiredProgress)
	{
		foreach (AdventureWing adventureWing in this.m_BossWings)
		{
			if (adventureWing.GetWingId() == (WingDbId)wingId)
			{
				return adventureWing.HasJustAckedRequiredProgress(requiredProgress);
			}
		}
		return false;
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00016EC4 File Offset: 0x000150C4
	public void SetWingHasJustAckedProgress(int wingId, bool hasJustAckedProgress)
	{
		foreach (AdventureWing adventureWing in this.m_BossWings)
		{
			if (adventureWing.GetWingId() == (WingDbId)wingId)
			{
				adventureWing.SetHasJustAckedProgress(hasJustAckedProgress);
				break;
			}
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00016F24 File Offset: 0x00015124
	private IEnumerator UpdateAndAnimateWingCoinsAndChests(List<AdventureWing> wings, bool scrollToCoin, bool forceCoinAnimation)
	{
		this.DisableSelection(true);
		if (AdventureScene.Get().IsInitialScreen())
		{
			yield return new WaitForSeconds(1.8f);
		}
		int num = 0;
		foreach (AdventureWing adventureWing in wings)
		{
			AdventureWing.DelOnCoinAnimateCallback dlg = null;
			if (scrollToCoin)
			{
				AdventureWing thisWing = adventureWing;
				dlg = delegate(Vector3 p)
				{
					thisWing.BringToFocus();
				};
			}
			if (adventureWing.UpdateAndAnimateCoinsAndChests(this.m_CoinFlipDelayTime * (float)num, forceCoinAnimation, dlg))
			{
				num++;
			}
		}
		if (num > 0)
		{
			yield return new WaitForSeconds(this.m_CoinFlipDelayTime * (float)num + this.m_CoinFlipAnimationTime);
		}
		this.DisableSelection(false);
		yield break;
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00016F48 File Offset: 0x00015148
	private IEnumerator AnimateWingCompleteBigChests()
	{
		if (this.m_WingsToGiveBigChest.Count != 0)
		{
			AdventureMissionDisplay.<>c__DisplayClass85_0 CS$<>8__locals1 = new AdventureMissionDisplay.<>c__DisplayClass85_0();
			CS$<>8__locals1.<>4__this = this;
			this.DisableSelection(true);
			if (AdventureScene.Get().IsInitialScreen())
			{
				yield return new WaitForSeconds(1.8f);
			}
			CS$<>8__locals1.animDone = 0;
			using (List<AdventureWing>.Enumerator enumerator = this.m_WingsToGiveBigChest.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AdventureWing adventureWing = enumerator.Current;
					int animDone = CS$<>8__locals1.animDone + 1;
					CS$<>8__locals1.animDone = animDone;
					AdventureWingEventTable wingEventTable = adventureWing.m_WingEventTable;
					StateEventTable.StateEventTrigger dlg;
					if ((dlg = CS$<>8__locals1.<>9__1) == null)
					{
						dlg = (CS$<>8__locals1.<>9__1 = delegate(Spell s)
						{
							int animDone2 = CS$<>8__locals1.animDone - 1;
							CS$<>8__locals1.animDone = animDone2;
						});
					}
					wingEventTable.AddOpenChestEndEventListener(dlg, true);
					adventureWing.OpenBigChest();
				}
				goto IL_133;
			}
			IL_11C:
			yield return null;
			IL_133:
			if (CS$<>8__locals1.animDone > 0)
			{
				goto IL_11C;
			}
			base.StartCoroutine(this.PlayWingNotifications());
			CS$<>8__locals1.wingIds = new List<int>();
			foreach (AdventureWing adventureWing2 in this.m_WingsToGiveBigChest)
			{
				List<AdventureMissionDbfRecord> list = ClassChallengeUnlock.AdventureMissionsUnlockedByWingId((int)adventureWing2.GetWingId());
				if (list != null && list.Count > 0)
				{
					CS$<>8__locals1.wingIds.Add((int)adventureWing2.GetWingId());
				}
			}
			if (UserAttentionManager.CanShowAttentionGrabber("AdventureMissionDisplay.ShowFixedRewards"))
			{
				this.m_WaitingForClassChallengeUnlocks = true;
				PopupDisplayManager.Get().ShowAnyOutstandingPopups(delegate()
				{
					CS$<>8__locals1.<>4__this.ShowClassChallengeUnlock(CS$<>8__locals1.wingIds);
				});
			}
			while (this.m_WaitingForClassChallengeUnlocks)
			{
				yield return null;
			}
			foreach (AdventureWing adventureWing3 in this.m_WingsToGiveBigChest)
			{
				AdventureWing nextUnlockedWing = this.GetNextUnlockedWing(adventureWing3.GetWingDef());
				string text;
				string text2;
				adventureWing3.GetCompleteQuoteAssetsFromTargetWingEventTiming((int)((nextUnlockedWing == null) ? WingDbId.INVALID : nextUnlockedWing.GetWingDef().GetWingId()), out text, out text2);
				string legacyAssetName = new AssetReference(text2).GetLegacyAssetName();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
				{
					NotificationManager.Get().CreateCharacterQuote(text, GameStrings.Get(legacyAssetName), text2, true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
				}
				adventureWing3.BigChestStayOpen();
			}
			this.m_WingsToGiveBigChest.Clear();
			this.DisableSelection(false);
			CS$<>8__locals1 = null;
		}
		yield break;
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00016F57 File Offset: 0x00015157
	private IEnumerator AnimateProgressDisplay()
	{
		if (this.m_progressDisplay != null)
		{
			while (this.m_progressDisplay.HasProgressAnimationToPlay())
			{
				AdventureMissionDisplay.<>c__DisplayClass86_0 CS$<>8__locals1 = new AdventureMissionDisplay.<>c__DisplayClass86_0();
				CS$<>8__locals1.<>4__this = this;
				this.m_ScrollBar.SetScroll(0f, false, true);
				this.DisableSelection(true);
				CS$<>8__locals1.isAnimComplete = false;
				this.m_progressDisplay.PlayProgressAnimation(delegate
				{
					CS$<>8__locals1.<>4__this.DisableSelection(false);
					CS$<>8__locals1.isAnimComplete = true;
				});
				while (!CS$<>8__locals1.isAnimComplete)
				{
					yield return null;
				}
				CS$<>8__locals1 = null;
			}
		}
		yield break;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00016F68 File Offset: 0x00015168
	private void CheckForWingUnlocks()
	{
		foreach (AdventureWing adventureWing in this.m_BossWings)
		{
			adventureWing.UpdatePlateState();
		}
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00016FB8 File Offset: 0x000151B8
	private IEnumerator AnimateAdventureCompleteCheckmarksAndPopups(bool forceAnimation = false)
	{
		if ((this.m_TotalBosses != this.m_TotalBossesDefeated || !this.m_BossJustDefeated) && !forceAnimation)
		{
			yield break;
		}
		List<KeyValuePair<AdventureRewardsChest, float>> chestAnimates = new List<KeyValuePair<AdventureRewardsChest, float>>();
		float num = 0.7f;
		float totalAnimTime = 0f;
		List<AdventureWing> list = new List<AdventureWing>(this.m_BossWings);
		list.Sort(new Comparison<AdventureWing>(this.WingUnlockOrderSortComparison));
		foreach (AdventureWing adventureWing in list)
		{
			foreach (AdventureRewardsChest key in adventureWing.GetChests())
			{
				num *= 0.9f;
				if (num < 0.1f)
				{
					num = 0.1f;
				}
				totalAnimTime += num;
				chestAnimates.Add(new KeyValuePair<AdventureRewardsChest, float>(key, num));
			}
		}
		this.DisableSelection(true);
		float percentage = 0f;
		float endScroll = 1f;
		if (this.m_progressDisplay != null)
		{
			totalAnimTime -= num;
			percentage = 1f / (float)this.m_BossWings.Count;
		}
		else if (this.m_BossWings[0].GetWingDef().GetUnlockOrder() > this.m_BossWings[this.m_BossWings.Count - 1].GetWingDef().GetUnlockOrder())
		{
			percentage = 1f;
			endScroll = 0f;
		}
		this.m_ScrollBar.SetScroll(percentage, iTween.EaseType.easeOutSine, 0.25f, true, true);
		yield return new WaitForSeconds(0.3f);
		this.m_ScrollBar.SetScroll(endScroll, iTween.EaseType.easeInQuart, totalAnimTime - 0.1f, true, true);
		foreach (KeyValuePair<AdventureRewardsChest, float> keyValuePair in chestAnimates)
		{
			keyValuePair.Key.BurstCheckmark();
			yield return new WaitForSeconds(keyValuePair.Value);
		}
		List<KeyValuePair<AdventureRewardsChest, float>>.Enumerator enumerator3 = default(List<KeyValuePair<AdventureRewardsChest, float>>.Enumerator);
		this.DisableSelection(false);
		this.ShowAdventureCompletePopup();
		while (this.m_showingAdventureCompletePopup)
		{
			yield return null;
		}
		yield break;
		yield break;
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00016FD0 File Offset: 0x000151D0
	public void ShowClassChallengeUnlock(List<int> classChallengeUnlocks)
	{
		if (classChallengeUnlocks == null || classChallengeUnlocks.Count == 0)
		{
			this.m_WaitingForClassChallengeUnlocks = false;
			return;
		}
		foreach (int wingID in classChallengeUnlocks)
		{
			this.m_ClassChallengeUnlockShowing++;
			new ClassChallengeUnlockData(wingID).LoadRewardObject(delegate(Reward reward, object data)
			{
				reward.RegisterHideListener(delegate(object userData)
				{
					this.m_ClassChallengeUnlockShowing--;
					if (this.m_ClassChallengeUnlockShowing == 0)
					{
						this.m_WaitingForClassChallengeUnlocks = false;
					}
				});
				this.OnRewardObjectLoaded(reward, data);
			});
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00017050 File Offset: 0x00015250
	private void ShowAdventureCompletePopup()
	{
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
		this.DisableSelection(true);
		AdventureDef adventureDef = AdventureScene.Get().GetAdventureDef(selectedAdventure);
		AdventureSubDef subDef = adventureDef.GetSubDef(selectedMode);
		AdventureDef.BannerRewardType bannerRewardType = adventureDef.m_BannerRewardType;
		if (bannerRewardType != AdventureDef.BannerRewardType.AdventureCompleteReward)
		{
			if (bannerRewardType == AdventureDef.BannerRewardType.BannerManagerPopup)
			{
				this.m_showingAdventureCompletePopup = true;
				BannerManager.Get().ShowBanner(adventureDef.m_BannerRewardPrefab, null, subDef.GetCompleteBannerText(), delegate()
				{
					this.AdventureCompletePopupDismissed(null);
				}, null);
			}
		}
		else
		{
			this.m_showingAdventureCompletePopup = true;
			new AdventureCompleteRewardData(selectedMode, adventureDef.m_BannerRewardPrefab, subDef.GetCompleteBannerText()).LoadRewardObject(delegate(Reward reward, object data)
			{
				reward.RegisterHideListener(new Reward.OnHideCallback(this.AdventureCompletePopupDismissed));
				this.OnRewardObjectLoaded(reward, data);
			});
		}
		if (!string.IsNullOrEmpty(adventureDef.m_AdventureCompleteQuotePrefab) && !string.IsNullOrEmpty(adventureDef.m_AdventureCompleteQuoteVOLine))
		{
			string legacyAssetName = new AssetReference(adventureDef.m_AdventureCompleteQuoteVOLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(adventureDef.m_AdventureCompleteQuotePrefab, GameStrings.Get(legacyAssetName), adventureDef.m_AdventureCompleteQuoteVOLine, true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x0001714B File Offset: 0x0001534B
	private void AdventureCompletePopupDismissed(object userData)
	{
		this.m_showingAdventureCompletePopup = false;
		this.DisableSelection(false);
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x0001715B File Offset: 0x0001535B
	private void PositionReward(Reward reward)
	{
		GameUtils.SetParent(reward, base.transform, false);
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x0001716A File Offset: 0x0001536A
	private void OnRewardObjectLoaded(Reward reward, object callbackData)
	{
		this.PositionReward(reward);
		reward.Show(false);
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x0001717C File Offset: 0x0001537C
	private List<AdventureMissionDisplay.WingCreateParams> BuildWingCreateParamsList()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId selectedAdventure = adventureConfig.GetSelectedAdventure();
		AdventureModeDbId selectedMode = adventureConfig.GetSelectedMode();
		int adventureDbId = (int)selectedAdventure;
		int modeDbId = (int)selectedMode;
		List<AdventureMissionDisplay.WingCreateParams> list = new List<AdventureMissionDisplay.WingCreateParams>();
		int num = 0;
		foreach (ScenarioDbfRecord scenarioDbfRecord in GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == adventureDbId && r.ModeId == modeDbId, -1))
		{
			ScenarioDbId id = (ScenarioDbId)scenarioDbfRecord.ID;
			WingDbId wingId = (WingDbId)scenarioDbfRecord.WingId;
			int num2 = scenarioDbfRecord.ClientPlayer2HeroCardId;
			if (num2 == 0)
			{
				num2 = scenarioDbfRecord.Player2HeroCardId;
			}
			AdventureWingDef wingDef = AdventureScene.Get().GetWingDef(wingId);
			if (wingDef == null)
			{
				Debug.LogError(string.Format("Unable to find wing record for scenario {0} with ID: {1}", id, wingId));
			}
			else
			{
				CardDbfRecord record = GameDbf.Card.GetRecord(num2);
				AdventureMissionDisplay.WingCreateParams wingCreateParams = list.Find((AdventureMissionDisplay.WingCreateParams currParams) => wingId == currParams.m_WingDef.GetWingId());
				if (wingCreateParams == null)
				{
					wingCreateParams = new AdventureMissionDisplay.WingCreateParams();
					wingCreateParams.m_WingDef = wingDef;
					if (wingCreateParams.m_WingDef == null)
					{
						Error.AddDevFatal("AdventureDisplay.BuildWingCreateParamsMap() - failed to find a WingDef for adventure {0} wing {1}", new object[]
						{
							selectedAdventure,
							wingId
						});
						continue;
					}
					list.Add(wingCreateParams);
				}
				AdventureMissionDisplay.BossCreateParams bossCreateParams = new AdventureMissionDisplay.BossCreateParams();
				bossCreateParams.m_ScenarioRecord = scenarioDbfRecord;
				bossCreateParams.m_MissionId = id;
				bossCreateParams.m_CardDefId = record.NoteMiniGuid;
				if (!this.m_BossInfoCache.ContainsKey(id))
				{
					AdventureMissionDisplay.BossInfo value = new AdventureMissionDisplay.BossInfo
					{
						m_Title = scenarioDbfRecord.ShortName,
						m_Description = ((UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(scenarioDbfRecord.ShortDescription)) ? scenarioDbfRecord.ShortDescription : scenarioDbfRecord.Description)
					};
					this.m_BossInfoCache[id] = value;
				}
				wingCreateParams.m_BossCreateParams.Add(bossCreateParams);
				num++;
			}
		}
		if (num == 0)
		{
			Debug.LogError(string.Format("Unable to find any bosses associated with wing {0} and mode {1}.\nCheck if the scenario DBF has valid entries!", selectedAdventure, selectedMode));
		}
		list.Sort(new Comparison<AdventureMissionDisplay.WingCreateParams>(this.WingCreateParamsSortComparison));
		foreach (AdventureMissionDisplay.WingCreateParams wingCreateParams2 in list)
		{
			wingCreateParams2.m_BossCreateParams.Sort(new Comparison<AdventureMissionDisplay.BossCreateParams>(this.BossCreateParamsSortComparison));
		}
		return list;
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00017448 File Offset: 0x00015648
	private int WingCreateParamsSortComparison(AdventureMissionDisplay.WingCreateParams params1, AdventureMissionDisplay.WingCreateParams params2)
	{
		return params1.m_WingDef.GetSortOrder() - params2.m_WingDef.GetSortOrder();
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00017461 File Offset: 0x00015661
	private int BossCreateParamsSortComparison(AdventureMissionDisplay.BossCreateParams params1, AdventureMissionDisplay.BossCreateParams params2)
	{
		return GameUtils.MissionSortComparison(params1.m_ScenarioRecord, params2.m_ScenarioRecord);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00017474 File Offset: 0x00015674
	private int WingUnlockOrderSortComparison(AdventureWing wing1, AdventureWing wing2)
	{
		return wing1.GetWingDef().GetUnlockOrder() - wing2.GetWingDef().GetUnlockOrder();
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00017490 File Offset: 0x00015690
	private void ShowAdventureStore(AdventureWing selectedWing)
	{
		if (SetRotationManager.Get().CheckForSetRotationRollover())
		{
			return;
		}
		if (PlayerMigrationManager.Get() != null && PlayerMigrationManager.Get().CheckForPlayerMigrationRequired())
		{
			return;
		}
		StoreManager.Get().StartAdventureTransaction(selectedWing.GetProductType(), selectedWing.GetProductData(), null, null, ShopType.ADVENTURE_STORE, 1, false, null, 0);
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x000174DB File Offset: 0x000156DB
	private void OnStoreShown()
	{
		this.DisableSelection(true);
	}

	// Token: 0x060003AA RID: 938 RVA: 0x000174E4 File Offset: 0x000156E4
	private void OnStoreHidden()
	{
		this.DisableSelection(false);
	}

	// Token: 0x060003AB RID: 939 RVA: 0x000174F0 File Offset: 0x000156F0
	private void OnAdventureProgressUpdate(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress, object userData)
	{
		bool flag = oldProgress != null && oldProgress.IsOwned();
		bool flag2 = newProgress != null && newProgress.IsOwned();
		if (flag == flag2)
		{
			return;
		}
		base.StartCoroutine(this.UpdateWingPlateStates());
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00017527 File Offset: 0x00015727
	private IEnumerator UpdateWingPlateStates()
	{
		while (StoreManager.Get().IsShown())
		{
			yield return null;
		}
		using (List<AdventureWing>.Enumerator enumerator = this.m_BossWings.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				AdventureWing adventureWing = enumerator.Current;
				adventureWing.UpdatePlateState();
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00017538 File Offset: 0x00015738
	private void ShowRewardsPreview(AdventureWing wing, int[] scenarioids, List<RewardData> wingRewards, string wingName)
	{
		if (this.m_ShowingRewardsPreview)
		{
			return;
		}
		if (this.m_ClickBlocker != null)
		{
			this.m_ClickBlocker.SetActive(true);
		}
		this.m_ShowingRewardsPreview = true;
		this.m_PreviewPane.Reset();
		this.m_PreviewPane.SetHeaderText(wingName);
		List<string> specificRewardsPreviewCards = wing.GetWingDef().m_SpecificRewardsPreviewCards;
		List<int> specificRewardsPreviewCardBacks = wing.GetWingDef().m_SpecificRewardsPreviewCardBacks;
		List<BoosterDbId> specificRewardsPreviewBoosters = wing.GetWingDef().m_SpecificRewardsPreviewBoosters;
		int hiddenRewardsPreviewCount = wing.GetWingDef().m_HiddenRewardsPreviewCount;
		object obj = specificRewardsPreviewCards != null && specificRewardsPreviewCards.Count > 0;
		bool flag = specificRewardsPreviewCardBacks != null && specificRewardsPreviewCardBacks.Count > 0;
		bool flag2 = specificRewardsPreviewBoosters != null && specificRewardsPreviewBoosters.Count > 0;
		object obj2 = obj;
		if (obj2 != null)
		{
			this.m_PreviewPane.AddSpecificCards(specificRewardsPreviewCards);
		}
		if (flag)
		{
			this.m_PreviewPane.AddSpecificCardBacks(specificRewardsPreviewCardBacks);
		}
		if (flag2)
		{
			this.m_PreviewPane.AddSpecificBoosters(specificRewardsPreviewBoosters);
		}
		if (obj2 == null && !flag && !flag2)
		{
			foreach (int scenarioId in scenarioids)
			{
				this.m_PreviewPane.AddRewardBatch(scenarioId);
			}
			if (wingRewards != null && wingRewards.Count > 0)
			{
				this.m_PreviewPane.AddRewardBatch(wingRewards);
			}
		}
		this.m_PreviewPane.SetHiddenCardCount(hiddenRewardsPreviewCount);
		this.m_PreviewPane.Show(true);
	}

	// Token: 0x060003AE RID: 942 RVA: 0x0001767B File Offset: 0x0001587B
	private void OnHideRewardsPreview()
	{
		if (this.m_ClickBlocker != null)
		{
			this.m_ClickBlocker.SetActive(false);
		}
		this.m_ShowingRewardsPreview = false;
	}

	// Token: 0x060003AF RID: 943 RVA: 0x0001769E File Offset: 0x0001589E
	private void OnStartUnlockPlate(AdventureWing wing)
	{
		if (!wing.ContainsBossCoin(this.m_SelectedCoin))
		{
			this.UnselectBoss();
		}
		this.DisableSelection(true);
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x000176BC File Offset: 0x000158BC
	private void OnEndUnlockPlate(AdventureWing wing)
	{
		this.DisableSelection(false);
		if (!string.IsNullOrEmpty(wing.GetWingDef().m_WingOpenPopup))
		{
			AdventureWingOpenBanner adventureWingOpenBanner = GameUtils.LoadGameObjectWithComponent<AdventureWingOpenBanner>(wing.GetWingDef().m_WingOpenPopup);
			if (adventureWingOpenBanner != null)
			{
				adventureWingOpenBanner.ShowBanner(delegate
				{
					this.StartCoroutine(this.UpdateAndAnimateProgress(new List<AdventureWing>
					{
						wing
					}, false, false));
				});
				return;
			}
		}
		else
		{
			base.StartCoroutine(this.UpdateAndAnimateProgress(new List<AdventureWing>
			{
				wing
			}, false, false));
		}
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x0001774D File Offset: 0x0001594D
	private void BatchBringWingToFocus(AdventureWing wing)
	{
		if (!this.m_wingsToFocus.Contains(wing))
		{
			this.m_wingsToFocus.Add(wing);
		}
		if (this.m_scheduledBringWingsToFocusCallback == null)
		{
			this.m_scheduledBringWingsToFocusCallback = base.StartCoroutine(this.WaitThenBringWingsToFocus());
		}
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00017783 File Offset: 0x00015983
	private IEnumerator WaitThenBringWingsToFocus()
	{
		yield return new WaitForEndOfFrame();
		if (this.m_wingsToFocus.Count == 0)
		{
			yield break;
		}
		this.m_wingsToFocus.Sort(new Comparison<AdventureWing>(this.WingUnlockOrderSortComparison));
		this.BringWingToFocus(this.m_wingsToFocus[0]);
		this.m_scheduledBringWingsToFocusCallback = null;
		this.m_wingsToFocus.Clear();
		yield break;
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00017794 File Offset: 0x00015994
	private void BringWingToFocus(AdventureWing wing)
	{
		if (this.m_ScrollBar == null)
		{
			return;
		}
		float positionOffset = 0f;
		UIBScrollableItem component = wing.GetComponent<UIBScrollableItem>();
		if (component != null)
		{
			positionOffset = component.m_offset.z * wing.gameObject.transform.lossyScale.z;
		}
		this.m_ScrollBar.CenterObjectInView(wing.gameObject, positionOffset, null, this.m_ScrollBar.m_ScrollEaseType, this.m_ScrollBar.m_ScrollTweenTime, true);
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00017813 File Offset: 0x00015A13
	private IEnumerator RememberLastBossSelection(AdventureBossCoin coin, ScenarioDbId mission)
	{
		while (base.AssetLoadingHelper.AssetsLoading > 0)
		{
			yield return null;
		}
		this.OnBossSelected(coin, mission, false);
		yield break;
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00017830 File Offset: 0x00015A30
	private IEnumerator PlayWingNotifications()
	{
		yield return new WaitForSeconds(3f);
		using (List<AdventureWing>.Enumerator enumerator = this.m_WingsToGiveBigChest.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				AdventureWing adventureWing = enumerator.Current;
				if (adventureWing.GetAdventureId() == AdventureDbId.NAXXRAMAS && adventureWing.GetWingId() == WingDbId.NAXX_ARACHNID)
				{
					NotificationManager.Get().CreateKTQuote("VO_KT_MAEXXNA5_50", "VO_KT_MAEXXNA5_50.prefab:71879e77d87e0e745be507be968067bf", true);
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x0001783F File Offset: 0x00015A3F
	private void ResumeMainMusic()
	{
		if (this.m_mainMusic != MusicPlaylistType.Invalid)
		{
			MusicManager.Get().StartPlaylist(this.m_mainMusic);
		}
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x0001785C File Offset: 0x00015A5C
	private AdventureWing GetNextUnlockedWing(AdventureWingDef currentWingDef)
	{
		AdventureWing adventureWing = null;
		foreach (AdventureWing adventureWing2 in this.m_BossWings)
		{
			if (adventureWing2.GetWingDef().GetUnlockOrder() > currentWingDef.GetUnlockOrder() && (adventureWing == null || adventureWing2.GetWingDef().GetUnlockOrder() < adventureWing.GetWingDef().GetUnlockOrder()))
			{
				adventureWing = adventureWing2;
			}
		}
		return adventureWing;
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x000178E4 File Offset: 0x00015AE4
	private void SaveScrollbarValue()
	{
		if (this.m_ScrollBar != null && AdventureConfig.Get() != null)
		{
			this.m_ScrollBar.SaveScroll(AdventureConfig.Get().GetSelectedAdventureAndModeString());
		}
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00017918 File Offset: 0x00015B18
	private void Cheat_OpenNextWing()
	{
		if (!AdventureScene.Get().IsDevMode)
		{
			return;
		}
		AdventureWing adventureWing = null;
		foreach (AdventureWing adventureWing2 in this.m_BossWings)
		{
			if (adventureWing2.m_WingEventTable.IsPlateInOrGoingToAnActiveState() && (adventureWing == null || adventureWing2.GetWingDef().GetUnlockOrder() < adventureWing.GetWingDef().GetUnlockOrder()))
			{
				adventureWing = adventureWing2;
			}
		}
		if (adventureWing != null)
		{
			adventureWing.UnlockPlate();
		}
	}

	// Token: 0x060003BA RID: 954 RVA: 0x000179B4 File Offset: 0x00015BB4
	private void Cheat_OpenNextChest()
	{
		if (!AdventureScene.Get().IsDevMode)
		{
			return;
		}
		this.m_WingsToGiveBigChest.Clear();
		if (AdventureMissionDisplay.s_cheat_nextWingToGrantChest >= this.m_BossWings.Count)
		{
			AdventureMissionDisplay.s_cheat_nextWingToGrantChest = 0;
		}
		AdventureWing item = this.m_BossWings[AdventureMissionDisplay.s_cheat_nextWingToGrantChest];
		this.m_WingsToGiveBigChest.Add(item);
		base.StartCoroutine(this.UpdateAndAnimateProgress(new List<AdventureWing>
		{
			item
		}, false, true));
		AdventureMissionDisplay.s_cheat_nextWingToGrantChest++;
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00017A38 File Offset: 0x00015C38
	public bool Cheat_AdventureEvent(string eventName)
	{
		if (!AdventureScene.Get().IsDevMode)
		{
			return false;
		}
		foreach (AdventureWing adventureWing in this.m_BossWings)
		{
			adventureWing.m_WingEventTable.TriggerState(eventName, true, null);
		}
		return true;
	}

	// Token: 0x04000272 RID: 626
	[CustomEditField(Sections = "Boss Layout Settings")]
	public GameObject m_BossWingContainer;

	// Token: 0x04000273 RID: 627
	[CustomEditField(Sections = "Boss Info")]
	public UberText m_BossTitle;

	// Token: 0x04000274 RID: 628
	[CustomEditField(Sections = "Boss Info")]
	public UberText m_BossDescription;

	// Token: 0x04000275 RID: 629
	[CustomEditField(Sections = "UI")]
	public UberText m_AdventureTitle;

	// Token: 0x04000276 RID: 630
	[CustomEditField(Sections = "UI")]
	public PegUIElement m_BackButton;

	// Token: 0x04000277 RID: 631
	[CustomEditField(Sections = "UI")]
	public PlayButton m_ChooseButton;

	// Token: 0x04000278 RID: 632
	[CustomEditField(Sections = "UI")]
	public GameObject m_BossPortraitContainer;

	// Token: 0x04000279 RID: 633
	[CustomEditField(Sections = "UI")]
	public GameObject m_BossPowerContainer;

	// Token: 0x0400027A RID: 634
	[CustomEditField(Sections = "UI")]
	public PegUIElement m_BossPowerHoverArea;

	// Token: 0x0400027B RID: 635
	[CustomEditField(Sections = "UI", T = EditType.GAME_OBJECT)]
	public MeshRenderer m_WatermarkIcon;

	// Token: 0x0400027C RID: 636
	[CustomEditField(Sections = "UI")]
	public AdventureRewardsDisplayArea m_RewardsDisplay;

	// Token: 0x0400027D RID: 637
	[CustomEditField(Sections = "UI")]
	public GameObject m_ClickBlocker;

	// Token: 0x0400027E RID: 638
	[CustomEditField(Sections = "UI/Animation")]
	public float m_CoinFlipDelayTime = 1.25f;

	// Token: 0x0400027F RID: 639
	[CustomEditField(Sections = "UI/Animation")]
	public float m_CoinFlipAnimationTime = 1f;

	// Token: 0x04000280 RID: 640
	[CustomEditField(Sections = "UI/Scroll Bar")]
	public UIBScrollable m_ScrollBar;

	// Token: 0x04000281 RID: 641
	[CustomEditField(Sections = "UI/Preview Pane")]
	public AdventureRewardsPreview m_PreviewPane;

	// Token: 0x04000282 RID: 642
	public AdventureMissionDisplayTray m_advMissionDisplayTray;

	// Token: 0x04000283 RID: 643
	[SerializeField]
	private Vector3 m_BossWingOffset = Vector3.zero;

	// Token: 0x04000284 RID: 644
	private static AdventureMissionDisplay s_instance;

	// Token: 0x04000285 RID: 645
	private AdventureWingProgressDisplay m_progressDisplay;

	// Token: 0x04000286 RID: 646
	private List<AdventureWing> m_BossWings = new List<AdventureWing>();

	// Token: 0x04000287 RID: 647
	private GameObject m_BossWingBorder;

	// Token: 0x04000288 RID: 648
	private AdventureBossCoin m_SelectedCoin;

	// Token: 0x04000289 RID: 649
	private Map<ScenarioDbId, DefLoader.DisposableFullDef> m_BossPortraitDefCache = new Map<ScenarioDbId, DefLoader.DisposableFullDef>();

	// Token: 0x0400028A RID: 650
	private Map<ScenarioDbId, DefLoader.DisposableFullDef> m_BossPowerDefCache = new Map<ScenarioDbId, DefLoader.DisposableFullDef>();

	// Token: 0x0400028B RID: 651
	private int m_DisableSelectionCount;

	// Token: 0x0400028C RID: 652
	private List<AdventureWing> m_WingsToGiveBigChest = new List<AdventureWing>();

	// Token: 0x0400028D RID: 653
	private bool m_ShowingRewardsPreview;

	// Token: 0x0400028E RID: 654
	private int m_TotalBosses;

	// Token: 0x0400028F RID: 655
	private int m_TotalBossesDefeated;

	// Token: 0x04000290 RID: 656
	private bool m_BossJustDefeated;

	// Token: 0x04000291 RID: 657
	private bool m_WaitingForClassChallengeUnlocks;

	// Token: 0x04000292 RID: 658
	private int m_ClassChallengeUnlockShowing;

	// Token: 0x04000293 RID: 659
	private Map<ScenarioDbId, AdventureMissionDisplay.BossInfo> m_BossInfoCache = new Map<ScenarioDbId, AdventureMissionDisplay.BossInfo>();

	// Token: 0x04000294 RID: 660
	private MusicPlaylistType m_mainMusic;

	// Token: 0x04000295 RID: 661
	private List<AdventureWing> m_wingsToFocus = new List<AdventureWing>();

	// Token: 0x04000296 RID: 662
	private Coroutine m_scheduledBringWingsToFocusCallback;

	// Token: 0x04000297 RID: 663
	private AssetHandle<Texture> m_watermarkTexture;

	// Token: 0x04000298 RID: 664
	private List<AdventureMissionDisplay.ProgressStepCompletedCallback> m_ProgressStepCompletedListeners = new List<AdventureMissionDisplay.ProgressStepCompletedCallback>();

	// Token: 0x04000299 RID: 665
	private bool m_waitingOnExternalLock;

	// Token: 0x0400029A RID: 666
	private const float s_ScreenBackTransitionDelay = 1.8f;

	// Token: 0x0400029B RID: 667
	private bool m_showingAdventureCompletePopup;

	// Token: 0x0400029C RID: 668
	private static int s_cheat_nextWingToGrantChest;

	// Token: 0x020012F8 RID: 4856
	protected class BossCreateParams
	{
		// Token: 0x0400A53B RID: 42299
		public ScenarioDbfRecord m_ScenarioRecord;

		// Token: 0x0400A53C RID: 42300
		public ScenarioDbId m_MissionId;

		// Token: 0x0400A53D RID: 42301
		public string m_CardDefId;
	}

	// Token: 0x020012F9 RID: 4857
	protected class WingCreateParams
	{
		// Token: 0x0400A53E RID: 42302
		public AdventureWingDef m_WingDef;

		// Token: 0x0400A53F RID: 42303
		[CustomEditField(ListTable = true)]
		public List<AdventureMissionDisplay.BossCreateParams> m_BossCreateParams = new List<AdventureMissionDisplay.BossCreateParams>();
	}

	// Token: 0x020012FA RID: 4858
	protected class BossInfo
	{
		// Token: 0x0400A540 RID: 42304
		public string m_Title;

		// Token: 0x0400A541 RID: 42305
		public string m_Description;
	}

	// Token: 0x020012FB RID: 4859
	public enum ProgressStep
	{
		// Token: 0x0400A543 RID: 42307
		INVALID,
		// Token: 0x0400A544 RID: 42308
		WING_COINS_AND_CHESTS_UPDATED
	}

	// Token: 0x020012FC RID: 4860
	// (Invoke) Token: 0x0600D60E RID: 54798
	public delegate void ProgressStepCompletedCallback(AdventureMissionDisplay.ProgressStep progress);
}
