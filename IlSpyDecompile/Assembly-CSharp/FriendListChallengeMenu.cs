using bgs;
using PegasusShared;
using UnityEngine;

public class FriendListChallengeMenu : MonoBehaviour
{
	public UIBButton m_StandardDuelButton;

	public UIBButton m_WildDuelButton;

	public UIBButton m_ClassicDuelButton;

	public UIBButton m_TavernBrawlButton;

	public UIBButton m_BaconButton;

	public UIBButton m_AddAsFriendButton;

	public UIBButton m_SpectateButton;

	public TooltipZone m_StandardDuelTooltipZone;

	public TooltipZone m_WildDuelTooltipZone;

	public TooltipZone m_ClassicDuelTooltipZone;

	public TooltipZone m_TavernBrawlTooltipZone;

	public TooltipZone m_BaconTooltipZone;

	public GameObject m_StandardDuelButtonX;

	public GameObject m_StandardDuelButtonDisabled;

	public GameObject m_WildDuelButtonX;

	public GameObject m_WildDuelButtonDisabled;

	public GameObject m_ClassicDuelButtonX;

	public GameObject m_ClassicDuelButtonDisabled;

	public GameObject m_TavernBrawlButtonX;

	public GameObject m_TavernBrawlButtonDisabled;

	public GameObject m_BaconButtonDisabled;

	public MultiSliceElement m_FrameContainer;

	public MultiSliceElement m_ShadowContainer;

	public GameObject m_MiddleFrame;

	public GameObject m_MiddleShadow;

	public GameObject m_FiresideBrawlButtonGlow;

	public UberText m_SpectateText;

	public float m_middleFrameScaleOffsetForWild = 0.5f;

	public float m_middleShadowScaleOffsetForWild = 0.1f;

	public float m_middleFrameScaleOffsetForSpectateMode = 0.5f;

	public float m_middleShadowScaleOffsetForSpectateMode = 0.16f;

	public float m_middleFrameScaleOffsetForAddFriend = 0.63f;

	public float m_middleShadowScaleOffsetForAddFriend = 0.12f;

	private FriendListChallengeButton m_challengeButton;

	private bool m_bHasStandardDeck;

	private bool m_bHasWildDeck;

	private bool m_bHasClassicDeck;

	private bool m_bHasTavernBrawlDeck;

	private bool m_bCanChallengeTavernBrawl;

	private bool m_bIsTavernBrawlUnlocked;

	private float m_defaultMiddleFrameScaleY;

	private float m_defaultMiddleShadowScaleY;

	private Vector3 m_defaultAddAsFriendButtonPosition;

	private static readonly PlatformDependentValue<Vector3> CHALLENGE_MENU_DEFAULT_POSITION = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(205.9f, -130f, 0f),
		Phone = new Vector3(184.9f, -130f, 0f)
	};

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

	private void Awake()
	{
		m_challengeButton = GetComponentInParent<FriendListChallengeButton>();
		m_defaultMiddleFrameScaleY = m_MiddleFrame.transform.localScale.y;
		m_defaultMiddleShadowScaleY = m_MiddleShadow.transform.localScale.y;
		m_defaultAddAsFriendButtonPosition = m_AddAsFriendButton.gameObject.transform.localPosition;
	}

	private void Start()
	{
		m_StandardDuelButton.AddEventListener(UIEventType.RELEASE, OnStandardDuelButtonReleased);
		m_WildDuelButton.AddEventListener(UIEventType.RELEASE, OnWildDuelButtonReleased);
		m_ClassicDuelButton.AddEventListener(UIEventType.RELEASE, OnClassicDuelButtonReleased);
		m_TavernBrawlButton.AddEventListener(UIEventType.RELEASE, OnRegularTavernBrawlButtonReleased);
		m_BaconButton.AddEventListener(UIEventType.RELEASE, OnBaconButtonReleased);
		m_AddAsFriendButton.AddEventListener(UIEventType.RELEASE, OnAddAsFriendButtonReleased);
		m_SpectateButton.AddEventListener(UIEventType.RELEASE, OnSpectateButtonReleased);
		InitializeButtonLayout();
		InitializeMenuPosition();
	}

	private void OnEnable()
	{
		InitializeButtonLayout();
		InitializeMenuPosition();
	}

	public void InitializeButtonLayout()
	{
		if (m_challengeButton == null)
		{
			return;
		}
		BnetPlayer player = m_challengeButton.GetPlayer();
		if (player == null)
		{
			return;
		}
		float num = m_defaultMiddleFrameScaleY;
		float num2 = m_defaultMiddleShadowScaleY;
		bool flag = FriendChallengeMgr.Get().CanChallenge(player);
		if (flag || !BnetFriendMgr.Get().IsFriend(player))
		{
			m_SpectateButton.gameObject.SetActive(value: false);
			m_SpectateText.gameObject.SetActive(value: false);
			m_StandardDuelButton.gameObject.SetActive(value: true);
			m_WildDuelButton.gameObject.SetActive(value: true);
			m_ClassicDuelButton.gameObject.SetActive(value: true);
			m_TavernBrawlButton.gameObject.SetActive(value: true);
			m_BaconButton.gameObject.SetActive(value: true);
			bool flag2 = !flag || PartyManager.Get().IsInBattlegroundsParty();
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			if (!netObject.Games.Friendly || flag2)
			{
				DisableDuelButton(m_StandardDuelButton, m_StandardDuelButtonDisabled);
				DisableDuelButton(m_WildDuelButton, m_WildDuelButtonDisabled);
				DisableDuelButton(m_ClassicDuelButton, m_ClassicDuelButtonDisabled);
				m_StandardDuelButton.RemoveEventListener(UIEventType.RELEASE, OnStandardDuelButtonReleased);
				m_StandardDuelButton.AddEventListener(UIEventType.ROLLOVER, OnStandardDuelButtonOver);
				m_WildDuelButton.RemoveEventListener(UIEventType.RELEASE, OnWildDuelButtonReleased);
				m_WildDuelButton.AddEventListener(UIEventType.ROLLOVER, OnWildDuelButtonOver);
				m_ClassicDuelButton.RemoveEventListener(UIEventType.RELEASE, OnClassicDuelButtonReleased);
				m_ClassicDuelButton.AddEventListener(UIEventType.ROLLOVER, OnClassicDuelButtonOver);
			}
			bool num3 = !netObject.Games.BattlegroundsFriendlyChallenge || flag2;
			bool flag3 = SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", activeIfDoesNotExist: false) && !AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetBattlegroundsEarlyAccessLicenseId());
			if (num3 || flag3)
			{
				DisableBaconButton();
			}
			if (!CollectionManager.Get().ShouldAccountSeeStandardWild())
			{
				m_StandardDuelButton.SetText("GLOBAL_FRIENDLIST_CHALLENGE_MENU_DUEL_BUTTON");
				m_TavernBrawlButton.gameObject.transform.localPosition = m_WildDuelButton.gameObject.transform.localPosition;
				float num4 = m_StandardDuelButton.transform.localPosition.y - m_TavernBrawlButton.transform.localPosition.y;
				m_BaconButton.gameObject.transform.localPosition = m_TavernBrawlButton.gameObject.transform.localPosition - Vector3.up * num4;
				float num5 = m_StandardDuelButton.transform.localPosition.y - m_ClassicDuelButton.transform.localPosition.y;
				m_AddAsFriendButton.gameObject.transform.localPosition = m_defaultAddAsFriendButtonPosition + Vector3.up * num5;
				m_WildDuelButton.gameObject.SetActive(value: false);
				m_ClassicDuelButton.gameObject.SetActive(value: false);
				num -= m_middleFrameScaleOffsetForWild * 2f;
				num2 -= m_middleShadowScaleOffsetForWild * 2f;
				m_MiddleFrame.transform.localScale = new Vector3(m_MiddleFrame.transform.localScale.x, num, m_MiddleFrame.transform.localScale.z);
				m_MiddleShadow.transform.localScale = new Vector3(m_MiddleShadow.transform.localScale.x, num2, m_MiddleShadow.transform.localScale.z);
			}
			m_bHasStandardDeck = CollectionManager.Get().AccountHasValidDeck(FormatType.FT_STANDARD);
			m_bHasWildDeck = CollectionManager.Get().AccountHasAnyValidDeck();
			m_bHasClassicDeck = CollectionManager.Get().AccountHasValidDeck(FormatType.FT_CLASSIC);
			m_bCanChallengeTavernBrawl = TavernBrawlManager.Get().CanChallengeToTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			m_bIsTavernBrawlUnlocked = TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			if (m_bCanChallengeTavernBrawl && flag)
			{
				TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
				m_bHasTavernBrawlDeck = !mission.canCreateDeck || TavernBrawlManager.Get().HasValidDeck(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			}
			else
			{
				DisableTavernBrawlButton();
			}
			SetupAddFriendButtonAndFixFrameLength(num, num2);
			m_FrameContainer.UpdateSlices();
			m_ShadowContainer.UpdateSlices();
			if (!CollectionManager.Get().AreAllDeckContentsReady() && CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded(OnDeckContents_UpdateButtons))
			{
				m_bHasStandardDeck = true;
				m_bHasWildDeck = true;
				m_bHasClassicDeck = true;
			}
		}
		else if (SpectatorManager.Get().CanSpectate(player))
		{
			m_SpectateButton.gameObject.SetActive(value: true);
			m_SpectateText.gameObject.SetActive(value: true);
			m_SpectateText.Text = GameStrings.Format("GLOBAL_FRIENDLIST_SPECTATE_MENU_DESCRIPTION", player.GetBestName());
			m_StandardDuelButton.gameObject.SetActive(value: false);
			m_WildDuelButton.gameObject.SetActive(value: false);
			m_ClassicDuelButton.gameObject.SetActive(value: false);
			m_TavernBrawlButton.gameObject.SetActive(value: false);
			m_BaconButton.gameObject.SetActive(value: false);
			num -= m_middleFrameScaleOffsetForSpectateMode;
			num2 -= m_middleShadowScaleOffsetForSpectateMode;
			SetupAddFriendButtonAndFixFrameLength(num, num2);
			m_FrameContainer.UpdateSlices();
			m_ShadowContainer.UpdateSlices();
		}
		UpdateActiveChallengeButtons();
	}

	public void InitializeMenuPosition()
	{
		base.transform.localPosition = CHALLENGE_MENU_DEFAULT_POSITION;
		Bounds bounds = base.gameObject.GetComponent<Collider>().bounds;
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Vector3 vector = camera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.min.y, bounds.center.z));
		if (vector.y < 0f)
		{
			Vector3 vector2 = camera.WorldToScreenPoint(base.transform.position);
			base.transform.position = camera.ScreenToWorldPoint(new Vector3(vector2.x, vector2.y - vector.y, vector2.z));
		}
	}

	private void DisableDuelButton(UIBButton buttonToDisable, GameObject disabledVisual)
	{
		disabledVisual.SetActive(value: true);
		buttonToDisable.gameObject.GetComponent<UIBHighlight>().m_MouseOverHighlight = null;
		buttonToDisable.AddEventListener(UIEventType.ROLLOUT, OnButtonOut);
	}

	private void DisableTavernBrawlButton()
	{
		DisableDuelButton(m_TavernBrawlButton, m_TavernBrawlButtonDisabled);
		m_TavernBrawlButton.RemoveEventListener(UIEventType.RELEASE, OnRegularTavernBrawlButtonReleased);
		m_TavernBrawlButton.AddEventListener(UIEventType.ROLLOVER, OnTavernBrawlButtonOver);
	}

	private void DisableBaconButton()
	{
		DisableDuelButton(m_BaconButton, m_BaconButtonDisabled);
		m_BaconButton.RemoveEventListener(UIEventType.RELEASE, OnBaconButtonReleased);
		m_BaconButton.AddEventListener(UIEventType.ROLLOVER, OnBaconButtonOver);
	}

	private void UpdateActiveChallengeButtons()
	{
		m_StandardDuelButtonX.SetActive(!m_bHasStandardDeck);
		m_WildDuelButtonX.SetActive(!m_bHasWildDeck);
		m_ClassicDuelButtonX.SetActive(!m_bHasClassicDeck);
		m_TavernBrawlButtonX.SetActive(m_bCanChallengeTavernBrawl && (!m_bIsTavernBrawlUnlocked || !m_bHasTavernBrawlDeck));
	}

	private void OnDeckContents_UpdateButtons()
	{
		if (!(this == null))
		{
			m_bHasStandardDeck = CollectionManager.Get().AccountHasValidDeck(FormatType.FT_STANDARD);
			m_bHasWildDeck = CollectionManager.Get().AccountHasAnyValidDeck();
			m_bHasClassicDeck = CollectionManager.Get().AccountHasValidDeck(FormatType.FT_CLASSIC);
			UpdateActiveChallengeButtons();
		}
	}

	private void OnStandardDuelButtonOver(UIEvent e)
	{
		string text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
		string text2 = GetDescriptionTextWhenUnavailable();
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
		ShowTooltip(text, text2, m_StandardDuelTooltipZone, m_StandardDuelButton);
	}

	private void OnWildDuelButtonOver(UIEvent e)
	{
		string text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
		string text2 = GetDescriptionTextWhenUnavailable();
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
		ShowTooltip(text, text2, m_WildDuelTooltipZone, m_WildDuelButton);
	}

	private void OnClassicDuelButtonOver(UIEvent e)
	{
		string text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
		string text2 = GetDescriptionTextWhenUnavailable();
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
		ShowTooltip(text, text2, m_ClassicDuelTooltipZone, m_ClassicDuelButton);
	}

	private void OnTavernBrawlButtonOver(UIEvent e)
	{
		string text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
		string text2 = GetDescriptionTextWhenUnavailable();
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
		ShowTooltip(text, text2, m_TavernBrawlTooltipZone, m_TavernBrawlButton);
	}

	private void OnBaconButtonOver(UIEvent e)
	{
		string text = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_HEADER";
		string text2 = GetDescriptionTextWhenUnavailable();
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.BattlegroundsFriendlyChallenge)
		{
			text2 = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_MODE_UNAVAILABLE";
		}
		else if (SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", activeIfDoesNotExist: false) && !AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetBattlegroundsEarlyAccessLicenseId()))
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
		ShowTooltip(text, text2, m_BaconTooltipZone, m_BaconButton);
	}

	private string GetDescriptionTextWhenUnavailable()
	{
		string result = "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_THEYRE_UNAVAILABLE";
		if (!FriendChallengeMgr.Get().AmIAvailable())
		{
			result = (BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline() ? "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_IM_APPEARING_OFFLINE" : ((!PartyManager.Get().IsInBattlegroundsParty() || PartyManager.Get().IsPartyLeader()) ? "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_IM_UNAVAILABLE" : "GLOBAL_FRIENDLIST_CHALLENGE_BUTTON_BATTLEGROUNDS_PARTY_MEMBER"));
		}
		return result;
	}

	private void OnButtonOut(UIEvent e)
	{
		HideTooltip();
	}

	private void OnStandardDuelButtonReleased(UIEvent e)
	{
		if (!m_bHasStandardDeck)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGER_NO_STANDARD_DECK"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
			m_challengeButton.CloseChallengeMenu();
		}
		else
		{
			FriendChallengeMgr.Get().SetChallengeMethod(FriendChallengeMgr.ChallengeMethod.FROM_FRIEND_LIST);
			BnetPlayer player = m_challengeButton.GetPlayer();
			FriendChallengeMgr.Get().SendChallenge(player, FormatType.FT_STANDARD, enableDeckShare: true);
			m_challengeButton.CloseFriendsListMenu();
		}
	}

	private void OnWildDuelButtonReleased(UIEvent e)
	{
		if (!m_bHasWildDeck)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGER_NO_DECK"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
			m_challengeButton.CloseChallengeMenu();
		}
		else
		{
			FriendChallengeMgr.Get().SetChallengeMethod(FriendChallengeMgr.ChallengeMethod.FROM_FRIEND_LIST);
			BnetPlayer player = m_challengeButton.GetPlayer();
			FriendChallengeMgr.Get().SendChallenge(player, FormatType.FT_WILD, enableDeckShare: true);
			m_challengeButton.CloseFriendsListMenu();
		}
	}

	private void OnClassicDuelButtonReleased(UIEvent e)
	{
		if (!m_bHasClassicDeck)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGER_NO_CLASSIC_DECK"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
			m_challengeButton.CloseChallengeMenu();
		}
		else
		{
			FriendChallengeMgr.Get().SetChallengeMethod(FriendChallengeMgr.ChallengeMethod.FROM_FRIEND_LIST);
			BnetPlayer player = m_challengeButton.GetPlayer();
			FriendChallengeMgr.Get().SendChallenge(player, FormatType.FT_CLASSIC, enableDeckShare: true);
			m_challengeButton.CloseFriendsListMenu();
		}
	}

	private void OnRegularTavernBrawlButtonReleased(UIEvent e)
	{
		if (!m_bIsTavernBrawlUnlocked)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGER_TAVERN_BRAWL_LOCKED"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
			m_challengeButton.CloseChallengeMenu();
		}
		else if (!m_bCanChallengeTavernBrawl)
		{
			AlertPopup.PopupInfo info2 = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_TAVERN_BRAWL_ERROR_SEASON_INCREMENTED"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info2);
			m_challengeButton.CloseChallengeMenu();
		}
		else if (!m_bHasTavernBrawlDeck)
		{
			FriendChallengeMgr.ShowChallengerNeedsToCreateTavernBrawlDeckAlert();
			m_challengeButton.CloseChallengeMenu();
		}
		else
		{
			TavernBrawlManager.Get().CurrentBrawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL;
			BnetPlayer player = m_challengeButton.GetPlayer();
			FriendChallengeMgr.Get().SendTavernBrawlChallenge(player, BrawlType.BRAWL_TYPE_TAVERN_BRAWL, TavernBrawlManager.Get().CurrentMission().seasonId, TavernBrawlManager.Get().CurrentMission().SelectedBrawlLibraryItemId);
			m_challengeButton.CloseFriendsListMenu();
		}
	}

	private void OnBaconButtonReleased(UIEvent e)
	{
		BnetPlayer player = m_challengeButton.GetPlayer();
		NavigateToSceneForPartyChallenge(SceneMgr.Mode.BACON);
		PartyManager.Get().SendInvite(PartyType.BATTLEGROUNDS_PARTY, player.GetBestGameAccountId());
		m_challengeButton.CloseChallengeMenu();
	}

	private void NavigateToSceneForPartyChallenge(SceneMgr.Mode nextMode)
	{
		GameMgr.Get().SetPendingAutoConcede(pendingAutoConcede: true);
		if (CollectionManager.Get().IsInEditMode())
		{
			CollectionManager.Get().GetEditedDeck()?.SendChanges();
		}
		SceneMgr.Get().SetNextMode(nextMode);
	}

	private void OnAddAsFriendButtonReleased(UIEvent e)
	{
		BnetPlayer player = m_challengeButton.GetPlayer();
		BnetFriendMgr.Get().SendInvite(player.GetBattleTag().GetString());
		m_challengeButton.CloseChallengeMenu();
	}

	private void OnSpectateButtonReleased(UIEvent e)
	{
		BnetPlayer player = m_challengeButton.GetPlayer();
		SpectatorManager.Get().SpectatePlayer(player);
		m_challengeButton.CloseChallengeMenu();
	}

	private void SetupAddFriendButtonAndFixFrameLength(float middleFrameScaleY, float middleShadowScaleY)
	{
		BnetPlayer player = m_challengeButton.GetPlayer();
		if (BnetFriendMgr.Get().IsFriend(player))
		{
			m_AddAsFriendButton.gameObject.SetActive(value: false);
			middleFrameScaleY -= m_middleFrameScaleOffsetForAddFriend;
			middleShadowScaleY -= m_middleShadowScaleOffsetForAddFriend;
		}
		else
		{
			m_AddAsFriendButton.gameObject.SetActive(value: true);
		}
		m_MiddleFrame.transform.localScale = new Vector3(m_MiddleFrame.transform.localScale.x, middleFrameScaleY, m_MiddleFrame.transform.localScale.z);
		m_MiddleShadow.transform.localScale = new Vector3(m_MiddleShadow.transform.localScale.x, middleShadowScaleY, m_MiddleShadow.transform.localScale.z);
	}

	private void ShowTooltip(string headerKey, string descriptionFormat, TooltipZone tooltipZone, UIBButton button)
	{
		string headline = GameStrings.Get(headerKey);
		BnetPlayer player = m_challengeButton.GetPlayer();
		string bodytext = GameStrings.Format(descriptionFormat, player.GetBestName());
		HideTooltip();
		tooltipZone.ShowSocialTooltip(button, headline, bodytext, 75f, GameLayer.BattleNetDialog);
		tooltipZone.AnchorTooltipTo(button.gameObject, Anchor.TOP_RIGHT, Anchor.TOP_LEFT);
	}

	private void UpdateTooltip()
	{
	}

	private void HideTooltip()
	{
		if (m_StandardDuelTooltipZone != null)
		{
			m_StandardDuelTooltipZone.HideTooltip();
		}
		if (m_WildDuelTooltipZone != null)
		{
			m_WildDuelTooltipZone.HideTooltip();
		}
		if (m_ClassicDuelTooltipZone != null)
		{
			m_ClassicDuelTooltipZone.HideTooltip();
		}
		if (m_TavernBrawlTooltipZone != null)
		{
			m_TavernBrawlTooltipZone.HideTooltip();
		}
		if (m_BaconTooltipZone != null)
		{
			m_BaconTooltipZone.HideTooltip();
		}
	}
}
