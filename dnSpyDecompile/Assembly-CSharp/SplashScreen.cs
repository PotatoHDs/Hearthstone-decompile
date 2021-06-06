using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using Blizzard.T5.Jobs;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Login;
using Hearthstone.Streaming;
using UnityEngine;

// Token: 0x020006DF RID: 1759
[CustomEditClass]
public class SplashScreen : MonoBehaviour
{
	// Token: 0x06006226 RID: 25126 RVA: 0x002006FC File Offset: 0x001FE8FC
	private void Awake()
	{
		SplashScreen.s_instance = this;
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		WebAuthDisplay.SetCanvasPrefab(this.m_webLoginCanvasPrefab);
		this.Show();
		LogoAnimation.Get().ShowLogo();
		if (Vars.Key("Aurora.ClientCheck").GetBool(true) && BattleNetClient.needsToRun)
		{
			BattleNetClient.quitHearthstoneAndRun();
			return;
		}
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZ_MUSEUM)
		{
			this.m_demoDisclaimer.SetActive(true);
		}
		if (HearthstoneApplication.IsInternal() && HearthstoneApplication.AllowResetFromFatalError && !HearthstoneServices.Get<GameDownloadManager>().IsAnyDownloadRequestedAndIncomplete)
		{
			this.m_devClearLoginButton.gameObject.SetActive(true);
			if (Cheats.SimulateWebPaneLogin)
			{
				this.m_devClearLoginButton.SetText("Simulate Login");
				this.m_devClearLoginButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.SimulateLogin));
			}
			else
			{
				this.m_devClearLoginButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ClearLogin));
			}
		}
		HearthstoneApplication.Get().WillReset += this.OnWillReset;
	}

	// Token: 0x06006227 RID: 25127 RVA: 0x0020080C File Offset: 0x001FEA0C
	private void OnDestroy()
	{
		if (this.m_inputCameraSet)
		{
			if (PegUI.Get() != null && OverlayUI.Get() != null)
			{
				PegUI.Get().RemoveInputCamera(OverlayUI.Get().m_UICamera);
			}
			this.m_inputCameraSet = false;
		}
		WebAuthDisplay.CloseWebAuth();
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset -= this.OnWillReset;
		}
		SplashScreen.s_instance = null;
	}

	// Token: 0x06006228 RID: 25128 RVA: 0x00200884 File Offset: 0x001FEA84
	private void Update()
	{
		if (!this.m_inputCameraSet && PegUI.Get() != null && OverlayUI.Get() != null)
		{
			this.m_inputCameraSet = true;
			PegUI.Get().AddInputCamera(OverlayUI.Get().m_UICamera);
		}
		this.HandleKeyboardInput();
	}

	// Token: 0x06006229 RID: 25129 RVA: 0x002008D5 File Offset: 0x001FEAD5
	public void HideWebLoginCanvas()
	{
		WebAuthDisplay.HideWebLoginCanvas();
	}

	// Token: 0x0600622A RID: 25130 RVA: 0x002008DC File Offset: 0x001FEADC
	public static SplashScreen Get()
	{
		return SplashScreen.s_instance;
	}

	// Token: 0x0600622B RID: 25131 RVA: 0x002008E4 File Offset: 0x001FEAE4
	public void Show()
	{
		base.gameObject.SetActive(true);
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			1f,
			"time",
			0.25f,
			"easeType",
			iTween.EaseType.easeOutCubic
		});
		iTween.FadeTo(base.gameObject, args);
		if (!this.m_fadingStarted)
		{
			this.FadeGlowsIn();
		}
	}

	// Token: 0x0600622C RID: 25132 RVA: 0x0020095E File Offset: 0x001FEB5E
	public IEnumerator<IAsyncJobResult> Hide(JobDefinition sceneTransitionJob)
	{
		WebAuthDisplay.CloseWebAuth();
		this.HideWebLoginCanvas();
		yield return new JobDefinition("Splashscreen.AnimateStartupSequence", this.Job_AnimateStartupSequence(sceneTransitionJob), Array.Empty<IJobDependency>());
		yield break;
	}

	// Token: 0x0600622D RID: 25133 RVA: 0x00200974 File Offset: 0x001FEB74
	public void HideWebAuth()
	{
		Debug.Log("HideWebAuth");
		WebAuthDisplay.HideWebAuth();
	}

	// Token: 0x0600622E RID: 25134 RVA: 0x00200985 File Offset: 0x001FEB85
	public void UnHideWebAuth()
	{
		Debug.Log("ShowWebAuth");
		WebAuthDisplay.UnHideWebAuth();
	}

	// Token: 0x0600622F RID: 25135 RVA: 0x00200998 File Offset: 0x001FEB98
	private void UpdateQueueInfo(Network.QueueInfo queueInfo)
	{
		if (queueInfo.secondsTilEnd / 60L > 15L)
		{
			this.m_queueTime.Text = GameStrings.Format("GLOBAL_DATETIME_GREATER_THAN_X_MINUTES", new object[]
			{
				15L
			});
		}
		else
		{
			global::TimeUtils.ElapsedStringSet splashscreen_DATETIME_STRINGSET = global::TimeUtils.SPLASHSCREEN_DATETIME_STRINGSET;
			this.m_queueTime.Text = global::TimeUtils.GetElapsedTimeString((int)queueInfo.secondsTilEnd, splashscreen_DATETIME_STRINGSET, false);
		}
		this.m_queueTime.TextAlpha = 1f;
		if (!this.m_queueShown && queueInfo.secondsTilEnd > 1L)
		{
			this.m_queueShown = true;
			if (PlatformSettings.IsMobile())
			{
				this.m_quitButtonParent.SetActive(false);
			}
			else
			{
				this.m_quitButton.SetOriginalLocalPosition();
				this.m_quitButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.QuitGame));
			}
			RenderUtils.SetAlpha(this.m_queueSign, 0f);
			this.m_queueSign.SetActive(true);
			Hashtable args = iTween.Hash(new object[]
			{
				"amount",
				1f,
				"time",
				0.5f,
				"easeType",
				iTween.EaseType.easeInCubic
			});
			iTween.FadeTo(this.m_queueSign, args);
			Hashtable args2 = iTween.Hash(new object[]
			{
				"amount",
				0f,
				"time",
				0.5f,
				"includechildren",
				true,
				"easeType",
				iTween.EaseType.easeOutCubic
			});
			iTween.FadeTo(LogoAnimation.Get().m_logoContainer, args2);
		}
	}

	// Token: 0x06006230 RID: 25136 RVA: 0x001DD3FF File Offset: 0x001DB5FF
	private void QuitGame(UIEvent e)
	{
		HearthstoneApplication.Get().Exit();
	}

	// Token: 0x06006231 RID: 25137 RVA: 0x00200B3A File Offset: 0x001FED3A
	private void ClearLogin(UIEvent e)
	{
		Debug.Log("Clear Login Button pressed from the Splash Screen!");
		ILoginService loginService = HearthstoneServices.Get<ILoginService>();
		if (loginService != null)
		{
			loginService.ClearAuthentication();
		}
		if (loginService == null)
		{
			return;
		}
		loginService.ClearAllSavedAccounts();
	}

	// Token: 0x06006232 RID: 25138 RVA: 0x00200B61 File Offset: 0x001FED61
	private void SimulateLogin(UIEvent e)
	{
		if (!Cheats.SimulateWebPaneLogin)
		{
			return;
		}
		Debug.Log("Simulate Login Button pressed from the Splash Screen!");
		WebAuth.m_SimulateWebLoginWebViewStatus = WebAuth.Status.Success;
		this.m_devClearLoginButton.gameObject.SetActive(false);
	}

	// Token: 0x06006233 RID: 25139 RVA: 0x00200B8C File Offset: 0x001FED8C
	private IEnumerator FadeGlowInOut(Glow glow, float timeDelay, bool shouldStartOver)
	{
		yield return new WaitForSeconds(timeDelay);
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			1f,
			"easeType",
			iTween.EaseType.linear,
			"from",
			0f,
			"to",
			0.4f,
			"onupdate",
			"UpdateAlpha",
			"onupdatetarget",
			glow.gameObject
		});
		iTween.ValueTo(glow.gameObject, args);
		Hashtable hashtable = iTween.Hash(new object[]
		{
			"delay",
			1f,
			"time",
			1f,
			"easeType",
			iTween.EaseType.linear,
			"from",
			0.4f,
			"to",
			0f,
			"onupdate",
			"UpdateAlpha",
			"onupdatetarget",
			glow.gameObject
		});
		if (shouldStartOver)
		{
			hashtable.Add("oncomplete", "FadeGlowsIn");
			hashtable.Add("oncompletetarget", base.gameObject);
		}
		iTween.ValueTo(glow.gameObject, hashtable);
		yield break;
	}

	// Token: 0x06006234 RID: 25140 RVA: 0x00200BB0 File Offset: 0x001FEDB0
	private void FadeGlowsIn()
	{
		this.m_fadingStarted = true;
		base.StartCoroutine(this.FadeGlowInOut(this.m_glow1, 0f, false));
		base.StartCoroutine(this.FadeGlowInOut(this.m_glow2, 1f, true));
	}

	// Token: 0x06006235 RID: 25141 RVA: 0x00200BEC File Offset: 0x001FEDEC
	private SplashScreen.RatingsScreenRegion GetRatingsScreenRegion()
	{
		SplashScreen.RatingsScreenRegion ratingsScreenRegion = (BattleNet.GetAccountCountry() == "KOR") ? SplashScreen.RatingsScreenRegion.KOREA : SplashScreen.RatingsScreenRegion.NONE;
		if (PlatformSettings.IsMobile() && ratingsScreenRegion == SplashScreen.RatingsScreenRegion.NONE && MobileDeviceLocale.GetCountryCode() == "KR")
		{
			ratingsScreenRegion = SplashScreen.RatingsScreenRegion.KOREA;
		}
		if (PlatformSettings.LocaleVariant == LocaleVariant.China)
		{
			ratingsScreenRegion = SplashScreen.RatingsScreenRegion.CHINA;
		}
		return ratingsScreenRegion;
	}

	// Token: 0x06006236 RID: 25142 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool HandleKeyboardInput()
	{
		return false;
	}

	// Token: 0x06006237 RID: 25143 RVA: 0x00200C37 File Offset: 0x001FEE37
	public IEnumerator<IAsyncJobResult> Job_AnimateStartupSequence(JobDefinition sceneTransitionJob)
	{
		yield return new JobDefinition("Splashscreen,ShowScaryWarnings", this.Job_ShowScaryWarnings(), Array.Empty<IJobDependency>());
		yield return new JobDefinition("Splashscreen.AnimateRatings", this.Job_AnimateRatings(), Array.Empty<IJobDependency>());
		yield return new JobDefinition("SplashScreen.FadeLogoIn", LogoAnimation.Get().Job_FadeLogoIn(), Array.Empty<IJobDependency>());
		Processor.QueueJob(sceneTransitionJob);
		yield return new WaitForDuration(2f);
		yield return new JobDefinition("Splashscreen.FadeOutSplashscreen", this.Job_FadeOutSplashscreen(), Array.Empty<IJobDependency>());
		this.OnSplashScreenFadeOutComplete();
		yield break;
	}

	// Token: 0x06006238 RID: 25144 RVA: 0x00200C4D File Offset: 0x001FEE4D
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
		for (;;)
		{
			yield return OnQueueModified;
			Network.QueueInfo arg = OnQueueModified.Data.Arg1;
			if (arg.position == 0)
			{
				break;
			}
			this.UpdateQueueInfo(arg);
			OnQueueModified.Reset();
		}
		HearthstoneServices.Get<LoginManager>().RemoveQueueModifiedListener(OnQueueModified.Callback);
		this.m_queueShown = false;
		this.m_queueSign.SetActive(false);
		yield break;
	}

	// Token: 0x06006239 RID: 25145 RVA: 0x00200C5C File Offset: 0x001FEE5C
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
		this.ShowDevicePerformanceWarning();
		this.ShowGraphicsDeviceWarning();
		this.ShowTextureCompressionWarning();
		yield break;
	}

	// Token: 0x0600623A RID: 25146 RVA: 0x00200C6B File Offset: 0x001FEE6B
	public IEnumerator<IAsyncJobResult> Job_AnimateRatings()
	{
		SplashScreen.RatingsScreenRegion ratingsScreenRegion = this.GetRatingsScreenRegion();
		if (ratingsScreenRegion == SplashScreen.RatingsScreenRegion.NONE)
		{
			yield break;
		}
		GameObject ratingsObject = AssetLoader.Get().InstantiatePrefab((ratingsScreenRegion == SplashScreen.RatingsScreenRegion.CHINA) ? "China_Ratings_SplashScreen.prefab:5ed637bf515b50e499b1214c53e09d51" : "Korean_Ratings_SplashScreen.prefab:370c3f31230294eb89f264f4537b525c", AssetLoadingOptions.None);
		OverlayUI.Get().AddGameObject(ratingsObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"time",
			0.5f,
			"includechildren",
			true,
			"easeType",
			iTween.EaseType.easeOutCubic
		});
		LogoAnimation logoAnimation = LogoAnimation.Get();
		iTween.FadeTo(logoAnimation.m_logoContainer, args);
		yield return new WaitForDuration(0.5f);
		logoAnimation.HideLogo();
		ratingsObject.SetActive(true);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"amount",
			1f,
			"time",
			0.5f,
			"includechildren",
			true,
			"easeType",
			iTween.EaseType.easeInCubic
		});
		iTween.FadeTo(ratingsObject, args2);
		yield return new WaitForDuration(5.5f);
		Hashtable args3 = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"time",
			0.5f,
			"includechildren",
			true,
			"easeType",
			iTween.EaseType.easeInCubic
		});
		iTween.FadeTo(ratingsObject, args3);
		yield return new WaitForDuration(0.5f);
		ratingsObject.SetActive(false);
		UnityEngine.Object.Destroy(ratingsObject);
		yield break;
	}

	// Token: 0x0600623B RID: 25147 RVA: 0x00200C7A File Offset: 0x001FEE7A
	public IEnumerator<IAsyncJobResult> Job_FadeOutSplashscreen()
	{
		float num = 0.5f;
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"delay",
			0f,
			"time",
			num,
			"easeType",
			iTween.EaseType.linear,
			"oncompletetarget",
			base.gameObject
		});
		iTween.FadeTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"delay",
			0f,
			"time",
			num,
			"easeType",
			iTween.EaseType.linear,
			"oncompletetarget",
			base.gameObject
		});
		if (this.m_glow1 != null)
		{
			iTween.FadeTo(this.m_glow1.gameObject, args2);
		}
		Hashtable args3 = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"delay",
			0f,
			"time",
			num,
			"easeType",
			iTween.EaseType.linear,
			"oncompletetarget",
			base.gameObject
		});
		if (this.m_glow2 != null)
		{
			iTween.FadeTo(this.m_glow2.gameObject, args3);
		}
		Processor.QueueJob("SplashScreen.FadeLogoOut", LogoAnimation.Get().Job_FadeLogoOut(), Array.Empty<IJobDependency>());
		yield return new WaitForDuration(num);
		if (this.m_glow1 != null)
		{
			this.m_glow1.gameObject.SetActive(false);
		}
		if (this.m_glow2 != null)
		{
			this.m_glow2.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x0600623C RID: 25148 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	private void OnSplashScreenFadeOutComplete()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600623D RID: 25149 RVA: 0x00200C8C File Offset: 0x001FEE8C
	private void ShowDevicePerformanceWarning()
	{
		if (Options.Get().GetBool(Option.HAS_SHOWN_DEVICE_PERFORMANCE_WARNING, false))
		{
			return;
		}
		if (PlatformSettings.s_isDeviceInMinSpec)
		{
			return;
		}
		Options.Get().SetBool(Option.HAS_SHOWN_DEVICE_PERFORMANCE_WARNING, true);
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

	// Token: 0x0600623E RID: 25150 RVA: 0x00200D4C File Offset: 0x001FEF4C
	private void ShowGraphicsDeviceWarning()
	{
		if (PlatformSettings.RuntimeOS != OSCategory.Android)
		{
			return;
		}
		if (Options.Get().GetBool(Option.SHOWN_GFX_DEVICE_WARNING, false))
		{
			return;
		}
		Options.Get().SetBool(Option.SHOWN_GFX_DEVICE_WARNING, true);
		string text = SystemInfo.graphicsDeviceName.ToLower();
		if (!text.Contains("powervr"))
		{
			return;
		}
		if (!text.Contains("540") && !text.Contains("544"))
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

	// Token: 0x0600623F RID: 25151 RVA: 0x00200E40 File Offset: 0x001FF040
	private void ShowTextureCompressionWarning()
	{
		if (PlatformSettings.RuntimeOS != OSCategory.Android)
		{
			return;
		}
		if (!HearthstoneApplication.IsInternal())
		{
			return;
		}
		if (PlatformSettings.LocaleVariant != LocaleVariant.China)
		{
			return;
		}
		if (!AndroidDeviceSettings.Get().IsCurrentTextureFormatSupported())
		{
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
	}

	// Token: 0x06006240 RID: 25152 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	private void OnWillReset()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040051A4 RID: 20900
	private const float RATINGS_SCREEN_DISPLAY_TIME = 5f;

	// Token: 0x040051A5 RID: 20901
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_queueSign;

	// Token: 0x040051A6 RID: 20902
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_webLoginCanvasPrefab;

	// Token: 0x040051A7 RID: 20903
	public GameObject m_quitButtonParent;

	// Token: 0x040051A8 RID: 20904
	public UberText m_queueTitle;

	// Token: 0x040051A9 RID: 20905
	public UberText m_queueText;

	// Token: 0x040051AA RID: 20906
	public UberText m_queueTime;

	// Token: 0x040051AB RID: 20907
	public StandardPegButtonNew m_quitButton;

	// Token: 0x040051AC RID: 20908
	public Glow m_glow1;

	// Token: 0x040051AD RID: 20909
	public Glow m_glow2;

	// Token: 0x040051AE RID: 20910
	public GameObject m_blizzardLogo;

	// Token: 0x040051AF RID: 20911
	public GameObject m_demoDisclaimer;

	// Token: 0x040051B0 RID: 20912
	public StandardPegButtonNew m_devClearLoginButton;

	// Token: 0x040051B1 RID: 20913
	private const float GLOW_FADE_TIME = 1f;

	// Token: 0x040051B2 RID: 20914
	private static SplashScreen s_instance;

	// Token: 0x040051B3 RID: 20915
	private bool m_queueShown;

	// Token: 0x040051B4 RID: 20916
	private bool m_fadingStarted;

	// Token: 0x040051B5 RID: 20917
	private bool m_inputCameraSet;

	// Token: 0x040051B6 RID: 20918
	private const long MAX_MINUTES_TO_SHOW_FOR_QUEUE_ETA = 15L;

	// Token: 0x02002243 RID: 8771
	private enum RatingsScreenRegion
	{
		// Token: 0x0400E2F9 RID: 58105
		NONE,
		// Token: 0x0400E2FA RID: 58106
		KOREA,
		// Token: 0x0400E2FB RID: 58107
		CHINA
	}

	// Token: 0x02002244 RID: 8772
	// (Invoke) Token: 0x060126A4 RID: 75428
	public delegate void FinishedHandler();
}
