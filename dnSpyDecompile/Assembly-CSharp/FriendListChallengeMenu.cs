using System;
using bgs;
using PegasusShared;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class FriendListChallengeMenu : MonoBehaviour
{
	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000845 RID: 2117 RVA: 0x00030574 File Offset: 0x0002E774
	// (set) Token: 0x06000846 RID: 2118 RVA: 0x00030585 File Offset: 0x0002E785
	private bool HasClickedOnFiresideBrawlButton
	{
		get
		{
			return Options.Get().GetBool(Option.HAS_CLICKED_FIRESIDE_BRAWL_BUTTON);
		}
		set
		{
			Options.Get().SetBool(Option.HAS_CLICKED_FIRESIDE_BRAWL_BUTTON, value);
		}
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00030598 File Offset: 0x0002E798
	private void Awake()
	{
		this.m_challengeButton = base.GetComponentInParent<FriendListChallengeButton>();
		this.m_defaultMiddleFrameScaleY = this.m_MiddleFrame.transform.localScale.y;
		this.m_defaultMiddleShadowScaleY = this.m_MiddleShadow.transform.localScale.y;
		this.m_defaultAddAsFriendButtonPosition = this.m_AddAsFriendButton.gameObject.transform.localPosition;
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x00030604 File Offset: 0x0002E804
	private void Start()
	{
		this.m_StandardDuelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnStandardDuelButtonReleased));
		this.m_WildDuelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnWildDuelButtonReleased));
		this.m_ClassicDuelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClassicDuelButtonReleased));
		this.m_TavernBrawlButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRegularTavernBrawlButtonReleased));
		this.m_BaconButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBaconButtonReleased));
		this.m_AddAsFriendButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnAddAsFriendButtonReleased));
		this.m_SpectateButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnSpectateButtonReleased));
		this.InitializeButtonLayout();
		this.InitializeMenuPosition();
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x000306CC File Offset: 0x0002E8CC
	private void OnEnable()
	{
		this.InitializeButtonLayout();
		this.InitializeMenuPosition();
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x000306DC File Offset: 0x0002E8DC
	public void InitializeButtonLayout()
	{
		if (this.m_challengeButton == null)
		{
			return;
		}
		BnetPlayer player = this.m_challengeButton.GetPlayer();
		if (player == null)
		{
			return;
		}
		float num = this.m_defaultMiddleFrameScaleY;
		float num2 = this.m_defaultMiddleShadowScaleY;
		bool flag = FriendChallengeMgr.Get().CanChallenge(player);
		if (flag || !BnetFriendMgr.Get().IsFriend(player))
		{
			this.m_SpectateButton.gameObject.SetActive(false);
			this.m_SpectateText.gameObject.SetActive(false);
			this.m_StandardDuelButton.gameObject.SetActive(true);
			this.m_WildDuelButton.gameObject.SetActive(true);
			this.m_ClassicDuelButton.gameObject.SetActive(true);
			this.m_TavernBrawlButton.gameObject.SetActive(true);
			this.m_BaconButton.gameObject.SetActive(true);
			bool flag2 = !flag || PartyManager.Get().IsInBattlegroundsParty();
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			if (!netObject.Games.Friendly || flag2)
			{
				this.DisableDuelButton(this.m_StandardDuelButton, this.m_StandardDuelButtonDisabled);
				this.DisableDuelButton(this.m_WildDuelButton, this.m_WildDuelButtonDisabled);
				this.DisableDuelButton(this.m_ClassicDuelButton, this.m_ClassicDuelButtonDisabled);
				this.m_StandardDuelButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnStandardDuelButtonReleased));
				this.m_StandardDuelButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnStandardDuelButtonOver));
				this.m_WildDuelButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnWildDuelButtonReleased));
				this.m_WildDuelButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnWildDuelButtonOver));
				this.m_ClassicDuelButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClassicDuelButtonReleased));
				this.m_ClassicDuelButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnClassicDuelButtonOver));
			}
			bool flag3 = !netObject.Games.BattlegroundsFriendlyChallenge || flag2;
			bool flag4 = SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", false) && !AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetBattlegroundsEarlyAccessLicenseId());
			if (flag3 || flag4)
			{
				this.DisableBaconButton();
			}
			if (!CollectionManager.Get().ShouldAccountSeeStandardWild())
			{
				this.m_StandardDuelButton.SetText("GLOBAL_FRIENDLIST_CHALLENGE_MENU_DUEL_BUTTON");
				this.m_TavernBrawlButton.gameObject.transform.localPosition = this.m_WildDuelButton.gameObject.transform.localPosition;
				float d = this.m_StandardDuelButton.transform.localPosition.y - this.m_TavernBrawlButton.transform.localPosition.y;
				this.m_BaconButton.gameObject.transform.localPosition = this.m_TavernBrawlButton.gameObject.transform.localPosition - Vector3.up * d;
				float d2 = this.m_StandardDuelButton.transform.localPosition.y - this.m_ClassicDuelButton.transform.localPosition.y;
				this.m_AddAsFriendButton.gameObject.transform.localPosition = this.m_defaultAddAsFriendButtonPosition + Vector3.up * d2;
				this.m_WildDuelButton.gameObject.SetActive(false);
				this.m_ClassicDuelButton.gameObject.SetActive(false);
				num -= this.m_middleFrameScaleOffsetForWild * 2f;
				num2 -= this.m_middleShadowScaleOffsetForWild * 2f;
				this.m_MiddleFrame.transform.localScale = new Vector3(this.m_MiddleFrame.transform.localScale.x, num, this.m_MiddleFrame.transform.localScale.z);
				this.m_MiddleShadow.transform.localScale = new Vector3(this.m_MiddleShadow.transform.localScale.x, num2, this.m_MiddleShadow.transform.localScale.z);
			}
			this.m_bHasStandardDeck = CollectionManager.Get().AccountHasValidDeck(FormatType.FT_STANDARD);
			this.m_bHasWildDeck = CollectionManager.Get().AccountHasAnyValidDeck();
			this.m_bHasClassicDeck = CollectionManager.Get().AccountHasValidDeck(FormatType.FT_CLASSIC);
			this.m_bCanChallengeTavernBrawl = TavernBrawlManager.Get().CanChallengeToTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			this.m_bIsTavernBrawlUnlocked = TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			if (this.m_bCanChallengeTavernBrawl && flag)
			{
				TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
				this.m_bHasTavernBrawlDeck = (!mission.canCreateDeck || TavernBrawlManager.Get().HasValidDeck(BrawlType.BRAWL_TYPE_TAVERN_BRAWL, 0));
			}
			else
			{
				this.DisableTavernBrawlButton();
			}
			this.SetupAddFriendButtonAndFixFrameLength(num, num2);
			this.m_FrameContainer.UpdateSlices();
			this.m_ShadowContainer.UpdateSlices();
			if (!CollectionManager.Get().AreAllDeckContentsReady() && CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded(new CollectionManager.DelOnAllDeckContents(this.OnDeckContents_UpdateButtons)))
			{
				this.m_bHasStandardDeck = true;
				this.m_bHasWildDeck = true;
				this.m_bHasClassicDeck = true;
			}
		}
		else if (SpectatorManager.Get().CanSpectate(player))
		{
			this.m_SpectateButton.gameObject.SetActive(true);
			this.m_SpectateText.gameObject.SetActive(true);
			this.m_SpectateText.Text = GameStrings.Format("GLOBAL_FRIENDLIST_SPECTATE_MENU_DESCRIPTION", new object[]
			{
				player.GetBestName()
			});
			this.m_StandardDuelButton.gameObject.SetActive(false);
			this.m_WildDuelButton.gameObject.SetActive(false);
			this.m_ClassicDuelButton.gameObject.SetActive(false);
			this.m_TavernBrawlButton.gameObject.SetActive(false);
			this.m_BaconButton.gameObject.SetActive(false);
			num -= this.m_middleFrameScaleOffsetForSpectateMode;
			num2 -= this.m_middleShadowScaleOffsetForSpectateMode;
			this.SetupAddFriendButtonAndFixFrameLength(num, num2);
			this.m_FrameContainer.UpdateSlices();
			this.m_ShadowContainer.UpdateSlices();
		}
		this.UpdateActiveChallengeButtons();
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x00030C94 File Offset: 0x0002EE94
	public void InitializeMenuPosition()
	{
		base.transform.localPosition = FriendListChallengeMenu.CHALLENGE_MENU_DEFAULT_POSITION;
		Bounds bounds = base.gameObject.GetComponent<Collider>().bounds;
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Vector3 vector = camera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.min.y, bounds.center.z));
		if (vector.y < 0f)
		{
			Vector3 vector2 = camera.WorldToScreenPoint(base.transform.position);
			base.transform.position = camera.ScreenToWorldPoint(new Vector3(vector2.x, vector2.y - vector.y, vector2.z));
		}
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x00030D56 File Offset: 0x0002EF56
	private void DisableDuelButton(UIBButton buttonToDisable, GameObject disabledVisual)
	{
		disabledVisual.SetActive(true);
		buttonToDisable.gameObject.GetComponent<UIBHighlight>().m_MouseOverHighlight = null;
		buttonToDisable.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnButtonOut));
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x00030D84 File Offset: 0x0002EF84
	private void DisableTavernBrawlButton()
	{
		this.DisableDuelButton(this.m_TavernBrawlButton, this.m_TavernBrawlButtonDisabled);
		this.m_TavernBrawlButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRegularTavernBrawlButtonReleased));
		this.m_TavernBrawlButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnTavernBrawlButtonOver));
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x00030DD8 File Offset: 0x0002EFD8
	private void DisableBaconButton()
	{
		this.DisableDuelButton(this.m_BaconButton, this.m_BaconButtonDisabled);
		this.m_BaconButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBaconButtonReleased));
		this.m_BaconButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnBaconButtonOver));
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x00030E2C File Offset: 0x0002F02C
	private void UpdateActiveChallengeButtons()
	{
		this.m_StandardDuelButtonX.SetActive(!this.m_bHasStandardDeck);
		this.m_WildDuelButtonX.SetActive(!this.m_bHasWildDeck);
		this.m_ClassicDuelButtonX.SetActive(!this.m_bHasClassicDeck);
		this.m_TavernBrawlButtonX.SetActive(this.m_bCanChallengeTavernBrawl && (!this.m_bIsTavernBrawlUnlocked || !this.m_bHasTavernBrawlDeck));
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x00030EA0 File Offset: 0x0002F0A0
	private void OnDeckContents_UpdateButtons()
	{
		if (this == null)
		{
			return;
		}
		this.m_bHasStandardDeck = CollectionManager.Get().AccountHasValidDeck(FormatType.FT_STANDARD);
		this.m_bHasWildDeck = CollectionManager.Get().AccountHasAnyValidDeck();
		this.m_bHasClassicDeck = CollectionManager.Get().AccountHasValidDeck(FormatType.FT_CLASSIC);
		this.UpdateActiveChallengeButtons();
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00030EF0 File Offset: 0x0002F0F0
	private void OnStandardDuelButtonOver(UIEvent e)
	{
		string text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
		string text2 = this.GetDescriptionTextWhenUnavailable();
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.Friendly)
		{
			text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_MODE_UNAVAILABLE";
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (GameStrings.HasKey(text + "_TOUCH"))
			{
				text += "_TOUCH";
			}
			if (GameStrings.HasKey(text2 + "_TOUCH"))
			{
				text2 += "_TOUCH";
			}
		}
		this.ShowTooltip(text, text2, this.m_StandardDuelTooltipZone, this.m_StandardDuelButton);
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x00030F84 File Offset: 0x0002F184
	private void OnWildDuelButtonOver(UIEvent e)
	{
		string text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
		string text2 = this.GetDescriptionTextWhenUnavailable();
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.Friendly)
		{
			text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_MODE_UNAVAILABLE";
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (GameStrings.HasKey(text + "_TOUCH"))
			{
				text += "_TOUCH";
			}
			if (GameStrings.HasKey(text2 + "_TOUCH"))
			{
				text2 += "_TOUCH";
			}
		}
		this.ShowTooltip(text, text2, this.m_WildDuelTooltipZone, this.m_WildDuelButton);
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00031018 File Offset: 0x0002F218
	private void OnClassicDuelButtonOver(UIEvent e)
	{
		string text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
		string text2 = this.GetDescriptionTextWhenUnavailable();
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.Friendly)
		{
			text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_MODE_UNAVAILABLE";
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (GameStrings.HasKey(text + "_TOUCH"))
			{
				text += "_TOUCH";
			}
			if (GameStrings.HasKey(text2 + "_TOUCH"))
			{
				text2 += "_TOUCH";
			}
		}
		this.ShowTooltip(text, text2, this.m_ClassicDuelTooltipZone, this.m_ClassicDuelButton);
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x000310AC File Offset: 0x0002F2AC
	private void OnTavernBrawlButtonOver(UIEvent e)
	{
		string text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
		string text2 = this.GetDescriptionTextWhenUnavailable();
		if (!TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			text2 = "GLOBAL_FRIENDLIST_CHALLENGE_TOOLTIP_NO_TAVERN_BRAWL";
		}
		else if (TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL) && !TavernBrawlManager.Get().CanChallengeToTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			text2 = "GLOBAL_FRIENDLIST_CHALLENGE_TOOLTIP_TAVERN_BRAWL_NOT_CHALLENGEABLE";
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (GameStrings.HasKey(text + "_TOUCH"))
			{
				text += "_TOUCH";
			}
			if (GameStrings.HasKey(text2 + "_TOUCH"))
			{
				text2 += "_TOUCH";
			}
		}
		this.ShowTooltip(text, text2, this.m_TavernBrawlTooltipZone, this.m_TavernBrawlButton);
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x00031158 File Offset: 0x0002F358
	private void OnBaconButtonOver(UIEvent e)
	{
		string text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
		string text2 = this.GetDescriptionTextWhenUnavailable();
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.BattlegroundsFriendlyChallenge)
		{
			text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_MODE_UNAVAILABLE";
		}
		else if (SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", false) && !AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetBattlegroundsEarlyAccessLicenseId()))
		{
			text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_EARLY_ACCESS";
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (GameStrings.HasKey(text + "_TOUCH"))
			{
				text += "_TOUCH";
			}
			if (GameStrings.HasKey(text2 + "_TOUCH"))
			{
				text2 += "_TOUCH";
			}
		}
		this.ShowTooltip(text, text2, this.m_BaconTooltipZone, this.m_BaconButton);
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0003121C File Offset: 0x0002F41C
	private string GetDescriptionTextWhenUnavailable()
	{
		string result = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_THEYRE_UNAVAILABLE";
		if (!FriendChallengeMgr.Get().AmIAvailable())
		{
			if (BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline())
			{
				result = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_IM_APPEARING_OFFLINE";
			}
			else if (PartyManager.Get().IsInBattlegroundsParty() && !PartyManager.Get().IsPartyLeader())
			{
				result = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_BATTLEGROUNDS_PARTY_MEMBER";
			}
			else
			{
				result = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_IM_UNAVAILABLE";
			}
		}
		return result;
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0003127B File Offset: 0x0002F47B
	private void OnButtonOut(UIEvent e)
	{
		this.HideTooltip();
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00031284 File Offset: 0x0002F484
	private void OnStandardDuelButtonReleased(UIEvent e)
	{
		if (!this.m_bHasStandardDeck)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGER_NO_STANDARD_DECK", Array.Empty<object>()),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
			this.m_challengeButton.CloseChallengeMenu();
			return;
		}
		FriendChallengeMgr.Get().SetChallengeMethod(FriendChallengeMgr.ChallengeMethod.FROM_FRIEND_LIST);
		BnetPlayer player = this.m_challengeButton.GetPlayer();
		FriendChallengeMgr.Get().SendChallenge(player, FormatType.FT_STANDARD, true);
		this.m_challengeButton.CloseFriendsListMenu();
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00031318 File Offset: 0x0002F518
	private void OnWildDuelButtonReleased(UIEvent e)
	{
		if (!this.m_bHasWildDeck)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGER_NO_DECK", Array.Empty<object>()),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
			this.m_challengeButton.CloseChallengeMenu();
			return;
		}
		FriendChallengeMgr.Get().SetChallengeMethod(FriendChallengeMgr.ChallengeMethod.FROM_FRIEND_LIST);
		BnetPlayer player = this.m_challengeButton.GetPlayer();
		FriendChallengeMgr.Get().SendChallenge(player, FormatType.FT_WILD, true);
		this.m_challengeButton.CloseFriendsListMenu();
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x000313AC File Offset: 0x0002F5AC
	private void OnClassicDuelButtonReleased(UIEvent e)
	{
		if (!this.m_bHasClassicDeck)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGER_NO_CLASSIC_DECK", Array.Empty<object>()),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
			this.m_challengeButton.CloseChallengeMenu();
			return;
		}
		FriendChallengeMgr.Get().SetChallengeMethod(FriendChallengeMgr.ChallengeMethod.FROM_FRIEND_LIST);
		BnetPlayer player = this.m_challengeButton.GetPlayer();
		FriendChallengeMgr.Get().SendChallenge(player, FormatType.FT_CLASSIC, true);
		this.m_challengeButton.CloseFriendsListMenu();
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00031440 File Offset: 0x0002F640
	private void OnRegularTavernBrawlButtonReleased(UIEvent e)
	{
		if (!this.m_bIsTavernBrawlUnlocked)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGER_TAVERN_BRAWL_LOCKED", Array.Empty<object>()),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
			this.m_challengeButton.CloseChallengeMenu();
			return;
		}
		if (!this.m_bCanChallengeTavernBrawl)
		{
			AlertPopup.PopupInfo info2 = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_TAVERN_BRAWL_ERROR_SEASON_INCREMENTED", Array.Empty<object>()),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info2);
			this.m_challengeButton.CloseChallengeMenu();
			return;
		}
		if (!this.m_bHasTavernBrawlDeck)
		{
			FriendChallengeMgr.ShowChallengerNeedsToCreateTavernBrawlDeckAlert();
			this.m_challengeButton.CloseChallengeMenu();
			return;
		}
		TavernBrawlManager.Get().CurrentBrawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL;
		BnetPlayer player = this.m_challengeButton.GetPlayer();
		FriendChallengeMgr.Get().SendTavernBrawlChallenge(player, BrawlType.BRAWL_TYPE_TAVERN_BRAWL, TavernBrawlManager.Get().CurrentMission().seasonId, TavernBrawlManager.Get().CurrentMission().SelectedBrawlLibraryItemId);
		this.m_challengeButton.CloseFriendsListMenu();
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x00031564 File Offset: 0x0002F764
	private void OnBaconButtonReleased(UIEvent e)
	{
		BnetPlayer player = this.m_challengeButton.GetPlayer();
		this.NavigateToSceneForPartyChallenge(SceneMgr.Mode.BACON);
		PartyManager.Get().SendInvite(PartyType.BATTLEGROUNDS_PARTY, player.GetBestGameAccountId());
		this.m_challengeButton.CloseChallengeMenu();
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x000315A4 File Offset: 0x0002F7A4
	private void NavigateToSceneForPartyChallenge(SceneMgr.Mode nextMode)
	{
		GameMgr.Get().SetPendingAutoConcede(true);
		if (CollectionManager.Get().IsInEditMode())
		{
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			if (editedDeck != null)
			{
				editedDeck.SendChanges();
			}
		}
		SceneMgr.Get().SetNextMode(nextMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x000315EC File Offset: 0x0002F7EC
	private void OnAddAsFriendButtonReleased(UIEvent e)
	{
		BnetPlayer player = this.m_challengeButton.GetPlayer();
		BnetFriendMgr.Get().SendInvite(player.GetBattleTag().GetString());
		this.m_challengeButton.CloseChallengeMenu();
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00031628 File Offset: 0x0002F828
	private void OnSpectateButtonReleased(UIEvent e)
	{
		BnetPlayer player = this.m_challengeButton.GetPlayer();
		SpectatorManager.Get().SpectatePlayer(player);
		this.m_challengeButton.CloseChallengeMenu();
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x00031658 File Offset: 0x0002F858
	private void SetupAddFriendButtonAndFixFrameLength(float middleFrameScaleY, float middleShadowScaleY)
	{
		BnetPlayer player = this.m_challengeButton.GetPlayer();
		if (BnetFriendMgr.Get().IsFriend(player))
		{
			this.m_AddAsFriendButton.gameObject.SetActive(false);
			middleFrameScaleY -= this.m_middleFrameScaleOffsetForAddFriend;
			middleShadowScaleY -= this.m_middleShadowScaleOffsetForAddFriend;
		}
		else
		{
			this.m_AddAsFriendButton.gameObject.SetActive(true);
		}
		this.m_MiddleFrame.transform.localScale = new Vector3(this.m_MiddleFrame.transform.localScale.x, middleFrameScaleY, this.m_MiddleFrame.transform.localScale.z);
		this.m_MiddleShadow.transform.localScale = new Vector3(this.m_MiddleShadow.transform.localScale.x, middleShadowScaleY, this.m_MiddleShadow.transform.localScale.z);
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x00031738 File Offset: 0x0002F938
	private void ShowTooltip(string headerKey, string descriptionFormat, TooltipZone tooltipZone, UIBButton button)
	{
		string headline = GameStrings.Get(headerKey);
		BnetPlayer player = this.m_challengeButton.GetPlayer();
		string bodytext = GameStrings.Format(descriptionFormat, new object[]
		{
			player.GetBestName()
		});
		this.HideTooltip();
		tooltipZone.ShowSocialTooltip(button, headline, bodytext, 75f, GameLayer.BattleNetDialog, 0);
		tooltipZone.AnchorTooltipTo(button.gameObject, Anchor.TOP_RIGHT, Anchor.TOP_LEFT, 0);
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void UpdateTooltip()
	{
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00031798 File Offset: 0x0002F998
	private void HideTooltip()
	{
		if (this.m_StandardDuelTooltipZone != null)
		{
			this.m_StandardDuelTooltipZone.HideTooltip();
		}
		if (this.m_WildDuelTooltipZone != null)
		{
			this.m_WildDuelTooltipZone.HideTooltip();
		}
		if (this.m_ClassicDuelTooltipZone != null)
		{
			this.m_ClassicDuelTooltipZone.HideTooltip();
		}
		if (this.m_TavernBrawlTooltipZone != null)
		{
			this.m_TavernBrawlTooltipZone.HideTooltip();
		}
		if (this.m_BaconTooltipZone != null)
		{
			this.m_BaconTooltipZone.HideTooltip();
		}
	}

	// Token: 0x04000597 RID: 1431
	public UIBButton m_StandardDuelButton;

	// Token: 0x04000598 RID: 1432
	public UIBButton m_WildDuelButton;

	// Token: 0x04000599 RID: 1433
	public UIBButton m_ClassicDuelButton;

	// Token: 0x0400059A RID: 1434
	public UIBButton m_TavernBrawlButton;

	// Token: 0x0400059B RID: 1435
	public UIBButton m_BaconButton;

	// Token: 0x0400059C RID: 1436
	public UIBButton m_AddAsFriendButton;

	// Token: 0x0400059D RID: 1437
	public UIBButton m_SpectateButton;

	// Token: 0x0400059E RID: 1438
	public TooltipZone m_StandardDuelTooltipZone;

	// Token: 0x0400059F RID: 1439
	public TooltipZone m_WildDuelTooltipZone;

	// Token: 0x040005A0 RID: 1440
	public TooltipZone m_ClassicDuelTooltipZone;

	// Token: 0x040005A1 RID: 1441
	public TooltipZone m_TavernBrawlTooltipZone;

	// Token: 0x040005A2 RID: 1442
	public TooltipZone m_BaconTooltipZone;

	// Token: 0x040005A3 RID: 1443
	public GameObject m_StandardDuelButtonX;

	// Token: 0x040005A4 RID: 1444
	public GameObject m_StandardDuelButtonDisabled;

	// Token: 0x040005A5 RID: 1445
	public GameObject m_WildDuelButtonX;

	// Token: 0x040005A6 RID: 1446
	public GameObject m_WildDuelButtonDisabled;

	// Token: 0x040005A7 RID: 1447
	public GameObject m_ClassicDuelButtonX;

	// Token: 0x040005A8 RID: 1448
	public GameObject m_ClassicDuelButtonDisabled;

	// Token: 0x040005A9 RID: 1449
	public GameObject m_TavernBrawlButtonX;

	// Token: 0x040005AA RID: 1450
	public GameObject m_TavernBrawlButtonDisabled;

	// Token: 0x040005AB RID: 1451
	public GameObject m_BaconButtonDisabled;

	// Token: 0x040005AC RID: 1452
	public MultiSliceElement m_FrameContainer;

	// Token: 0x040005AD RID: 1453
	public MultiSliceElement m_ShadowContainer;

	// Token: 0x040005AE RID: 1454
	public GameObject m_MiddleFrame;

	// Token: 0x040005AF RID: 1455
	public GameObject m_MiddleShadow;

	// Token: 0x040005B0 RID: 1456
	public GameObject m_FiresideBrawlButtonGlow;

	// Token: 0x040005B1 RID: 1457
	public UberText m_SpectateText;

	// Token: 0x040005B2 RID: 1458
	public float m_middleFrameScaleOffsetForWild = 0.5f;

	// Token: 0x040005B3 RID: 1459
	public float m_middleShadowScaleOffsetForWild = 0.1f;

	// Token: 0x040005B4 RID: 1460
	public float m_middleFrameScaleOffsetForSpectateMode = 0.5f;

	// Token: 0x040005B5 RID: 1461
	public float m_middleShadowScaleOffsetForSpectateMode = 0.16f;

	// Token: 0x040005B6 RID: 1462
	public float m_middleFrameScaleOffsetForAddFriend = 0.63f;

	// Token: 0x040005B7 RID: 1463
	public float m_middleShadowScaleOffsetForAddFriend = 0.12f;

	// Token: 0x040005B8 RID: 1464
	private FriendListChallengeButton m_challengeButton;

	// Token: 0x040005B9 RID: 1465
	private bool m_bHasStandardDeck;

	// Token: 0x040005BA RID: 1466
	private bool m_bHasWildDeck;

	// Token: 0x040005BB RID: 1467
	private bool m_bHasClassicDeck;

	// Token: 0x040005BC RID: 1468
	private bool m_bHasTavernBrawlDeck;

	// Token: 0x040005BD RID: 1469
	private bool m_bCanChallengeTavernBrawl;

	// Token: 0x040005BE RID: 1470
	private bool m_bIsTavernBrawlUnlocked;

	// Token: 0x040005BF RID: 1471
	private float m_defaultMiddleFrameScaleY;

	// Token: 0x040005C0 RID: 1472
	private float m_defaultMiddleShadowScaleY;

	// Token: 0x040005C1 RID: 1473
	private Vector3 m_defaultAddAsFriendButtonPosition;

	// Token: 0x040005C2 RID: 1474
	private static readonly PlatformDependentValue<Vector3> CHALLENGE_MENU_DEFAULT_POSITION = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(205.9f, -130f, 0f),
		Phone = new Vector3(184.9f, -130f, 0f)
	};
}
