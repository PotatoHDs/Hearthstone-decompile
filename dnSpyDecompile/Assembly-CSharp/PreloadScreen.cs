using System;
using System.IO;
using Blizzard.T5.Jobs;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Core.Streaming;
using Hearthstone.Streaming;
using UnityEngine;

// Token: 0x02000620 RID: 1568
public class PreloadScreen : MonoBehaviour
{
	// Token: 0x17000526 RID: 1318
	// (get) Token: 0x06005801 RID: 22529 RVA: 0x000274B4 File Offset: 0x000256B4
	private IGameDownloadManager DownloadManager
	{
		get
		{
			return GameDownloadManagerProvider.Get();
		}
	}

	// Token: 0x06005802 RID: 22530 RVA: 0x001CC53C File Offset: 0x001CA73C
	private void Start()
	{
		this.m_updateProgressBar.SetProgressBar(0f);
		this.m_prevSleepTimeout = Screen.sleepTimeout;
		Screen.sleepTimeout = -1;
		Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("HearthstoneApplication.ShowPrivacyPolicyPopup", new Action(this.ShowPrivacyPolicyPopup), new object[]
		{
			typeof(UniversalInputManager)
		}));
	}

	// Token: 0x17000527 RID: 1319
	// (get) Token: 0x06005803 RID: 22531 RVA: 0x001CC599 File Offset: 0x001CA799
	// (set) Token: 0x06005804 RID: 22532 RVA: 0x001CC5A1 File Offset: 0x001CA7A1
	public int EnabledDoubleTapFingerCount { get; private set; }

	// Token: 0x06005805 RID: 22533 RVA: 0x001CC5AC File Offset: 0x001CA7AC
	private void DoubleTapCheck()
	{
		if (Input.touchCount >= 2 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			bool flag = true;
			int num = 0;
			while (num < Input.touchCount && flag)
			{
				float deltaTime = Input.GetTouch(num).deltaTime;
				if (deltaTime <= 0f || deltaTime >= 1f || Input.GetTouch(num).deltaPosition.magnitude >= 1f)
				{
					flag = false;
				}
				num++;
			}
			if (flag)
			{
				this.EnabledDoubleTapFingerCount = Input.touchCount;
			}
		}
	}

	// Token: 0x06005806 RID: 22534 RVA: 0x001CC634 File Offset: 0x001CA834
	private int GetCountInstalledLocales()
	{
		int num = 1;
		foreach (string text in Enum.GetNames(typeof(AssetVariantTags.Locale)))
		{
			if (File.Exists(AssetBundleInfo.GetAssetBundlePath(string.Format("asset_manifest_{0}.unity3d", text.ToString().ToLower()))))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06005807 RID: 22535 RVA: 0x001CC68C File Offset: 0x001CA88C
	private void Update()
	{
		this.DoubleTapCheck();
		if (this.DownloadManager == null)
		{
			return;
		}
		if (this.DownloadManager.IsCompletedInitialBaseDownload())
		{
			if (!this.m_isInitialDownloadComplete)
			{
				HearthstoneApplication.SendStartupTimeTelemetry("GameDownloadManager.CompletedInitialBaseDownload");
				this.m_isInitialDownloadComplete = true;
			}
			Screen.sleepTimeout = this.m_prevSleepTimeout;
			this.m_updateProgressBar.SetProgressBar(1f);
			if (SplashScreen.Get() != null && SplashScreen.Get().gameObject.activeInHierarchy)
			{
				if (PlatformSettings.IsMobileRuntimeOS)
				{
					TelemetryManager.Client().SendRepairPrestep(this.EnabledDoubleTapFingerCount, this.GetCountInstalledLocales());
				}
				HearthstoneApplication.SendStartupTimeTelemetry("GameDownloadManager.EnteringSplashScreen");
				Log.Downloader.Print("killing preloadscreen", Array.Empty<object>());
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		TagDownloadStatus currentDownloadStatus = this.DownloadManager.GetCurrentDownloadStatus();
		if (currentDownloadStatus == null)
		{
			return;
		}
		this.m_progress = currentDownloadStatus.Progress;
		this.m_downloadSpeed = this.DownloadManager.BytesPerSecond;
		if (this.DownloadManager.InterruptionReason == InterruptionReason.AgentImpeded)
		{
			this.m_updateProgressBar.SetLabel(GameStrings.Get("GLUE_LOADINGSCREEN_PROGRESS_IMPEDED"));
			return;
		}
		if (this.DownloadManager.InterruptionReason == InterruptionReason.Error && this.m_updateFrame.activeSelf)
		{
			this.m_updateFrame.SetActive(false);
		}
		if (!this.m_updateFrame.activeSelf)
		{
			Log.Downloader.Print("Preloadscreen setting bar active", Array.Empty<object>());
			this.m_updateFrame.SetActive(true);
			this.m_updateFrameShownTime = Time.realtimeSinceStartup;
		}
		float num = Time.realtimeSinceStartup - this.m_updateFrameShownTime;
		if (!currentDownloadStatus.Complete)
		{
			int num2 = 8;
			string arg = "GLUE_LOADINGSCREEN_PROGRESS_UNITY_";
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				num2 = 6;
				arg = "GLUE_LOADINGSCREEN_PROGRESS_";
			}
			int num3 = (int)(num / 10f) % num2 + 1;
			this.m_updateProgressBar.SetLabel(GameStrings.Get(arg + num3));
			this.m_updateProgressBar.SetProgressBar(this.m_progress);
			string text = DownloadStatusView.FormatBytesAsHumanReadable(currentDownloadStatus.BytesRemaining);
			string text2 = DownloadStatusView.FormatBytesAsHumanReadable((long)this.m_downloadSpeed);
			if (this.m_updateProgressText != null)
			{
				this.m_updateProgressText.text = GameStrings.Format("GLUE_LOADINGSCREEN_PROGRESS_TEXT", new object[]
				{
					text,
					text2
				});
				return;
			}
		}
		else
		{
			int num4 = (int)(num / 10f) % 2 + 1;
			this.m_updateProgressBar.SetLabel(GameStrings.Get("GLUE_LOADINGSCREEN_CHECKING_UNITY_" + num4));
			this.m_updateProgressBar.SetProgressBar(1f);
		}
	}

	// Token: 0x06005808 RID: 22536 RVA: 0x001CC8FC File Offset: 0x001CAAFC
	public void ShowPrivacyPolicyPopup()
	{
		if (PlatformSettings.LocaleVariant == LocaleVariant.China && !Options.Get().GetBool(Option.HAS_ACCEPTED_PRIVACY_POLICY_AND_EULA, false))
		{
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_PrivacyPolicyPopup", new LoadResource("Prefabs/PrivacyPolicyPopup", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError)));
			return;
		}
		HearthstoneApplication.Get().DataTransferDependency.Callback();
	}

	// Token: 0x04004B77 RID: 19319
	public GameObject m_updateFrame;

	// Token: 0x04004B78 RID: 19320
	public ProgressBar m_updateProgressBar;

	// Token: 0x04004B79 RID: 19321
	public TextMesh m_updateProgressText;

	// Token: 0x04004B7A RID: 19322
	private const int DOWNLOAD_MESSAGES_COUNT = 8;

	// Token: 0x04004B7B RID: 19323
	private const int CHECKING_MESSAGES_COUNT = 2;

	// Token: 0x04004B7C RID: 19324
	private const int DOWNLOAD_MESSAGES_INTERVAL = 10;

	// Token: 0x04004B7D RID: 19325
	private float m_updateFrameShownTime;

	// Token: 0x04004B7E RID: 19326
	private int m_prevSleepTimeout;

	// Token: 0x04004B7F RID: 19327
	private float m_progress;

	// Token: 0x04004B80 RID: 19328
	private double m_downloadSpeed;

	// Token: 0x04004B81 RID: 19329
	private bool m_isInitialDownloadComplete;

	// Token: 0x04004B82 RID: 19330
	private const float REPAIR_TOUCH_MAX_TIMEOUT = 1f;

	// Token: 0x04004B83 RID: 19331
	private const float REPAIR_TOUCH_VARIANCE_POSITION = 1f;
}
