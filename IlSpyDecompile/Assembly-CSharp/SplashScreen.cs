using System.Collections;
using System.Collections.Generic;
using bgs;
using Blizzard.T5.Jobs;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Login;
using Hearthstone.Streaming;
using UnityEngine;

[CustomEditClass]
public class SplashScreen : MonoBehaviour
{
	private enum RatingsScreenRegion
	{
		NONE,
		KOREA,
		CHINA
	}

	public delegate void FinishedHandler();

	private const float RATINGS_SCREEN_DISPLAY_TIME = 5f;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_queueSign;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_webLoginCanvasPrefab;

	public GameObject m_quitButtonParent;

	public UberText m_queueTitle;

	public UberText m_queueText;

	public UberText m_queueTime;

	public StandardPegButtonNew m_quitButton;

	public Glow m_glow1;

	public Glow m_glow2;

	public GameObject m_blizzardLogo;

	public GameObject m_demoDisclaimer;

	public StandardPegButtonNew m_devClearLoginButton;

	private const float GLOW_FADE_TIME = 1f;

	private static SplashScreen s_instance;

	private bool m_queueShown;

	private bool m_fadingStarted;

	private bool m_inputCameraSet;

	private const long MAX_MINUTES_TO_SHOW_FOR_QUEUE_ETA = 15L;

	private void Awake()
	{
		s_instance = this;
		OverlayUI.Get().AddGameObject(base.gameObject);
		WebAuthDisplay.SetCanvasPrefab(m_webLoginCanvasPrefab);
		Show();
		LogoAnimation.Get().ShowLogo();
		if (Vars.Key("Aurora.ClientCheck").GetBool(def: true) && BattleNetClient.needsToRun)
		{
			BattleNetClient.quitHearthstoneAndRun();
			return;
		}
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZ_MUSEUM)
		{
			m_demoDisclaimer.SetActive(value: true);
		}
		if (HearthstoneApplication.IsInternal() && (bool)HearthstoneApplication.AllowResetFromFatalError && !HearthstoneServices.Get<GameDownloadManager>().IsAnyDownloadRequestedAndIncomplete)
		{
			m_devClearLoginButton.gameObject.SetActive(value: true);
			if (Cheats.SimulateWebPaneLogin)
			{
				m_devClearLoginButton.SetText("Simulate Login");
				m_devClearLoginButton.AddEventListener(UIEventType.RELEASE, SimulateLogin);
			}
			else
			{
				m_devClearLoginButton.AddEventListener(UIEventType.RELEASE, ClearLogin);
			}
		}
		HearthstoneApplication.Get().WillReset += OnWillReset;
	}

	private void OnDestroy()
	{
		if (m_inputCameraSet)
		{
			if (PegUI.Get() != null && OverlayUI.Get() != null)
			{
				PegUI.Get().RemoveInputCamera(OverlayUI.Get().m_UICamera);
			}
			m_inputCameraSet = false;
		}
		WebAuthDisplay.CloseWebAuth();
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset -= OnWillReset;
		}
		s_instance = null;
	}

	private void Update()
	{
		if (!m_inputCameraSet && PegUI.Get() != null && OverlayUI.Get() != null)
		{
			m_inputCameraSet = true;
			PegUI.Get().AddInputCamera(OverlayUI.Get().m_UICamera);
		}
		HandleKeyboardInput();
	}

	public void HideWebLoginCanvas()
	{
		WebAuthDisplay.HideWebLoginCanvas();
	}

	public static SplashScreen Get()
	{
		return s_instance;
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		Hashtable args = iTween.Hash("amount", 1f, "time", 0.25f, "easeType", iTween.EaseType.easeOutCubic);
		iTween.FadeTo(base.gameObject, args);
		if (!m_fadingStarted)
		{
			FadeGlowsIn();
		}
	}

	public IEnumerator<IAsyncJobResult> Hide(JobDefinition sceneTransitionJob)
	{
		WebAuthDisplay.CloseWebAuth();
		HideWebLoginCanvas();
		yield return new JobDefinition("Splashscreen.AnimateStartupSequence", Job_AnimateStartupSequence(sceneTransitionJob));
	}

	public void HideWebAuth()
	{
		Debug.Log("HideWebAuth");
		WebAuthDisplay.HideWebAuth();
	}

	public void UnHideWebAuth()
	{
		Debug.Log("ShowWebAuth");
		WebAuthDisplay.UnHideWebAuth();
	}

	private void UpdateQueueInfo(Network.QueueInfo queueInfo)
	{
		if (queueInfo.secondsTilEnd / 60 > 15)
		{
			m_queueTime.Text = GameStrings.Format("GLOBAL_DATETIME_GREATER_THAN_X_MINUTES", 15L);
		}
		else
		{
			TimeUtils.ElapsedStringSet sPLASHSCREEN_DATETIME_STRINGSET = TimeUtils.SPLASHSCREEN_DATETIME_STRINGSET;
			m_queueTime.Text = TimeUtils.GetElapsedTimeString((int)queueInfo.secondsTilEnd, sPLASHSCREEN_DATETIME_STRINGSET);
		}
		m_queueTime.TextAlpha = 1f;
		if (!m_queueShown && queueInfo.secondsTilEnd > 1)
		{
			m_queueShown = true;
			if (PlatformSettings.IsMobile())
			{
				m_quitButtonParent.SetActive(value: false);
			}
			else
			{
				m_quitButton.SetOriginalLocalPosition();
				m_quitButton.AddEventListener(UIEventType.RELEASE, QuitGame);
			}
			RenderUtils.SetAlpha(m_queueSign, 0f);
			m_queueSign.SetActive(value: true);
			Hashtable args = iTween.Hash("amount", 1f, "time", 0.5f, "easeType", iTween.EaseType.easeInCubic);
			iTween.FadeTo(m_queueSign, args);
			Hashtable args2 = iTween.Hash("amount", 0f, "time", 0.5f, "includechildren", true, "easeType", iTween.EaseType.easeOutCubic);
			iTween.FadeTo(LogoAnimation.Get().m_logoContainer, args2);
		}
	}

	private void QuitGame(UIEvent e)
	{
		HearthstoneApplication.Get().Exit();
	}

	private void ClearLogin(UIEvent e)
	{
		Debug.Log("Clear Login Button pressed from the Splash Screen!");
		ILoginService loginService = HearthstoneServices.Get<ILoginService>();
		loginService?.ClearAuthentication();
		loginService?.ClearAllSavedAccounts();
	}

	private void SimulateLogin(UIEvent e)
	{
		if (Cheats.SimulateWebPaneLogin)
		{
			Debug.Log("Simulate Login Button pressed from the Splash Screen!");
			WebAuth.m_SimulateWebLoginWebViewStatus = WebAuth.Status.Success;
			m_devClearLoginButton.gameObject.SetActive(value: false);
		}
	}

	private IEnumerator FadeGlowInOut(Glow glow, float timeDelay, bool shouldStartOver)
	{
		yield return new WaitForSeconds(timeDelay);
		Hashtable args = iTween.Hash("time", 1f, "easeType", iTween.EaseType.linear, "from", 0f, "to", 0.4f, "onupdate", "UpdateAlpha", "onupdatetarget", glow.gameObject);
		iTween.ValueTo(glow.gameObject, args);
		Hashtable hashtable = iTween.Hash("delay", 1f, "time", 1f, "easeType", iTween.EaseType.linear, "from", 0.4f, "to", 0f, "onupdate", "UpdateAlpha", "onupdatetarget", glow.gameObject);
		if (shouldStartOver)
		{
			hashtable.Add("oncomplete", "FadeGlowsIn");
			hashtable.Add("oncompletetarget", base.gameObject);
		}
		iTween.ValueTo(glow.gameObject, hashtable);
	}

	private void FadeGlowsIn()
	{
		m_fadingStarted = true;
		StartCoroutine(FadeGlowInOut(m_glow1, 0f, shouldStartOver: false));
		StartCoroutine(FadeGlowInOut(m_glow2, 1f, shouldStartOver: true));
	}

	private RatingsScreenRegion GetRatingsScreenRegion()
	{
		RatingsScreenRegion ratingsScreenRegion = ((BattleNet.GetAccountCountry() == "KOR") ? RatingsScreenRegion.KOREA : RatingsScreenRegion.NONE);
		if (PlatformSettings.IsMobile() && ratingsScreenRegion == RatingsScreenRegion.NONE && MobileDeviceLocale.GetCountryCode() == "KR")
		{
			ratingsScreenRegion = RatingsScreenRegion.KOREA;
		}
		if (PlatformSettings.LocaleVariant == LocaleVariant.China)
		{
			ratingsScreenRegion = RatingsScreenRegion.CHINA;
		}
		return ratingsScreenRegion;
	}

	public bool HandleKeyboardInput()
	{
		return false;
	}

	public IEnumerator<IAsyncJobResult> Job_AnimateStartupSequence(JobDefinition sceneTransitionJob)
	{
		yield return new JobDefinition("Splashscreen,ShowScaryWarnings", Job_ShowScaryWarnings());
		yield return new JobDefinition("Splashscreen.AnimateRatings", Job_AnimateRatings());
		yield return new JobDefinition("SplashScreen.FadeLogoIn", LogoAnimation.Get().Job_FadeLogoIn());
		Processor.QueueJob(sceneTransitionJob);
		yield return new WaitForDuration(2f);
		yield return new JobDefinition("Splashscreen.FadeOutSplashscreen", Job_FadeOutSplashscreen());
		OnSplashScreenFadeOutComplete();
	}

	public IEnumerator<IAsyncJobResult> Job_ShowLoginQueue()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			yield break;
		}
		WaitForCallback<Network.QueueInfo> OnQueueModified = new WaitForCallback<Network.QueueInfo>();
		LoginManager.Get().RegisterQueueModifiedListener(OnQueueModified.Callback);
		if (LoginManager.Get().CurrentQueueInfo != null)
		{
			OnQueueModified.Callback(LoginManager.Get().CurrentQueueInfo);
		}
		while (true)
		{
			yield return OnQueueModified;
			Network.QueueInfo arg = OnQueueModified.Data.Arg1;
			if (arg.position == 0)
			{
				break;
			}
			UpdateQueueInfo(arg);
			OnQueueModified.Reset();
		}
		HearthstoneServices.Get<LoginManager>().RemoveQueueModifiedListener(OnQueueModified.Callback);
		m_queueShown = false;
		m_queueSign.SetActive(value: false);
	}

	private IEnumerator<IAsyncJobResult> Job_ShowScaryWarnings()
	{
		while (DialogManager.Get() == null)
		{
			yield return null;
		}
		while (DialogManager.Get().ShowingDialog())
		{
			yield return null;
		}
		ShowDevicePerformanceWarning();
		ShowGraphicsDeviceWarning();
		ShowTextureCompressionWarning();
	}

	public IEnumerator<IAsyncJobResult> Job_AnimateRatings()
	{
		RatingsScreenRegion ratingsScreenRegion = GetRatingsScreenRegion();
		if (ratingsScreenRegion != 0)
		{
			GameObject ratingsObject = AssetLoader.Get().InstantiatePrefab((ratingsScreenRegion == RatingsScreenRegion.CHINA) ? "China_Ratings_SplashScreen.prefab:5ed637bf515b50e499b1214c53e09d51" : "Korean_Ratings_SplashScreen.prefab:370c3f31230294eb89f264f4537b525c");
			OverlayUI.Get().AddGameObject(ratingsObject);
			Hashtable args = iTween.Hash("amount", 0f, "time", 0.5f, "includechildren", true, "easeType", iTween.EaseType.easeOutCubic);
			LogoAnimation logoAnimation = LogoAnimation.Get();
			iTween.FadeTo(logoAnimation.m_logoContainer, args);
			yield return new WaitForDuration(0.5f);
			logoAnimation.HideLogo();
			ratingsObject.SetActive(value: true);
			Hashtable args2 = iTween.Hash("amount", 1f, "time", 0.5f, "includechildren", true, "easeType", iTween.EaseType.easeInCubic);
			iTween.FadeTo(ratingsObject, args2);
			yield return new WaitForDuration(5.5f);
			Hashtable args3 = iTween.Hash("amount", 0f, "time", 0.5f, "includechildren", true, "easeType", iTween.EaseType.easeInCubic);
			iTween.FadeTo(ratingsObject, args3);
			yield return new WaitForDuration(0.5f);
			ratingsObject.SetActive(value: false);
			Object.Destroy(ratingsObject);
		}
	}

	public IEnumerator<IAsyncJobResult> Job_FadeOutSplashscreen()
	{
		float num = 0.5f;
		Hashtable args = iTween.Hash("amount", 0f, "delay", 0f, "time", num, "easeType", iTween.EaseType.linear, "oncompletetarget", base.gameObject);
		iTween.FadeTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash("amount", 0f, "delay", 0f, "time", num, "easeType", iTween.EaseType.linear, "oncompletetarget", base.gameObject);
		if (m_glow1 != null)
		{
			iTween.FadeTo(m_glow1.gameObject, args2);
		}
		Hashtable args3 = iTween.Hash("amount", 0f, "delay", 0f, "time", num, "easeType", iTween.EaseType.linear, "oncompletetarget", base.gameObject);
		if (m_glow2 != null)
		{
			iTween.FadeTo(m_glow2.gameObject, args3);
		}
		Processor.QueueJob("SplashScreen.FadeLogoOut", LogoAnimation.Get().Job_FadeLogoOut());
		yield return new WaitForDuration(num);
		if (m_glow1 != null)
		{
			m_glow1.gameObject.SetActive(value: false);
		}
		if (m_glow2 != null)
		{
			m_glow2.gameObject.SetActive(value: false);
		}
	}

	private void OnSplashScreenFadeOutComplete()
	{
		Object.Destroy(base.gameObject);
	}

	private void ShowDevicePerformanceWarning()
	{
		if (Options.Get().GetBool(Option.HAS_SHOWN_DEVICE_PERFORMANCE_WARNING, defaultVal: false) || PlatformSettings.s_isDeviceInMinSpec)
		{
			return;
		}
		Options.Get().SetBool(Option.HAS_SHOWN_DEVICE_PERFORMANCE_WARNING, val: true);
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_DEVICE_PERFORMANCE_WARNING_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_DEVICE_PERFORMANCE_WARNING");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_iconSet = AlertPopup.PopupInfo.IconSet.None;
		popupInfo.m_confirmText = GameStrings.Get("GLOBAL_OKAY");
		popupInfo.m_cancelText = GameStrings.Get("GLOBAL_SUPPORT");
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object data)
		{
			if (response == AlertPopup.Response.CANCEL)
			{
				Application.OpenURL(ExternalUrlService.Get().GetSystemRequirementsLink());
			}
		};
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void ShowGraphicsDeviceWarning()
	{
		if (PlatformSettings.RuntimeOS != OSCategory.Android || Options.Get().GetBool(Option.SHOWN_GFX_DEVICE_WARNING, defaultVal: false))
		{
			return;
		}
		Options.Get().SetBool(Option.SHOWN_GFX_DEVICE_WARNING, val: true);
		string text = SystemInfo.graphicsDeviceName.ToLower();
		if (!text.Contains("powervr") || (!text.Contains("540") && !text.Contains("544")))
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_UNRELIABLE_GPU_WARNING_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_UNRELIABLE_GPU_WARNING");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_iconSet = AlertPopup.PopupInfo.IconSet.None;
		popupInfo.m_cancelText = GameStrings.Get("GLOBAL_SUPPORT");
		popupInfo.m_confirmText = GameStrings.Get("GLOBAL_OKAY");
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object data)
		{
			if (response == AlertPopup.Response.CANCEL)
			{
				Application.OpenURL(ExternalUrlService.Get().GetSystemRequirementsLink());
			}
		};
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void ShowTextureCompressionWarning()
	{
		if (PlatformSettings.RuntimeOS != OSCategory.Android || !HearthstoneApplication.IsInternal() || PlatformSettings.LocaleVariant != LocaleVariant.China || AndroidDeviceSettings.Get().IsCurrentTextureFormatSupported())
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_TEXTURE_COMPRESSION_WARNING_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_TEXTURE_COMPRESSION_WARNING");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_iconSet = AlertPopup.PopupInfo.IconSet.None;
		popupInfo.m_cancelText = GameStrings.Get("GLOBAL_SUPPORT");
		popupInfo.m_confirmText = GameStrings.Get("GLOBAL_OKAY");
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object data)
		{
			if (response == AlertPopup.Response.CANCEL)
			{
				Application.OpenURL("http://www.hearthstone.com.cn/download");
			}
		};
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void OnWillReset()
	{
		Object.Destroy(base.gameObject);
	}
}
