using System;
using System.Collections.Generic;
using bgs;
using Hearthstone;
using Hearthstone.Streaming;
using UnityEngine;

// Token: 0x02000B12 RID: 2834
[CustomEditClass]
public class GameMenu : ButtonListMenu, GameMenuInterface
{
	// Token: 0x1700088D RID: 2189
	// (get) Token: 0x060096AB RID: 38571 RVA: 0x000274B4 File Offset: 0x000256B4
	private IGameDownloadManager DownloadManager
	{
		get
		{
			return GameDownloadManagerProvider.Get();
		}
	}

	// Token: 0x060096AC RID: 38572 RVA: 0x0030C308 File Offset: 0x0030A508
	protected override void Awake()
	{
		this.m_menuParent = this.m_menuBone;
		this.m_targetLayer = GameLayer.HighPriorityUI;
		base.Awake();
		GameMenu.s_instance = this;
		this.m_gameMenuBase = new GameMenuBase();
		this.m_gameMenuBase.m_showCallback = new GameMenuBase.ShowCallback(this.Show);
		this.m_gameMenuBase.m_hideCallback = new GameMenuBase.HideCallback(this.Hide);
		this.LoadRatings();
		this.m_concedeButton = base.CreateMenuButton("ConcedeButton", "GLOBAL_CONCEDE", new UIEvent.Handler(this.ConcedeButtonPressed));
		ButtonListMenu.MakeButtonRed(this.m_concedeButton, this.m_redButtonMaterial);
		this.m_endGameButton = base.CreateMenuButton("EndGameButton", "GLOBAL_END_GAME", new UIEvent.Handler(this.ConcedeButtonPressed));
		ButtonListMenu.MakeButtonRed(this.m_endGameButton, this.m_redButtonMaterial);
		this.m_leaveButton = base.CreateMenuButton("LeaveButton", "GLOBAL_LEAVE_SPECTATOR_MODE", new UIEvent.Handler(this.LeaveButtonPressed));
		this.m_restartButton = base.CreateMenuButton("RestartButton", "GLOBAL_RESTART", new UIEvent.Handler(this.RestartButtonPressed));
		if (HearthstoneApplication.CanQuitGame)
		{
			this.m_quitButton = base.CreateMenuButton("QuitButton", "GLOBAL_QUIT", new UIEvent.Handler(this.QuitButtonPressed));
		}
		if (PlatformSettings.IsMobile())
		{
			this.m_loginButton = base.CreateMenuButton("LogoutButton", Network.ShouldBeConnectedToAurora() ? "GLOBAL_SWITCH_ACCOUNT" : "GLOBAL_LOGIN", new UIEvent.Handler(this.LogoutButtonPressed));
		}
		this.m_optionsButton = base.CreateMenuButton("OptionsButton", "GLOBAL_OPTIONS", new UIEvent.Handler(this.OptionsButtonPressed));
		if (this.m_menu.m_templateDownloadButton != null)
		{
			this.m_downloadButton = base.CreateMenuButton("AssetDownloadButton", "GLOBAL_ASSET_DOWNLOAD", new UIEvent.Handler(this.AssetDownloadButtonPressed), this.m_menu.m_templateDownloadButton);
		}
		if (this.m_menu.m_templateSignUpButton != null)
		{
			this.m_signUpButton = base.CreateMenuButton("SignUpButton", "GLUE_TEMPORARY_ACCOUNT_SIGN_UP", new UIEvent.Handler(this.OnSignUpPressed), this.m_menu.m_templateSignUpButton);
		}
		this.m_menu.m_headerText.Text = GameStrings.Get("GLOBAL_GAME_MENU");
	}

	// Token: 0x060096AD RID: 38573 RVA: 0x0030C541 File Offset: 0x0030A741
	protected override void OnDestroy()
	{
		base.OnDestroy();
		this.m_gameMenuBase.DestroyOptionsMenu();
		GameMenu.s_instance = null;
	}

	// Token: 0x060096AE RID: 38574 RVA: 0x00028167 File Offset: 0x00026367
	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060096AF RID: 38575 RVA: 0x0030C55C File Offset: 0x0030A75C
	private void OnEnable()
	{
		string text = GameStrings.Get("GLOBAL_LEAVE_SPECTATOR_MODE");
		this.m_leaveButton.SetText(text);
	}

	// Token: 0x060096B0 RID: 38576 RVA: 0x0030C580 File Offset: 0x0030A780
	public bool GameMenuIsShown()
	{
		return base.IsShown();
	}

	// Token: 0x060096B1 RID: 38577 RVA: 0x0030C588 File Offset: 0x0030A788
	public void GameMenuShow()
	{
		this.Show();
	}

	// Token: 0x060096B2 RID: 38578 RVA: 0x0030C590 File Offset: 0x0030A790
	public void GameMenuHide()
	{
		this.Hide();
	}

	// Token: 0x060096B3 RID: 38579 RVA: 0x0030C598 File Offset: 0x0030A798
	public void GameMenuShowOptionsMenu()
	{
		this.ShowOptionsMenu();
	}

	// Token: 0x060096B4 RID: 38580 RVA: 0x00036786 File Offset: 0x00034986
	public GameObject GameMenuGetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x060096B5 RID: 38581 RVA: 0x0030C5A0 File Offset: 0x0030A7A0
	public static GameMenu Get()
	{
		return GameMenu.s_instance;
	}

	// Token: 0x060096B6 RID: 38582 RVA: 0x0030C5A8 File Offset: 0x0030A7A8
	public override void Show()
	{
		if (MiscellaneousMenu.Get() != null && MiscellaneousMenu.Get().IsShown())
		{
			MiscellaneousMenu.Get().Hide();
		}
		if (OptionsMenu.Get() != null && OptionsMenu.Get().IsShown())
		{
			UniversalInputManager.Get().CancelTextInput(base.gameObject, true);
			OptionsMenu.Get().Hide(true);
			return;
		}
		this.UpdateConcedeButtonAlternativeText();
		base.Show();
		if (UniversalInputManager.UsePhoneUI && this.m_ratingsObject != null)
		{
			this.m_ratingsObject.SetActive(this.m_gameMenuBase.UseKoreanRating());
		}
		this.ShowLoginTooltipIfNeeded();
		BnetBar.Get().m_menuButton.SetSelected(true);
	}

	// Token: 0x060096B7 RID: 38583 RVA: 0x0030C660 File Offset: 0x0030A860
	public override void Hide()
	{
		base.Hide();
		this.HideLoginTooltip();
		BnetBar.Get().m_menuButton.SetSelected(false);
	}

	// Token: 0x060096B8 RID: 38584 RVA: 0x0030C680 File Offset: 0x0030A880
	public void ShowLoginTooltipIfNeeded()
	{
		if (Network.ShouldBeConnectedToAurora() || this.m_hasSeenLoginTooltip)
		{
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			Vector3 position = new Vector3(-82.9f, 42.1f, 17.2f);
			this.m_loginButtonPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, this.BUTTON_SCALE_PHONE, GameStrings.Get("GLOBAL_MOBILE_LOG_IN_TOOLTIP"), false, NotificationManager.PopupTextType.BASIC);
		}
		else
		{
			Vector3 position = new Vector3(-46.9f, 34.2f, 9.4f);
			this.m_loginButtonPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, this.BUTTON_SCALE, GameStrings.Get("GLOBAL_MOBILE_LOG_IN_TOOLTIP"), false, NotificationManager.PopupTextType.BASIC);
		}
		if (this.m_loginButtonPopup != null)
		{
			this.m_loginButtonPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			this.m_hasSeenLoginTooltip = true;
		}
	}

	// Token: 0x060096B9 RID: 38585 RVA: 0x0030C740 File Offset: 0x0030A940
	public void HideLoginTooltip()
	{
		if (this.m_loginButtonPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_loginButtonPopup);
		}
		this.m_loginButtonPopup = null;
	}

	// Token: 0x060096BA RID: 38586 RVA: 0x0030C768 File Offset: 0x0030A968
	public static bool IsInGameMenu()
	{
		return SceneMgr.Get().IsInGame() && SceneMgr.Get().IsSceneLoaded() && !LoadingScreen.Get().IsTransitioning() && GameState.Get() != null && !GameState.Get().IsGameOver() && (!(TutorialProgressScreen.Get() != null) || !TutorialProgressScreen.Get().gameObject.activeInHierarchy);
	}

	// Token: 0x060096BB RID: 38587 RVA: 0x0030C7D4 File Offset: 0x0030A9D4
	public static bool CanLogInOrCreateAccount()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		return mode != SceneMgr.Mode.STARTUP && mode != SceneMgr.Mode.LOGIN;
	}

	// Token: 0x060096BC RID: 38588 RVA: 0x0030C7F7 File Offset: 0x0030A9F7
	public void ShowOptionsMenu()
	{
		if (this.m_gameMenuBase != null)
		{
			this.m_gameMenuBase.ShowOptionsMenu();
		}
	}

	// Token: 0x060096BD RID: 38589 RVA: 0x0030C80C File Offset: 0x0030AA0C
	protected override List<UIBButton> GetButtons()
	{
		List<UIBButton> list = new List<UIBButton>();
		bool flag = GameMenu.IsInGameMenu();
		if (flag)
		{
			bool flag2 = false;
			if (GameUtils.CanConcedeCurrentMission())
			{
				if (GameUtils.IsWaitingForOpponentReconnect())
				{
					list.Add(this.m_endGameButton);
				}
				else
				{
					list.Add(this.m_concedeButton);
				}
				flag2 = true;
			}
			if (SpectatorManager.Get().IsSpectatingOrWatching)
			{
				list.Add(this.m_leaveButton);
				flag2 = true;
			}
			if (GameUtils.CanRestartCurrentMission(true) && !this.ShouldHideRestartButton())
			{
				list.Add(this.m_restartButton);
				flag2 = true;
			}
			if (flag2)
			{
				list.Add(null);
			}
		}
		bool flag3 = false;
		if (!GameMenu.IsInGameMenu() && GameMenu.CanLogInOrCreateAccount() && TemporaryAccountManager.IsTemporaryAccount() && this.m_signUpButton != null && !flag)
		{
			flag3 = true;
			list.Add(this.m_signUpButton);
		}
		if (!DemoMgr.Get().IsExpoDemo())
		{
			list.Add(this.m_optionsButton);
			if (GameMenu.CanLogInOrCreateAccount() && !flag3)
			{
				if (HearthstoneApplication.CanQuitGame)
				{
					if (PlatformSettings.OS == OSCategory.Android)
					{
						list.Add(this.m_loginButton);
					}
					list.Add(this.m_quitButton);
				}
				else if (!flag)
				{
					list.Add(this.m_loginButton);
				}
			}
		}
		if (this.m_downloadButton != null && this.DownloadManager != null && this.DownloadManager.IsAnyDownloadRequestedAndIncomplete && this.DownloadManager.InterruptionReason != InterruptionReason.Fetching && !this.DownloadManager.IsRunningNewerBinaryThanLive)
		{
			list.Add(this.m_downloadButton);
		}
		return list;
	}

	// Token: 0x060096BE RID: 38590 RVA: 0x0030C978 File Offset: 0x0030AB78
	protected override void LayoutMenu()
	{
		base.LayoutMenuButtons();
		this.m_menu.m_buttonContainer.UpdateSlices();
		base.LayoutMenuBackground();
		if (this.m_ratingsObject != null && this.m_ratingsAnchor != null)
		{
			this.m_ratingsObject.transform.position = this.m_ratingsAnchor.position;
		}
	}

	// Token: 0x060096BF RID: 38591 RVA: 0x0030C9D8 File Offset: 0x0030ABD8
	private void QuitButtonPressed(UIEvent e)
	{
		Network.Get().AutoConcede();
		HearthstoneApplication.Get().Exit();
	}

	// Token: 0x060096C0 RID: 38592 RVA: 0x0030C9EE File Offset: 0x0030ABEE
	private void LogoutButtonPressed(UIEvent e)
	{
		this.HideLoginTooltip();
		this.Hide();
		this.m_regionSwitchMenuController.ShowRegionMenuWithDefaultSettings();
	}

	// Token: 0x060096C1 RID: 38593 RVA: 0x0030CA07 File Offset: 0x0030AC07
	private void ConcedeButtonPressed(UIEvent e)
	{
		if (GameState.Get() != null)
		{
			GameState.Get().Concede();
		}
		this.Hide();
	}

	// Token: 0x060096C2 RID: 38594 RVA: 0x0030CA20 File Offset: 0x0030AC20
	private void LeaveButtonPressed(UIEvent e)
	{
		if (SpectatorManager.Get().IsInSpectatorMode())
		{
			SpectatorManager.Get().LeaveSpectatorMode();
		}
		this.Hide();
	}

	// Token: 0x060096C3 RID: 38595 RVA: 0x0030CA3E File Offset: 0x0030AC3E
	private void RestartButtonPressed(UIEvent e)
	{
		if (GameState.Get() != null)
		{
			GameState.Get().Restart();
		}
		this.Hide();
	}

	// Token: 0x060096C4 RID: 38596 RVA: 0x0030C598 File Offset: 0x0030A798
	private void OptionsButtonPressed(UIEvent e)
	{
		this.ShowOptionsMenu();
	}

	// Token: 0x060096C5 RID: 38597 RVA: 0x0030CA57 File Offset: 0x0030AC57
	private void AssetDownloadButtonPressed(UIEvent e)
	{
		this.Hide();
		DialogManager.Get().ShowAssetDownloadPopup(new AssetDownloadDialog.Info());
	}

	// Token: 0x060096C6 RID: 38598 RVA: 0x0030CA6F File Offset: 0x0030AC6F
	private void OnSignUpPressed(UIEvent e)
	{
		this.Hide();
		TemporaryAccountManager.Get().ShowHealUpPage(TemporaryAccountManager.HealUpReason.GAME_MENU, null);
	}

	// Token: 0x060096C7 RID: 38599 RVA: 0x0030CA84 File Offset: 0x0030AC84
	private void LoadRatings()
	{
		this.m_ratingsAnchor = this.m_menu.transform.Find(this.m_anchorForKoreanRatings);
		if (this.m_gameMenuBase.UseKoreanRating() && this.m_ratingsAnchor != null)
		{
			AssetLoader.Get().InstantiatePrefab("Korean_Ratings_OptionsScreen.prefab:aea866fab02b24ca697ede020cd85772", delegate(AssetReference name, GameObject go, object data)
			{
				if (go == null)
				{
					return;
				}
				Quaternion localRotation = go.transform.localRotation;
				go.transform.parent = this.m_menu.transform;
				go.transform.localScale = Vector3.one;
				go.transform.localRotation = localRotation;
				go.transform.position = this.m_ratingsAnchor.position;
				this.m_ratingsObject = go;
				this.LayoutMenu();
			}, null, AssetLoadingOptions.None);
		}
	}

	// Token: 0x060096C8 RID: 38600 RVA: 0x0030CAEC File Offset: 0x0030ACEC
	private bool ShouldHideRestartButton()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return false;
		}
		GameEntity gameEntity = gameState.GetGameEntity();
		return gameEntity != null && gameEntity.HasTag(GAME_TAG.HIDE_RESTART_BUTTON);
	}

	// Token: 0x060096C9 RID: 38601 RVA: 0x0030CB1C File Offset: 0x0030AD1C
	private void UpdateConcedeButtonAlternativeText()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return;
		}
		GameEntity gameEntity = gameState.GetGameEntity();
		if (gameEntity == null)
		{
			return;
		}
		switch (gameEntity.GetTag(GAME_TAG.CONCEDE_BUTTON_ALTERNATIVE_TEXT))
		{
		case 0:
			this.m_concedeButton.SetText(GameStrings.Get("GLOBAL_CONCEDE"));
			return;
		case 1:
			this.m_concedeButton.SetText(GameStrings.Get("GLOBAL_LEAVE"));
			return;
		case 2:
			this.m_concedeButton.SetText(GameStrings.Get("GLOBAL_SKIP_TUTORIAL"));
			return;
		default:
			this.m_concedeButton.SetText(GameStrings.Get("GLOBAL_CONCEDE"));
			global::Log.Gameplay.PrintError(string.Format("GameMenu.UpdateConcedeButtonAlternativeText() - invalid concede button alternative text", Array.Empty<object>()), Array.Empty<object>());
			return;
		}
	}

	// Token: 0x04007E3A RID: 32314
	[CustomEditField(Sections = "Template Items")]
	public Vector3 m_ratingsObjectMinPadding = new Vector3(0f, 0f, -0.06f);

	// Token: 0x04007E3B RID: 32315
	public Transform m_menuBone;

	// Token: 0x04007E3C RID: 32316
	public Material m_redButtonMaterial;

	// Token: 0x04007E3D RID: 32317
	public string m_anchorForKoreanRatings;

	// Token: 0x04007E3E RID: 32318
	private static GameMenu s_instance;

	// Token: 0x04007E3F RID: 32319
	private GameMenuBase m_gameMenuBase;

	// Token: 0x04007E40 RID: 32320
	private UIBButton m_concedeButton;

	// Token: 0x04007E41 RID: 32321
	private UIBButton m_endGameButton;

	// Token: 0x04007E42 RID: 32322
	private UIBButton m_leaveButton;

	// Token: 0x04007E43 RID: 32323
	private UIBButton m_restartButton;

	// Token: 0x04007E44 RID: 32324
	private UIBButton m_quitButton;

	// Token: 0x04007E45 RID: 32325
	private UIBButton m_loginButton;

	// Token: 0x04007E46 RID: 32326
	private UIBButton m_optionsButton;

	// Token: 0x04007E47 RID: 32327
	private UIBButton m_downloadButton;

	// Token: 0x04007E48 RID: 32328
	private UIBButton m_signUpButton;

	// Token: 0x04007E49 RID: 32329
	private Notification m_loginButtonPopup;

	// Token: 0x04007E4A RID: 32330
	private bool m_hasSeenLoginTooltip;

	// Token: 0x04007E4B RID: 32331
	private constants.BnetRegion m_AccountRegion;

	// Token: 0x04007E4C RID: 32332
	private GameObject m_ratingsObject;

	// Token: 0x04007E4D RID: 32333
	private Transform m_ratingsAnchor;

	// Token: 0x04007E4E RID: 32334
	private RegionSwitchMenuController m_regionSwitchMenuController = new RegionSwitchMenuController();

	// Token: 0x04007E4F RID: 32335
	private readonly Vector3 BUTTON_SCALE = 15f * Vector3.one;

	// Token: 0x04007E50 RID: 32336
	private readonly Vector3 BUTTON_SCALE_PHONE = 25f * Vector3.one;
}
