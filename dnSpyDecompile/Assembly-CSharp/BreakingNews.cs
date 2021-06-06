using System;
using System.Collections;
using Hearthstone.Http;
using UnityEngine;

// Token: 0x02000869 RID: 2153
public class BreakingNews : MonoBehaviour
{
	// Token: 0x170006E1 RID: 1761
	// (get) Token: 0x06007435 RID: 29749 RVA: 0x0025449F File Offset: 0x0025269F
	public static bool ShouldShowForCurrentPlatform
	{
		get
		{
			return PlatformSettings.IsMobile() || Cheats.ShowFakeBreakingNews;
		}
	}

	// Token: 0x06007436 RID: 29750 RVA: 0x002544AF File Offset: 0x002526AF
	public static BreakingNews Get()
	{
		return BreakingNews.s_instance;
	}

	// Token: 0x06007437 RID: 29751 RVA: 0x002544B6 File Offset: 0x002526B6
	public void Awake()
	{
		BreakingNews.SHOWS_BREAKING_NEWS = Network.TUTORIALS_WITHOUT_ACCOUNT;
		BreakingNews.s_instance = this;
	}

	// Token: 0x06007438 RID: 29752 RVA: 0x002544CD File Offset: 0x002526CD
	public void OnDestroy()
	{
		BreakingNews.s_instance = null;
	}

	// Token: 0x06007439 RID: 29753 RVA: 0x002544D5 File Offset: 0x002526D5
	public static void FetchBreakingNews(string url, BreakingNews.BreakingNewsRecievedDelegate callback)
	{
		BreakingNews.s_instance.StartCoroutine(BreakingNews.s_instance.FetchBreakingNewsProgress(url, callback));
	}

	// Token: 0x0600743A RID: 29754 RVA: 0x002544EE File Offset: 0x002526EE
	public IEnumerator FetchBreakingNewsProgress(string url, BreakingNews.BreakingNewsRecievedDelegate callback)
	{
		using (IHttpRequest webRequest = HttpRequestFactory.Get().CreateGetRequest(url))
		{
			yield return webRequest.SendRequest();
			if (webRequest.IsNetworkError || webRequest.IsHttpError)
			{
				callback(webRequest.ErrorString, true);
			}
			else
			{
				callback(webRequest.ResponseAsString, false);
			}
		}
		IHttpRequest webRequest = null;
		yield break;
		yield break;
	}

	// Token: 0x0600743B RID: 29755 RVA: 0x00254504 File Offset: 0x00252704
	public BreakingNews.Status GetStatus()
	{
		if (!BreakingNews.SHOWS_BREAKING_NEWS)
		{
			return BreakingNews.Status.Available;
		}
		if (this.m_status == BreakingNews.Status.Fetching && Time.realtimeSinceStartup - this.m_timeFetched > 15f)
		{
			this.m_status = BreakingNews.Status.TimedOut;
		}
		return this.m_status;
	}

	// Token: 0x0600743C RID: 29756 RVA: 0x00254538 File Offset: 0x00252738
	public void Fetch()
	{
		if (!BreakingNews.SHOWS_BREAKING_NEWS)
		{
			return;
		}
		this.m_error = null;
		this.m_status = BreakingNews.Status.Fetching;
		this.m_text = "";
		this.m_timeFetched = Time.realtimeSinceStartup;
		BreakingNews.FetchBreakingNews(ExternalUrlService.Get().GetBreakingNewsLink(), delegate(string response, bool error)
		{
			if (error)
			{
				BreakingNews.s_instance.OnBreakingNewsError(response);
				return;
			}
			BreakingNews.s_instance.OnBreakingNewsResponse(response);
		});
	}

	// Token: 0x0600743D RID: 29757 RVA: 0x002545A0 File Offset: 0x002527A0
	public void OnBreakingNewsResponse(string response)
	{
		Log.BreakingNews.Print("Breaking News response received: {0}", new object[]
		{
			response
		});
		this.m_text = response;
		if (this.m_text.Length <= 2 || this.m_text.ToLowerInvariant().Contains("<html>"))
		{
			this.m_text = "";
		}
		this.m_status = BreakingNews.Status.Available;
	}

	// Token: 0x0600743E RID: 29758 RVA: 0x00254604 File Offset: 0x00252804
	public void OnBreakingNewsError(string error)
	{
		this.m_error = error;
		Log.BreakingNews.Print("Breaking News error received: {0}", new object[]
		{
			error
		});
	}

	// Token: 0x0600743F RID: 29759 RVA: 0x00254628 File Offset: 0x00252828
	public string GetText()
	{
		if (!BreakingNews.SHOWS_BREAKING_NEWS)
		{
			return "";
		}
		if (this.m_status == BreakingNews.Status.Fetching || this.m_status == BreakingNews.Status.TimedOut)
		{
			Debug.LogError(string.Format("Fetched breaking news when it was unavailable, status={0}", this.m_status));
			return "";
		}
		return this.m_text;
	}

	// Token: 0x06007440 RID: 29760 RVA: 0x00254679 File Offset: 0x00252879
	public string GetError()
	{
		return this.m_error;
	}

	// Token: 0x04005C4E RID: 23630
	public static bool SHOWS_BREAKING_NEWS;

	// Token: 0x04005C4F RID: 23631
	private static BreakingNews s_instance;

	// Token: 0x04005C50 RID: 23632
	private BreakingNews.Status m_status;

	// Token: 0x04005C51 RID: 23633
	private string m_text = "";

	// Token: 0x04005C52 RID: 23634
	private string m_error;

	// Token: 0x04005C53 RID: 23635
	private float m_timeFetched;

	// Token: 0x04005C54 RID: 23636
	private const float TIMEOUT = 15f;

	// Token: 0x02002465 RID: 9317
	public enum Status
	{
		// Token: 0x0400EA21 RID: 59937
		Fetching,
		// Token: 0x0400EA22 RID: 59938
		Available,
		// Token: 0x0400EA23 RID: 59939
		TimedOut
	}

	// Token: 0x02002466 RID: 9318
	// (Invoke) Token: 0x06012F2C RID: 77612
	public delegate void BreakingNewsRecievedDelegate(string response, bool error);
}
