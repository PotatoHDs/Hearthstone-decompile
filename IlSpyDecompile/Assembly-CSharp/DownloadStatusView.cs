using System;
using System.Collections;
using Hearthstone.Core.Streaming;
using Hearthstone.Streaming;
using UnityEngine;

public class DownloadStatusView : MonoBehaviour
{
	private static Color s_normalColor = Color.white;

	private static Color s_warningColor = Color.yellow;

	private static Color s_errorColor = Color.red;

	[SerializeField]
	private UberText m_contentDetailsText;

	[SerializeField]
	private UberText m_transferDetailsText;

	[SerializeField]
	private ProgressBar m_progressBar;

	[SerializeField]
	private UIBButton m_button;

	[SerializeField]
	private float m_crossfadeSeconds = 1f;

	[SerializeField]
	private float m_secondsUntilCrossfade = 2f;

	[SerializeField]
	private bool m_shortenText;

	private string m_remaningBytesStr = string.Empty;

	private bool m_isShowingProgressPercentage = true;

	private Coroutine m_crossfadeCoroutine;

	private static string[] s_suffixes = new string[4] { "GLOBAL_ASSET_DOWNLOAD_BYTE_SYMBOL", "GLOBAL_ASSET_DOWNLOAD_KILOBYTE_SYMBOL", "GLOBAL_ASSET_DOWNLOAD_MEGABYTE_SYMBOL", "GLOBAL_ASSET_DOWNLOAD_GIGABYTE_SYMBOL" };

	private IGameDownloadManager DownloadManager => GameDownloadManagerProvider.Get();

	private void Update()
	{
		if (DownloadManager == null)
		{
			SetButtonState(state: false);
			return;
		}
		if (DownloadManager.IsInterrupted)
		{
			StartCrossfade();
			if (m_transferDetailsText != null)
			{
				switch (DownloadManager.InterruptionReason)
				{
				case InterruptionReason.Paused:
					m_transferDetailsText.TextColor = s_normalColor;
					m_transferDetailsText.Text = GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_PAUSED");
					break;
				case InterruptionReason.Disabled:
					m_transferDetailsText.TextColor = s_errorColor;
					m_transferDetailsText.Text = GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_ERROR_DOWNLOAD_DISABLED");
					break;
				case InterruptionReason.AgentImpeded:
					m_transferDetailsText.TextColor = s_errorColor;
					m_transferDetailsText.Text = GameStrings.Format("GLOBAL_ASSET_DOWNLOAD_ERROR_AGENT_IMPEDED", m_remaningBytesStr);
					break;
				case InterruptionReason.AwaitingWifi:
					m_transferDetailsText.TextColor = s_warningColor;
					m_transferDetailsText.Text = GameStrings.Format("GLOBAL_ASSET_DOWNLOAD_ERROR_CELLULAR_DISABLED");
					break;
				case InterruptionReason.DiskFull:
					m_transferDetailsText.TextColor = s_warningColor;
					m_transferDetailsText.Text = GameStrings.Format("GLOBAL_ASSET_DOWNLOAD_ERROR_OUT_OF_STORAGE");
					break;
				case InterruptionReason.Fetching:
					m_transferDetailsText.TextColor = s_warningColor;
					m_transferDetailsText.Text = GameStrings.Format("GLOBAL_ASSET_DOWNLOAD_AWAITING_FETCH");
					break;
				}
			}
		}
		ContentDownloadStatus contentDownloadStatus = DownloadManager.GetContentDownloadStatus(DownloadTags.Content.Base);
		if (!HasDownloadStarted(contentDownloadStatus))
		{
			SetStartingProgressAndText();
			return;
		}
		float progress = contentDownloadStatus.Progress;
		m_remaningBytesStr = FormatBytesAsHumanReadable(contentDownloadStatus.BytesTotal - contentDownloadStatus.BytesDownloaded);
		SetButtonState(DownloadManager.IsAnyDownloadRequestedAndIncomplete);
		if (!DownloadManager.IsInterrupted)
		{
			if (!DownloadManager.IsAnyDownloadRequestedAndIncomplete)
			{
				StopCrossfade();
				if (m_contentDetailsText != null)
				{
					m_contentDetailsText.Text = GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_COMPLETE");
					m_contentDetailsText.TextAlpha = 1f;
				}
				if (m_transferDetailsText != null)
				{
					m_transferDetailsText.TextColor = s_normalColor;
					m_transferDetailsText.Text = string.Empty;
				}
			}
			else
			{
				StartCrossfade();
				if (m_transferDetailsText != null)
				{
					string text = FormatBytesAsHumanReadable((long)DownloadManager.BytesPerSecond);
					m_transferDetailsText.TextColor = s_normalColor;
					string key = (m_shortenText ? "GLOBAL_ASSET_DOWNLOAD_STATUS_SHORT" : "GLOBAL_ASSET_DOWNLOAD_STATUS");
					m_transferDetailsText.Text = GameStrings.Format(key, m_remaningBytesStr, text);
				}
			}
		}
		double num = Mathf.Clamp01(progress);
		if (m_progressBar != null)
		{
			m_progressBar.SetProgressBar((float)num);
		}
		if (m_contentDetailsText != null && DownloadManager.IsAnyDownloadRequestedAndIncomplete)
		{
			string format = GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_INTENTION_PAUSED");
			string text2 = ((!m_isShowingProgressPercentage) ? GameStrings.Get(LocalizedDescriptionForDownloadStatus(contentDownloadStatus)) : $"{num * 100.0:0.}%");
			m_contentDetailsText.Text = (DownloadManager.IsInterrupted ? string.Format(format, text2) : text2);
		}
	}

	private static bool HasDownloadStarted(ContentDownloadStatus baseContentStatus)
	{
		if (baseContentStatus != null)
		{
			return baseContentStatus.BytesTotal > 0;
		}
		return false;
	}

	private void SetStartingProgressAndText()
	{
		SetProgressBarToZero();
		SetStartingContentDetailsText();
	}

	private void SetStartingContentDetailsText()
	{
		if (!(m_contentDetailsText == null))
		{
			m_contentDetailsText.Text = GetStartingTextForContentDetails();
		}
	}

	private string GetStartingTextForContentDetails()
	{
		if (DownloadManager.InterruptionReason == InterruptionReason.Disabled)
		{
			return string.Empty;
		}
		return GameStrings.Format("GLOBAL_ASSET_INTENTION_UNINITIALIZED");
	}

	private void SetProgressBarToZero()
	{
		if (!(m_progressBar == null))
		{
			m_progressBar.SetProgressBar(0f);
		}
	}

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

	private void OnDisable()
	{
		m_crossfadeCoroutine = null;
	}

	private void StartCrossfade()
	{
		if (m_crossfadeCoroutine == null && m_contentDetailsText != null)
		{
			m_crossfadeCoroutine = StartCoroutine(CrossfadeBetweenProgressAndContentDetailsText());
		}
	}

	private void StopCrossfade()
	{
		if (m_crossfadeCoroutine != null)
		{
			StopCoroutine(m_crossfadeCoroutine);
			m_crossfadeCoroutine = null;
		}
	}

	private IEnumerator CrossfadeBetweenProgressAndContentDetailsText()
	{
		m_contentDetailsText.TextAlpha = 0f;
		while (true)
		{
			m_isShowingProgressPercentage = !m_isShowingProgressPercentage;
			yield return LerpBetweenValues(m_crossfadeSeconds, 0f, 1f, delegate(float a)
			{
				m_contentDetailsText.TextAlpha = a;
			});
			yield return new WaitForSeconds(m_secondsUntilCrossfade);
			yield return LerpBetweenValues(m_crossfadeSeconds, 1f, 0f, delegate(float a)
			{
				m_contentDetailsText.TextAlpha = a;
			});
		}
	}

	private IEnumerator LerpBetweenValues(float duration, float from, float to, Action<float> onUpdate)
	{
		float timeLeft = duration;
		while (timeLeft >= 0f)
		{
			onUpdate(Mathf.Lerp(to, from, timeLeft / duration));
			timeLeft -= Time.deltaTime;
			yield return null;
		}
	}

	private void SetButtonState(bool state)
	{
		if (m_button != null && state != m_button.IsEnabled())
		{
			m_button.SetEnabled(state);
			m_button.Flip(state);
		}
	}

	public static string FormatBytesAsHumanReadable(long bytes)
	{
		int num = 0;
		long num2 = 0L;
		long num3 = 0L;
		while (bytes > 0 && num < s_suffixes.Length)
		{
			num++;
			num3 = num2;
			num2 = bytes % 1024;
			bytes /= 1024;
		}
		num = Mathf.Max(1, num);
		int num4 = Mathf.RoundToInt((float)num3 * 10f / 1024f);
		if (num4 == 10)
		{
			num2++;
			num4 = 0;
		}
		return string.Format(GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_STATUS_DECIMAL_FORMAT"), num2, num4, GameStrings.Get(s_suffixes[num - 1]));
	}
}
