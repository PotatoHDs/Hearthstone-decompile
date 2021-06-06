using System;
using System.Collections.Generic;
using Hearthstone;
using Hearthstone.Streaming;
using UnityEngine;

[CustomEditClass]
public class OptionsMenu : MonoBehaviour
{
	public delegate void hideHandler();

	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_leftPane;

	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_rightPane;

	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_middlePane;

	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_middleBottomPane;

	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_middleBottomLeftPane;

	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_middleBottomRightPane;

	[CustomEditField(Sections = "Placeholder")]
	public GameObject m_middleLeftPaneLabel;

	[CustomEditField(Sections = "Placeholder")]
	public GameObject m_middleRightPaneLabel;

	[CustomEditField(Sections = "Graphics")]
	public GameObject m_graphicsGroup;

	[CustomEditField(Sections = "Graphics")]
	public DropdownControl m_graphicsRes;

	[CustomEditField(Sections = "Graphics")]
	public DropdownControl m_graphicsQuality;

	[CustomEditField(Sections = "Graphics")]
	public CheckBox m_fullScreenCheckbox;

	[CustomEditField(Sections = "Sound")]
	public GameObject m_soundGroup;

	[CustomEditField(Sections = "Sound")]
	public ScrollbarControl m_masterVolume;

	[CustomEditField(Sections = "Sound")]
	public ScrollbarControl m_musicVolume;

	[CustomEditField(Sections = "Sound")]
	public CheckBox m_backgroundSound;

	[CustomEditField(Sections = "Language")]
	public GameObject m_languageGroup;

	[CustomEditField(Sections = "Language")]
	public DropdownControl m_languageDropdown;

	[CustomEditField(Sections = "Language")]
	public FontDefinition m_languageDropdownFont;

	[CustomEditField(Sections = "Language")]
	public CheckBox m_languagePackCheckbox;

	[CustomEditField(Sections = "Other")]
	public CheckBox m_spectatorOpenJoinCheckbox;

	[CustomEditField(Sections = "Other")]
	public CheckBox m_screenShakeCheckbox;

	[CustomEditField(Sections = "Other")]
	public UIBButton m_switchAccountButton;

	[CustomEditField(Sections = "Other")]
	public UIBButton m_miscellaneousButton;

	[CustomEditField(Sections = "Internal Stuff")]
	public UberText m_versionLabel;

	private static OptionsMenu s_instance;

	private bool m_isShown;

	private hideHandler m_hideHandler;

	private MiscellaneousMenu m_miscellaneousMenu;

	private bool m_miscellaneousMenuLoading;

	private PegUIElement m_inputBlocker;

	private RegionSwitchMenuController m_controller = new RegionSwitchMenuController();

	private List<GraphicsResolution> m_fullScreenResolutions = new List<GraphicsResolution>();

	private Vector3 NORMAL_SCALE;

	private Vector3 HIDDEN_SCALE;

	private readonly PlatformDependentValue<bool> LANGUAGE_SELECTION = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = true,
		Android = true,
		PC = false,
		Mac = false
	};

	private IGameDownloadManager DownloadManager => GameDownloadManagerProvider.Get();

	private void Awake()
	{
		s_instance = this;
		NORMAL_SCALE = base.transform.localScale;
		HIDDEN_SCALE = 0.01f * NORMAL_SCALE;
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		OverlayUI.Get().AddGameObject(base.gameObject);
		if (!UniversalInputManager.UsePhoneUI)
		{
			m_graphicsRes.setUnselectedItemText(GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_RESOLUTION_CUSTOM"));
			m_graphicsRes.setItemTextCallback(OnGraphicsResolutionDropdownText);
			m_graphicsRes.setItemChosenCallback(OnNewGraphicsResolution);
			foreach (GraphicsResolution item in GetGoodGraphicsResolution())
			{
				m_graphicsRes.addItem(item);
			}
			m_graphicsRes.setSelection(GetCurrentGraphicsResolution());
			m_fullScreenCheckbox.AddEventListener(UIEventType.RELEASE, OnToggleFullScreenCheckbox);
			m_fullScreenCheckbox.SetChecked(Options.Get().GetBool(Option.GFX_FULLSCREEN, Screen.fullScreen));
			m_graphicsQuality.addItem(GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_LOW"));
			m_graphicsQuality.addItem(GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_MEDIUM"));
			m_graphicsQuality.addItem(GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_HIGH"));
			m_graphicsQuality.setSelection(GetCurrentGraphicsQuality());
			m_graphicsQuality.setItemChosenCallback(OnNewGraphicsQuality);
		}
		m_masterVolume.SetValue(Options.Get().GetFloat(Option.SOUND_VOLUME));
		m_masterVolume.SetUpdateHandler(OnNewMasterVolume);
		m_masterVolume.SetFinishHandler(OnMasterVolumeRelease);
		m_musicVolume.SetValue(Options.Get().GetFloat(Option.MUSIC_VOLUME));
		m_musicVolume.SetUpdateHandler(OnNewMusicVolume);
		if (m_backgroundSound != null)
		{
			m_backgroundSound.AddEventListener(UIEventType.RELEASE, ToggleBackgroundSound);
			m_backgroundSound.SetChecked(Options.Get().GetBool(Option.BACKGROUND_SOUND));
		}
		m_languageGroup.gameObject.SetActive(LANGUAGE_SELECTION);
		if ((bool)LANGUAGE_SELECTION)
		{
			m_languageDropdown.setFont(m_languageDropdownFont.m_Font);
			foreach (Locale value in Enum.GetValues(typeof(Locale)))
			{
				if (value != Locale.UNKNOWN && (PlatformSettings.LocaleVariant != LocaleVariant.China || value == Locale.enUS || value == Locale.zhCN))
				{
					m_languageDropdown.addItem(GameStrings.Get(StringNameFromLocale(value)));
				}
			}
			m_languageDropdown.setSelection(GetCurrentLanguage());
			m_languageDropdown.setItemChosenCallback(OnNewLanguage);
		}
		UpdateOtherUI();
		if (TemporaryAccountManager.IsTemporaryAccount())
		{
			m_spectatorOpenJoinCheckbox.gameObject.SetActive(value: false);
			m_switchAccountButton.gameObject.SetActive(value: true);
			m_switchAccountButton.AddEventListener(UIEventType.RELEASE, OnSwitchAccountButtonReleased);
		}
		else
		{
			m_spectatorOpenJoinCheckbox.AddEventListener(UIEventType.RELEASE, ToggleSpectatorOpenJoin);
			m_spectatorOpenJoinCheckbox.SetChecked(Options.Get().GetBool(Option.SPECTATOR_OPEN_JOIN));
		}
		if (m_screenShakeCheckbox != null)
		{
			m_screenShakeCheckbox.AddEventListener(UIEventType.RELEASE, ToggleScreenShake);
			m_screenShakeCheckbox.SetChecked(Options.Get().GetBool(Option.SCREEN_SHAKE_ENABLED));
		}
		m_miscellaneousButton.AddEventListener(UIEventType.RELEASE, OnMiscellaneousButtonReleased);
		CreateInputBlocker();
		ShowOrHide(showOrHide: false);
		if (PlatformSettings.IsMobile())
		{
			if (m_backgroundSound != null)
			{
				m_backgroundSound.gameObject.SetActive(value: false);
			}
			m_graphicsGroup.SetActive(value: false);
			m_graphicsRes.gameObject.SetActive(value: false);
			m_graphicsQuality.gameObject.SetActive(value: false);
			string text = string.Format("{0} {1}.{2}", GameStrings.Get("GLOBAL_VERSION"), "20.4", 84593);
			string str = Vars.Key("Application.Referral").GetStr("none");
			if (str != "none")
			{
				text = text + "-" + str;
			}
			m_versionLabel.Text = text;
			m_versionLabel.gameObject.SetActive(value: true);
		}
		UpdateUI();
		GraphicsManager.Get().OnResolutionChangedEvent += UpdateMenuItemValues;
	}

	public void OnDestroy()
	{
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
		}
		if (GraphicsManager.Get() != null)
		{
			GraphicsManager.Get().OnResolutionChangedEvent -= UpdateMenuItemValues;
		}
		s_instance = null;
	}

	public static OptionsMenu Get()
	{
		return s_instance;
	}

	public hideHandler GetHideHandler()
	{
		return m_hideHandler;
	}

	public void SetHideHandler(hideHandler handler)
	{
		m_hideHandler = handler;
	}

	public void RemoveHideHandler(hideHandler handler)
	{
		if (m_hideHandler == handler)
		{
			m_hideHandler = null;
		}
	}

	public bool IsShown()
	{
		return m_isShown;
	}

	public void Show()
	{
		UpdateOtherUI();
		ShowOrHide(showOrHide: true);
		AnimationUtil.ShowWithPunch(base.gameObject, HIDDEN_SCALE, 1.1f * NORMAL_SCALE, NORMAL_SCALE, null, noFade: true);
	}

	public void Hide(bool callHideHandler = true)
	{
		ShowOrHide(showOrHide: false);
		if (m_hideHandler != null && callHideHandler)
		{
			m_hideHandler();
			m_hideHandler = null;
		}
	}

	private GraphicsResolution GetCurrentGraphicsResolution()
	{
		int @int = Options.Get().GetInt(Option.GFX_WIDTH, Screen.currentResolution.width);
		int int2 = Options.Get().GetInt(Option.GFX_HEIGHT, Screen.currentResolution.height);
		return GraphicsResolution.create(@int, int2);
	}

	private string GetCurrentGraphicsQuality()
	{
		return Options.Get().GetInt(Option.GFX_QUALITY) switch
		{
			0 => GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_LOW"), 
			1 => GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_MEDIUM"), 
			2 => GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_HIGH"), 
			_ => GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_LOW"), 
		};
	}

	private List<GraphicsResolution> GetGoodGraphicsResolution()
	{
		if (m_fullScreenResolutions.Count == 0)
		{
			foreach (GraphicsResolution item in GraphicsResolution.list)
			{
				if (item.x >= 1024 && item.y >= 728 && !((double)item.aspectRatio - 0.01 > 1.7777777777777777) && !((double)item.aspectRatio + 0.01 < 1.3333333333333333))
				{
					m_fullScreenResolutions.Add(item);
				}
			}
		}
		return m_fullScreenResolutions;
	}

	private string GetCurrentLanguage()
	{
		return GameStrings.Get(StringNameFromLocale(Localization.GetLocale()));
	}

	private void ShowOrHide(bool showOrHide)
	{
		m_isShown = showOrHide;
		base.gameObject.SetActive(showOrHide);
		UpdateUI();
	}

	private string StringNameFromLocale(Locale locale)
	{
		return "GLOBAL_LANGUAGE_NATIVE_" + locale.ToString().ToUpper();
	}

	private void UpdateOtherUI()
	{
		bool active = CanShowOtherMenuOptions();
		if (TemporaryAccountManager.IsTemporaryAccount())
		{
			m_middlePane.gameObject.SetActive(active);
		}
		else
		{
			m_middleBottomRightPane.gameObject.SetActive(active);
		}
	}

	private void UpdateUI()
	{
		m_middleLeftPaneLabel.SetActive(value: true);
		m_middleRightPaneLabel.SetActive(value: true);
		m_middleBottomLeftPane.UpdateSlices();
		m_middleBottomRightPane.UpdateSlices();
		m_middleBottomPane.UpdateSlices();
		m_leftPane.UpdateSlices();
		m_rightPane.UpdateSlices();
		m_middlePane.UpdateSlices();
		m_middleLeftPaneLabel.SetActive(value: false);
		m_middleRightPaneLabel.SetActive(value: false);
	}

	private bool CanShowOtherMenuOptions()
	{
		if (UserAttentionManager.GetAvailabilityBlockerReason(isFriendlyChallenge: false) != 0)
		{
			return false;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.PACKOPENING))
		{
			return false;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.ADVENTURE))
		{
			return false;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.CREDITS))
		{
			return false;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FRIENDLY))
		{
			return false;
		}
		return true;
	}

	private void CreateInputBlocker()
	{
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "OptionMenuInputBlocker", this, base.transform, 10f);
		gameObject.layer = base.gameObject.layer;
		m_inputBlocker = gameObject.AddComponent<PegUIElement>();
		m_inputBlocker.AddEventListener(UIEventType.RELEASE, delegate
		{
			Hide();
		});
	}

	private void UpdateMenuItemValues(int newWidth, int newHeight)
	{
		if (m_fullScreenCheckbox.IsChecked() != Screen.fullScreen)
		{
			m_fullScreenCheckbox.SetChecked(Screen.fullScreen);
			GraphicsResolution graphicsResolution = m_graphicsRes.getSelection() as GraphicsResolution;
			if (graphicsResolution == null || m_fullScreenCheckbox.IsChecked())
			{
				m_graphicsRes.setSelectionToFirstItem();
				graphicsResolution = m_graphicsRes.getSelection() as GraphicsResolution;
			}
			else if (!m_fullScreenCheckbox.IsChecked())
			{
				graphicsResolution = GraphicsResolution.create(newWidth, newHeight);
			}
			if (graphicsResolution != null)
			{
				int x = graphicsResolution.x;
				int y = graphicsResolution.y;
				m_graphicsRes.setSelection(GraphicsResolution.create(x, y));
			}
		}
		else if (!Screen.fullScreen)
		{
			m_graphicsRes.setSelection(GraphicsResolution.create(newWidth, newHeight));
		}
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		if (SceneMgr.Get().GetNextMode() == SceneMgr.Mode.FATAL_ERROR)
		{
			Hide();
		}
	}

	private void OnToggleFullScreenCheckbox(UIEvent e)
	{
		GraphicsResolution graphicsResolution = m_graphicsRes.getSelection() as GraphicsResolution;
		if (graphicsResolution == null)
		{
			m_graphicsRes.setSelectionToFirstItem();
			graphicsResolution = m_graphicsRes.getSelection() as GraphicsResolution;
		}
		if (graphicsResolution != null)
		{
			int width = graphicsResolution.x;
			int height = graphicsResolution.y;
			if (m_fullScreenCheckbox.IsChecked())
			{
				width = Screen.currentResolution.width;
				height = Screen.currentResolution.height;
			}
			m_graphicsRes.setSelection(GraphicsResolution.create(width, height));
			GraphicsManager.Get().SetScreenResolution(width, height, m_fullScreenCheckbox.IsChecked());
			Options.Get().SetBool(Option.GFX_FULLSCREEN, m_fullScreenCheckbox.IsChecked());
		}
	}

	private void OnNewGraphicsQuality(object selection, object prevSelection)
	{
		GraphicsQuality renderQualityLevel = GraphicsQuality.Low;
		string text = (string)selection;
		if (text == GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_LOW"))
		{
			renderQualityLevel = GraphicsQuality.Low;
		}
		else if (text == GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_MEDIUM"))
		{
			renderQualityLevel = GraphicsQuality.Medium;
		}
		else if (text == GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_HIGH"))
		{
			renderQualityLevel = GraphicsQuality.High;
		}
		Log.Options.Print("Graphics Quality: " + renderQualityLevel);
		GraphicsManager.Get().RenderQualityLevel = renderQualityLevel;
	}

	private void OnNewGraphicsResolution(object selection, object prevSelection)
	{
		GraphicsResolution graphicsResolution = (GraphicsResolution)selection;
		GraphicsManager.Get().SetScreenResolution(graphicsResolution.x, graphicsResolution.y, m_fullScreenCheckbox.IsChecked());
		Options.Get().SetInt(Option.GFX_WIDTH, graphicsResolution.x);
		Options.Get().SetInt(Option.GFX_HEIGHT, graphicsResolution.y);
	}

	private void OnNewLanguage(object selection, object prevSelection)
	{
		if (selection != prevSelection)
		{
			long num = FreeSpace.Measure();
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			if (num < 314572800)
			{
				popupInfo.m_headerText = GameStrings.Get("GLOBAL_LANGUAGE_CHANGE_OUT_OF_SPACE_TITLE");
				popupInfo.m_text = string.Format(GameStrings.Get("GLOBAL_LANGUAGE_CHANGE_OUT_OF_SPACE_MESSAGE"), DownloadStatusView.FormatBytesAsHumanReadable(314572800L));
				popupInfo.m_showAlertIcon = false;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			}
			else
			{
				popupInfo.m_headerText = GameStrings.Get("GLOBAL_LANGUAGE_CHANGE_CONFIRM_TITLE");
				popupInfo.m_text = GameStrings.Get("GLOBAL_LANGUAGE_CHANGE_CONFIRM_MESSAGE");
				popupInfo.m_showAlertIcon = false;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
				popupInfo.m_responseCallback = OnChangeLanguageConfirmationResponse;
				popupInfo.m_responseUserData = selection;
			}
			DialogManager.Get().ShowPopup(popupInfo);
		}
	}

	private void OnChangeLanguageConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			m_languageDropdown.setSelection(GetCurrentLanguage());
			return;
		}
		string text = (string)userData;
		Locale locale = Locale.UNKNOWN;
		foreach (Locale value in Enum.GetValues(typeof(Locale)))
		{
			if (text == GameStrings.Get(StringNameFromLocale(value)))
			{
				locale = value;
				break;
			}
		}
		if (locale == Locale.UNKNOWN)
		{
			Debug.LogError($"OptionsMenu.OnChangeLanguageConfirmationResponse() - locale not found");
			return;
		}
		TelemetryManager.Client().SendLanguageChanged(Localization.GetLocaleName(), locale.ToString());
		Localization.SetLocale(locale);
		Options.Get().SetString(Option.LOCALE, locale.ToString());
		Debug.LogFormat("Change Locale: {0}", locale);
		Hide(callHideHandler: false);
		HearthstoneApplication.Get().IsLocaleChanged = true;
		if (DownloadManager.ShouldDownloadLocalizedAssets)
		{
			HearthstoneApplication.Get().Resetting += StartUpdateProcessAfterReset;
		}
		HearthstoneApplication.Get().Reset();
	}

	private void StartUpdateProcessAfterReset()
	{
		HearthstoneApplication.Get().Resetting -= StartUpdateProcessAfterReset;
		DownloadManager.StartUpdateProcess(localeChange: true);
	}

	private string OnGraphicsResolutionDropdownText(object val)
	{
		GraphicsResolution graphicsResolution = (GraphicsResolution)val;
		return $"{graphicsResolution.x} x {graphicsResolution.y}";
	}

	private void OnNewMasterVolume(float newVolume)
	{
		Options.Get().SetFloat(Option.SOUND_VOLUME, newVolume);
	}

	private void OnMasterVolumeRelease()
	{
		SoundManager.LoadedCallback callback = delegate(AudioSource source, object userData)
		{
			SoundManager.Get().Set3d(source, enable: false);
		};
		SoundManager.Get().LoadAndPlay("UI_MouseClick_01.prefab:fa537702a0db1c3478c989967458788b", base.gameObject, 1f, callback);
	}

	private void OnNewMusicVolume(float newVolume)
	{
		Options.Get().SetFloat(Option.MUSIC_VOLUME, newVolume);
	}

	private void ToggleBackgroundSound(UIEvent e)
	{
		Options.Get().SetBool(Option.BACKGROUND_SOUND, m_backgroundSound.IsChecked());
	}

	private void OnSwitchAccountButtonReleased(UIEvent e)
	{
		Hide(callHideHandler: false);
		m_controller.ShowRegionMenuWithDefaultSettings();
	}

	private void ToggleSpectatorOpenJoin(UIEvent e)
	{
		Options.Get().SetBool(Option.SPECTATOR_OPEN_JOIN, m_spectatorOpenJoinCheckbox.IsChecked());
	}

	private void ToggleScreenShake(UIEvent e)
	{
		Options.Get().SetBool(Option.SCREEN_SHAKE_ENABLED, m_screenShakeCheckbox.IsChecked());
	}

	private void OnMiscellaneousButtonReleased(UIEvent e)
	{
		LoadMiscellaneousMenu();
		Hide(callHideHandler: false);
	}

	private void LoadMiscellaneousMenu()
	{
		if (!m_miscellaneousMenuLoading)
		{
			if (m_miscellaneousMenu == null)
			{
				m_miscellaneousMenuLoading = true;
				AssetLoader.Get().InstantiatePrefab("MiscellaneousMenu.prefab:ee334ff827a9f834ea8b96e3dd2f5c5d", ShowMiscellaneousMenu);
			}
			else
			{
				m_miscellaneousMenu.Show();
			}
		}
	}

	private void ShowMiscellaneousMenu(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_miscellaneousMenu = go.GetComponent<MiscellaneousMenu>();
		m_miscellaneousMenu.Show();
		m_miscellaneousMenuLoading = false;
	}
}
