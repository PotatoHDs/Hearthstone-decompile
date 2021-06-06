using System;
using System.Collections;
using Hearthstone.Core.Streaming;
using Hearthstone.Streaming;
using UnityEngine;

// Token: 0x02000B0C RID: 2828
public class DownloadStatusView : MonoBehaviour
{
	// Token: 0x1700088B RID: 2187
	// (get) Token: 0x06009665 RID: 38501 RVA: 0x000274B4 File Offset: 0x000256B4
	private IGameDownloadManager DownloadManager
	{
		get
		{
			return GameDownloadManagerProvider.Get();
		}
	}

	// Token: 0x06009666 RID: 38502 RVA: 0x0030AF08 File Offset: 0x00309108
	private void Update()
	{
		if (this.DownloadManager == null)
		{
			this.SetButtonState(false);
			return;
		}
		if (this.DownloadManager.IsInterrupted)
		{
			this.StartCrossfade();
			if (this.m_transferDetailsText != null)
			{
				switch (this.DownloadManager.InterruptionReason)
				{
				case InterruptionReason.Disabled:
					this.m_transferDetailsText.TextColor = DownloadStatusView.s_errorColor;
					this.m_transferDetailsText.Text = GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_ERROR_DOWNLOAD_DISABLED");
					break;
				case InterruptionReason.Paused:
					this.m_transferDetailsText.TextColor = DownloadStatusView.s_normalColor;
					this.m_transferDetailsText.Text = GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_PAUSED");
					break;
				case InterruptionReason.AwaitingWifi:
					this.m_transferDetailsText.TextColor = DownloadStatusView.s_warningColor;
					this.m_transferDetailsText.Text = GameStrings.Format("GLOBAL_ASSET_DOWNLOAD_ERROR_CELLULAR_DISABLED", Array.Empty<object>());
					break;
				case InterruptionReason.DiskFull:
					this.m_transferDetailsText.TextColor = DownloadStatusView.s_warningColor;
					this.m_transferDetailsText.Text = GameStrings.Format("GLOBAL_ASSET_DOWNLOAD_ERROR_OUT_OF_STORAGE", Array.Empty<object>());
					break;
				case InterruptionReason.AgentImpeded:
					this.m_transferDetailsText.TextColor = DownloadStatusView.s_errorColor;
					this.m_transferDetailsText.Text = GameStrings.Format("GLOBAL_ASSET_DOWNLOAD_ERROR_AGENT_IMPEDED", new object[]
					{
						this.m_remaningBytesStr
					});
					break;
				case InterruptionReason.Fetching:
					this.m_transferDetailsText.TextColor = DownloadStatusView.s_warningColor;
					this.m_transferDetailsText.Text = GameStrings.Format("GLOBAL_ASSET_DOWNLOAD_AWAITING_FETCH", Array.Empty<object>());
					break;
				}
			}
		}
		ContentDownloadStatus contentDownloadStatus = this.DownloadManager.GetContentDownloadStatus(DownloadTags.Content.Base);
		if (!DownloadStatusView.HasDownloadStarted(contentDownloadStatus))
		{
			this.SetStartingProgressAndText();
			return;
		}
		float progress = contentDownloadStatus.Progress;
		this.m_remaningBytesStr = DownloadStatusView.FormatBytesAsHumanReadable(contentDownloadStatus.BytesTotal - contentDownloadStatus.BytesDownloaded);
		this.SetButtonState(this.DownloadManager.IsAnyDownloadRequestedAndIncomplete);
		if (!this.DownloadManager.IsInterrupted)
		{
			if (!this.DownloadManager.IsAnyDownloadRequestedAndIncomplete)
			{
				this.StopCrossfade();
				if (this.m_contentDetailsText != null)
				{
					this.m_contentDetailsText.Text = GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_COMPLETE");
					this.m_contentDetailsText.TextAlpha = 1f;
				}
				if (this.m_transferDetailsText != null)
				{
					this.m_transferDetailsText.TextColor = DownloadStatusView.s_normalColor;
					this.m_transferDetailsText.Text = string.Empty;
				}
			}
			else
			{
				this.StartCrossfade();
				if (this.m_transferDetailsText != null)
				{
					string text = DownloadStatusView.FormatBytesAsHumanReadable((long)this.DownloadManager.BytesPerSecond);
					this.m_transferDetailsText.TextColor = DownloadStatusView.s_normalColor;
					string key = this.m_shortenText ? "GLOBAL_ASSET_DOWNLOAD_STATUS_SHORT" : "GLOBAL_ASSET_DOWNLOAD_STATUS";
					this.m_transferDetailsText.Text = GameStrings.Format(key, new object[]
					{
						this.m_remaningBytesStr,
						text
					});
				}
			}
		}
		double num = (double)Mathf.Clamp01(progress);
		if (this.m_progressBar != null)
		{
			this.m_progressBar.SetProgressBar((float)num);
		}
		if (this.m_contentDetailsText != null && this.DownloadManager.IsAnyDownloadRequestedAndIncomplete)
		{
			string format = GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_INTENTION_PAUSED");
			string text2;
			if (this.m_isShowingProgressPercentage)
			{
				text2 = string.Format("{0:0.}%", num * 100.0);
			}
			else
			{
				text2 = GameStrings.Get(this.LocalizedDescriptionForDownloadStatus(contentDownloadStatus));
			}
			this.m_contentDetailsText.Text = (this.DownloadManager.IsInterrupted ? string.Format(format, text2) : text2);
		}
	}

	// Token: 0x06009667 RID: 38503 RVA: 0x0030B277 File Offset: 0x00309477
	private static bool HasDownloadStarted(ContentDownloadStatus baseContentStatus)
	{
		return baseContentStatus != null && baseContentStatus.BytesTotal > 0L;
	}

	// Token: 0x06009668 RID: 38504 RVA: 0x0030B288 File Offset: 0x00309488
	private void SetStartingProgressAndText()
	{
		this.SetProgressBarToZero();
		this.SetStartingContentDetailsText();
	}

	// Token: 0x06009669 RID: 38505 RVA: 0x0030B296 File Offset: 0x00309496
	private void SetStartingContentDetailsText()
	{
		if (this.m_contentDetailsText == null)
		{
			return;
		}
		this.m_contentDetailsText.Text = this.GetStartingTextForContentDetails();
	}

	// Token: 0x0600966A RID: 38506 RVA: 0x0030B2B8 File Offset: 0x003094B8
	private string GetStartingTextForContentDetails()
	{
		if (this.DownloadManager.InterruptionReason == InterruptionReason.Disabled)
		{
			return string.Empty;
		}
		return GameStrings.Format("GLOBAL_ASSET_INTENTION_UNINITIALIZED", Array.Empty<object>());
	}

	// Token: 0x0600966B RID: 38507 RVA: 0x0030B2DD File Offset: 0x003094DD
	private void SetProgressBarToZero()
	{
		if (this.m_progressBar == null)
		{
			return;
		}
		this.m_progressBar.SetProgressBar(0f);
	}

	// Token: 0x0600966C RID: 38508 RVA: 0x0030B300 File Offset: 0x00309500
	private string LocalizedDescriptionForDownloadStatus(ContentDownloadStatus downloadStatus)
	{
		if (downloadStatus.ContentTag == DownloadTags.GetTagString(DownloadTags.Content.Base))
		{
			if (downloadStatus.InProgressQualityTag == DownloadTags.Quality.Fonts)
			{
				return "GLOBAL_ASSET_INTENTION_DOWNLOADING_FONTS";
			}
			if (downloadStatus.InProgressQualityTag == DownloadTags.Quality.PortHigh)
			{
				return "GLOBAL_ASSET_INTENTION_DOWNLOADING_HIGH_RES_PORTRAITS";
			}
			if (downloadStatus.InProgressQualityTag == DownloadTags.Quality.PortPremium)
			{
				return "GLOBAL_ASSET_INTENTION_DOWNLOADING_PREMIUM_ANIMATIONS";
			}
			if (downloadStatus.InProgressQualityTag == DownloadTags.Quality.SoundSpell)
			{
				return "GLOBAL_ASSET_INTENTION_DOWNLOADING_SPELL_SOUNDS";
			}
			if (downloadStatus.InProgressQualityTag == DownloadTags.Quality.SoundLegend)
			{
				return "GLOBAL_ASSET_INTENTION_DOWNLOADING_LEGEND_STINGERS";
			}
			if (downloadStatus.InProgressQualityTag == DownloadTags.Quality.MusicExpansion)
			{
				return "GLOBAL_ASSET_INTENTION_DOWNLOADING_EXPANSION_MUSIC";
			}
			if (downloadStatus.InProgressQualityTag == DownloadTags.Quality.SoundOtherMinion)
			{
				return "GLOBAL_ASSET_INTENTION_DOWNLOADING_OTHER_MINION_SOUNDS";
			}
			if (downloadStatus.InProgressQualityTag == DownloadTags.Quality.PlaySounds)
			{
				return "GLOBAL_ASSET_INTENTION_DOWNLOADING_MINION_PLAY_SOUNDS";
			}
			if (downloadStatus.InProgressQualityTag == DownloadTags.Quality.SoundMission)
			{
				return "GLOBAL_ASSET_INTENTION_DOWNLOADING_MISSION_SOUNDS";
			}
			if (downloadStatus.InProgressQualityTag == DownloadTags.Quality.HeroMusic)
			{
				return "GLOBAL_ASSET_INTENTION_DOWNLOADING_HERO_MUSIC";
			}
		}
		return "";
	}

	// Token: 0x0600966D RID: 38509 RVA: 0x0030B3C5 File Offset: 0x003095C5
	private void OnDisable()
	{
		this.m_crossfadeCoroutine = null;
	}

	// Token: 0x0600966E RID: 38510 RVA: 0x0030B3CE File Offset: 0x003095CE
	private void StartCrossfade()
	{
		if (this.m_crossfadeCoroutine == null && this.m_contentDetailsText != null)
		{
			this.m_crossfadeCoroutine = base.StartCoroutine(this.CrossfadeBetweenProgressAndContentDetailsText());
		}
	}

	// Token: 0x0600966F RID: 38511 RVA: 0x0030B3F8 File Offset: 0x003095F8
	private void StopCrossfade()
	{
		if (this.m_crossfadeCoroutine != null)
		{
			base.StopCoroutine(this.m_crossfadeCoroutine);
			this.m_crossfadeCoroutine = null;
		}
	}

	// Token: 0x06009670 RID: 38512 RVA: 0x0030B415 File Offset: 0x00309615
	private IEnumerator CrossfadeBetweenProgressAndContentDetailsText()
	{
		this.m_contentDetailsText.TextAlpha = 0f;
		for (;;)
		{
			this.m_isShowingProgressPercentage = !this.m_isShowingProgressPercentage;
			yield return this.LerpBetweenValues(this.m_crossfadeSeconds, 0f, 1f, delegate(float a)
			{
				this.m_contentDetailsText.TextAlpha = a;
			});
			yield return new WaitForSeconds(this.m_secondsUntilCrossfade);
			yield return this.LerpBetweenValues(this.m_crossfadeSeconds, 1f, 0f, delegate(float a)
			{
				this.m_contentDetailsText.TextAlpha = a;
			});
		}
		yield break;
	}

	// Token: 0x06009671 RID: 38513 RVA: 0x0030B424 File Offset: 0x00309624
	private IEnumerator LerpBetweenValues(float duration, float from, float to, Action<float> onUpdate)
	{
		float timeLeft = duration;
		while (timeLeft >= 0f)
		{
			onUpdate(Mathf.Lerp(to, from, timeLeft / duration));
			timeLeft -= Time.deltaTime;
			yield return null;
		}
		yield break;
	}

	// Token: 0x06009672 RID: 38514 RVA: 0x0030B449 File Offset: 0x00309649
	private void SetButtonState(bool state)
	{
		if (this.m_button != null && state != this.m_button.IsEnabled())
		{
			this.m_button.SetEnabled(state, false);
			this.m_button.Flip(state, false);
		}
	}

	// Token: 0x06009673 RID: 38515 RVA: 0x0030B484 File Offset: 0x00309684
	public static string FormatBytesAsHumanReadable(long bytes)
	{
		int num = 0;
		long num2 = 0L;
		long num3 = 0L;
		while (bytes > 0L && num < DownloadStatusView.s_suffixes.Length)
		{
			num++;
			num3 = num2;
			num2 = bytes % 1024L;
			bytes /= 1024L;
		}
		num = Mathf.Max(1, num);
		int num4 = Mathf.RoundToInt((float)num3 * 10f / 1024f);
		if (num4 == 10)
		{
			num2 += 1L;
			num4 = 0;
		}
		return string.Format(GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_STATUS_DECIMAL_FORMAT"), num2, num4, GameStrings.Get(DownloadStatusView.s_suffixes[num - 1]));
	}

	// Token: 0x04007E0B RID: 32267
	private static Color s_normalColor = Color.white;

	// Token: 0x04007E0C RID: 32268
	private static Color s_warningColor = Color.yellow;

	// Token: 0x04007E0D RID: 32269
	private static Color s_errorColor = Color.red;

	// Token: 0x04007E0E RID: 32270
	[SerializeField]
	private UberText m_contentDetailsText;

	// Token: 0x04007E0F RID: 32271
	[SerializeField]
	private UberText m_transferDetailsText;

	// Token: 0x04007E10 RID: 32272
	[SerializeField]
	private ProgressBar m_progressBar;

	// Token: 0x04007E11 RID: 32273
	[SerializeField]
	private UIBButton m_button;

	// Token: 0x04007E12 RID: 32274
	[SerializeField]
	private float m_crossfadeSeconds = 1f;

	// Token: 0x04007E13 RID: 32275
	[SerializeField]
	private float m_secondsUntilCrossfade = 2f;

	// Token: 0x04007E14 RID: 32276
	[SerializeField]
	private bool m_shortenText;

	// Token: 0x04007E15 RID: 32277
	private string m_remaningBytesStr = string.Empty;

	// Token: 0x04007E16 RID: 32278
	private bool m_isShowingProgressPercentage = true;

	// Token: 0x04007E17 RID: 32279
	private Coroutine m_crossfadeCoroutine;

	// Token: 0x04007E18 RID: 32280
	private static string[] s_suffixes = new string[]
	{
		"GLOBAL_ASSET_DOWNLOAD_BYTE_SYMBOL",
		"GLOBAL_ASSET_DOWNLOAD_KILOBYTE_SYMBOL",
		"GLOBAL_ASSET_DOWNLOAD_MEGABYTE_SYMBOL",
		"GLOBAL_ASSET_DOWNLOAD_GIGABYTE_SYMBOL"
	};
}
