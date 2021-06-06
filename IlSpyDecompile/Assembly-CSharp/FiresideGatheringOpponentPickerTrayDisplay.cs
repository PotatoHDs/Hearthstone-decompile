using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class FiresideGatheringOpponentPickerTrayDisplay : MonoBehaviour
{
	public delegate void TrayLoaded();

	[CustomEditField(Sections = "UI")]
	public UberText m_trayLabel;

	[CustomEditField(Sections = "UI")]
	public StandardPegButtonNew m_backButton;

	[CustomEditField(Sections = "UI")]
	public PlayButton m_playButton;

	[CustomEditField(Sections = "UI")]
	public FiresideGatheringPlayButtonLantern m_FiresideGatheringPlayButtonLantern;

	[CustomEditField(Sections = "UI")]
	public PegUIElement m_inputBlocker;

	[CustomEditField(Sections = "Opponent Button Settings")]
	public FiresideGatheringOpponentButton m_OpponentButtonPrefab;

	[CustomEditField(Sections = "Opponent Button Settings")]
	public UIBScrollableItem m_HeaderPrefab;

	[CustomEditField(Sections = "Opponent Button Settings")]
	public UIBScrollableItem m_FooterPrefab;

	[CustomEditField(Sections = "Opponent Button Settings")]
	public GameObject m_OpponentButtonsContainer;

	[CustomEditField(Sections = "Opponent Button Settings")]
	public float m_HeaderHeight = 6f;

	[SerializeField]
	private float m_opponentButtonHeight = 5f;

	[CustomEditField(Sections = "Animation Settings")]
	public float m_trayInAnimationTime = 0.5f;

	[CustomEditField(Sections = "Animation Settings")]
	public float m_trayOutAnimationTime;

	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayInEaseType = iTween.EaseType.easeOutBounce;

	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayOutEaseType = iTween.EaseType.easeOutCubic;

	private static FiresideGatheringOpponentPickerTrayDisplay s_instance;

	private const float m_fadeOutTime = 0.2f;

	private List<FiresideGatheringOpponentButton> m_opponentButtons = new List<FiresideGatheringOpponentButton>();

	private UIBScrollableItem m_footer;

	private List<Achievement> m_lockedHeroes;

	private FiresideGatheringOpponentButton m_selectedOpponentButton;

	private List<TrayLoaded> m_TrayLoadedListeners = new List<TrayLoaded>();

	private List<Action> m_trayHiddenListeners = new List<Action>();

	private bool m_buttonsReady;

	private bool m_shown;

	[CustomEditField(Sections = "Opponent Button Settings")]
	public float OpponentButtonHeight
	{
		get
		{
			return m_opponentButtonHeight;
		}
		set
		{
			m_opponentButtonHeight = value;
			UpdateOpponentButtonPositions();
		}
	}

	private void Awake()
	{
		s_instance = this;
		Transform[] components = base.gameObject.GetComponents<Transform>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].gameObject.SetActive(value: false);
		}
		base.gameObject.SetActive(value: true);
		if (m_backButton != null)
		{
			m_backButton.SetText(GameStrings.Get("GLOBAL_BACK"));
			m_backButton.AddEventListener(UIEventType.RELEASE, BackButtonReleased);
		}
		m_trayLabel.Text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_CHOOSE_OPPONENT");
		SceneMgr.Get().RegisterScenePreUnloadEvent(HideTrayWhenLeavingScreen);
		m_playButton.AddEventListener(UIEventType.RELEASE, PlayGameButtonRelease);
		m_FiresideGatheringPlayButtonLantern.gameObject.SetActive(value: false);
		m_inputBlocker.AddEventListener(UIEventType.RELEASE, BackButtonReleased);
		SoundManager.Get().Load("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880");
		SoundManager.Get().Load("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf");
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
		StartCoroutine(NotifyWhenTrayLoaded());
	}

	private void OnDestroy()
	{
		s_instance = null;
		FriendChallengeMgr.Get().RemoveChangedListener(OnFriendChallengeChanged);
		m_inputBlocker.RemoveEventListener(UIEventType.RELEASE, BackButtonReleased);
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterScenePreUnloadEvent(HideTrayWhenLeavingScreen);
		}
		GameMgr.Get().UnregisterFindGameEvent(OnFindGameEvent);
	}

	private void Start()
	{
		m_playButton.SetOriginalLocalPosition();
		SetPlayButtonEnabled(enabled: false);
	}

	public static FiresideGatheringOpponentPickerTrayDisplay Get()
	{
		return s_instance;
	}

	public void Init()
	{
		m_footer = (UIBScrollableItem)GameUtils.Instantiate(m_FooterPrefab, m_OpponentButtonsContainer);
		SceneUtils.SetLayer(m_footer, m_OpponentButtonsContainer.gameObject.layer);
	}

	public void Show()
	{
		m_shown = true;
		iTween.Stop(base.gameObject);
		base.gameObject.SetActive(value: true);
		UpdateOpponentButtons();
		Transform[] components = base.gameObject.GetComponents<Transform>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].gameObject.SetActive(value: true);
		}
		Hashtable args = iTween.Hash("position", FiresideGatheringDisplay.Get().GetOpponentPickerShowPosition(), "isLocal", true, "time", m_trayInAnimationTime, "easetype", m_trayInEaseType, "delay", 0.001f);
		iTween.MoveTo(base.gameObject, args);
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880");
		if (m_selectedOpponentButton != null)
		{
			SetPlayButtonEnabled(enabled: true);
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			FadeMobileBlurEffectsIn();
		}
		m_inputBlocker.gameObject.SetActive(value: true);
		Navigation.Push(OnNavigateBack);
	}

	private void Hide()
	{
		m_shown = false;
		iTween.Stop(base.gameObject);
		Hashtable args = iTween.Hash("position", FiresideGatheringDisplay.Get().GetOpponentPickerHidePosition(), "isLocal", true, "time", m_trayOutAnimationTime, "easetype", m_trayOutEaseType, "oncomplete", (Action<object>)delegate
		{
			base.gameObject.SetActive(value: false);
		}, "delay", 0.001f);
		iTween.MoveTo(base.gameObject, args);
		m_inputBlocker.gameObject.SetActive(value: false);
		FireTrayHiddenListeners();
		m_trayHiddenListeners.Clear();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			FadeMobileBlurEffectsOut();
		}
		FriendChallengeMgr.Get().RemoveChangedListener(OnFriendChallengeChanged);
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf");
	}

	public void OnGameDenied()
	{
		UpdateOpponentButtons();
	}

	public bool IsShown()
	{
		return m_shown;
	}

	public void AddTrayLoadedListener(TrayLoaded dlg)
	{
		m_TrayLoadedListeners.Add(dlg);
	}

	public void RemoveTrayLoadedListener(TrayLoaded dlg)
	{
		m_TrayLoadedListeners.Remove(dlg);
	}

	public bool IsLoaded()
	{
		return m_buttonsReady;
	}

	public void RegisterTrayHiddenListener(Action listener)
	{
		if (!m_trayHiddenListeners.Contains(listener))
		{
			m_trayHiddenListeners.Add(listener);
		}
	}

	public void UnregisterTrayHiddenListener(Action listener)
	{
		if (m_trayHiddenListeners.Contains(listener))
		{
			m_trayHiddenListeners.Remove(listener);
		}
	}

	public void FireTrayHiddenListeners()
	{
		foreach (Action trayHiddenListener in m_trayHiddenListeners)
		{
			trayHiddenListener();
		}
	}

	private void SetSelectedButton(FiresideGatheringOpponentButton button)
	{
		if (m_selectedOpponentButton != null)
		{
			m_selectedOpponentButton.Deselect();
		}
		m_selectedOpponentButton = button;
	}

	private void DisableOpponentButtons()
	{
		for (int i = 0; i < m_opponentButtons.Count; i++)
		{
			m_opponentButtons[i].SetEnabled(enabled: false);
		}
	}

	private void EnableOpponentButtons()
	{
		for (int i = 0; i < m_opponentButtons.Count; i++)
		{
			m_opponentButtons[i].SetEnabled(enabled: true);
		}
	}

	private bool OnNavigateBack()
	{
		Hide();
		if (DeckPickerTray.GetTray() != null)
		{
			DeckPickerTray.GetTray().ResetCurrentMode();
		}
		return true;
	}

	private void BackButtonReleased(UIEvent e)
	{
		FriendChallengeMgr.Get().ClearSelectedDeckAndHeroBeforeSendingChallenge();
		Navigation.GoBack();
	}

	private void PlayGameButtonRelease(UIEvent e)
	{
		e.GetElement().SetEnabled(enabled: false);
		DisableOpponentButtons();
		FriendChallengeMgr.Get().AddChangedListener(OnFriendChallengeChanged);
		BnetPlayer associatedBnetPlayer = m_selectedOpponentButton.AssociatedBnetPlayer;
		TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
		if (!associatedBnetPlayer.GetHearthstoneGameAccount().CanBeInvitedToGame() || !associatedBnetPlayer.IsOnline() || !FiresideGatheringManager.Get().OpponentHasValidDeckForSelectedPlaymode(associatedBnetPlayer))
		{
			Navigation.GoBack();
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_FIRESIDE_GATHERING");
			popupInfo.m_text = (associatedBnetPlayer.IsOnline() ? GameStrings.Get("GLUE_FIRESIDE_GATHERING_OPPONENT_UNAVAILABLE") : GameStrings.Get("GLUE_FIRESIDE_GATHERING_OPPONENT_OFFLINE"));
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			popupInfo.m_responseCallback = OnOpponentUnavailableResponse;
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		FriendChallengeMgr.Get().SetChallengeMethod(FriendChallengeMgr.ChallengeMethod.FROM_FIRESIDE_GATHERING_OPPONENT_PICKER);
		if (FriendChallengeMgr.Get().HasChallenge() && FriendChallengeMgr.Get().GetMyOpponent() != associatedBnetPlayer)
		{
			FriendChallengeMgr.Get().CancelChallenge();
		}
		switch (FiresideGatheringManager.Get().CurrentFiresideGatheringMode)
		{
		case FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL:
			FriendChallengeMgr.Get().SendTavernBrawlChallenge(associatedBnetPlayer, BrawlType.BRAWL_TYPE_TAVERN_BRAWL, tavernBrawlMission.seasonId, tavernBrawlMission.SelectedBrawlLibraryItemId);
			break;
		case FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL:
			FriendChallengeMgr.Get().SendTavernBrawlChallenge(associatedBnetPlayer, BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING, tavernBrawlMission.seasonId, tavernBrawlMission.SelectedBrawlLibraryItemId);
			break;
		case FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE:
		{
			FormatType @enum = Options.Get().GetEnum<FormatType>(Option.FORMAT_TYPE);
			FriendChallengeMgr.Get().SendChallenge(associatedBnetPlayer, @enum, enableDeckShare: false);
			break;
		}
		}
	}

	private void OnOpponentUnavailableResponse(AlertPopup.Response response, object userData)
	{
		if (!(this == null) && !(this != s_instance))
		{
			Show();
		}
	}

	private void OpponentButtonPressed(UIEvent e)
	{
		FiresideGatheringOpponentButton firesideGatheringOpponentButton = (FiresideGatheringOpponentButton)e.GetElement();
		SetSelectedButton(firesideGatheringOpponentButton);
		SetPlayButtonEnabled(enabled: true);
		firesideGatheringOpponentButton.Select();
	}

	private void UpdateOpponentButtons()
	{
		UpdateOpponentPlayerButtons();
		UpdateOpponentButtonPositions();
		if (m_selectedOpponentButton == null)
		{
			SetPlayButtonEnabled(enabled: false);
		}
		else
		{
			SetPlayButtonEnabled(enabled: true);
		}
	}

	private void UpdateOpponentButtonPositions()
	{
		for (int i = 0; i < m_opponentButtons.Count; i++)
		{
			TransformUtil.SetLocalPosZ(m_opponentButtons[i], (0f - m_opponentButtonHeight) * (float)i);
		}
		TransformUtil.SetLocalPosZ(m_footer, (0f - m_opponentButtonHeight) * (float)m_opponentButtons.Count);
	}

	private void UpdateOpponentPlayerButtons()
	{
		List<BnetPlayer> list = new List<BnetPlayer>();
		if (FiresideGatheringManager.Get() != null)
		{
			foreach (BnetPlayer displayablePatron in FiresideGatheringManager.Get().DisplayablePatronList)
			{
				if (displayablePatron != null && !(displayablePatron.GetHearthstoneGameAccount() == null) && FiresideGatheringManager.Get().OpponentHasValidDeckForSelectedPlaymode(displayablePatron) && displayablePatron.GetHearthstoneGameAccount().CanBeInvitedToGame())
				{
					list.Add(displayablePatron);
				}
			}
			list.Sort(FiresideGatheringManager.Get().FiresideGatheringPlayerSort);
		}
		int num = Mathf.Min(FiresideGatheringPresenceManager.MAX_SUBSCRIBED_PATRONS, list.Count());
		if (m_opponentButtons.Count < num)
		{
			for (int i = m_opponentButtons.Count; i < num; i++)
			{
				FiresideGatheringOpponentButton firesideGatheringOpponentButton = (FiresideGatheringOpponentButton)GameUtils.Instantiate(m_OpponentButtonPrefab, m_OpponentButtonsContainer);
				SceneUtils.SetLayer(firesideGatheringOpponentButton, m_OpponentButtonsContainer.gameObject.layer);
				m_opponentButtons.Add(firesideGatheringOpponentButton);
				firesideGatheringOpponentButton.SetOriginalLocalPosition();
				firesideGatheringOpponentButton.AddEventListener(UIEventType.RELEASE, OpponentButtonPressed);
			}
		}
		else if (m_opponentButtons.Count > num)
		{
			for (int num2 = m_opponentButtons.Count; num2 > num; num2--)
			{
				UnityEngine.Object.Destroy(m_opponentButtons[num2 - 1].gameObject);
				m_opponentButtons.RemoveAt(num2 - 1);
			}
		}
		m_buttonsReady = true;
		bool flag = false;
		for (int j = 0; j < m_opponentButtons.Count; j++)
		{
			if (j < list.Count)
			{
				BnetPlayer bnetPlayer = list[j];
				FiresideGatheringOpponentButton firesideGatheringOpponentButton2 = m_opponentButtons[j];
				firesideGatheringOpponentButton2.SetIsFriend(BnetFriendMgr.Get().IsFriend(bnetPlayer));
				firesideGatheringOpponentButton2.SetIsFiresideBrawl(FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL);
				firesideGatheringOpponentButton2.SetName(bnetPlayer.GetBestName());
				firesideGatheringOpponentButton2.AssociatedBnetPlayer = bnetPlayer;
				if (m_selectedOpponentButton != null && bnetPlayer == m_selectedOpponentButton.AssociatedBnetPlayer)
				{
					firesideGatheringOpponentButton2.Select();
					flag = true;
				}
				else
				{
					firesideGatheringOpponentButton2.Deselect();
				}
				continue;
			}
			Debug.LogError("Attempting to update more buttons than there are patrons and friends.");
			return;
		}
		if (!flag)
		{
			m_selectedOpponentButton = null;
		}
		FiresideGatheringOpponentPickerFooter component = m_footer.GetComponent<FiresideGatheringOpponentPickerFooter>();
		if (component != null)
		{
			component.SetTextOnFooter(list.Count == 0);
		}
	}

	private IEnumerator NotifyWhenTrayLoaded()
	{
		while (!m_buttonsReady)
		{
			yield return null;
		}
		FireTrayLoadedEvent();
	}

	private void FireTrayLoadedEvent()
	{
		TrayLoaded[] array = m_TrayLoadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	private void SetPlayButtonEnabled(bool enabled)
	{
		if (enabled)
		{
			m_playButton.Enable();
		}
		else
		{
			m_playButton.Disable();
		}
		if (FiresideGatheringDisplay.Get() != null)
		{
			m_FiresideGatheringPlayButtonLantern.gameObject.SetActive(FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL);
			m_FiresideGatheringPlayButtonLantern.SetLanternLit(enabled);
		}
	}

	private void OnFriendChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		if (challengeEvent != FriendChallengeEvent.I_RESCINDED_CHALLENGE && challengeEvent != FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE && challengeEvent != FriendChallengeEvent.OPPONENT_DECLINED_CHALLENGE && challengeEvent != FriendChallengeEvent.OPPONENT_REMOVED_FROM_FRIENDS && challengeEvent != FriendChallengeEvent.QUEUE_CANCELED)
		{
			return;
		}
		EnableOpponentButtons();
		SetPlayButtonEnabled(enabled: true);
		if (DeckPickerTrayDisplay.Get() != null)
		{
			long num = 0L;
			long num2 = 0L;
			if (challengeData.DidSendChallenge)
			{
				num = challengeData.m_challengerDeckId;
				num2 = challengeData.m_challengerHeroId;
			}
			else
			{
				num = challengeData.m_challengeeDeckId;
				num2 = challengeData.m_challengeeHeroId;
			}
			if (num != 0L)
			{
				FriendChallengeMgr.Get().SelectDeckBeforeSendingChallenge(num);
			}
			if (num2 != 0L)
			{
				FriendChallengeMgr.Get().SelectHeroBeforeSendingChallenge(num2);
			}
		}
		FriendChallengeMgr.Get().RemoveChangedListener(OnFriendChallengeChanged);
	}

	private void FadeMobileBlurEffectsIn()
	{
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		SceneUtils.SetLayer(Box.Get().m_letterboxingContainer, GameLayer.IgnoreFullScreenEffects);
		FullScreenFXMgr.Get().StartStandardBlurVignette(0f);
	}

	private void FadeMobileBlurEffectsOut()
	{
		FullScreenFXMgr.Get().EndStandardBlurVignette(0.2f, OnFadeMobileBlurEffectsOutFinished);
	}

	private void OnFadeMobileBlurEffectsOutFinished()
	{
		if (!(this == null) && !(base.gameObject == null) && !(Box.Get() == null))
		{
			SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
			SceneUtils.SetLayer(Box.Get().m_letterboxingContainer, GameLayer.Default);
		}
	}

	private void HideTrayWhenLeavingScreen(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		Hide();
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		if (!IsShown())
		{
			return false;
		}
		FindGameState state = eventData.m_state;
		if ((uint)(state - 2) <= 1u || (uint)(state - 7) <= 1u || state == FindGameState.SERVER_GAME_CANCELED)
		{
			FriendChallengeMgr.Get().CancelChallenge();
			Navigation.RemoveHandler(OnNavigateBack);
			OnNavigateBack();
		}
		return false;
	}
}
