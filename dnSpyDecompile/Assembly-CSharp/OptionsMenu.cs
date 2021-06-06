using System;
using System.Collections.Generic;
using Hearthstone;
using Hearthstone.Streaming;
using UnityEngine;

// Token: 0x0200060F RID: 1551
[CustomEditClass]
public class OptionsMenu : MonoBehaviour
{
	// Token: 0x17000521 RID: 1313
	// (get) Token: 0x060056B1 RID: 22193 RVA: 0x000274B4 File Offset: 0x000256B4
	private IGameDownloadManager DownloadManager
	{
		get
		{
			return GameDownloadManagerProvider.Get();
		}
	}

	// Token: 0x060056B2 RID: 22194 RVA: 0x001C6508 File Offset: 0x001C4708
	private void Awake()
	{
		OptionsMenu.s_instance = this;
		this.NORMAL_SCALE = base.transform.localScale;
		this.HIDDEN_SCALE = 0.01f * this.NORMAL_SCALE;
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_graphicsRes.setUnselectedItemText(GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_RESOLUTION_CUSTOM"));
			this.m_graphicsRes.setItemTextCallback(new DropdownControl.itemTextCallback(this.OnGraphicsResolutionDropdownText));
			this.m_graphicsRes.setItemChosenCallback(new DropdownControl.itemChosenCallback(this.OnNewGraphicsResolution));
			foreach (GraphicsResolution value in this.GetGoodGraphicsResolution())
			{
				this.m_graphicsRes.addItem(value);
			}
			this.m_graphicsRes.setSelection(this.GetCurrentGraphicsResolution());
			this.m_fullScreenCheckbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnToggleFullScreenCheckbox));
			this.m_fullScreenCheckbox.SetChecked(Options.Get().GetBool(Option.GFX_FULLSCREEN, Screen.fullScreen));
			this.m_graphicsQuality.addItem(GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_LOW"));
			this.m_graphicsQuality.addItem(GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_MEDIUM"));
			this.m_graphicsQuality.addItem(GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_HIGH"));
			this.m_graphicsQuality.setSelection(this.GetCurrentGraphicsQuality());
			this.m_graphicsQuality.setItemChosenCallback(new DropdownControl.itemChosenCallback(this.OnNewGraphicsQuality));
		}
		this.m_masterVolume.SetValue(Options.Get().GetFloat(Option.SOUND_VOLUME));
		this.m_masterVolume.SetUpdateHandler(new ScrollbarControl.UpdateHandler(this.OnNewMasterVolume));
		this.m_masterVolume.SetFinishHandler(new ScrollbarControl.FinishHandler(this.OnMasterVolumeRelease));
		this.m_musicVolume.SetValue(Options.Get().GetFloat(Option.MUSIC_VOLUME));
		this.m_musicVolume.SetUpdateHandler(new ScrollbarControl.UpdateHandler(this.OnNewMusicVolume));
		if (this.m_backgroundSound != null)
		{
			this.m_backgroundSound.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ToggleBackgroundSound));
			this.m_backgroundSound.SetChecked(Options.Get().GetBool(Option.BACKGROUND_SOUND));
		}
		this.m_languageGroup.gameObject.SetActive(this.LANGUAGE_SELECTION);
		if (this.LANGUAGE_SELECTION)
		{
			this.m_languageDropdown.setFont(this.m_languageDropdownFont.m_Font);
			foreach (object obj in Enum.GetValues(typeof(Locale)))
			{
				Locale locale = (Locale)obj;
				if (locale != Locale.UNKNOWN && (PlatformSettings.LocaleVariant != LocaleVariant.China || locale == Locale.enUS || locale == Locale.zhCN))
				{
					this.m_languageDropdown.addItem(GameStrings.Get(this.StringNameFromLocale(locale)));
				}
			}
			this.m_languageDropdown.setSelection(this.GetCurrentLanguage());
			this.m_languageDropdown.setItemChosenCallback(new DropdownControl.itemChosenCallback(this.OnNewLanguage));
		}
		this.UpdateOtherUI();
		if (TemporaryAccountManager.IsTemporaryAccount())
		{
			this.m_spectatorOpenJoinCheckbox.gameObject.SetActive(false);
			this.m_switchAccountButton.gameObject.SetActive(true);
			this.m_switchAccountButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnSwitchAccountButtonReleased));
		}
		else
		{
			this.m_spectatorOpenJoinCheckbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ToggleSpectatorOpenJoin));
			this.m_spectatorOpenJoinCheckbox.SetChecked(Options.Get().GetBool(Option.SPECTATOR_OPEN_JOIN));
		}
		if (this.m_screenShakeCheckbox != null)
		{
			this.m_screenShakeCheckbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ToggleScreenShake));
			this.m_screenShakeCheckbox.SetChecked(Options.Get().GetBool(Option.SCREEN_SHAKE_ENABLED));
		}
		this.m_miscellaneousButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnMiscellaneousButtonReleased));
		this.CreateInputBlocker();
		this.ShowOrHide(false);
		if (PlatformSettings.IsMobile())
		{
			if (this.m_backgroundSound != null)
			{
				this.m_backgroundSound.gameObject.SetActive(false);
			}
			this.m_graphicsGroup.SetActive(false);
			this.m_graphicsRes.gameObject.SetActive(false);
			this.m_graphicsQuality.gameObject.SetActive(false);
			string text = string.Format("{0} {1}.{2}", GameStrings.Get("GLOBAL_VERSION"), "20.4", 84593);
			string str = Vars.Key("Application.Referral").GetStr("none");
			if (str != "none")
			{
				text = text + "-" + str;
			}
			this.m_versionLabel.Text = text;
			this.m_versionLabel.gameObject.SetActive(true);
		}
		this.UpdateUI();
		GraphicsManager.Get().OnResolutionChangedEvent += this.UpdateMenuItemValues;
	}

	// Token: 0x060056B3 RID: 22195 RVA: 0x001C6A14 File Offset: 0x001C4C14
	public void OnDestroy()
	{
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		}
		if (GraphicsManager.Get() != null)
		{
			GraphicsManager.Get().OnResolutionChangedEvent -= this.UpdateMenuItemValues;
		}
		OptionsMenu.s_instance = null;
	}

	// Token: 0x060056B4 RID: 22196 RVA: 0x001C6A62 File Offset: 0x001C4C62
	public static OptionsMenu Get()
	{
		return OptionsMenu.s_instance;
	}

	// Token: 0x060056B5 RID: 22197 RVA: 0x001C6A69 File Offset: 0x001C4C69
	public OptionsMenu.hideHandler GetHideHandler()
	{
		return this.m_hideHandler;
	}

	// Token: 0x060056B6 RID: 22198 RVA: 0x001C6A71 File Offset: 0x001C4C71
	public void SetHideHandler(OptionsMenu.hideHandler handler)
	{
		this.m_hideHandler = handler;
	}

	// Token: 0x060056B7 RID: 22199 RVA: 0x001C6A7A File Offset: 0x001C4C7A
	public void RemoveHideHandler(OptionsMenu.hideHandler handler)
	{
		if (this.m_hideHandler == handler)
		{
			this.m_hideHandler = null;
		}
	}

	// Token: 0x060056B8 RID: 22200 RVA: 0x001C6A91 File Offset: 0x001C4C91
	public bool IsShown()
	{
		return this.m_isShown;
	}

	// Token: 0x060056B9 RID: 22201 RVA: 0x001C6A9C File Offset: 0x001C4C9C
	public void Show()
	{
		this.UpdateOtherUI();
		this.ShowOrHide(true);
		AnimationUtil.ShowWithPunch(base.gameObject, this.HIDDEN_SCALE, 1.1f * this.NORMAL_SCALE, this.NORMAL_SCALE, null, true, null, null, null);
	}

	// Token: 0x060056BA RID: 22202 RVA: 0x001C6AE2 File Offset: 0x001C4CE2
	public void Hide(bool callHideHandler = true)
	{
		this.ShowOrHide(false);
		if (this.m_hideHandler != null && callHideHandler)
		{
			this.m_hideHandler();
			this.m_hideHandler = null;
		}
	}

	// Token: 0x060056BB RID: 22203 RVA: 0x001C6B0C File Offset: 0x001C4D0C
	private GraphicsResolution GetCurrentGraphicsResolution()
	{
		int @int = Options.Get().GetInt(Option.GFX_WIDTH, Screen.currentResolution.width);
		int int2 = Options.Get().GetInt(Option.GFX_HEIGHT, Screen.currentResolution.height);
		return GraphicsResolution.create(@int, int2);
	}

	// Token: 0x060056BC RID: 22204 RVA: 0x001C6B54 File Offset: 0x001C4D54
	private string GetCurrentGraphicsQuality()
	{
		switch (Options.Get().GetInt(Option.GFX_QUALITY))
		{
		case 0:
			return GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_LOW");
		case 1:
			return GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_MEDIUM");
		case 2:
			return GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_HIGH");
		default:
			return GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_LOW");
		}
	}

	// Token: 0x060056BD RID: 22205 RVA: 0x001C6BB0 File Offset: 0x001C4DB0
	private List<GraphicsResolution> GetGoodGraphicsResolution()
	{
		if (this.m_fullScreenResolutions.Count == 0)
		{
			foreach (GraphicsResolution graphicsResolution in GraphicsResolution.list)
			{
				if (graphicsResolution.x >= 1024 && graphicsResolution.y >= 728 && (double)graphicsResolution.aspectRatio - 0.01 <= 1.7777777777777777 && (double)graphicsResolution.aspectRatio + 0.01 >= 1.3333333333333333)
				{
					this.m_fullScreenResolutions.Add(graphicsResolution);
				}
			}
		}
		return this.m_fullScreenResolutions;
	}

	// Token: 0x060056BE RID: 22206 RVA: 0x001C6C70 File Offset: 0x001C4E70
	private string GetCurrentLanguage()
	{
		return GameStrings.Get(this.StringNameFromLocale(Localization.GetLocale()));
	}

	// Token: 0x060056BF RID: 22207 RVA: 0x001C6C82 File Offset: 0x001C4E82
	private void ShowOrHide(bool showOrHide)
	{
		this.m_isShown = showOrHide;
		base.gameObject.SetActive(showOrHide);
		this.UpdateUI();
	}

	// Token: 0x060056C0 RID: 22208 RVA: 0x001C6C9D File Offset: 0x001C4E9D
	private string StringNameFromLocale(Locale locale)
	{
		return "GLOBAL_LANGUAGE_NATIVE_" + locale.ToString().ToUpper();
	}

	// Token: 0x060056C1 RID: 22209 RVA: 0x001C6CBC File Offset: 0x001C4EBC
	private void UpdateOtherUI()
	{
		bool active = this.CanShowOtherMenuOptions();
		if (TemporaryAccountManager.IsTemporaryAccount())
		{
			this.m_middlePane.gameObject.SetActive(active);
			return;
		}
		this.m_middleBottomRightPane.gameObject.SetActive(active);
	}

	// Token: 0x060056C2 RID: 22210 RVA: 0x001C6CFC File Offset: 0x001C4EFC
	private void UpdateUI()
	{
		this.m_middleLeftPaneLabel.SetActive(true);
		this.m_middleRightPaneLabel.SetActive(true);
		this.m_middleBottomLeftPane.UpdateSlices();
		this.m_middleBottomRightPane.UpdateSlices();
		this.m_middleBottomPane.UpdateSlices();
		this.m_leftPane.UpdateSlices();
		this.m_rightPane.UpdateSlices();
		this.m_middlePane.UpdateSlices();
		this.m_middleLeftPaneLabel.SetActive(false);
		this.m_middleRightPaneLabel.SetActive(false);
	}

	// Token: 0x060056C3 RID: 22211 RVA: 0x001C6D7C File Offset: 0x001C4F7C
	private bool CanShowOtherMenuOptions()
	{
		return UserAttentionManager.GetAvailabilityBlockerReason(false) == AvailabilityBlockerReasons.NONE && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.PACKOPENING) && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.ADVENTURE) && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.CREDITS) && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FRIENDLY);
	}

	// Token: 0x060056C4 RID: 22212 RVA: 0x001C6DD4 File Offset: 0x001C4FD4
	private void CreateInputBlocker()
	{
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "OptionMenuInputBlocker", this, base.transform, 10f);
		gameObject.layer = base.gameObject.layer;
		this.m_inputBlocker = gameObject.AddComponent<PegUIElement>();
		this.m_inputBlocker.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Hide(true);
		});
	}

	// Token: 0x060056C5 RID: 22213 RVA: 0x001C6E40 File Offset: 0x001C5040
	private void UpdateMenuItemValues(int newWidth, int newHeight)
	{
		if (this.m_fullScreenCheckbox.IsChecked() == Screen.fullScreen)
		{
			if (!Screen.fullScreen)
			{
				this.m_graphicsRes.setSelection(GraphicsResolution.create(newWidth, newHeight));
			}
			return;
		}
		this.m_fullScreenCheckbox.SetChecked(Screen.fullScreen);
		GraphicsResolution graphicsResolution = this.m_graphicsRes.getSelection() as GraphicsResolution;
		if (graphicsResolution == null || this.m_fullScreenCheckbox.IsChecked())
		{
			this.m_graphicsRes.setSelectionToFirstItem();
			graphicsResolution = (this.m_graphicsRes.getSelection() as GraphicsResolution);
		}
		else if (!this.m_fullScreenCheckbox.IsChecked())
		{
			graphicsResolution = GraphicsResolution.create(newWidth, newHeight);
		}
		if (graphicsResolution == null)
		{
			return;
		}
		int x = graphicsResolution.x;
		int y = graphicsResolution.y;
		this.m_graphicsRes.setSelection(GraphicsResolution.create(x, y));
	}

	// Token: 0x060056C6 RID: 22214 RVA: 0x001C6F04 File Offset: 0x001C5104
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		if (SceneMgr.Get().GetNextMode() == SceneMgr.Mode.FATAL_ERROR)
		{
			this.Hide(true);
		}
	}

	// Token: 0x060056C7 RID: 22215 RVA: 0x001C6F1C File Offset: 0x001C511C
	private void OnToggleFullScreenCheckbox(UIEvent e)
	{
		GraphicsResolution graphicsResolution = this.m_graphicsRes.getSelection() as GraphicsResolution;
		if (graphicsResolution == null)
		{
			this.m_graphicsRes.setSelectionToFirstItem();
			graphicsResolution = (this.m_graphicsRes.getSelection() as GraphicsResolution);
		}
		if (graphicsResolution == null)
		{
			return;
		}
		int width = graphicsResolution.x;
		int height = graphicsResolution.y;
		if (this.m_fullScreenCheckbox.IsChecked())
		{
			width = Screen.currentResolution.width;
			height = Screen.currentResolution.height;
		}
		this.m_graphicsRes.setSelection(GraphicsResolution.create(width, height));
		GraphicsManager.Get().SetScreenResolution(width, height, this.m_fullScreenCheckbox.IsChecked());
		Options.Get().SetBool(Option.GFX_FULLSCREEN, this.m_fullScreenCheckbox.IsChecked());
	}

	// Token: 0x060056C8 RID: 22216 RVA: 0x001C6FD4 File Offset: 0x001C51D4
	private void OnNewGraphicsQuality(object selection, object prevSelection)
	{
		GraphicsQuality renderQualityLevel = GraphicsQuality.Low;
		string a = (string)selection;
		if (a == GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_LOW"))
		{
			renderQualityLevel = GraphicsQuality.Low;
		}
		else if (a == GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_MEDIUM"))
		{
			renderQualityLevel = GraphicsQuality.Medium;
		}
		else if (a == GameStrings.Get("GLOBAL_OPTIONS_GRAPHICS_QUALITY_HIGH"))
		{
			renderQualityLevel = GraphicsQuality.High;
		}
		Log.Options.Print("Graphics Quality: " + renderQualityLevel.ToString(), Array.Empty<object>());
		GraphicsManager.Get().RenderQualityLevel = renderQualityLevel;
	}

	// Token: 0x060056C9 RID: 22217 RVA: 0x001C705C File Offset: 0x001C525C
	private void OnNewGraphicsResolution(object selection, object prevSelection)
	{
		GraphicsResolution graphicsResolution = (GraphicsResolution)selection;
		GraphicsManager.Get().SetScreenResolution(graphicsResolution.x, graphicsResolution.y, this.m_fullScreenCheckbox.IsChecked());
		Options.Get().SetInt(Option.GFX_WIDTH, graphicsResolution.x);
		Options.Get().SetInt(Option.GFX_HEIGHT, graphicsResolution.y);
	}

	// Token: 0x060056CA RID: 22218 RVA: 0x001C70B8 File Offset: 0x001C52B8
	private void OnNewLanguage(object selection, object prevSelection)
	{
		if (selection == prevSelection)
		{
			return;
		}
		long num = FreeSpace.Measure();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		if (num < 314572800L)
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
			popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnChangeLanguageConfirmationResponse);
			popupInfo.m_responseUserData = selection;
		}
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060056CB RID: 22219 RVA: 0x001C7170 File Offset: 0x001C5370
	private void OnChangeLanguageConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			this.m_languageDropdown.setSelection(this.GetCurrentLanguage());
			return;
		}
		string a = (string)userData;
		Locale locale = Locale.UNKNOWN;
		foreach (object obj in Enum.GetValues(typeof(Locale)))
		{
			Locale locale2 = (Locale)obj;
			if (a == GameStrings.Get(this.StringNameFromLocale(locale2)))
			{
				locale = locale2;
				break;
			}
		}
		if (locale == Locale.UNKNOWN)
		{
			Debug.LogError(string.Format("OptionsMenu.OnChangeLanguageConfirmationResponse() - locale not found", Array.Empty<object>()));
			return;
		}
		TelemetryManager.Client().SendLanguageChanged(Localization.GetLocaleName(), locale.ToString());
		Localization.SetLocale(locale);
		Options.Get().SetString(Option.LOCALE, locale.ToString());
		Debug.LogFormat("Change Locale: {0}", new object[]
		{
			locale
		});
		this.Hide(false);
		HearthstoneApplication.Get().IsLocaleChanged = true;
		if (this.DownloadManager.ShouldDownloadLocalizedAssets)
		{
			HearthstoneApplication.Get().Resetting += this.StartUpdateProcessAfterReset;
		}
		HearthstoneApplication.Get().Reset();
	}

	// Token: 0x060056CC RID: 22220 RVA: 0x001C72B4 File Offset: 0x001C54B4
	private void StartUpdateProcessAfterReset()
	{
		HearthstoneApplication.Get().Resetting -= this.StartUpdateProcessAfterReset;
		this.DownloadManager.StartUpdateProcess(true);
	}

	// Token: 0x060056CD RID: 22221 RVA: 0x001C72D8 File Offset: 0x001C54D8
	private string OnGraphicsResolutionDropdownText(object val)
	{
		GraphicsResolution graphicsResolution = (GraphicsResolution)val;
		return string.Format("{0} x {1}", graphicsResolution.x, graphicsResolution.y);
	}

	// Token: 0x060056CE RID: 22222 RVA: 0x001C730C File Offset: 0x001C550C
	private void OnNewMasterVolume(float newVolume)
	{
		Options.Get().SetFloat(Option.SOUND_VOLUME, newVolume);
	}

	// Token: 0x060056CF RID: 22223 RVA: 0x001C731C File Offset: 0x001C551C
	private void OnMasterVolumeRelease()
	{
		SoundManager.LoadedCallback callback = delegate(AudioSource source, object userData)
		{
			SoundManager.Get().Set3d(source, false);
		};
		SoundManager.Get().LoadAndPlay("UI_MouseClick_01.prefab:fa537702a0db1c3478c989967458788b", base.gameObject, 1f, callback);
	}

	// Token: 0x060056D0 RID: 22224 RVA: 0x001C7369 File Offset: 0x001C5569
	private void OnNewMusicVolume(float newVolume)
	{
		Options.Get().SetFloat(Option.MUSIC_VOLUME, newVolume);
	}

	// Token: 0x060056D1 RID: 22225 RVA: 0x001C7377 File Offset: 0x001C5577
	private void ToggleBackgroundSound(UIEvent e)
	{
		Options.Get().SetBool(Option.BACKGROUND_SOUND, this.m_backgroundSound.IsChecked());
	}

	// Token: 0x060056D2 RID: 22226 RVA: 0x001C7390 File Offset: 0x001C5590
	private void OnSwitchAccountButtonReleased(UIEvent e)
	{
		this.Hide(false);
		this.m_controller.ShowRegionMenuWithDefaultSettings();
	}

	// Token: 0x060056D3 RID: 22227 RVA: 0x001C73A4 File Offset: 0x001C55A4
	private void ToggleSpectatorOpenJoin(UIEvent e)
	{
		Options.Get().SetBool(Option.SPECTATOR_OPEN_JOIN, this.m_spectatorOpenJoinCheckbox.IsChecked());
	}

	// Token: 0x060056D4 RID: 22228 RVA: 0x001C73C0 File Offset: 0x001C55C0
	private void ToggleScreenShake(UIEvent e)
	{
		Options.Get().SetBool(Option.SCREEN_SHAKE_ENABLED, this.m_screenShakeCheckbox.IsChecked());
	}

	// Token: 0x060056D5 RID: 22229 RVA: 0x001C73D9 File Offset: 0x001C55D9
	private void OnMiscellaneousButtonReleased(UIEvent e)
	{
		this.LoadMiscellaneousMenu();
		this.Hide(false);
	}

	// Token: 0x060056D6 RID: 22230 RVA: 0x001C73E8 File Offset: 0x001C55E8
	private void LoadMiscellaneousMenu()
	{
		if (!this.m_miscellaneousMenuLoading)
		{
			if (this.m_miscellaneousMenu == null)
			{
				this.m_miscellaneousMenuLoading = true;
				AssetLoader.Get().InstantiatePrefab("MiscellaneousMenu.prefab:ee334ff827a9f834ea8b96e3dd2f5c5d", new PrefabCallback<GameObject>(this.ShowMiscellaneousMenu), null, AssetLoadingOptions.None);
				return;
			}
			this.m_miscellaneousMenu.Show();
		}
	}

	// Token: 0x060056D7 RID: 22231 RVA: 0x001C7441 File Offset: 0x001C5641
	private void ShowMiscellaneousMenu(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_miscellaneousMenu = go.GetComponent<MiscellaneousMenu>();
		this.m_miscellaneousMenu.Show();
		this.m_miscellaneousMenuLoading = false;
	}

	// Token: 0x04004AA6 RID: 19110
	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_leftPane;

	// Token: 0x04004AA7 RID: 19111
	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_rightPane;

	// Token: 0x04004AA8 RID: 19112
	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_middlePane;

	// Token: 0x04004AA9 RID: 19113
	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_middleBottomPane;

	// Token: 0x04004AAA RID: 19114
	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_middleBottomLeftPane;

	// Token: 0x04004AAB RID: 19115
	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_middleBottomRightPane;

	// Token: 0x04004AAC RID: 19116
	[CustomEditField(Sections = "Placeholder")]
	public GameObject m_middleLeftPaneLabel;

	// Token: 0x04004AAD RID: 19117
	[CustomEditField(Sections = "Placeholder")]
	public GameObject m_middleRightPaneLabel;

	// Token: 0x04004AAE RID: 19118
	[CustomEditField(Sections = "Graphics")]
	public GameObject m_graphicsGroup;

	// Token: 0x04004AAF RID: 19119
	[CustomEditField(Sections = "Graphics")]
	public DropdownControl m_graphicsRes;

	// Token: 0x04004AB0 RID: 19120
	[CustomEditField(Sections = "Graphics")]
	public DropdownControl m_graphicsQuality;

	// Token: 0x04004AB1 RID: 19121
	[CustomEditField(Sections = "Graphics")]
	public CheckBox m_fullScreenCheckbox;

	// Token: 0x04004AB2 RID: 19122
	[CustomEditField(Sections = "Sound")]
	public GameObject m_soundGroup;

	// Token: 0x04004AB3 RID: 19123
	[CustomEditField(Sections = "Sound")]
	public ScrollbarControl m_masterVolume;

	// Token: 0x04004AB4 RID: 19124
	[CustomEditField(Sections = "Sound")]
	public ScrollbarControl m_musicVolume;

	// Token: 0x04004AB5 RID: 19125
	[CustomEditField(Sections = "Sound")]
	public CheckBox m_backgroundSound;

	// Token: 0x04004AB6 RID: 19126
	[CustomEditField(Sections = "Language")]
	public GameObject m_languageGroup;

	// Token: 0x04004AB7 RID: 19127
	[CustomEditField(Sections = "Language")]
	public DropdownControl m_languageDropdown;

	// Token: 0x04004AB8 RID: 19128
	[CustomEditField(Sections = "Language")]
	public FontDefinition m_languageDropdownFont;

	// Token: 0x04004AB9 RID: 19129
	[CustomEditField(Sections = "Language")]
	public CheckBox m_languagePackCheckbox;

	// Token: 0x04004ABA RID: 19130
	[CustomEditField(Sections = "Other")]
	public CheckBox m_spectatorOpenJoinCheckbox;

	// Token: 0x04004ABB RID: 19131
	[CustomEditField(Sections = "Other")]
	public CheckBox m_screenShakeCheckbox;

	// Token: 0x04004ABC RID: 19132
	[CustomEditField(Sections = "Other")]
	public UIBButton m_switchAccountButton;

	// Token: 0x04004ABD RID: 19133
	[CustomEditField(Sections = "Other")]
	public UIBButton m_miscellaneousButton;

	// Token: 0x04004ABE RID: 19134
	[CustomEditField(Sections = "Internal Stuff")]
	public UberText m_versionLabel;

	// Token: 0x04004ABF RID: 19135
	private static OptionsMenu s_instance;

	// Token: 0x04004AC0 RID: 19136
	private bool m_isShown;

	// Token: 0x04004AC1 RID: 19137
	private OptionsMenu.hideHandler m_hideHandler;

	// Token: 0x04004AC2 RID: 19138
	private MiscellaneousMenu m_miscellaneousMenu;

	// Token: 0x04004AC3 RID: 19139
	private bool m_miscellaneousMenuLoading;

	// Token: 0x04004AC4 RID: 19140
	private PegUIElement m_inputBlocker;

	// Token: 0x04004AC5 RID: 19141
	private RegionSwitchMenuController m_controller = new RegionSwitchMenuController();

	// Token: 0x04004AC6 RID: 19142
	private List<GraphicsResolution> m_fullScreenResolutions = new List<GraphicsResolution>();

	// Token: 0x04004AC7 RID: 19143
	private Vector3 NORMAL_SCALE;

	// Token: 0x04004AC8 RID: 19144
	private Vector3 HIDDEN_SCALE;

	// Token: 0x04004AC9 RID: 19145
	private readonly PlatformDependentValue<bool> LANGUAGE_SELECTION = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = true,
		Android = true,
		PC = false,
		Mac = false
	};

	// Token: 0x02002111 RID: 8465
	// (Invoke) Token: 0x06012221 RID: 74273
	public delegate void hideHandler();
}
