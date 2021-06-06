using System;
using System.Collections;
using Assets;
using Blizzard.T5.AssetManager;
using Hearthstone.DungeonCrawl;
using PegasusShared;
using UnityEngine;

// Token: 0x02000736 RID: 1846
[CustomEditClass]
public class TavernBrawlDisplay : MonoBehaviour
{
	// Token: 0x060067AF RID: 26543 RVA: 0x0021C930 File Offset: 0x0021AB30
	private void Awake()
	{
		TavernBrawlDisplay.s_instance = this;
		base.transform.localScale = Vector3.one;
		this.m_currentMission = TavernBrawlManager.Get().CurrentMission();
		this.m_assetLoadingHelper = new AssetLoadingHelper();
		this.m_assetLoadingHelper.AssetLoadingComplete += this.OnAssetLoadingComplete;
		this.Awake_InitializeRewardDisplay();
		this.SetupUniversalButtons();
		this.RegisterListeners();
		this.SetUIForFriendlyChallenge(FriendChallengeMgr.Get().IsChallengeTavernBrawl() && !FiresideGatheringManager.Get().InBrawlMode());
	}

	// Token: 0x060067B0 RID: 26544 RVA: 0x0021C9BC File Offset: 0x0021ABBC
	private void Start()
	{
		this.m_tavernBrawlTray.ToggleTraySlider(true, null, false);
		this.m_rewardChestDeprecated = false;
		if (PresenceMgr.Get().CurrentStatus != Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING)
		{
			Global.PresenceStatus presenceStatus = (this.m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_TAVERN_BRAWL) ? Global.PresenceStatus.TAVERN_BRAWL_SCREEN : Global.PresenceStatus.FIRESIDE_BRAWL_SCREEN;
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				presenceStatus
			});
		}
		base.StartCoroutine(this.RefreshUIWhenReady());
		if (this.m_currentMission == null)
		{
			return;
		}
		MusicPlaylistType type = (this.m_currentMission.brawlMode == TavernBrawlMode.TB_MODE_HEROIC) ? MusicPlaylistType.UI_HeroicBrawl : MusicPlaylistType.UI_TavernBrawl;
		MusicManager.Get().StartPlaylist(type);
		NarrativeManager.Get().OnTavernBrawlEntered();
		this.InitExpoDemoMode();
	}

	// Token: 0x060067B1 RID: 26545 RVA: 0x0021CA63 File Offset: 0x0021AC63
	private IEnumerator RefreshUIWhenReady()
	{
		while (TavernBrawlManager.Get() == null)
		{
			yield return null;
		}
		TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
		if (tavernBrawlMission != null && tavernBrawlMission.canCreateDeck && !UniversalInputManager.UsePhoneUI)
		{
			while (CollectionDeckTray.Get() == null)
			{
				yield return null;
			}
		}
		this.RefreshStateBasedUI(false);
		this.RefreshDataBasedUI(this.m_wipeAnimStartDelay);
		yield break;
	}

	// Token: 0x060067B2 RID: 26546 RVA: 0x0021CA72 File Offset: 0x0021AC72
	private void OnDestroy()
	{
		this.HideDemoQuotes();
		this.UnregisterListeners();
		TavernBrawlDisplay.s_instance = null;
	}

	// Token: 0x060067B3 RID: 26547 RVA: 0x0021CA86 File Offset: 0x0021AC86
	public static TavernBrawlDisplay Get()
	{
		return TavernBrawlDisplay.s_instance;
	}

	// Token: 0x060067B4 RID: 26548 RVA: 0x0021CA90 File Offset: 0x0021AC90
	public void Unload()
	{
		if (FriendChallengeMgr.Get().IsChallengeTavernBrawl() && !SceneMgr.Get().IsInGame() && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FRIENDLY) && (!FriendChallengeMgr.Get().DidReceiveChallenge() || FriendChallengeMgr.Get().DidChallengeeAccept()))
		{
			FriendChallengeMgr.Get().CancelChallenge();
		}
		if (this.IsInDeckEditMode())
		{
			Navigation.Pop();
		}
	}

	// Token: 0x060067B5 RID: 26549 RVA: 0x0021CAF4 File Offset: 0x0021ACF4
	public void RefreshDataBasedUI(float animDelay = 0f)
	{
		if (this.m_currentlyShowingMode == TavernBrawlStatus.TB_STATUS_IN_REWARDS)
		{
			return;
		}
		if (this.m_tavernBrawlHasEndedDialogActive)
		{
			return;
		}
		this.RefreshTavernBrawlInfo(animDelay);
		if (this.m_currentMission == null)
		{
			return;
		}
		if (this.m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING && !FiresideGatheringManager.Get().IsCheckedIn)
		{
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			}
			return;
		}
		this.UpdateRecordUI();
		if (this.m_currentMission.brawlMode == TavernBrawlMode.TB_MODE_HEROIC && !this.m_firstTimeIntroductionPopupShowing && !Options.Get().GetBool(Option.HAS_SEEN_HEROIC_BRAWL, false) && UserAttentionManager.CanShowAttentionGrabber("TavernBrawlDisplay.RefreshDataBasedUI:" + Option.HAS_SEEN_HEROIC_BRAWL))
		{
			this.m_firstTimeIntroductionPopupShowing = true;
			base.StartCoroutine(this.DoFirstTimeHeroicIntro());
			return;
		}
		if (this.m_firstTimeIntroductionPopupShowing)
		{
			return;
		}
		TavernBrawlStatus playerStatus = TavernBrawlManager.Get().PlayerStatus;
		if (playerStatus != TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED)
		{
			StoreManager.Get().HideStore(ShopType.TAVERN_BRAWL_STORE);
		}
		switch (playerStatus)
		{
		case TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED:
			base.StartCoroutine(this.ShowPurchaseScreen());
			return;
		case TavernBrawlStatus.TB_STATUS_IN_REWARDS:
			base.StartCoroutine(this.ShowRewardsScreen());
			return;
		}
		if (playerStatus != TavernBrawlStatus.TB_STATUS_ACTIVE && this.m_currentMission != null && this.m_currentMission.IsSessionBased)
		{
			Debug.LogErrorFormat("TavernBrawlDisplay.UpdateDisplayState(): don't know how to handle currentStatus={0}. Kicking to HUB", new object[]
			{
				playerStatus
			});
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			}
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			if (this.m_currentMission.brawlMode == TavernBrawlMode.TB_MODE_HEROIC)
			{
				popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR_TITLE");
				popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR");
			}
			else
			{
				popupInfo.m_headerText = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR_TITLE");
				popupInfo.m_text = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR");
			}
			popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				TavernBrawlManager.Get().RefreshServerData(BrawlType.BRAWL_TYPE_UNKNOWN);
			};
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		this.ShowActiveScreen(animDelay);
	}

	// Token: 0x060067B6 RID: 26550 RVA: 0x0021CCFD File Offset: 0x0021AEFD
	public bool IsInDeckEditMode()
	{
		return this.m_deckBeingEdited > 0L;
	}

	// Token: 0x060067B7 RID: 26551 RVA: 0x0021CD09 File Offset: 0x0021AF09
	public bool IsInRewards()
	{
		return this.m_currentlyShowingMode == TavernBrawlStatus.TB_STATUS_IN_REWARDS;
	}

	// Token: 0x060067B8 RID: 26552 RVA: 0x0021CD14 File Offset: 0x0021AF14
	public bool BackFromDeckEdit(bool animate)
	{
		if (!this.IsInDeckEditMode())
		{
			return false;
		}
		if (animate)
		{
			PresenceMgr.Get().SetPrevStatus();
		}
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != CollectionUtils.ViewMode.CARDS)
		{
			if (TavernBrawlManager.Get().CurrentDeck() == null)
			{
				CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARDS, null);
			}
			else
			{
				CollectionManager.Get().GetCollectibleDisplay().m_pageManager.JumpToCollectionClassPage(TavernBrawlManager.Get().CurrentDeck().GetClass());
			}
		}
		this.m_tavernBrawlTray.ToggleTraySlider(true, null, animate);
		this.RefreshStateBasedUI(animate);
		this.m_deckBeingEdited = 0L;
		BnetBar.Get().RefreshCurrency();
		FriendChallengeMgr.Get().UpdateMyAvailability();
		this.UpdateEditOrCreate();
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_editDeckButton.SetText(GameStrings.Get("GLUE_EDIT"));
			if (this.m_editIcon != null)
			{
				this.m_editIcon.SetActive(true);
			}
			if (this.m_deleteIcon != null)
			{
				this.m_deleteIcon.SetActive(false);
			}
		}
		CollectionDeckTray.Get().ExitEditDeckModeForTavernBrawl();
		return true;
	}

	// Token: 0x060067B9 RID: 26553 RVA: 0x0021CE24 File Offset: 0x0021B024
	public static bool IsTavernBrawlOpen()
	{
		return SceneMgr.Get().IsInTavernBrawlMode() && !(TavernBrawlDisplay.s_instance == null);
	}

	// Token: 0x060067BA RID: 26554 RVA: 0x0021CE44 File Offset: 0x0021B044
	public static bool IsTavernBrawlEditing()
	{
		return TavernBrawlDisplay.IsTavernBrawlOpen() && TavernBrawlDisplay.s_instance.IsInDeckEditMode();
	}

	// Token: 0x060067BB RID: 26555 RVA: 0x0021CE59 File Offset: 0x0021B059
	public static bool IsTavernBrawlViewing()
	{
		return TavernBrawlDisplay.IsTavernBrawlOpen() && !TavernBrawlDisplay.s_instance.IsInDeckEditMode();
	}

	// Token: 0x060067BC RID: 26556 RVA: 0x0021CE74 File Offset: 0x0021B074
	public void ValidateDeck()
	{
		if (this.m_currentMission == null)
		{
			this.DisablePlayButton();
			return;
		}
		if (this.m_currentMission.canCreateDeck)
		{
			if (TavernBrawlManager.Get().HasValidDeckForCurrent())
			{
				if (TavernBrawlManager.Get().PlayerStatus == TavernBrawlStatus.TB_STATUS_ACTIVE)
				{
					if (this.m_playButton != null)
					{
						this.m_playButton.Enable();
					}
					this.m_FiresideGatheringPlayButtonLantern.SetLanternLit(true);
				}
				if (this.m_editDeckHighlight != null)
				{
					this.m_editDeckHighlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
					return;
				}
			}
			else
			{
				this.DisablePlayButton();
				if (this.m_editDeckHighlight != null)
				{
					this.m_editDeckHighlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
				}
			}
		}
	}

	// Token: 0x060067BD RID: 26557 RVA: 0x0021CF1C File Offset: 0x0021B11C
	public void EnablePlayButton()
	{
		if (this.m_currentMission == null || this.m_currentMission.canCreateDeck)
		{
			this.ValidateDeck();
			return;
		}
		if (this.m_playButton != null)
		{
			this.m_playButton.Enable();
		}
		if (this.m_FiresideGatheringPlayButtonLantern != null)
		{
			this.m_FiresideGatheringPlayButtonLantern.SetLanternLit(true);
		}
	}

	// Token: 0x060067BE RID: 26558 RVA: 0x0021CF78 File Offset: 0x0021B178
	private void DisablePlayButton()
	{
		if (this.m_playButton != null)
		{
			this.m_playButton.Disable(false);
		}
		if (this.m_FiresideGatheringPlayButtonLantern != null)
		{
			this.m_FiresideGatheringPlayButtonLantern.SetLanternLit(false);
		}
	}

	// Token: 0x060067BF RID: 26559 RVA: 0x0021CFAE File Offset: 0x0021B1AE
	public void EnableBackButton(bool enable)
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetEnabled(enable, false);
			this.m_backButton.Flip(enable, false);
		}
	}

	// Token: 0x060067C0 RID: 26560 RVA: 0x0021CFD8 File Offset: 0x0021B1D8
	public void OnHeroPickerClosed()
	{
		if (this.m_dungeonCrawlDisplay != null && this.m_dungeonCrawlServices != null)
		{
			this.m_dungeonCrawlDisplay.EnablePlayButton();
			return;
		}
		this.EnablePlayButton();
		this.EnableBackButton(true);
	}

	// Token: 0x060067C1 RID: 26561 RVA: 0x0021D009 File Offset: 0x0021B209
	public AdventureDef GetAdventureDef(AdventureDbId id)
	{
		return this.m_adventureDefCache.GetDef(id);
	}

	// Token: 0x060067C2 RID: 26562 RVA: 0x0021D017 File Offset: 0x0021B217
	public AdventureWingDef GetAdventureWingDef(WingDbId id)
	{
		return this.m_adventureWingDefCache.GetDef(id);
	}

	// Token: 0x060067C3 RID: 26563 RVA: 0x0021D028 File Offset: 0x0021B228
	private void RefreshTavernBrawlInfo(float animDelay)
	{
		this.UpdateEditOrCreate();
		this.m_currentMission = TavernBrawlManager.Get().CurrentMission();
		if (this.m_currentMission == null || this.m_currentMission.missionId < 0)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_TAVERN_BRAWL_HAS_ENDED_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_TAVERN_BRAWL_HAS_ENDED_TEXT");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.RefreshTavernBrawlInfo_ConfirmEnded);
			popupInfo.m_offset = new Vector3(0f, 104f, 0f);
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			DialogManager.Get().ShowPopup(popupInfo);
			this.m_tavernBrawlHasEndedDialogActive = true;
			return;
		}
		if (this.m_rewardChestDeprecated || this.m_currentMission.rewardType == RewardType.REWARD_CHEST || this.m_currentMission.rewardType == RewardType.REWARD_NONE || DemoMgr.Get().IsDemo())
		{
			this.m_rewardChest.gameObject.SetActive(false);
		}
		if (this.m_currentMission.IsSessionBased)
		{
			if (this.m_sessionWinLocationBone != null)
			{
				this.m_winsBanner.transform.position = this.m_sessionWinLocationBone.transform.position;
			}
			if (this.m_lossMarks != null)
			{
				this.m_lossesRoot.SetActive(true);
				this.m_lossMarks.Init(this.m_currentMission.maxLosses);
			}
			if (this.m_currentMission.brawlMode == TavernBrawlMode.TB_MODE_HEROIC)
			{
				this.m_TavernBrawlHeadline.Text = GameStrings.Get("GLOBAL_HEROIC_BRAWL");
			}
			else
			{
				this.m_TavernBrawlHeadline.Text = GameStrings.Get("GLOBAL_BRAWLISEUM");
			}
		}
		else
		{
			if (this.m_normalWinLocationBone != null)
			{
				this.m_winsBanner.transform.position = this.m_normalWinLocationBone.transform.position;
			}
			if (this.m_lossMarks != null)
			{
				this.m_lossesRoot.SetActive(false);
			}
			this.m_TavernBrawlHeadline.Text = GameStrings.Get("GLOBAL_TAVERN_BRAWL");
		}
		if (this.m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
		{
			this.m_TavernBrawlHeadline.Text = GameStrings.Get("GLOBAL_FIRESIDE_BRAWL");
		}
		if (DemoMgr.Get().IsExpoDemo())
		{
			string str = Vars.Key("Demo.Header").GetStr("");
			if (!string.IsNullOrEmpty(str))
			{
				this.m_TavernBrawlHeadline.Text = str;
			}
		}
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(this.m_currentMission.missionId);
		if (record != null)
		{
			this.m_chalkboardHeader.Text = record.Name;
			this.m_chalkboardInfo.Text = ((UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(record.ShortDescription)) ? record.ShortDescription : record.Description);
		}
		this.LoadChalkboardTexture();
		base.CancelInvoke("UpdateTimeText");
		base.InvokeRepeating("UpdateTimeText", 0.1f, 0.1f);
		this.UpdateTimeText();
	}

	// Token: 0x060067C4 RID: 26564 RVA: 0x0021D30C File Offset: 0x0021B50C
	private void RefreshTavernBrawlInfo_ConfirmEnded(AlertPopup.Response response, object userData)
	{
		if (TavernBrawlDisplay.s_instance == null)
		{
			return;
		}
		Navigation.Clear();
		this.ExitScene();
	}

	// Token: 0x060067C5 RID: 26565 RVA: 0x0021D328 File Offset: 0x0021B528
	private void SetUIForFriendlyChallenge(bool isTavernBrawlChallenge)
	{
		string key = "GLUE_BRAWL";
		bool flag = this.m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING;
		if (this.ShouldPlayButtonShowOpponentPickerTray())
		{
			key = "GLUE_CHOOSE_OPPONENT";
		}
		else if (flag)
		{
			key = "GLUE_BRAWL";
		}
		else if (TavernBrawlManager.Get().SelectHeroBeforeMission())
		{
			key = "GLUE_CHOOSE";
		}
		else if (isTavernBrawlChallenge && !DemoMgr.Get().IsExpoDemo())
		{
			key = "GLUE_BRAWL_FRIEND";
		}
		this.m_playButton.SetText(GameStrings.Get(key));
		bool flag2 = true;
		if (this.m_rewardChestDeprecated)
		{
			flag2 = false;
		}
		else
		{
			if (isTavernBrawlChallenge)
			{
				flag2 = false;
				NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
				if (netObject != null && netObject.FriendWeekAllowsTavernBrawlRecordUpdate && SpecialEventManager.Get().IsEventActive(SpecialEventType.FRIEND_WEEK, false))
				{
					flag2 = true;
				}
			}
			flag2 = (flag2 && this.m_currentMission.rewardType != RewardType.REWARD_CHEST && this.m_currentMission.rewardType != RewardType.REWARD_NONE);
			if (DemoMgr.Get().IsDemo())
			{
				flag2 = false;
			}
		}
		this.m_rewardChest.gameObject.SetActive(flag2);
		this.m_winsBanner.SetActive(!isTavernBrawlChallenge && !flag);
		if (this.m_lossMarks != null)
		{
			this.m_lossMarks.gameObject.SetActive(!isTavernBrawlChallenge);
		}
		if (this.m_editDeckButton != null)
		{
			if (this.m_originalEditTextColor == null)
			{
				this.m_originalEditTextColor = new Color?(this.m_editText.TextColor);
			}
			if (isTavernBrawlChallenge)
			{
				this.m_editText.TextColor = this.m_disabledTextColor;
				this.m_editDeckButton.SetEnabled(false, false);
			}
			else
			{
				this.m_editText.TextColor = this.m_originalEditTextColor.Value;
				this.m_editDeckButton.SetEnabled(true, false);
			}
			if (this.m_editIcon != null)
			{
				Material material = this.m_editIcon.GetComponent<Renderer>().GetMaterial();
				if (this.m_originalEditIconColor == null)
				{
					this.m_originalEditIconColor = new Color?(material.color);
				}
				if (isTavernBrawlChallenge)
				{
					material.color = this.m_disabledTextColor;
					return;
				}
				material.color = this.m_originalEditIconColor.Value;
			}
		}
	}

	// Token: 0x060067C6 RID: 26566 RVA: 0x0021D538 File Offset: 0x0021B738
	private void LoadChalkboardTexture()
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(this.m_currentMission.missionId);
		if (this.m_chalkboard != null && this.m_chalkboard.GetComponent<MeshRenderer>() != null && this.m_chalkboard.GetComponent<MeshRenderer>().GetMaterial() != null)
		{
			Material material = this.m_chalkboard.GetComponent<MeshRenderer>().GetMaterial();
			string text = null;
			UnityEngine.Vector2 value = UnityEngine.Vector2.zero;
			if (record != null)
			{
				if (!string.IsNullOrEmpty(record.ScriptObject))
				{
					AssetReference assetReference = new AssetReference(record.ScriptObject);
					using (AssetHandle<ScenarioData> assetHandle = AssetLoader.Get().LoadAsset<ScenarioData>(assetReference, AssetLoadingOptions.None))
					{
						if (assetHandle == null)
						{
							Debug.LogErrorFormat("Pointing to {0} but unable to load.  Rebuilding RAD will fix.", new object[]
							{
								assetReference.ToString()
							});
						}
						else if (PlatformSettings.Screen == ScreenCategory.Phone)
						{
							text = assetHandle.Asset.m_Texture_Phone;
							value.y = assetHandle.Asset.m_Texture_Phone_offsetY;
						}
						else
						{
							text = assetHandle.Asset.m_Texture;
						}
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = record.TbTexture;
					if (PlatformSettings.Screen == ScreenCategory.Phone)
					{
						text = record.TbTexturePhone;
						value.y = (float)record.TbTexturePhoneOffsetY;
					}
				}
				this.m_chalkboardTexture = (string.IsNullOrEmpty(text) ? null : AssetLoader.Get().LoadTexture(text, false, false));
			}
			if (this.m_chalkboardTexture == null)
			{
				bool canCreateDeck = this.m_currentMission.canCreateDeck;
				text = (canCreateDeck ? TavernBrawlDisplay.DEFAULT_CHALKBOARD_TEXTURE_NAME_WITH_DECK : TavernBrawlDisplay.DEFAULT_CHALKBOARD_TEXTURE_NAME_NO_DECK);
				value = (canCreateDeck ? TavernBrawlDisplay.DEFAULT_CHALKBOARD_TEXTURE_OFFSET_WITH_DECK : TavernBrawlDisplay.DEFAULT_CHALKBOARD_TEXTURE_OFFSET_NO_DECK);
				this.m_chalkboardTexture = AssetLoader.Get().LoadTexture(text, false, false);
			}
			if (this.m_chalkboardTexture != null)
			{
				material.SetTexture("_TopTex", this.m_chalkboardTexture);
				material.SetTextureOffset("_MainTex", value);
			}
		}
	}

	// Token: 0x060067C7 RID: 26567 RVA: 0x0021D734 File Offset: 0x0021B934
	private void UpdateChalkboardVisual(float animDelay)
	{
		if (this.m_chalkboardTexture == null)
		{
			this.LoadChalkboardTexture();
		}
		base.StartCoroutine(this.WaitThenPlayWipeAnim(this.m_doFirstSeenAnimations ? animDelay : 0f));
	}

	// Token: 0x060067C8 RID: 26568 RVA: 0x0021D768 File Offset: 0x0021B968
	private void UpdateTimeText()
	{
		if (DemoMgr.Get().IsExpoDemo())
		{
			return;
		}
		if (this.m_currentMission != null && this.m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
		{
			this.m_chalkboardEndInfo.Text = "";
			return;
		}
		string endingTimeText = TavernBrawlManager.Get().EndingTimeText;
		if (endingTimeText == null)
		{
			base.CancelInvoke("UpdateTimeText");
			return;
		}
		this.m_chalkboardEndInfo.Text = endingTimeText;
	}

	// Token: 0x060067C9 RID: 26569 RVA: 0x0021D7D0 File Offset: 0x0021B9D0
	private void UpdateRecordUI()
	{
		this.m_numWins.Text = TavernBrawlManager.Get().GamesWon.ToString();
		if (this.m_currentMission.IsSessionBased)
		{
			int gamesLost = TavernBrawlManager.Get().GamesLost;
			this.m_lossMarks.SetNumMarked(gamesLost);
			return;
		}
		if (TavernBrawlManager.Get().RewardProgress >= this.m_currentMission.RewardTriggerQuota)
		{
			this.m_rewardChest.GetComponent<Renderer>().SetMaterial(this.m_chestOpenMaterial);
			this.m_rewardHighlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
			this.m_rewardChest.SetEnabled(false, false);
		}
	}

	// Token: 0x060067CA RID: 26570 RVA: 0x0021D867 File Offset: 0x0021BA67
	private IEnumerator DoFirstTimeHeroicIntro()
	{
		Box.Get().SetToIgnoreFullScreenEffects(true);
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.DisablePlayButton();
		}
		string text = GameStrings.Get("GLUE_HEROIC_BRAWL_INTRO");
		while (SceneMgr.Get().IsTransitioning())
		{
			yield return 0;
		}
		if (!BannerManager.Get().ShowBanner(TavernBrawlDisplay.HEROIC_BRAWL_DIFFICULTY_WARNING_POPUP, null, text, new BannerManager.DelOnCloseBanner(TavernBrawlDisplay.OnFirstTimeIntroClosed), new Action<BannerPopup>(TavernBrawlDisplay.OnFirstTimeIntroCreated)))
		{
			Log.TavernBrawl.PrintWarning("TavernBrawlManager.DoFirstTimeHeroicIntro: First time popup failed to show.", Array.Empty<object>());
			this.ExitScene();
		}
		yield break;
	}

	// Token: 0x060067CB RID: 26571 RVA: 0x0021D878 File Offset: 0x0021BA78
	private static void OnFirstTimeIntroCreated(BannerPopup popup)
	{
		TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
		if (tavernBrawlDisplay == null)
		{
			return;
		}
		tavernBrawlDisplay.m_firstTimeIntroBanner = popup;
	}

	// Token: 0x060067CC RID: 26572 RVA: 0x0021D89C File Offset: 0x0021BA9C
	private static void OnFirstTimeIntroClosed()
	{
		Box.Get().SetToIgnoreFullScreenEffects(false);
		TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
		if (tavernBrawlDisplay == null)
		{
			return;
		}
		tavernBrawlDisplay.m_firstTimeIntroBanner = null;
		tavernBrawlDisplay.m_firstTimeIntroductionPopupShowing = false;
		Options.Get().SetBool(Option.HAS_SEEN_HEROIC_BRAWL, true);
		if (!SceneMgr.Get().IsInTavernBrawlMode())
		{
			return;
		}
		tavernBrawlDisplay.RefreshDataBasedUI(0f);
	}

	// Token: 0x060067CD RID: 26573 RVA: 0x0021D8FC File Offset: 0x0021BAFC
	private void RegisterListeners()
	{
		SceneMgr.Get().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		CollectionManager.Get().RegisterDeckCreatedListener(new CollectionManager.DelOnDeckCreated(this.OnDeckCreated));
		CollectionManager.Get().RegisterDeckDeletedListener(new CollectionManager.DelOnDeckDeleted(this.OnDeckDeleted));
		CollectionManager.Get().RegisterDeckContentsListener(new CollectionManager.DelOnDeckContents(this.OnDeckContents));
		CollectionManager.Get().RegisterCollectionChangedListener(new CollectionManager.DelOnCollectionChanged(this.OnCollectionChanged));
		FriendChallengeMgr.Get().AddChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		TavernBrawlManager.Get().OnTavernBrawlUpdated += this.OnTavernBrawlUpdated;
		FiresideGatheringManager.Get().OnLeaveFSG += this.OnLeaveFSG;
		if ((this.m_currentMission == null || !this.m_currentMission.canEditDeck) && SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		}
	}

	// Token: 0x060067CE RID: 26574 RVA: 0x0021DA04 File Offset: 0x0021BC04
	private void UnregisterListeners()
	{
		SceneMgr.UnregisterScenePreUnloadEventFromInstance(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager != null)
		{
			collectionManager.RemoveDeckCreatedListener(new CollectionManager.DelOnDeckCreated(this.OnDeckCreated));
			collectionManager.RemoveDeckDeletedListener(new CollectionManager.DelOnDeckDeleted(this.OnDeckDeleted));
			collectionManager.RemoveDeckContentsListener(new CollectionManager.DelOnDeckContents(this.OnDeckContents));
			collectionManager.RemoveCollectionChangedListener(new CollectionManager.DelOnCollectionChanged(this.OnCollectionChanged));
		}
		FriendChallengeMgr.RemoveChangedListenerFromInstance(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged), null);
		GameMgr gameMgr = GameMgr.Get();
		if (gameMgr != null)
		{
			gameMgr.UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		}
		if (TavernBrawlManager.Get() != null)
		{
			TavernBrawlManager.Get().OnTavernBrawlUpdated -= this.OnTavernBrawlUpdated;
		}
		if (FiresideGatheringManager.Get() != null)
		{
			FiresideGatheringManager.Get().OnLeaveFSG -= this.OnLeaveFSG;
		}
	}

	// Token: 0x060067CF RID: 26575 RVA: 0x0021DAE4 File Offset: 0x0021BCE4
	private void Start_ShowAttentionGrabbers()
	{
		if (this.m_currentMission == null)
		{
			return;
		}
		bool flag = UserAttentionManager.CanShowAttentionGrabber("TavernBrawlDisplay.Show");
		int latestSeenTavernBrawlChalkboard = TavernBrawlManager.Get().LatestSeenTavernBrawlChalkboard;
		if (latestSeenTavernBrawlChalkboard == 0)
		{
			this.m_doFirstSeenAnimations = true;
			if (flag && !NotificationManager.Get().HasSoundPlayedThisSession("VO_INNKEEPER_TAVERNBRAWL_WELCOME1_27.prefab:094070b7fecad8548b0b8fdb02bde052") && this.m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_TAVERN_BRAWL)
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_TAVERNBRAWL_WELCOME1_27"), "VO_INNKEEPER_TAVERNBRAWL_WELCOME1_27.prefab:094070b7fecad8548b0b8fdb02bde052", 0f, null, false);
				NotificationManager.Get().ForceAddSoundToPlayedList("VO_INNKEEPER_TAVERNBRAWL_WELCOME1_27.prefab:094070b7fecad8548b0b8fdb02bde052");
			}
		}
		else if (latestSeenTavernBrawlChalkboard < this.m_currentMission.seasonId)
		{
			this.m_doFirstSeenAnimations = true;
			if (this.m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_TAVERN_BRAWL)
			{
				int num = Options.Get().GetInt(Option.TIMES_SEEN_TAVERNBRAWL_CRAZY_RULES_QUOTE);
				if (flag && !NotificationManager.Get().HasSoundPlayedThisSession("VO_INNKEEPER_TAVERNBRAWL_DESC2_30.prefab:498657df8d08bc1468bfd1ad9f74ccac") && num < 3)
				{
					NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_TAVERNBRAWL_DESC2_30"), "VO_INNKEEPER_TAVERNBRAWL_DESC2_30.prefab:498657df8d08bc1468bfd1ad9f74ccac", 0f, null, false);
					NotificationManager.Get().ForceAddSoundToPlayedList("VO_INNKEEPER_TAVERNBRAWL_DESC2_30.prefab:498657df8d08bc1468bfd1ad9f74ccac");
					num++;
					Options.Get().SetInt(Option.TIMES_SEEN_TAVERNBRAWL_CRAZY_RULES_QUOTE, num);
				}
			}
		}
		if (flag && latestSeenTavernBrawlChalkboard != this.m_currentMission.seasonId)
		{
			TavernBrawlManager.Get().LatestSeenTavernBrawlChalkboard = this.m_currentMission.seasonId;
		}
	}

	// Token: 0x060067D0 RID: 26576 RVA: 0x0021DC34 File Offset: 0x0021BE34
	private void OnTavernBrawlUpdated()
	{
		this.m_currentMission = TavernBrawlManager.Get().CurrentMission();
		this.RefreshDataBasedUI(0f);
	}

	// Token: 0x060067D1 RID: 26577 RVA: 0x0021DC51 File Offset: 0x0021BE51
	private void OnLeaveFSG(FSGConfig fsg)
	{
		this.RefreshDataBasedUI(0f);
	}

	// Token: 0x060067D2 RID: 26578 RVA: 0x0021DC5E File Offset: 0x0021BE5E
	private IEnumerator ShowPurchaseScreen()
	{
		if (TavernBrawlManager.Get().CurrentTavernBrawlSeasonNewSessionsClosedInSeconds <= 0L)
		{
			Log.TavernBrawl.Print("TavernBrawlManager.ShowPurchaseScreen: New sessions in this season closed! Kicking out of TB", Array.Empty<object>());
			StoreManager.Get().HideStore(ShopType.TAVERN_BRAWL_STORE);
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			}
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SIGNUPS_CLOSED_TITLE");
			popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_SIGNUPS_CLOSED");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			DialogManager.Get().ShowPopup(popupInfo);
			yield break;
		}
		if (this.m_currentlyShowingMode == TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED)
		{
			yield break;
		}
		this.m_currentlyShowingMode = TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED;
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.DisablePlayButton();
		}
		while (SceneMgr.Get().IsTransitioning())
		{
			yield return 0;
		}
		if (this.m_currentlyShowingMode != TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED)
		{
			yield break;
		}
		StoreManager.Get().StartTavernBrawlTransaction(new Store.ExitCallback(this.OnStoreBackButtonPressed), false);
		yield break;
	}

	// Token: 0x060067D3 RID: 26579 RVA: 0x0021DC70 File Offset: 0x0021BE70
	private void ShowActiveScreen(float animDelay)
	{
		if (this.m_currentlyShowingMode == TavernBrawlStatus.TB_STATUS_ACTIVE)
		{
			return;
		}
		this.m_currentlyShowingMode = TavernBrawlStatus.TB_STATUS_ACTIVE;
		this.Start_ShowAttentionGrabbers();
		this.UpdateChalkboardVisual(animDelay);
		this.UpdateDeckUI(false);
		if (this.m_currentMission.IsDungeonRun)
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(this.m_currentMission.missionId);
			AdventureDbId adventureId = (AdventureDbId)record.AdventureId;
			AdventureModeDbId adventureModeId = (AdventureModeDbId)record.ModeId;
			this.m_adventureDefCache.LoadDefForId(adventureId);
			this.m_adventureWingDefCache.LoadDefForId((WingDbId)record.WingId);
			if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY)
			{
				DungeonCrawlUtil.ClearDungeonRunServerData(adventureId, adventureModeId);
			}
			if (DungeonCrawlUtil.IsDungeonRunDataReady(adventureId, adventureModeId))
			{
				this.StartDungeonRunIfInProgress(adventureId, adventureModeId);
				return;
			}
			this.DisablePlayButton();
			DungeonCrawlUtil.LoadDungeonRunData(adventureId, adventureModeId, delegate(bool success)
			{
				if (this.gameObject == null)
				{
					return;
				}
				this.EnablePlayButton();
				if (success)
				{
					AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)adventureModeId);
					if (adventureDataRecord != null)
					{
						DungeonCrawlUtil.MigrateDungeonCrawlSubkeys((GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey, (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey);
					}
					this.StartDungeonRunIfInProgress(adventureId, adventureModeId);
				}
			});
		}
	}

	// Token: 0x060067D4 RID: 26580 RVA: 0x0021DD78 File Offset: 0x0021BF78
	private void StartDungeonRunIfInProgress(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (DungeonCrawlUtil.IsDungeonRunInProgress(adventureId, modeId))
		{
			this.StartDungeonRun();
		}
	}

	// Token: 0x060067D5 RID: 26581 RVA: 0x0021DD89 File Offset: 0x0021BF89
	private IEnumerator ShowRewardsScreen()
	{
		if (this.m_currentlyShowingMode == TavernBrawlStatus.TB_STATUS_IN_REWARDS)
		{
			yield break;
		}
		this.m_currentlyShowingMode = TavernBrawlStatus.TB_STATUS_IN_REWARDS;
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.DisablePlayButton();
		}
		if (!TavernBrawlManager.Get().CurrentSession.HasChest)
		{
			Log.TavernBrawl.PrintError("TavernBrawlManager.ShowHeroicRewardsScreen: Server said we're in rewards but no rewards were specified!", Array.Empty<object>());
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			}
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			if (this.m_currentMission != null && this.m_currentMission.brawlMode == TavernBrawlMode.TB_MODE_HEROIC)
			{
				popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR_TITLE");
				popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR");
			}
			else
			{
				popupInfo.m_headerText = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR_TITLE");
				popupInfo.m_text = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR");
			}
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			DialogManager.Get().ShowPopup(popupInfo);
			yield break;
		}
		while (SceneMgr.Get().IsTransitioning())
		{
			yield return null;
		}
		if (this.m_PhoneDeckTrayView != null)
		{
			this.m_PhoneDeckTrayView.gameObject.GetComponent<SlidingTray>().HideTray();
		}
		Transform rewardBone = (TavernBrawlManager.Get().CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_HEROIC) ? this.m_rewardBoxesBone.transform : this.m_sessionRewardBoxesBone.transform;
		RewardUtils.ShowTavernBrawlRewards(TavernBrawlManager.Get().GamesWon, TavernBrawlManager.Get().CurrentSessionRewards, rewardBone, new Action(this.OnRewardsDone), false, null);
		yield break;
	}

	// Token: 0x060067D6 RID: 26582 RVA: 0x0021DD98 File Offset: 0x0021BF98
	private void OnRewardsDone()
	{
		if (this == null || base.gameObject == null)
		{
			return;
		}
		Network.Get().AckTavernBrawlSessionRewards();
		this.OnOpenRewardsComplete();
	}

	// Token: 0x060067D7 RID: 26583 RVA: 0x0021DDC2 File Offset: 0x0021BFC2
	public void OnOpenRewardsComplete()
	{
		this.ExitScene();
	}

	// Token: 0x060067D8 RID: 26584 RVA: 0x0021DDCC File Offset: 0x0021BFCC
	private void ExitScene()
	{
		this.m_tavernBrawlTray.m_animateBounce = false;
		this.m_tavernBrawlTray.ShowTray();
		GameMgr.Get().CancelFindGame();
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.DisablePlayButton();
		}
		StoreManager.Get().HideStore(ShopType.TAVERN_BRAWL_STORE);
		SceneMgr.Mode mode = (FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.NONE) ? SceneMgr.Mode.HUB : SceneMgr.Mode.FIRESIDE_GATHERING;
		SceneMgr.Get().SetNextMode(mode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x060067D9 RID: 26585 RVA: 0x0021DE37 File Offset: 0x0021C037
	private void UpdateDeckUI(bool animate)
	{
		this.UpdateDeckPanels(animate);
		this.ValidateDeck();
	}

	// Token: 0x060067DA RID: 26586 RVA: 0x0021DE46 File Offset: 0x0021C046
	private bool OnNavigateBack()
	{
		if (this.m_backButton != null && !this.m_backButton.IsEnabled())
		{
			return false;
		}
		this.ExitScene();
		return true;
	}

	// Token: 0x060067DB RID: 26587 RVA: 0x0021DE6C File Offset: 0x0021C06C
	private void OnBackButton()
	{
		if (!this.m_backButton.IsEnabled())
		{
			return;
		}
		Navigation.GoBack();
	}

	// Token: 0x060067DC RID: 26588 RVA: 0x0021DDC2 File Offset: 0x0021BFC2
	private void OnStoreBackButtonPressed(bool authorizationBackButtonPressed, object userData)
	{
		this.ExitScene();
	}

	// Token: 0x060067DD RID: 26589 RVA: 0x0021DE82 File Offset: 0x0021C082
	private void RefreshStateBasedUI(bool animate)
	{
		this.UpdateDeckUI(animate);
	}

	// Token: 0x060067DE RID: 26590 RVA: 0x0021DE8C File Offset: 0x0021C08C
	private void UpdateEditOrCreate()
	{
		bool flag = this.m_currentMission != null && this.m_currentMission.canCreateDeck;
		bool flag2 = this.m_currentMission != null && this.m_currentMission.canEditDeck && !TavernBrawlManager.Get().IsDeckLocked;
		bool flag3 = TavernBrawlManager.Get().HasCreatedDeck();
		bool isDeckLocked = TavernBrawlManager.Get().IsDeckLocked;
		bool flag4 = UniversalInputManager.UsePhoneUI && isDeckLocked;
		bool flag5 = flag && !flag3;
		bool flag6 = flag4;
		bool active = flag2 && flag && flag3 && !flag6;
		if (this.m_viewDeckButton != null)
		{
			this.m_viewDeckButton.gameObject.SetActive(flag6);
		}
		if (this.m_editDeckButton != null)
		{
			this.m_editDeckButton.gameObject.SetActive(active);
			if (TavernBrawlManager.Get().IsDeckLocked)
			{
				if (UniversalInputManager.UsePhoneUI)
				{
					this.m_PhoneDeckTrayView.Initialize();
					this.InitializeDeckTrayManaCurve();
					this.LoadAndPositionPhoneDeckTrayHeroCard();
				}
				else
				{
					CollectionDeckTray.Get().m_cardsContent.UpdateDeckCompleteHighlight();
					if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && GameMgr.Get().WasTavernBrawl() && TavernBrawlManager.Get().GamesWon + TavernBrawlManager.Get().GamesLost == 1)
					{
						base.StartCoroutine(this.DoDeckTrayLockedAnimation());
					}
					else
					{
						this.ShowDeckTrayLocked();
					}
				}
			}
			if (this.m_editIcon != null)
			{
				this.m_editIcon.SetActive(true);
			}
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			if (this.m_createDeckButton != null)
			{
				this.m_createDeckButton.gameObject.SetActive(flag5);
			}
		}
		else
		{
			if (this.m_panelWithCreateDeck != null)
			{
				this.m_panelWithCreateDeck.SetActive(flag5);
			}
			if (this.m_fullPanel != null)
			{
				this.m_fullPanel.SetActive(!flag5);
			}
		}
		if (this.m_createDeckHighlight != null)
		{
			if (!this.m_createDeckHighlight.gameObject.activeInHierarchy && flag5)
			{
				Debug.LogWarning("Attempting to activate m_createDeckHighlight, but it is inactive! This will not behave correctly!");
			}
			this.m_createDeckHighlight.ChangeState(flag5 ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.HIGHLIGHT_OFF);
		}
	}

	// Token: 0x060067DF RID: 26591 RVA: 0x0021E0A0 File Offset: 0x0021C2A0
	private void LoadAndPositionPhoneDeckTrayHeroCard()
	{
		if (this.m_chosenHero != null)
		{
			return;
		}
		CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
		if (collectionDeck == null)
		{
			Log.TavernBrawl.PrintError("TavernBrawlManager.LoadAndPositionPhoneDeckTrayHeroCard: No deck found but trying to load the deck tray!", Array.Empty<object>());
			return;
		}
		string heroCardID = collectionDeck.HeroCardID;
		GameUtils.LoadAndPositionCardActor("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", heroCardID, collectionDeck.HeroPremium, new GameUtils.LoadActorCallback(this.OnHeroActorLoaded));
	}

	// Token: 0x060067E0 RID: 26592 RVA: 0x0021E104 File Offset: 0x0021C304
	private void OnHeroActorLoaded(Actor actor)
	{
		this.m_chosenHero = actor;
		this.m_chosenHero.transform.parent = this.m_SocketHeroBone.transform;
		this.m_chosenHero.transform.localPosition = Vector3.zero;
		this.m_chosenHero.transform.localScale = Vector3.one;
	}

	// Token: 0x060067E1 RID: 26593 RVA: 0x0021E15D File Offset: 0x0021C35D
	private IEnumerator DoDeckTrayLockedAnimation()
	{
		while (SceneMgr.Get().IsTransitioning())
		{
			yield return 0;
		}
		yield return new WaitForSeconds(1.5f);
		this.ShowDeckTrayLocked();
		yield break;
	}

	// Token: 0x060067E2 RID: 26594 RVA: 0x0021E16C File Offset: 0x0021C36C
	private void ShowDeckTrayLocked()
	{
		this.m_LockedDeckTray.enabled = true;
		this.m_LockedDeckTooltipZone.GetComponent<BoxCollider>().enabled = true;
	}

	// Token: 0x060067E3 RID: 26595 RVA: 0x0021E18C File Offset: 0x0021C38C
	private void InitializeDeckTrayManaCurve()
	{
		CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
		if (collectionDeck == null)
		{
			return;
		}
		foreach (CollectionDeckSlot collectionDeckSlot in collectionDeck.GetSlots())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(collectionDeckSlot.CardID);
			for (int i = 0; i < collectionDeckSlot.Count; i++)
			{
				this.AddCardToManaCurve(entityDef);
			}
		}
	}

	// Token: 0x060067E4 RID: 26596 RVA: 0x0021E214 File Offset: 0x0021C414
	public void AddCardToManaCurve(EntityDef entityDef)
	{
		if (this.m_ManaCurvePhone == null)
		{
			Debug.LogWarning(string.Format("TavernBrawlDisplay.AddCardToManaCurve({0}) - m_manaCurve is null", entityDef));
			return;
		}
		this.m_ManaCurvePhone.AddCardToManaCurve(entityDef);
	}

	// Token: 0x060067E5 RID: 26597 RVA: 0x0021E241 File Offset: 0x0021C441
	private void UpdateDeckPanels(bool animate = true)
	{
		this.UpdateDeckPanels(this.m_currentMission != null && this.m_currentMission.canCreateDeck && TavernBrawlManager.Get().HasCreatedDeck(), animate);
	}

	// Token: 0x060067E6 RID: 26598 RVA: 0x0021E26C File Offset: 0x0021C46C
	private void UpdateDeckPanels(bool hasDeck, bool animate)
	{
		if (this.m_cardListPanel != null)
		{
			bool flag = !hasDeck;
			if (animate && !flag)
			{
				this.m_createDeckButton.gameObject.SetActive(false);
				this.m_createDeckHighlight.gameObject.SetActive(false);
			}
			else if (flag)
			{
				this.m_createDeckButton.gameObject.SetActive(true);
				this.m_createDeckHighlight.gameObject.SetActive(true);
			}
			this.m_cardListPanel.ToggleTraySlider(flag, null, animate);
		}
		if (this.m_cardCountPanelAnim != null && this.m_cardCountPanelAnimOpen != hasDeck)
		{
			this.m_cardCountPanelAnim.Play(hasDeck ? this.CARD_COUNT_PANEL_OPEN_ANIM : this.CARD_COUNT_PANEL_CLOSE_ANIM);
			this.m_cardCountPanelAnimOpen = hasDeck;
		}
	}

	// Token: 0x060067E7 RID: 26599 RVA: 0x0021E324 File Offset: 0x0021C524
	private void CreateDeck()
	{
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.TAVERN_BRAWL_DECKEDITOR
		});
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.EnterSelectNewDeckHeroMode();
		}
		this.HideChalkboardFX();
	}

	// Token: 0x060067E8 RID: 26600 RVA: 0x0021E371 File Offset: 0x0021C571
	private void EditDeckButton_OnRelease(UIEvent e)
	{
		if (this.IsInDeckEditMode())
		{
			this.OnDeleteButtonPressed();
			return;
		}
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.TAVERN_BRAWL_DECKEDITOR
		});
		this.SwitchToEditDeckMode(TavernBrawlManager.Get().CurrentDeck());
	}

	// Token: 0x060067E9 RID: 26601 RVA: 0x0021E3AE File Offset: 0x0021C5AE
	private void ViewDeckButton_OnRelease(UIEvent e)
	{
		this.m_PhoneDeckTrayView.gameObject.GetComponent<SlidingTray>().ShowTray();
	}

	// Token: 0x060067EA RID: 26602 RVA: 0x0021E3C8 File Offset: 0x0021C5C8
	private bool SwitchToEditDeckMode(CollectionDeck deck)
	{
		if (CollectionManager.Get().GetCollectibleDisplay() == null || deck == null)
		{
			return false;
		}
		this.m_tavernBrawlTray.HideTray();
		this.UpdateDeckPanels(true, true);
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_editDeckButton.gameObject.SetActive(this.m_currentMission.canEditDeck);
			this.m_editDeckButton.SetText(GameStrings.Get("GLUE_COLLECTION_DECK_DELETE"));
			if (this.m_editIcon != null)
			{
				this.m_editIcon.SetActive(false);
			}
			if (this.m_deleteIcon != null)
			{
				this.m_deleteIcon.SetActive(true);
			}
			this.m_editDeckHighlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
		this.m_deckBeingEdited = deck.ID;
		BnetBar.Get().RefreshCurrency();
		CollectionDeckTray.Get().EnterEditDeckModeForTavernBrawl(deck);
		FriendChallengeMgr.Get().UpdateMyAvailability();
		return true;
	}

	// Token: 0x060067EB RID: 26603 RVA: 0x0021E4AC File Offset: 0x0021C6AC
	private void ShowNonSessionRewardPreview(UIEvent e)
	{
		if (this.m_currentMission == null)
		{
			return;
		}
		RewardType rewardType = this.m_currentMission.rewardType;
		if (rewardType != RewardType.REWARD_BOOSTER_PACKS)
		{
			if (rewardType != RewardType.REWARD_CARD_BACK)
			{
				Debug.LogErrorFormat("Tavern Brawl reward type currently not supported! Add type {0} to TaverBrawlDisplay.ShowReward().", new object[]
				{
					this.m_currentMission.rewardType
				});
				return;
			}
			if (this.m_rewardObject == null)
			{
				int num = (int)this.m_currentMission.RewardData1;
				CardBackManager.LoadCardBackData loadCardBackData = CardBackManager.Get().LoadCardBackByIndex(num, false, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", true);
				if (loadCardBackData == null)
				{
					Debug.LogErrorFormat("TavernBrawlDisplay.ShowReward() - Could not load cardback ID {0}!", new object[]
					{
						num
					});
					return;
				}
				this.m_rewardObject = loadCardBackData.m_GameObject;
				GameUtils.SetParent(this.m_rewardObject, this.m_rewardContainer, false);
				this.m_rewardObject.transform.localScale = Vector3.one * 5.92f;
			}
		}
		else if (this.m_rewardObject == null)
		{
			int num2 = (int)this.m_currentMission.RewardData2;
			BoosterDbfRecord record = GameDbf.Booster.GetRecord(num2);
			if (record == null)
			{
				Debug.LogErrorFormat("TavernBrawlDisplay.ShowReward() - no record found for booster {0}!", new object[]
				{
					num2
				});
				return;
			}
			string packOpeningPrefab = record.PackOpeningPrefab;
			if (string.IsNullOrEmpty(packOpeningPrefab))
			{
				Debug.LogErrorFormat("TavernBrawlDisplay.ShowReward() - no prefab found for booster {0}!", new object[]
				{
					num2
				});
				return;
			}
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(packOpeningPrefab, AssetLoadingOptions.IgnorePrefabPosition);
			if (gameObject == null)
			{
				Debug.LogError(string.Format("TavernBrawlDisplay.ShowReward() - failed to load prefab {0} for booster {1}!", packOpeningPrefab, num2));
				return;
			}
			this.m_rewardObject = gameObject;
			UnopenedPack component = gameObject.GetComponent<UnopenedPack>();
			if (component == null)
			{
				Debug.LogError(string.Format("TavernBrawlDisplay.ShowReward() - No UnopenedPack script found on prefab {0} for booster {1}!", packOpeningPrefab, num2));
				return;
			}
			GameUtils.SetParent(this.m_rewardObject, this.m_rewardContainer, false);
			component.SetBoosterStack(new NetCache.BoosterStack
			{
				Id = (int)this.m_currentMission.RewardData2,
				Count = (int)this.m_currentMission.RewardData1
			});
		}
		this.m_rewardsPreview.SetActive(true);
		iTween.Stop(this.m_rewardsPreview);
		iTween.ScaleTo(this.m_rewardsPreview, iTween.Hash(new object[]
		{
			"scale",
			this.m_rewardsScale,
			"time",
			0.15f
		}));
	}

	// Token: 0x060067EC RID: 26604 RVA: 0x0021E710 File Offset: 0x0021C910
	private void HideNonSessionRewardPreview(UIEvent e)
	{
		iTween.Stop(this.m_rewardsPreview);
		iTween.ScaleTo(this.m_rewardsPreview, iTween.Hash(new object[]
		{
			"scale",
			Vector3.one * 0.01f,
			"time",
			0.15f,
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.m_rewardsPreview.SetActive(false);
			})
		}));
	}

	// Token: 0x060067ED RID: 26605 RVA: 0x0021E78C File Offset: 0x0021C98C
	private void StartDungeonRun()
	{
		this.DisablePlayButton();
		ScenarioDbfRecord scen = GameDbf.Scenario.GetRecord(this.m_currentMission.missionId);
		if (scen == null)
		{
			return;
		}
		DungeonCrawlUtil.LoadDungeonRunPrefab(delegate(GameObject go)
		{
			DungeonCrawlServices dungeonCrawlServices = DungeonCrawlUtil.CreateTavernBrawlDungeonCrawlServices((AdventureDbId)scen.AdventureId, (AdventureModeDbId)scen.ModeId, this.m_assetLoadingHelper);
			AdventureDungeonCrawlDisplay component = go.GetComponent<AdventureDungeonCrawlDisplay>();
			if (component)
			{
				this.m_dungeonCrawlServices = dungeonCrawlServices;
				this.m_dungeonCrawlDisplay = component;
				GameUtils.SetParent(go, this.transform, false);
				go.transform.position = new Vector3(-500f, 0f, 0f);
				component.StartRun(dungeonCrawlServices);
			}
		});
	}

	// Token: 0x060067EE RID: 26606 RVA: 0x0021E7E4 File Offset: 0x0021C9E4
	private void PlayButton_OnRelease(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			DialogManager.Get().ShowReconnectHelperDialog(delegate
			{
				this.PlayButton_OnRelease(e);
			}, null);
			return;
		}
		if (this.m_currentMission == null)
		{
			this.RefreshDataBasedUI(0f);
			return;
		}
		if (SetRotationManager.Get().CheckForSetRotationRollover())
		{
			return;
		}
		if (PlayerMigrationManager.Get() != null && PlayerMigrationManager.Get().CheckForPlayerMigrationRequired())
		{
			return;
		}
		if (this.ShouldPlayButtonShowOpponentPickerTray())
		{
			FiresideGatheringDisplay.Get().ShowOpponentPickerTray(new Action(this.EnablePlayButton));
			this.DisablePlayButton();
			return;
		}
		if (this.m_currentMission.IsSessionBased && this.m_currentMission.canEditDeck && !TavernBrawlManager.Get().IsDeckLocked)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_PLAY_CONFIRMATION_TITLE");
			popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_PLAY_CONFIRMATION");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_confirmText = GameStrings.Get("GLUE_HEROIC_BRAWL_PLAY_CONFIRMATION_OK");
			popupInfo.m_cancelText = GameStrings.Get("GLUE_HEROIC_BRAWL_PLAY_CONFIRMATION_CANCEL");
			popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnPlayButtonConfirmationResponse);
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		this.OnPlayButtonExecute();
	}

	// Token: 0x060067EF RID: 26607 RVA: 0x0021E91F File Offset: 0x0021CB1F
	private void OnPlayButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			return;
		}
		this.OnPlayButtonExecute();
	}

	// Token: 0x060067F0 RID: 26608 RVA: 0x0021E92C File Offset: 0x0021CB2C
	private void OnPlayButtonExecute()
	{
		if (this.m_currentMission.IsDungeonRun)
		{
			this.StartDungeonRun();
		}
		else if (TavernBrawlManager.Get().SelectHeroBeforeMission())
		{
			bool flag = false;
			bool flag2 = GuestHeroPickerDisplay.Get() != null || HeroPickerDisplay.Get() != null;
			if (!flag2)
			{
				flag = AssetLoader.Get().InstantiatePrefab(this.GetHeroPickerAssetStr(this.m_currentMission.missionId), delegate(AssetReference name, GameObject go, object data)
				{
					if (go == null)
					{
						Debug.LogError("Failed to load hero picker.");
						return;
					}
					PresenceMgr.Get().SetStatus(new Enum[]
					{
						Global.PresenceStatus.TAVERN_BRAWL_DECKEDITOR
					});
					this.HideChalkboardFX();
				}, null, AssetLoadingOptions.None);
			}
			if (!flag)
			{
				Log.All.PrintWarning("Failed to load hero picker.", Array.Empty<object>());
			}
			if (flag2)
			{
				Log.All.PrintWarning("Attempting to load HeroPickerDisplay a second time!", Array.Empty<object>());
				return;
			}
		}
		else if (this.m_currentMission.canCreateDeck)
		{
			if (!TavernBrawlManager.Get().HasValidDeckForCurrent())
			{
				Debug.LogError("Attempting to start a Tavern Brawl game without having a valid deck!");
				return;
			}
			CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
			if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
			{
				FriendChallengeMgr.Get().SelectDeck(collectionDeck.ID);
				FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_TAVERN_BRAWL_OPPONENT_WAITING_READY", new AlertPopup.ResponseCallback(this.OnFriendChallengeWaitingForOpponentDialogResponse));
			}
			else
			{
				TavernBrawlManager.Get().StartGame(collectionDeck.ID);
			}
		}
		else if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			FriendChallengeMgr.Get().SkipDeckSelection();
			FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_TAVERN_BRAWL_OPPONENT_WAITING_READY", new AlertPopup.ResponseCallback(this.OnFriendChallengeWaitingForOpponentDialogResponse));
		}
		else
		{
			TavernBrawlManager.Get().StartGame(0L);
		}
		this.DisablePlayButton();
		this.EnableBackButton(false);
	}

	// Token: 0x060067F1 RID: 26609 RVA: 0x0021EAA6 File Offset: 0x0021CCA6
	private string GetHeroPickerAssetStr(int scenarioId)
	{
		if (GameUtils.GetScenarioGuestHeroes(scenarioId).Count > 0)
		{
			return "GuestHeroPicker.prefab:3ecbc18da1de3ef4fa30532f90b20e59";
		}
		return "HeroPicker.prefab:59e2d2f899d09f4488a194df18967915";
	}

	// Token: 0x060067F2 RID: 26610 RVA: 0x0021EAC1 File Offset: 0x0021CCC1
	private bool ShouldPlayButtonShowOpponentPickerTray()
	{
		return (this.m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING || SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING) && !GameUtils.IsAIMission(this.m_currentMission.missionId) && !TavernBrawlManager.Get().SelectHeroBeforeMission();
	}

	// Token: 0x060067F3 RID: 26611 RVA: 0x0021EB00 File Offset: 0x0021CD00
	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (prevMode != SceneMgr.Mode.TAVERN_BRAWL && (prevMode != SceneMgr.Mode.FIRESIDE_GATHERING || !FiresideGatheringManager.Get().InBrawlMode()))
		{
			return;
		}
		if (this.m_firstTimeIntroBanner != null)
		{
			this.m_firstTimeIntroBanner.Close();
		}
	}

	// Token: 0x060067F4 RID: 26612 RVA: 0x0021EB38 File Offset: 0x0021CD38
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
			FriendChallengeMgr.Get().RemoveChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
			break;
		}
		return false;
	}

	// Token: 0x060067F5 RID: 26613 RVA: 0x0021EB9E File Offset: 0x0021CD9E
	private void HandleGameStartupFailure()
	{
		if (!TavernBrawlManager.Get().SelectHeroBeforeMission())
		{
			this.EnablePlayButton();
			this.EnableBackButton(true);
		}
	}

	// Token: 0x060067F6 RID: 26614 RVA: 0x0021EBBC File Offset: 0x0021CDBC
	private void OnDeleteButtonPressed()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_DESC");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnDeleteButtonConfirmationResponse);
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060067F7 RID: 26615 RVA: 0x0021EC28 File Offset: 0x0021CE28
	private void OnDeleteButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			return;
		}
		CollectionDeckTray.Get().DeleteEditingDeck(true);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.OnDoneEditingDeck();
		}
	}

	// Token: 0x060067F8 RID: 26616 RVA: 0x0021EC64 File Offset: 0x0021CE64
	private void OnDeckCreated(long deckID)
	{
		CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
		if (collectionDeck != null && deckID == collectionDeck.ID)
		{
			this.SwitchToEditDeckMode(collectionDeck);
		}
	}

	// Token: 0x060067F9 RID: 26617 RVA: 0x0021EC90 File Offset: 0x0021CE90
	private void OnDeckDeleted(CollectionDeck removedDeck)
	{
		if (removedDeck.ID == this.m_deckBeingEdited && TavernBrawlDisplay.IsTavernBrawlOpen())
		{
			base.StartCoroutine(this.WaitThenCreateDeck());
		}
	}

	// Token: 0x060067FA RID: 26618 RVA: 0x0021ECB4 File Offset: 0x0021CEB4
	private IEnumerator WaitThenPlayWipeAnim(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (this.m_chalkboard != null && TavernBrawlManager.Get().IsCurrentBrawlTypeActive && TavernBrawlManager.Get().IsCurrentBrawlAllDataReady)
		{
			this.m_chalkboard.GetComponent<PlayMakerFSM>().SendEvent(this.m_doFirstSeenAnimations ? "Wipe" : "QuickShow");
		}
		while (NotificationManager.Get().IsQuotePlaying)
		{
			yield return null;
		}
		if (this.m_doFirstSeenAnimations && this.m_currentMission.FirstTimeSeenCharacterDialogID > 0)
		{
			NarrativeManager.Get().PushDialogSequence(this.m_currentMission.FirstTimeSeenCharacterDialogSequence);
		}
		yield return new WaitForSeconds(1f);
		yield break;
	}

	// Token: 0x060067FB RID: 26619 RVA: 0x0021ECCA File Offset: 0x0021CECA
	private IEnumerator WaitThenCreateDeck()
	{
		yield return new WaitForEndOfFrame();
		this.CreateDeck();
		yield return new WaitForSeconds(0.4f);
		this.BackFromDeckEdit(false);
		yield break;
	}

	// Token: 0x060067FC RID: 26620 RVA: 0x0021ECD9 File Offset: 0x0021CED9
	private void OnCollectionChanged()
	{
		if (TavernBrawlDisplay.IsTavernBrawlViewing())
		{
			this.ValidateDeck();
		}
	}

	// Token: 0x060067FD RID: 26621 RVA: 0x0021ECE8 File Offset: 0x0021CEE8
	private void OnDeckContents(long deckID)
	{
		CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
		if (collectionDeck != null && deckID == collectionDeck.ID && TavernBrawlDisplay.IsTavernBrawlOpen())
		{
			this.ValidateDeck();
		}
	}

	// Token: 0x060067FE RID: 26622 RVA: 0x0021ED1C File Offset: 0x0021CF1C
	private void Awake_InitializeRewardDisplay()
	{
		if (this.m_rewardChestDeprecated)
		{
			return;
		}
		RewardType rewardType = (this.m_currentMission == null) ? RewardType.REWARD_UNKNOWN : this.m_currentMission.rewardType;
		RewardTrigger rewardTrigger = (this.m_currentMission == null) ? RewardTrigger.REWARD_TRIGGER_UNKNOWN : this.m_currentMission.rewardTrigger;
		string text = null;
		long num = 1L;
		if (rewardType != RewardType.REWARD_BOOSTER_PACKS)
		{
			if (rewardType == RewardType.REWARD_CARD_BACK)
			{
				if (rewardTrigger != RewardTrigger.REWARD_TRIGGER_WIN_GAME && rewardTrigger == RewardTrigger.REWARD_TRIGGER_FINISH_GAME)
				{
					text = "GLUE_TAVERN_BRAWL_REWARD_DESC_FINISH_CARDBACK";
				}
				else
				{
					text = "GLUE_TAVERN_BRAWL_REWARD_DESC_CARDBACK";
				}
			}
		}
		else
		{
			num = this.m_currentMission.RewardData1;
			if (rewardTrigger != RewardTrigger.REWARD_TRIGGER_WIN_GAME && rewardTrigger == RewardTrigger.REWARD_TRIGGER_FINISH_GAME)
			{
				text = "GLUE_TAVERN_BRAWL_REWARD_DESC_FINISH";
			}
			else
			{
				text = "GLUE_TAVERN_BRAWL_REWARD_DESC";
			}
		}
		if (text != null)
		{
			if (this.m_currentMission.RewardTriggerQuota != 1)
			{
				text += "_QUOTA";
			}
			if (num != 1L)
			{
				text += "_MULTIPLE_PACKS";
			}
			this.m_rewardsText.Text = GameStrings.Format(text, new object[]
			{
				num,
				this.m_currentMission.RewardTriggerQuota
			});
		}
		if (this.m_rewardOffClickCatcher != null)
		{
			this.m_rewardChest.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.ShowNonSessionRewardPreview));
			this.m_rewardOffClickCatcher.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.HideNonSessionRewardPreview));
		}
		else
		{
			this.m_rewardChest.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.ShowNonSessionRewardPreview));
			this.m_rewardChest.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.HideNonSessionRewardPreview));
		}
		this.m_rewardsScale = this.m_rewardsPreview.transform.localScale;
		this.m_rewardsPreview.transform.localScale = Vector3.one * 0.01f;
		if (this.m_currentMission != null && TavernBrawlManager.Get().RewardProgress < this.m_currentMission.RewardTriggerQuota)
		{
			this.m_rewardHighlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
	}

	// Token: 0x060067FF RID: 26623 RVA: 0x0021EEE0 File Offset: 0x0021D0E0
	private void SetupUniversalButtons()
	{
		if (this.m_editDeckButton != null)
		{
			this.m_editDeckButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.EditDeckButton_OnRelease));
		}
		if (this.m_createDeckButton != null)
		{
			this.m_createDeckButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.CreateDeck();
			});
		}
		if (this.m_backButton != null)
		{
			this.m_backButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.OnBackButton();
			});
		}
		if (this.m_LockedDeckTooltipTrigger != null && this.m_LockedDeckTooltipZone != null)
		{
			this.m_LockedDeckTooltipTrigger.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnLockedTooltipRollover));
			this.m_LockedDeckTooltipTrigger.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnLockedTooltipRollout));
		}
		if (this.m_viewDeckButton != null)
		{
			this.m_viewDeckButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ViewDeckButton_OnRelease));
		}
		this.m_playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.PlayButton_OnRelease));
		this.m_FiresideGatheringPlayButtonLantern.gameObject.SetActive(FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL);
		this.m_FiresideGatheringPlayButtonLantern.SetLanternLit(true);
	}

	// Token: 0x06006800 RID: 26624 RVA: 0x0021F019 File Offset: 0x0021D219
	private void OnLockedTooltipRollover(UIEvent e)
	{
		if (TavernBrawlManager.Get().IsDeckLocked)
		{
			this.m_LockedDeckTooltipZone.ShowLayerTooltip(GameStrings.Get("GLUE_LOCKED_DECK_TOOLTIP_TITLE"), GameStrings.Get("GLUE_LOCKED_DECK_TOOLTIP"), 0);
		}
	}

	// Token: 0x06006801 RID: 26625 RVA: 0x0021F048 File Offset: 0x0021D248
	private void OnLockedTooltipRollout(UIEvent e)
	{
		this.m_LockedDeckTooltipZone.HideTooltip();
	}

	// Token: 0x06006802 RID: 26626 RVA: 0x0021F058 File Offset: 0x0021D258
	private void DoDungeonRunTransition()
	{
		Vector3 localPosition = base.transform.localPosition;
		Vector3 localPosition2 = base.transform.localPosition;
		localPosition2.x -= this.m_transitionStartingOffset;
		this.m_dungeonCrawlDisplay.gameObject.transform.localPosition = localPosition2;
		Transform transform = base.transform.Find("Root");
		GameObject rootGo = transform.gameObject;
		Hashtable args = iTween.Hash(new object[]
		{
			"islocal",
			true,
			"position",
			localPosition,
			"time",
			this.m_transitionTime,
			"easeType",
			"easeOutBounce",
			"oncomplete",
			new Action<object>(delegate(object e)
			{
				this.m_dungeonCrawlServices.SubsceneController.OnTransitionComplete();
				rootGo.SetActive(false);
			}),
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_dungeonCrawlDisplay.gameObject, args);
		if (!string.IsNullOrEmpty(this.m_SlideInSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_SlideInSound);
		}
		Vector3 localPosition3 = transform.localPosition;
		localPosition3.y -= this.m_rootDropHeight;
		object[] array = new object[12];
		array[0] = "islocal";
		array[1] = true;
		array[2] = "position";
		array[3] = localPosition3;
		array[4] = "time";
		array[5] = this.m_transitionTime / 2f;
		array[6] = "easeType";
		array[7] = "easeOutBounce";
		array[8] = "oncomplete";
		array[9] = new Action<object>(delegate(object e)
		{
		});
		array[10] = "oncompletetarget";
		array[11] = rootGo;
		Hashtable args2 = iTween.Hash(array);
		iTween.MoveTo(rootGo, args2);
	}

	// Token: 0x06006803 RID: 26627 RVA: 0x0021F244 File Offset: 0x0021D444
	private void OnAssetLoadingComplete(object sender, EventArgs args)
	{
		if (this.m_dungeonCrawlServices != null && this.m_dungeonCrawlDisplay != null)
		{
			this.DoDungeonRunTransition();
		}
	}

	// Token: 0x06006804 RID: 26628 RVA: 0x0021F262 File Offset: 0x0021D462
	private void OnFriendChallengeWaitingForOpponentDialogResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			return;
		}
		if (FriendChallengeMgr.Get().AmIInGameState())
		{
			return;
		}
		FriendChallengeMgr.Get().DeselectDeckOrHero();
		FriendlyChallengeHelper.Get().StopWaitingForFriendChallenge();
		if (!TavernBrawlManager.Get().SelectHeroBeforeMission())
		{
			this.EnablePlayButton();
		}
	}

	// Token: 0x06006805 RID: 26629 RVA: 0x0021F29C File Offset: 0x0021D49C
	private void OnFriendChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		if (challengeEvent == FriendChallengeEvent.OPPONENT_ACCEPTED_CHALLENGE || challengeEvent == FriendChallengeEvent.I_ACCEPTED_CHALLENGE)
		{
			this.SetUIForFriendlyChallenge(true);
			return;
		}
		if (challengeEvent == FriendChallengeEvent.SELECTED_DECK_OR_HERO)
		{
			if (player == BnetPresenceMgr.Get().GetMyPlayer())
			{
				return;
			}
			if (!FriendChallengeMgr.Get().DidISelectDeckOrHero())
			{
				return;
			}
			FriendlyChallengeHelper.Get().HideFriendChallengeWaitingForOpponentDialog();
			return;
		}
		else
		{
			if (challengeEvent == FriendChallengeEvent.I_RESCINDED_CHALLENGE || challengeEvent == FriendChallengeEvent.OPPONENT_DECLINED_CHALLENGE || challengeEvent == FriendChallengeEvent.OPPONENT_RESCINDED_CHALLENGE)
			{
				this.SetUIForFriendlyChallenge(false);
				return;
			}
			if (challengeEvent == FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE || challengeEvent == FriendChallengeEvent.OPPONENT_REMOVED_FROM_FRIENDS || challengeEvent == FriendChallengeEvent.QUEUE_CANCELED)
			{
				this.SetUIForFriendlyChallenge(false);
				FriendlyChallengeHelper.Get().StopWaitingForFriendChallenge();
				return;
			}
			if (challengeEvent == FriendChallengeEvent.DESELECTED_DECK_OR_HERO)
			{
				if (player == BnetPresenceMgr.Get().GetMyPlayer())
				{
					return;
				}
				if (!TavernBrawlManager.Get().SelectHeroBeforeMission())
				{
					this.EnablePlayButton();
					return;
				}
				if (FriendChallengeMgr.Get().DidISelectDeckOrHero())
				{
					FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_OPPONENT_WAITING_DECK", new AlertPopup.ResponseCallback(this.OnFriendChallengeWaitingForOpponentDialogResponse));
				}
			}
			return;
		}
	}

	// Token: 0x06006806 RID: 26630 RVA: 0x0021F368 File Offset: 0x0021D568
	private void InitExpoDemoMode()
	{
		if (!DemoMgr.Get().IsExpoDemo())
		{
			return;
		}
		if (this.m_backButton != null)
		{
			this.m_backButton.Flip(false, false);
			this.m_backButton.SetEnabled(false, false);
		}
		this.m_chalkboardEndInfo.gameObject.SetActive(false);
		base.StartCoroutine("ShowDemoQuotes");
	}

	// Token: 0x06006807 RID: 26631 RVA: 0x0021F3C7 File Offset: 0x0021D5C7
	private IEnumerator ShowDemoQuotes()
	{
		yield return new WaitForSeconds(1f);
		string text = Vars.Key("Demo.ThankQuote").GetStr("");
		int @int = Vars.Key("Demo.ThankQuoteMsTime").GetInt(0);
		text = text.Replace("\\n", "\n");
		if (!string.IsNullOrEmpty(text) && @int > 0)
		{
			this.m_expoThankQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(138.3f, NotificationManager.DEPTH, 58.7f), text, "", (float)@int / 1000f, null, false);
			this.EnableClickBlocker(true);
			yield return new WaitForSeconds((float)@int / 1000f);
			this.EnableClickBlocker(false);
		}
		yield break;
	}

	// Token: 0x06006808 RID: 26632 RVA: 0x0021F3D8 File Offset: 0x0021D5D8
	private void EnableClickBlocker(bool enable)
	{
		if (this.m_clickBlocker == null)
		{
			return;
		}
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (enable)
		{
			fullScreenFXMgr.SetBlurBrightness(1f);
			fullScreenFXMgr.SetBlurDesaturation(0f);
			fullScreenFXMgr.Vignette();
			fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc, null);
		}
		else
		{
			fullScreenFXMgr.StopVignette();
			fullScreenFXMgr.StopBlur();
		}
		this.m_clickBlocker.gameObject.SetActive(enable);
	}

	// Token: 0x06006809 RID: 26633 RVA: 0x0021F44C File Offset: 0x0021D64C
	private void HideDemoQuotes()
	{
		DemoMgr demoMgr = DemoMgr.Get();
		if (demoMgr != null && !demoMgr.IsExpoDemo())
		{
			return;
		}
		base.StopCoroutine("ShowDemoQuotes");
		if (this.m_expoThankQuote != null)
		{
			NotificationManager notificationManager = NotificationManager.Get();
			if (notificationManager != null)
			{
				notificationManager.DestroyNotification(this.m_expoThankQuote, 0f);
			}
			this.m_expoThankQuote = null;
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			if (fullScreenFXMgr != null)
			{
				fullScreenFXMgr.StopVignette();
				fullScreenFXMgr.StopBlur();
			}
		}
	}

	// Token: 0x0600680A RID: 26634 RVA: 0x0021F4C0 File Offset: 0x0021D6C0
	private void HideChalkboardFX()
	{
		this.m_chalkboardFX.SetActive(false);
	}

	// Token: 0x04005565 RID: 21861
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_createDeckButton;

	// Token: 0x04005566 RID: 21862
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_editDeckButton;

	// Token: 0x04005567 RID: 21863
	[CustomEditField(Sections = "Buttons")]
	public PlayButton m_playButton;

	// Token: 0x04005568 RID: 21864
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_backButton;

	// Token: 0x04005569 RID: 21865
	[CustomEditField(Sections = "Buttons")]
	public PegUIElement m_rewardChest;

	// Token: 0x0400556A RID: 21866
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_viewDeckButton;

	// Token: 0x0400556B RID: 21867
	[CustomEditField(Sections = "Strings")]
	public UberText m_chalkboardHeader;

	// Token: 0x0400556C RID: 21868
	[CustomEditField(Sections = "Strings")]
	public UberText m_chalkboardInfo;

	// Token: 0x0400556D RID: 21869
	[CustomEditField(Sections = "Strings")]
	public UberText m_chalkboardEndInfo;

	// Token: 0x0400556E RID: 21870
	[CustomEditField(Sections = "Strings")]
	public UberText m_numWins;

	// Token: 0x0400556F RID: 21871
	[CustomEditField(Sections = "Strings")]
	public UberText m_TavernBrawlHeadline;

	// Token: 0x04005570 RID: 21872
	[CustomEditField(Sections = "Animating Elements")]
	public SlidingTray m_tavernBrawlTray;

	// Token: 0x04005571 RID: 21873
	[CustomEditField(Sections = "Animating Elements")]
	public SlidingTray m_cardListPanel;

	// Token: 0x04005572 RID: 21874
	[CustomEditField(Sections = "Animating Elements")]
	public Animation m_cardCountPanelAnim;

	// Token: 0x04005573 RID: 21875
	[CustomEditField(Sections = "Animating Elements")]
	public GameObject m_rewardsPreview;

	// Token: 0x04005574 RID: 21876
	[CustomEditField(Sections = "Animating Elements")]
	public GameObject m_rewardContainer;

	// Token: 0x04005575 RID: 21877
	[CustomEditField(Sections = "Animating Elements")]
	public UberText m_rewardsText;

	// Token: 0x04005576 RID: 21878
	[CustomEditField(Sections = "Animating Elements")]
	public Animator m_LockedDeckTray;

	// Token: 0x04005577 RID: 21879
	[CustomEditField(Sections = "Animating Elements")]
	public TavernBrawlPhoneDeckTray m_PhoneDeckTrayView;

	// Token: 0x04005578 RID: 21880
	[CustomEditField(Sections = "Animating Elements")]
	public DraftManaCurve m_ManaCurvePhone;

	// Token: 0x04005579 RID: 21881
	[CustomEditField(Sections = "Highlights")]
	public HighlightState m_createDeckHighlight;

	// Token: 0x0400557A RID: 21882
	[CustomEditField(Sections = "Highlights")]
	public HighlightState m_rewardHighlight;

	// Token: 0x0400557B RID: 21883
	[CustomEditField(Sections = "Highlights")]
	public HighlightState m_editDeckHighlight;

	// Token: 0x0400557C RID: 21884
	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_transitionStartingOffset = 100f;

	// Token: 0x0400557D RID: 21885
	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_transitionTime = 1f;

	// Token: 0x0400557E RID: 21886
	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_rootDropHeight = 10f;

	// Token: 0x0400557F RID: 21887
	[CustomEditField(Sections = "DungeonCrawl", T = EditType.SOUND_PREFAB)]
	public string m_SlideInSound;

	// Token: 0x04005580 RID: 21888
	public GameObject m_winsBanner;

	// Token: 0x04005581 RID: 21889
	public GameObject m_panelWithCreateDeck;

	// Token: 0x04005582 RID: 21890
	public GameObject m_fullPanel;

	// Token: 0x04005583 RID: 21891
	public GameObject m_chalkboard;

	// Token: 0x04005584 RID: 21892
	public Material m_chestOpenMaterial;

	// Token: 0x04005585 RID: 21893
	public float m_wipeAnimStartDelay;

	// Token: 0x04005586 RID: 21894
	public PegUIElement m_rewardOffClickCatcher;

	// Token: 0x04005587 RID: 21895
	public GameObject m_editIcon;

	// Token: 0x04005588 RID: 21896
	public GameObject m_deleteIcon;

	// Token: 0x04005589 RID: 21897
	public UberText m_editText;

	// Token: 0x0400558A RID: 21898
	public Color m_disabledTextColor = new Color(0.5f, 0.5f, 0.5f);

	// Token: 0x0400558B RID: 21899
	public GameObject m_lossesRoot;

	// Token: 0x0400558C RID: 21900
	public LossMarks m_lossMarks;

	// Token: 0x0400558D RID: 21901
	public GameObject m_rewardBoxesBone;

	// Token: 0x0400558E RID: 21902
	public GameObject m_normalWinLocationBone;

	// Token: 0x0400558F RID: 21903
	public GameObject m_sessionWinLocationBone;

	// Token: 0x04005590 RID: 21904
	public PegUIElement m_LockedDeckTooltipTrigger;

	// Token: 0x04005591 RID: 21905
	public TooltipZone m_LockedDeckTooltipZone;

	// Token: 0x04005592 RID: 21906
	public Transform m_SocketHeroBone;

	// Token: 0x04005593 RID: 21907
	public BoxCollider m_clickBlocker;

	// Token: 0x04005594 RID: 21908
	public GameObject m_chalkboardFX;

	// Token: 0x04005595 RID: 21909
	public FiresideGatheringPlayButtonLantern m_FiresideGatheringPlayButtonLantern;

	// Token: 0x04005596 RID: 21910
	public GameObject m_sessionRewardBoxesBone;

	// Token: 0x04005597 RID: 21911
	public Vector3 m_firesideArrowHintPositionOffset;

	// Token: 0x04005598 RID: 21912
	public Vector3 m_firesideArrowRotation;

	// Token: 0x04005599 RID: 21913
	public float m_firesideArrowScale;

	// Token: 0x0400559A RID: 21914
	public Texture m_chalkboardTexture;

	// Token: 0x0400559B RID: 21915
	private static TavernBrawlDisplay s_instance;

	// Token: 0x0400559C RID: 21916
	private bool m_doFirstSeenAnimations;

	// Token: 0x0400559D RID: 21917
	private long m_deckBeingEdited;

	// Token: 0x0400559E RID: 21918
	private GameObject m_rewardObject;

	// Token: 0x0400559F RID: 21919
	private Vector3 m_rewardsScale;

	// Token: 0x040055A0 RID: 21920
	private readonly string CARD_COUNT_PANEL_OPEN_ANIM = "TavernBrawl_DecksNumberCoverUp_Open";

	// Token: 0x040055A1 RID: 21921
	private readonly string CARD_COUNT_PANEL_CLOSE_ANIM = "TavernBrawl_DecksNumberCoverUp_Close";

	// Token: 0x040055A2 RID: 21922
	private bool m_cardCountPanelAnimOpen;

	// Token: 0x040055A3 RID: 21923
	private Color? m_originalEditTextColor;

	// Token: 0x040055A4 RID: 21924
	private Color? m_originalEditIconColor;

	// Token: 0x040055A5 RID: 21925
	private TavernBrawlMission m_currentMission;

	// Token: 0x040055A6 RID: 21926
	private TavernBrawlStatus m_currentlyShowingMode;

	// Token: 0x040055A7 RID: 21927
	private bool m_firstTimeIntroductionPopupShowing;

	// Token: 0x040055A8 RID: 21928
	private BannerPopup m_firstTimeIntroBanner;

	// Token: 0x040055A9 RID: 21929
	private Actor m_chosenHero;

	// Token: 0x040055AA RID: 21930
	private Notification m_expoThankQuote;

	// Token: 0x040055AB RID: 21931
	private AssetLoadingHelper m_assetLoadingHelper;

	// Token: 0x040055AC RID: 21932
	private AdventureDungeonCrawlDisplay m_dungeonCrawlDisplay;

	// Token: 0x040055AD RID: 21933
	private DungeonCrawlServices m_dungeonCrawlServices;

	// Token: 0x040055AE RID: 21934
	private AdventureDefCache m_adventureDefCache = new AdventureDefCache(false);

	// Token: 0x040055AF RID: 21935
	private AdventureWingDefCache m_adventureWingDefCache = new AdventureWingDefCache(false);

	// Token: 0x040055B0 RID: 21936
	private bool m_rewardChestDeprecated;

	// Token: 0x040055B1 RID: 21937
	private bool m_tavernBrawlHasEndedDialogActive;

	// Token: 0x040055B2 RID: 21938
	private static readonly PlatformDependentValue<string> DEFAULT_CHALKBOARD_TEXTURE_NAME_NO_DECK = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "TavernBrawl_Chalkboard_Default_NoBorders.psd:556aa8938a98460498f590d2458e88b2",
		Phone = "TavernBrawl_Chalkboard_Default_phone.psd:c8421199aaf31fc4da69869c716fcf98"
	};

	// Token: 0x040055B3 RID: 21939
	private static readonly PlatformDependentValue<string> DEFAULT_CHALKBOARD_TEXTURE_NAME_WITH_DECK = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "TavernBrawl_Chalkboard_Default_Borders.psd:e61e732d5bdd27e408e21fd873c99aa0",
		Phone = "TavernBrawl_Chalkboard_Default_phone.psd:c8421199aaf31fc4da69869c716fcf98"
	};

	// Token: 0x040055B4 RID: 21940
	private static readonly PlatformDependentValue<UnityEngine.Vector2> DEFAULT_CHALKBOARD_TEXTURE_OFFSET_NO_DECK = new PlatformDependentValue<UnityEngine.Vector2>(PlatformCategory.Screen)
	{
		PC = UnityEngine.Vector2.zero,
		Phone = UnityEngine.Vector2.zero
	};

	// Token: 0x040055B5 RID: 21941
	private static readonly PlatformDependentValue<UnityEngine.Vector2> DEFAULT_CHALKBOARD_TEXTURE_OFFSET_WITH_DECK = new PlatformDependentValue<UnityEngine.Vector2>(PlatformCategory.Screen)
	{
		PC = UnityEngine.Vector2.zero,
		Phone = new UnityEngine.Vector2(0f, -0.389f)
	};

	// Token: 0x040055B6 RID: 21942
	private static readonly AssetReference HEROIC_BRAWL_DIFFICULTY_WARNING_POPUP = new AssetReference("NewPopUp_HeroicBrawl.prefab:cac9ec2e7b497e641a02a03f65609486");
}
