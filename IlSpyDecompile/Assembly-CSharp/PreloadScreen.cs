using System;
using System.IO;
using Blizzard.T5.Jobs;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Core.Streaming;
using Hearthstone.Streaming;
using UnityEngine;

public class PreloadScreen : MonoBehaviour
{
	public GameObject m_updateFrame;

	public ProgressBar m_updateProgressBar;

	public TextMesh m_updateProgressText;

	private const int DOWNLOAD_MESSAGES_COUNT = 8;

	private const int CHECKING_MESSAGES_COUNT = 2;

	private const int DOWNLOAD_MESSAGES_INTERVAL = 10;

	private float m_updateFrameShownTime;

	private int m_prevSleepTimeout;

	private float m_progress;

	private double m_downloadSpeed;

	private bool m_isInitialDownloadComplete;

	private const float REPAIR_TOUCH_MAX_TIMEOUT = 1f;

	private const float REPAIR_TOUCH_VARIANCE_POSITION = 1f;

	private IGameDownloadManager DownloadManager => GameDownloadManagerProvider.Get();

	public int EnabledDoubleTapFingerCount { get; private set; }

	private void Start()
	{
		m_updateProgressBar.SetProgressBar(0f);
		m_prevSleepTimeout = Screen.sleepTimeout;
		Screen.sleepTimeout = -1;
		Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("HearthstoneApplication.ShowPrivacyPolicyPopup", ShowPrivacyPolicyPopup, typeof(UniversalInputManager)));
	}

	private void DoubleTapCheck()
	{
		if (Input.touchCount < 2 || Input.GetTouch(0).phase != 0)
		{
			return;
		}
		bool flag = true;
		for (int i = 0; i < Input.touchCount && flag; i++)
		{
			float deltaTime = Input.GetTouch(i).deltaTime;
			if (!(deltaTime > 0f) || !(deltaTime < 1f) || !(Input.GetTouch(i).deltaPosition.magnitude < 1f))
			{
				flag = false;
			}
		}
		if (flag)
		{
			EnabledDoubleTapFingerCount = Input.touchCount;
		}
	}

	private int GetCountInstalledLocales()
	{
		int num = 1;
		string[] names = Enum.GetNames(typeof(AssetVariantTags.Locale));
		foreach (string text in names)
		{
			if (File.Exists(AssetBundleInfo.GetAssetBundlePath($"asset_manifest_{text.ToString().ToLower()}.unity3d")))
			{
				num++;
			}
		}
		return num;
	}

	private void Update()
	{
		DoubleTapCheck();
		if (DownloadManager == null)
		{
			return;
		}
		if (DownloadManager.IsCompletedInitialBaseDownload())
		{
			if (!m_isInitialDownloadComplete)
			{
				HearthstoneApplication.SendStartupTimeTelemetry("GameDownloadManager.CompletedInitialBaseDownload");
				m_isInitialDownloadComplete = true;
			}
			Screen.sleepTimeout = m_prevSleepTimeout;
			m_updateProgressBar.SetProgressBar(1f);
			if (SplashScreen.Get() != null && SplashScreen.Get().gameObject.activeInHierarchy)
			{
				if (PlatformSettings.IsMobileRuntimeOS)
				{
					TelemetryManager.Client().SendRepairPrestep(EnabledDoubleTapFingerCount, GetCountInstalledLocales());
				}
				HearthstoneApplication.SendStartupTimeTelemetry("GameDownloadManager.EnteringSplashScreen");
				Log.Downloader.Print("killing preloadscreen");
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		TagDownloadStatus currentDownloadStatus = DownloadManager.GetCurrentDownloadStatus();
		if (currentDownloadStatus == null)
		{
			return;
		}
		m_progress = currentDownloadStatus.Progress;
		m_downloadSpeed = DownloadManager.BytesPerSecond;
		if (DownloadManager.InterruptionReason == InterruptionReason.AgentImpeded)
		{
			m_updateProgressBar.SetLabel(GameStrings.Get("GLUE_LOADINGSCREEN_PROGRESS_IMPEDED"));
			return;
		}
		if (DownloadManager.InterruptionReason == InterruptionReason.Error && m_updateFrame.activeSelf)
		{
			m_updateFrame.SetActive(value: false);
		}
		if (!m_updateFrame.activeSelf)
		{
			Log.Downloader.Print("Preloadscreen setting bar active");
			m_updateFrame.SetActive(value: true);
			m_updateFrameShownTime = Time.realtimeSinceStartup;
		}
		float num = Time.realtimeSinceStartup - m_updateFrameShownTime;
		if (!currentDownloadStatus.Complete)
		{
			int num2 = 8;
			string text = "GLUE_LOADINGSCREEN_PROGRESS_UNITY_";
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				num2 = 6;
				text = "GLUE_LOADINGSCREEN_PROGRESS_";
			}
			int num3 = (int)(num / 10f) % num2 + 1;
			m_updateProgressBar.SetLabel(GameStrings.Get(text + num3));
			m_updateProgressBar.SetProgressBar(m_progress);
			string text2 = DownloadStatusView.FormatBytesAsHumanReadable(currentDownloadStatus.BytesRemaining);
			string text3 = DownloadStatusView.FormatBytesAsHumanReadable((long)m_downloadSpeed);
			if (m_updateProgressText != null)
			{
				m_updateProgressText.text = GameStrings.Format("GLUE_LOADINGSCREEN_PROGRESS_TEXT", text2, text3);
			}
		}
		else
		{
			int num4 = (int)(num / 10f) % 2 + 1;
			m_updateProgressBar.SetLabel(GameStrings.Get("GLUE_LOADINGSCREEN_CHECKING_UNITY_" + num4));
			m_updateProgressBar.SetProgressBar(1f);
		}
	}

	public void ShowPrivacyPolicyPopup()
	{
		if (PlatformSettings.LocaleVariant == LocaleVariant.China && !Options.Get().GetBool(Option.HAS_ACCEPTED_PRIVACY_POLICY_AND_EULA, defaultVal: false))
		{
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_PrivacyPolicyPopup", new LoadResource("Prefabs/PrivacyPolicyPopup", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError)));
		}
		else
		{
			HearthstoneApplication.Get().DataTransferDependency.Callback();
		}
	}
}
