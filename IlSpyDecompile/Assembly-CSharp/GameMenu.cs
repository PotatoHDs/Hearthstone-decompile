using System.Collections.Generic;
using bgs;
using Hearthstone;
using Hearthstone.Streaming;
using UnityEngine;

[CustomEditClass]
public class GameMenu : ButtonListMenu, GameMenuInterface
{
	[CustomEditField(Sections = "Template Items")]
	public Vector3 m_ratingsObjectMinPadding = new Vector3(0f, 0f, -0.06f);

	public Transform m_menuBone;

	public Material m_redButtonMaterial;

	public string m_anchorForKoreanRatings;

	private static GameMenu s_instance;

	private GameMenuBase m_gameMenuBase;

	private UIBButton m_concedeButton;

	private UIBButton m_endGameButton;

	private UIBButton m_leaveButton;

	private UIBButton m_restartButton;

	private UIBButton m_quitButton;

	private UIBButton m_loginButton;

	private UIBButton m_optionsButton;

	private UIBButton m_downloadButton;

	private UIBButton m_signUpButton;

	private Notification m_loginButtonPopup;

	private bool m_hasSeenLoginTooltip;

	private constants.BnetRegion m_AccountRegion;

	private GameObject m_ratingsObject;

	private Transform m_ratingsAnchor;

	private RegionSwitchMenuController m_regionSwitchMenuController = new RegionSwitchMenuController();

	private readonly Vector3 BUTTON_SCALE = 15f * Vector3.one;

	private readonly Vector3 BUTTON_SCALE_PHONE = 25f * Vector3.one;

	private IGameDownloadManager DownloadManager => GameDownloadManagerProvider.Get();

	protected override void Awake()
	{
		m_menuParent = m_menuBone;
		m_targetLayer = GameLayer.HighPriorityUI;
		base.Awake();
		s_instance = this;
		m_gameMenuBase = new GameMenuBase();
		m_gameMenuBase.m_showCallback = Show;
		m_gameMenuBase.m_hideCallback = Hide;
		LoadRatings();
		m_concedeButton = CreateMenuButton("ConcedeButton", "GLOBAL_CONCEDE", ConcedeButtonPressed);
		ButtonListMenu.MakeButtonRed(m_concedeButton, m_redButtonMaterial);
		m_endGameButton = CreateMenuButton("EndGameButton", "GLOBAL_END_GAME", ConcedeButtonPressed);
		ButtonListMenu.MakeButtonRed(m_endGameButton, m_redButtonMaterial);
		m_leaveButton = CreateMenuButton("LeaveButton", "GLOBAL_LEAVE_SPECTATOR_MODE", LeaveButtonPressed);
		m_restartButton = CreateMenuButton("RestartButton", "GLOBAL_RESTART", RestartButtonPressed);
		if ((bool)HearthstoneApplication.CanQuitGame)
		{
			m_quitButton = CreateMenuButton("QuitButton", "GLOBAL_QUIT", QuitButtonPressed);
		}
		if (PlatformSettings.IsMobile())
		{
			m_loginButton = CreateMenuButton("LogoutButton", Network.ShouldBeConnectedToAurora() ? "GLOBAL_SWITCH_ACCOUNT" : "GLOBAL_LOGIN", LogoutButtonPressed);
		}
		m_optionsButton = CreateMenuButton("OptionsButton", "GLOBAL_OPTIONS", OptionsButtonPressed);
		if (m_menu.m_templateDownloadButton != null)
		{
			m_downloadButton = CreateMenuButton("AssetDownloadButton", "GLOBAL_ASSET_DOWNLOAD", AssetDownloadButtonPressed, m_menu.m_templateDownloadButton);
		}
		if (m_menu.m_templateSignUpButton != null)
		{
			m_signUpButton = CreateMenuButton("SignUpButton", "GLUE_TEMPORARY_ACCOUNT_SIGN_UP", OnSignUpPressed, m_menu.m_templateSignUpButton);
		}
		m_menu.m_headerText.Text = GameStrings.Get("GLOBAL_GAME_MENU");
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		m_gameMenuBase.DestroyOptionsMenu();
		s_instance = null;
	}

	private void Start()
	{
		base.gameObject.SetActive(value: false);
	}

	private void OnEnable()
	{
		string text = GameStrings.Get("GLOBAL_LEAVE_SPECTATOR_MODE");
		m_leaveButton.SetText(text);
	}

	public bool GameMenuIsShown()
	{
		return IsShown();
	}

	public void GameMenuShow()
	{
		Show();
	}

	public void GameMenuHide()
	{
		Hide();
	}

	public void GameMenuShowOptionsMenu()
	{
		ShowOptionsMenu();
	}

	public GameObject GameMenuGetGameObject()
	{
		return base.gameObject;
	}

	public static GameMenu Get()
	{
		return s_instance;
	}

	public override void Show()
	{
		if (MiscellaneousMenu.Get() != null && MiscellaneousMenu.Get().IsShown())
		{
			MiscellaneousMenu.Get().Hide();
		}
		if (OptionsMenu.Get() != null && OptionsMenu.Get().IsShown())
		{
			UniversalInputManager.Get().CancelTextInput(base.gameObject, force: true);
			OptionsMenu.Get().Hide();
			return;
		}
		UpdateConcedeButtonAlternativeText();
		base.Show();
		if ((bool)UniversalInputManager.UsePhoneUI && m_ratingsObject != null)
		{
			m_ratingsObject.SetActive(m_gameMenuBase.UseKoreanRating());
		}
		ShowLoginTooltipIfNeeded();
		BnetBar.Get().m_menuButton.SetSelected(enable: true);
	}

	public override void Hide()
	{
		base.Hide();
		HideLoginTooltip();
		BnetBar.Get().m_menuButton.SetSelected(enable: false);
	}

	public void ShowLoginTooltipIfNeeded()
	{
		if (!Network.ShouldBeConnectedToAurora() && !m_hasSeenLoginTooltip)
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				Vector3 position = new Vector3(-82.9f, 42.1f, 17.2f);
				m_loginButtonPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, BUTTON_SCALE_PHONE, GameStrings.Get("GLOBAL_MOBILE_LOG_IN_TOOLTIP"), convertLegacyPosition: false);
			}
			else
			{
				Vector3 position = new Vector3(-46.9f, 34.2f, 9.4f);
				m_loginButtonPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, BUTTON_SCALE, GameStrings.Get("GLOBAL_MOBILE_LOG_IN_TOOLTIP"), convertLegacyPosition: false);
			}
			if (m_loginButtonPopup != null)
			{
				m_loginButtonPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
				m_hasSeenLoginTooltip = true;
			}
		}
	}

	public void HideLoginTooltip()
	{
		if (m_loginButtonPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_loginButtonPopup);
		}
		m_loginButtonPopup = null;
	}

	public static bool IsInGameMenu()
	{
		if (!SceneMgr.Get().IsInGame() || !SceneMgr.Get().IsSceneLoaded() || LoadingScreen.Get().IsTransitioning())
		{
			return false;
		}
		if (GameState.Get() == null)
		{
			return false;
		}
		if (GameState.Get().IsGameOver())
		{
			return false;
		}
		if (TutorialProgressScreen.Get() != null && TutorialProgressScreen.Get().gameObject.activeInHierarchy)
		{
			return false;
		}
		return true;
	}

	public static bool CanLogInOrCreateAccount()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.STARTUP || mode == SceneMgr.Mode.LOGIN)
		{
			return false;
		}
		return true;
	}

	public void ShowOptionsMenu()
	{
		if (m_gameMenuBase != null)
		{
			m_gameMenuBase.ShowOptionsMenu();
		}
	}

	protected override List<UIBButton> GetButtons()
	{
		List<UIBButton> list = new List<UIBButton>();
		bool flag = IsInGameMenu();
		if (flag)
		{
			bool flag2 = false;
			if (GameUtils.CanConcedeCurrentMission())
			{
				if (GameUtils.IsWaitingForOpponentReconnect())
				{
					list.Add(m_endGameButton);
				}
				else
				{
					list.Add(m_concedeButton);
				}
				flag2 = true;
			}
			if (SpectatorManager.Get().IsSpectatingOrWatching)
			{
				list.Add(m_leaveButton);
				flag2 = true;
			}
			if (GameUtils.CanRestartCurrentMission() && !ShouldHideRestartButton())
			{
				list.Add(m_restartButton);
				flag2 = true;
			}
			if (flag2)
			{
				list.Add(null);
			}
		}
		bool flag3 = false;
		if (!IsInGameMenu() && CanLogInOrCreateAccount() && TemporaryAccountManager.IsTemporaryAccount() && m_signUpButton != null && !flag)
		{
			flag3 = true;
			list.Add(m_signUpButton);
		}
		if (!DemoMgr.Get().IsExpoDemo())
		{
			list.Add(m_optionsButton);
			if (CanLogInOrCreateAccount() && !flag3)
			{
				if ((bool)HearthstoneApplication.CanQuitGame)
				{
					if (PlatformSettings.OS == OSCategory.Android)
					{
						list.Add(m_loginButton);
					}
					list.Add(m_quitButton);
				}
				else if (!flag)
				{
					list.Add(m_loginButton);
				}
			}
		}
		if (m_downloadButton != null && DownloadManager != null && DownloadManager.IsAnyDownloadRequestedAndIncomplete && DownloadManager.InterruptionReason != InterruptionReason.Fetching && !DownloadManager.IsRunningNewerBinaryThanLive)
		{
			list.Add(m_downloadButton);
		}
		return list;
	}

	protected override void LayoutMenu()
	{
		LayoutMenuButtons();
		m_menu.m_buttonContainer.UpdateSlices();
		LayoutMenuBackground();
		if (m_ratingsObject != null && m_ratingsAnchor != null)
		{
			m_ratingsObject.transform.position = m_ratingsAnchor.position;
		}
	}

	private void QuitButtonPressed(UIEvent e)
	{
		Network.Get().AutoConcede();
		HearthstoneApplication.Get().Exit();
	}

	private void LogoutButtonPressed(UIEvent e)
	{
		HideLoginTooltip();
		Hide();
		m_regionSwitchMenuController.ShowRegionMenuWithDefaultSettings();
	}

	private void ConcedeButtonPressed(UIEvent e)
	{
		if (GameState.Get() != null)
		{
			GameState.Get().Concede();
		}
		Hide();
	}

	private void LeaveButtonPressed(UIEvent e)
	{
		if (SpectatorManager.Get().IsInSpectatorMode())
		{
			SpectatorManager.Get().LeaveSpectatorMode();
		}
		Hide();
	}

	private void RestartButtonPressed(UIEvent e)
	{
		if (GameState.Get() != null)
		{
			GameState.Get().Restart();
		}
		Hide();
	}

	private void OptionsButtonPressed(UIEvent e)
	{
		ShowOptionsMenu();
	}

	private void AssetDownloadButtonPressed(UIEvent e)
	{
		Hide();
		DialogManager.Get().ShowAssetDownloadPopup(new AssetDownloadDialog.Info());
	}

	private void OnSignUpPressed(UIEvent e)
	{
		Hide();
		TemporaryAccountManager.Get().ShowHealUpPage(TemporaryAccountManager.HealUpReason.GAME_MENU);
	}

	private void LoadRatings()
	{
		m_ratingsAnchor = m_menu.transform.Find(m_anchorForKoreanRatings);
		if (!m_gameMenuBase.UseKoreanRating() || !(m_ratingsAnchor != null))
		{
			return;
		}
		AssetLoader.Get().InstantiatePrefab("Korean_Ratings_OptionsScreen.prefab:aea866fab02b24ca697ede020cd85772", delegate(AssetReference name, GameObject go, object data)
		{
			if (!(go == null))
			{
				Quaternion localRotation = go.transform.localRotation;
				go.transform.parent = m_menu.transform;
				go.transform.localScale = Vector3.one;
				go.transform.localRotation = localRotation;
				go.transform.position = m_ratingsAnchor.position;
				m_ratingsObject = go;
				LayoutMenu();
			}
		});
	}

	private bool ShouldHideRestartButton()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return false;
		}
		return gameState.GetGameEntity()?.HasTag(GAME_TAG.HIDE_RESTART_BUTTON) ?? false;
	}

	private void UpdateConcedeButtonAlternativeText()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return;
		}
		GameEntity gameEntity = gameState.GetGameEntity();
		if (gameEntity != null)
		{
			switch (gameEntity.GetTag(GAME_TAG.CONCEDE_BUTTON_ALTERNATIVE_TEXT))
			{
			case 0:
				m_concedeButton.SetText(GameStrings.Get("GLOBAL_CONCEDE"));
				break;
			case 1:
				m_concedeButton.SetText(GameStrings.Get("GLOBAL_LEAVE"));
				break;
			case 2:
				m_concedeButton.SetText(GameStrings.Get("GLOBAL_SKIP_TUTORIAL"));
				break;
			default:
				m_concedeButton.SetText(GameStrings.Get("GLOBAL_CONCEDE"));
				Log.Gameplay.PrintError($"GameMenu.UpdateConcedeButtonAlternativeText() - invalid concede button alternative text");
				break;
			}
		}
	}
}
