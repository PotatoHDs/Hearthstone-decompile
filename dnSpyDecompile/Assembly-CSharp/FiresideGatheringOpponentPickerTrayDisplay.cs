using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;
using UnityEngine;

// Token: 0x020002EE RID: 750
[CustomEditClass]
public class FiresideGatheringOpponentPickerTrayDisplay : MonoBehaviour
{
	// Token: 0x170004F5 RID: 1269
	// (get) Token: 0x060027DF RID: 10207 RVA: 0x000C83F5 File Offset: 0x000C65F5
	// (set) Token: 0x060027E0 RID: 10208 RVA: 0x000C83FD File Offset: 0x000C65FD
	[CustomEditField(Sections = "Opponent Button Settings")]
	public float OpponentButtonHeight
	{
		get
		{
			return this.m_opponentButtonHeight;
		}
		set
		{
			this.m_opponentButtonHeight = value;
			this.UpdateOpponentButtonPositions();
		}
	}

	// Token: 0x060027E1 RID: 10209 RVA: 0x000C840C File Offset: 0x000C660C
	private void Awake()
	{
		FiresideGatheringOpponentPickerTrayDisplay.s_instance = this;
		Transform[] components = base.gameObject.GetComponents<Transform>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].gameObject.SetActive(false);
		}
		base.gameObject.SetActive(true);
		if (this.m_backButton != null)
		{
			this.m_backButton.SetText(GameStrings.Get("GLOBAL_BACK"));
			this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.BackButtonReleased));
		}
		this.m_trayLabel.Text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_CHOOSE_OPPONENT");
		SceneMgr.Get().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.HideTrayWhenLeavingScreen));
		this.m_playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.PlayGameButtonRelease));
		this.m_FiresideGatheringPlayButtonLantern.gameObject.SetActive(false);
		this.m_inputBlocker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.BackButtonReleased));
		SoundManager.Get().Load("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880");
		SoundManager.Get().Load("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf");
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		base.StartCoroutine(this.NotifyWhenTrayLoaded());
	}

	// Token: 0x060027E2 RID: 10210 RVA: 0x000C854C File Offset: 0x000C674C
	private void OnDestroy()
	{
		FiresideGatheringOpponentPickerTrayDisplay.s_instance = null;
		FriendChallengeMgr.Get().RemoveChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
		this.m_inputBlocker.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.BackButtonReleased));
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.HideTrayWhenLeavingScreen));
		}
		GameMgr.Get().UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
	}

	// Token: 0x060027E3 RID: 10211 RVA: 0x000C85C4 File Offset: 0x000C67C4
	private void Start()
	{
		this.m_playButton.SetOriginalLocalPosition();
		this.SetPlayButtonEnabled(false);
	}

	// Token: 0x060027E4 RID: 10212 RVA: 0x000C85D8 File Offset: 0x000C67D8
	public static FiresideGatheringOpponentPickerTrayDisplay Get()
	{
		return FiresideGatheringOpponentPickerTrayDisplay.s_instance;
	}

	// Token: 0x060027E5 RID: 10213 RVA: 0x000C85DF File Offset: 0x000C67DF
	public void Init()
	{
		this.m_footer = (UIBScrollableItem)GameUtils.Instantiate(this.m_FooterPrefab, this.m_OpponentButtonsContainer, false);
		SceneUtils.SetLayer(this.m_footer, this.m_OpponentButtonsContainer.gameObject.layer);
	}

	// Token: 0x060027E6 RID: 10214 RVA: 0x000C861C File Offset: 0x000C681C
	public void Show()
	{
		this.m_shown = true;
		iTween.Stop(base.gameObject);
		base.gameObject.SetActive(true);
		this.UpdateOpponentButtons();
		Transform[] components = base.gameObject.GetComponents<Transform>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].gameObject.SetActive(true);
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			FiresideGatheringDisplay.Get().GetOpponentPickerShowPosition(),
			"isLocal",
			true,
			"time",
			this.m_trayInAnimationTime,
			"easetype",
			this.m_trayInEaseType,
			"delay",
			0.001f
		});
		iTween.MoveTo(base.gameObject, args);
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880");
		if (this.m_selectedOpponentButton != null)
		{
			this.SetPlayButtonEnabled(true);
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			this.FadeMobileBlurEffectsIn();
		}
		this.m_inputBlocker.gameObject.SetActive(true);
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
	}

	// Token: 0x060027E7 RID: 10215 RVA: 0x000C8758 File Offset: 0x000C6958
	private void Hide()
	{
		this.m_shown = false;
		iTween.Stop(base.gameObject);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			FiresideGatheringDisplay.Get().GetOpponentPickerHidePosition(),
			"isLocal",
			true,
			"time",
			this.m_trayOutAnimationTime,
			"easetype",
			this.m_trayOutEaseType,
			"oncomplete",
			new Action<object>(delegate(object e)
			{
				base.gameObject.SetActive(false);
			}),
			"delay",
			0.001f
		});
		iTween.MoveTo(base.gameObject, args);
		this.m_inputBlocker.gameObject.SetActive(false);
		this.FireTrayHiddenListeners();
		this.m_trayHiddenListeners.Clear();
		if (UniversalInputManager.UsePhoneUI)
		{
			this.FadeMobileBlurEffectsOut();
		}
		FriendChallengeMgr.Get().RemoveChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf");
	}

	// Token: 0x060027E8 RID: 10216 RVA: 0x000C8875 File Offset: 0x000C6A75
	public void OnGameDenied()
	{
		this.UpdateOpponentButtons();
	}

	// Token: 0x060027E9 RID: 10217 RVA: 0x000C887D File Offset: 0x000C6A7D
	public bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x060027EA RID: 10218 RVA: 0x000C8885 File Offset: 0x000C6A85
	public void AddTrayLoadedListener(FiresideGatheringOpponentPickerTrayDisplay.TrayLoaded dlg)
	{
		this.m_TrayLoadedListeners.Add(dlg);
	}

	// Token: 0x060027EB RID: 10219 RVA: 0x000C8893 File Offset: 0x000C6A93
	public void RemoveTrayLoadedListener(FiresideGatheringOpponentPickerTrayDisplay.TrayLoaded dlg)
	{
		this.m_TrayLoadedListeners.Remove(dlg);
	}

	// Token: 0x060027EC RID: 10220 RVA: 0x000C88A2 File Offset: 0x000C6AA2
	public bool IsLoaded()
	{
		return this.m_buttonsReady;
	}

	// Token: 0x060027ED RID: 10221 RVA: 0x000C88AA File Offset: 0x000C6AAA
	public void RegisterTrayHiddenListener(Action listener)
	{
		if (!this.m_trayHiddenListeners.Contains(listener))
		{
			this.m_trayHiddenListeners.Add(listener);
		}
	}

	// Token: 0x060027EE RID: 10222 RVA: 0x000C88C6 File Offset: 0x000C6AC6
	public void UnregisterTrayHiddenListener(Action listener)
	{
		if (this.m_trayHiddenListeners.Contains(listener))
		{
			this.m_trayHiddenListeners.Remove(listener);
		}
	}

	// Token: 0x060027EF RID: 10223 RVA: 0x000C88E4 File Offset: 0x000C6AE4
	public void FireTrayHiddenListeners()
	{
		foreach (Action action in this.m_trayHiddenListeners)
		{
			action();
		}
	}

	// Token: 0x060027F0 RID: 10224 RVA: 0x000C8934 File Offset: 0x000C6B34
	private void SetSelectedButton(FiresideGatheringOpponentButton button)
	{
		if (this.m_selectedOpponentButton != null)
		{
			this.m_selectedOpponentButton.Deselect();
		}
		this.m_selectedOpponentButton = button;
	}

	// Token: 0x060027F1 RID: 10225 RVA: 0x000C8958 File Offset: 0x000C6B58
	private void DisableOpponentButtons()
	{
		for (int i = 0; i < this.m_opponentButtons.Count; i++)
		{
			this.m_opponentButtons[i].SetEnabled(false, false);
		}
	}

	// Token: 0x060027F2 RID: 10226 RVA: 0x000C8990 File Offset: 0x000C6B90
	private void EnableOpponentButtons()
	{
		for (int i = 0; i < this.m_opponentButtons.Count; i++)
		{
			this.m_opponentButtons[i].SetEnabled(true, false);
		}
	}

	// Token: 0x060027F3 RID: 10227 RVA: 0x000C89C6 File Offset: 0x000C6BC6
	private bool OnNavigateBack()
	{
		this.Hide();
		if (DeckPickerTray.GetTray() != null)
		{
			DeckPickerTray.GetTray().ResetCurrentMode();
		}
		return true;
	}

	// Token: 0x060027F4 RID: 10228 RVA: 0x000C89E6 File Offset: 0x000C6BE6
	private void BackButtonReleased(UIEvent e)
	{
		FriendChallengeMgr.Get().ClearSelectedDeckAndHeroBeforeSendingChallenge();
		Navigation.GoBack();
	}

	// Token: 0x060027F5 RID: 10229 RVA: 0x000C89F8 File Offset: 0x000C6BF8
	private void PlayGameButtonRelease(UIEvent e)
	{
		e.GetElement().SetEnabled(false, false);
		this.DisableOpponentButtons();
		FriendChallengeMgr.Get().AddChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
		BnetPlayer associatedBnetPlayer = this.m_selectedOpponentButton.AssociatedBnetPlayer;
		TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
		if (!associatedBnetPlayer.GetHearthstoneGameAccount().CanBeInvitedToGame() || !associatedBnetPlayer.IsOnline() || !FiresideGatheringManager.Get().OpponentHasValidDeckForSelectedPlaymode(associatedBnetPlayer))
		{
			Navigation.GoBack();
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_FIRESIDE_GATHERING");
			popupInfo.m_text = (associatedBnetPlayer.IsOnline() ? GameStrings.Get("GLUE_FIRESIDE_GATHERING_OPPONENT_UNAVAILABLE") : GameStrings.Get("GLUE_FIRESIDE_GATHERING_OPPONENT_OFFLINE"));
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnOpponentUnavailableResponse);
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
		case FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE:
		{
			FormatType @enum = Options.Get().GetEnum<FormatType>(Option.FORMAT_TYPE);
			FriendChallengeMgr.Get().SendChallenge(associatedBnetPlayer, @enum, false);
			return;
		}
		case FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL:
			FriendChallengeMgr.Get().SendTavernBrawlChallenge(associatedBnetPlayer, BrawlType.BRAWL_TYPE_TAVERN_BRAWL, tavernBrawlMission.seasonId, tavernBrawlMission.SelectedBrawlLibraryItemId);
			return;
		case FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL:
			FriendChallengeMgr.Get().SendTavernBrawlChallenge(associatedBnetPlayer, BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING, tavernBrawlMission.seasonId, tavernBrawlMission.SelectedBrawlLibraryItemId);
			return;
		default:
			return;
		}
	}

	// Token: 0x060027F6 RID: 10230 RVA: 0x000C8B76 File Offset: 0x000C6D76
	private void OnOpponentUnavailableResponse(AlertPopup.Response response, object userData)
	{
		if (this == null || this != FiresideGatheringOpponentPickerTrayDisplay.s_instance)
		{
			return;
		}
		this.Show();
	}

	// Token: 0x060027F7 RID: 10231 RVA: 0x000C8B98 File Offset: 0x000C6D98
	private void OpponentButtonPressed(UIEvent e)
	{
		FiresideGatheringOpponentButton firesideGatheringOpponentButton = (FiresideGatheringOpponentButton)e.GetElement();
		this.SetSelectedButton(firesideGatheringOpponentButton);
		this.SetPlayButtonEnabled(true);
		firesideGatheringOpponentButton.Select();
	}

	// Token: 0x060027F8 RID: 10232 RVA: 0x000C8BC5 File Offset: 0x000C6DC5
	private void UpdateOpponentButtons()
	{
		this.UpdateOpponentPlayerButtons();
		this.UpdateOpponentButtonPositions();
		if (this.m_selectedOpponentButton == null)
		{
			this.SetPlayButtonEnabled(false);
			return;
		}
		this.SetPlayButtonEnabled(true);
	}

	// Token: 0x060027F9 RID: 10233 RVA: 0x000C8BF0 File Offset: 0x000C6DF0
	private void UpdateOpponentButtonPositions()
	{
		for (int i = 0; i < this.m_opponentButtons.Count; i++)
		{
			TransformUtil.SetLocalPosZ(this.m_opponentButtons[i], -this.m_opponentButtonHeight * (float)i);
		}
		TransformUtil.SetLocalPosZ(this.m_footer, -this.m_opponentButtonHeight * (float)this.m_opponentButtons.Count);
	}

	// Token: 0x060027FA RID: 10234 RVA: 0x000C8C50 File Offset: 0x000C6E50
	private void UpdateOpponentPlayerButtons()
	{
		List<BnetPlayer> list = new List<BnetPlayer>();
		if (FiresideGatheringManager.Get() != null)
		{
			foreach (BnetPlayer bnetPlayer in FiresideGatheringManager.Get().DisplayablePatronList)
			{
				if (bnetPlayer != null && !(bnetPlayer.GetHearthstoneGameAccount() == null) && FiresideGatheringManager.Get().OpponentHasValidDeckForSelectedPlaymode(bnetPlayer) && bnetPlayer.GetHearthstoneGameAccount().CanBeInvitedToGame())
				{
					list.Add(bnetPlayer);
				}
			}
			list.Sort(new Comparison<BnetPlayer>(FiresideGatheringManager.Get().FiresideGatheringPlayerSort));
		}
		int num = Mathf.Min(FiresideGatheringPresenceManager.MAX_SUBSCRIBED_PATRONS, list.Count<BnetPlayer>());
		if (this.m_opponentButtons.Count < num)
		{
			for (int i = this.m_opponentButtons.Count; i < num; i++)
			{
				FiresideGatheringOpponentButton firesideGatheringOpponentButton = (FiresideGatheringOpponentButton)GameUtils.Instantiate(this.m_OpponentButtonPrefab, this.m_OpponentButtonsContainer, false);
				SceneUtils.SetLayer(firesideGatheringOpponentButton, this.m_OpponentButtonsContainer.gameObject.layer);
				this.m_opponentButtons.Add(firesideGatheringOpponentButton);
				firesideGatheringOpponentButton.SetOriginalLocalPosition();
				firesideGatheringOpponentButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OpponentButtonPressed));
			}
		}
		else if (this.m_opponentButtons.Count > num)
		{
			for (int j = this.m_opponentButtons.Count; j > num; j--)
			{
				UnityEngine.Object.Destroy(this.m_opponentButtons[j - 1].gameObject);
				this.m_opponentButtons.RemoveAt(j - 1);
			}
		}
		this.m_buttonsReady = true;
		bool flag = false;
		for (int k = 0; k < this.m_opponentButtons.Count; k++)
		{
			if (k >= list.Count)
			{
				Debug.LogError("Attempting to update more buttons than there are patrons and friends.");
				return;
			}
			BnetPlayer bnetPlayer2 = list[k];
			FiresideGatheringOpponentButton firesideGatheringOpponentButton2 = this.m_opponentButtons[k];
			firesideGatheringOpponentButton2.SetIsFriend(BnetFriendMgr.Get().IsFriend(bnetPlayer2));
			firesideGatheringOpponentButton2.SetIsFiresideBrawl(FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL);
			firesideGatheringOpponentButton2.SetName(bnetPlayer2.GetBestName());
			firesideGatheringOpponentButton2.AssociatedBnetPlayer = bnetPlayer2;
			if (this.m_selectedOpponentButton != null && bnetPlayer2 == this.m_selectedOpponentButton.AssociatedBnetPlayer)
			{
				firesideGatheringOpponentButton2.Select();
				flag = true;
			}
			else
			{
				firesideGatheringOpponentButton2.Deselect();
			}
		}
		if (!flag)
		{
			this.m_selectedOpponentButton = null;
		}
		FiresideGatheringOpponentPickerFooter component = this.m_footer.GetComponent<FiresideGatheringOpponentPickerFooter>();
		if (component != null)
		{
			component.SetTextOnFooter(list.Count == 0);
		}
	}

	// Token: 0x060027FB RID: 10235 RVA: 0x000C8EDC File Offset: 0x000C70DC
	private IEnumerator NotifyWhenTrayLoaded()
	{
		while (!this.m_buttonsReady)
		{
			yield return null;
		}
		this.FireTrayLoadedEvent();
		yield break;
	}

	// Token: 0x060027FC RID: 10236 RVA: 0x000C8EEC File Offset: 0x000C70EC
	private void FireTrayLoadedEvent()
	{
		FiresideGatheringOpponentPickerTrayDisplay.TrayLoaded[] array = this.m_TrayLoadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x060027FD RID: 10237 RVA: 0x000C8F1C File Offset: 0x000C711C
	private void SetPlayButtonEnabled(bool enabled)
	{
		if (enabled)
		{
			this.m_playButton.Enable();
		}
		else
		{
			this.m_playButton.Disable(false);
		}
		if (FiresideGatheringDisplay.Get() != null)
		{
			this.m_FiresideGatheringPlayButtonLantern.gameObject.SetActive(FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL);
			this.m_FiresideGatheringPlayButtonLantern.SetLanternLit(enabled);
		}
	}

	// Token: 0x060027FE RID: 10238 RVA: 0x000C8F7C File Offset: 0x000C717C
	private void OnFriendChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		if (challengeEvent == FriendChallengeEvent.I_RESCINDED_CHALLENGE || challengeEvent == FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE || challengeEvent == FriendChallengeEvent.OPPONENT_DECLINED_CHALLENGE || challengeEvent == FriendChallengeEvent.OPPONENT_REMOVED_FROM_FRIENDS || challengeEvent == FriendChallengeEvent.QUEUE_CANCELED)
		{
			this.EnableOpponentButtons();
			this.SetPlayButtonEnabled(true);
			if (DeckPickerTrayDisplay.Get() != null)
			{
				long num;
				long num2;
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
			FriendChallengeMgr.Get().RemoveChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
		}
	}

	// Token: 0x060027FF RID: 10239 RVA: 0x000C9019 File Offset: 0x000C7219
	private void FadeMobileBlurEffectsIn()
	{
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		SceneUtils.SetLayer(Box.Get().m_letterboxingContainer, GameLayer.IgnoreFullScreenEffects);
		FullScreenFXMgr.Get().StartStandardBlurVignette(0f);
	}

	// Token: 0x06002800 RID: 10240 RVA: 0x000C9048 File Offset: 0x000C7248
	private void FadeMobileBlurEffectsOut()
	{
		FullScreenFXMgr.Get().EndStandardBlurVignette(0.2f, new Action(this.OnFadeMobileBlurEffectsOutFinished));
	}

	// Token: 0x06002801 RID: 10241 RVA: 0x000C9068 File Offset: 0x000C7268
	private void OnFadeMobileBlurEffectsOutFinished()
	{
		if (this == null || base.gameObject == null || Box.Get() == null)
		{
			return;
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		SceneUtils.SetLayer(Box.Get().m_letterboxingContainer, GameLayer.Default);
	}

	// Token: 0x06002802 RID: 10242 RVA: 0x000C90B7 File Offset: 0x000C72B7
	private void HideTrayWhenLeavingScreen(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		this.Hide();
	}

	// Token: 0x06002803 RID: 10243 RVA: 0x000C90C0 File Offset: 0x000C72C0
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		if (!this.IsShown())
		{
			return false;
		}
		FindGameState state = eventData.m_state;
		if (state - FindGameState.CLIENT_CANCELED <= 1 || state - FindGameState.BNET_QUEUE_CANCELED <= 1 || state == FindGameState.SERVER_GAME_CANCELED)
		{
			FriendChallengeMgr.Get().CancelChallenge();
			Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
			this.OnNavigateBack();
		}
		return false;
	}

	// Token: 0x040016A7 RID: 5799
	[CustomEditField(Sections = "UI")]
	public UberText m_trayLabel;

	// Token: 0x040016A8 RID: 5800
	[CustomEditField(Sections = "UI")]
	public StandardPegButtonNew m_backButton;

	// Token: 0x040016A9 RID: 5801
	[CustomEditField(Sections = "UI")]
	public PlayButton m_playButton;

	// Token: 0x040016AA RID: 5802
	[CustomEditField(Sections = "UI")]
	public FiresideGatheringPlayButtonLantern m_FiresideGatheringPlayButtonLantern;

	// Token: 0x040016AB RID: 5803
	[CustomEditField(Sections = "UI")]
	public PegUIElement m_inputBlocker;

	// Token: 0x040016AC RID: 5804
	[CustomEditField(Sections = "Opponent Button Settings")]
	public FiresideGatheringOpponentButton m_OpponentButtonPrefab;

	// Token: 0x040016AD RID: 5805
	[CustomEditField(Sections = "Opponent Button Settings")]
	public UIBScrollableItem m_HeaderPrefab;

	// Token: 0x040016AE RID: 5806
	[CustomEditField(Sections = "Opponent Button Settings")]
	public UIBScrollableItem m_FooterPrefab;

	// Token: 0x040016AF RID: 5807
	[CustomEditField(Sections = "Opponent Button Settings")]
	public GameObject m_OpponentButtonsContainer;

	// Token: 0x040016B0 RID: 5808
	[CustomEditField(Sections = "Opponent Button Settings")]
	public float m_HeaderHeight = 6f;

	// Token: 0x040016B1 RID: 5809
	[SerializeField]
	private float m_opponentButtonHeight = 5f;

	// Token: 0x040016B2 RID: 5810
	[CustomEditField(Sections = "Animation Settings")]
	public float m_trayInAnimationTime = 0.5f;

	// Token: 0x040016B3 RID: 5811
	[CustomEditField(Sections = "Animation Settings")]
	public float m_trayOutAnimationTime;

	// Token: 0x040016B4 RID: 5812
	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayInEaseType = iTween.EaseType.easeOutBounce;

	// Token: 0x040016B5 RID: 5813
	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayOutEaseType = iTween.EaseType.easeOutCubic;

	// Token: 0x040016B6 RID: 5814
	private static FiresideGatheringOpponentPickerTrayDisplay s_instance;

	// Token: 0x040016B7 RID: 5815
	private const float m_fadeOutTime = 0.2f;

	// Token: 0x040016B8 RID: 5816
	private List<FiresideGatheringOpponentButton> m_opponentButtons = new List<FiresideGatheringOpponentButton>();

	// Token: 0x040016B9 RID: 5817
	private UIBScrollableItem m_footer;

	// Token: 0x040016BA RID: 5818
	private List<Achievement> m_lockedHeroes;

	// Token: 0x040016BB RID: 5819
	private FiresideGatheringOpponentButton m_selectedOpponentButton;

	// Token: 0x040016BC RID: 5820
	private List<FiresideGatheringOpponentPickerTrayDisplay.TrayLoaded> m_TrayLoadedListeners = new List<FiresideGatheringOpponentPickerTrayDisplay.TrayLoaded>();

	// Token: 0x040016BD RID: 5821
	private List<Action> m_trayHiddenListeners = new List<Action>();

	// Token: 0x040016BE RID: 5822
	private bool m_buttonsReady;

	// Token: 0x040016BF RID: 5823
	private bool m_shown;

	// Token: 0x02001619 RID: 5657
	// (Invoke) Token: 0x0600E2D3 RID: 58067
	public delegate void TrayLoaded();
}
