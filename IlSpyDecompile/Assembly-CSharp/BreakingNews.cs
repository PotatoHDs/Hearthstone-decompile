using System.Collections;
using Hearthstone.Http;
using UnityEngine;

public class BreakingNews : MonoBehaviour
{
	public enum Status
	{
		Fetching,
		Available,
		TimedOut
	}

	public delegate void BreakingNewsRecievedDelegate(string response, bool error);

	public static bool SHOWS_BREAKING_NEWS;

	private static BreakingNews s_instance;

	private Status m_status;

	private string m_text = "";

	private string m_error;

	private float m_timeFetched;

	private const float TIMEOUT = 15f;

	public static bool ShouldShowForCurrentPlatform
	{
		get
		{
			if (!PlatformSettings.IsMobile())
			{
				return Cheats.ShowFakeBreakingNews;
			}
			return true;
		}
	}

	public static BreakingNews Get()
	{
		return s_instance;
	}

	public void Awake()
	{
		SHOWS_BREAKING_NEWS = Network.TUTORIALS_WITHOUT_ACCOUNT;
		s_instance = this;
	}

	public void OnDestroy()
	{
		s_instance = null;
	}

	public static void FetchBreakingNews(string url, BreakingNewsRecievedDelegate callback)
	{
		s_instance.StartCoroutine(s_instance.FetchBreakingNewsProgress(url, callback));
	}

	public IEnumerator FetchBreakingNewsProgress(string url, BreakingNewsRecievedDelegate callback)
	{
		using IHttpRequest webRequest = HttpRequestFactory.Get().CreateGetRequest(url);
		yield return webRequest.SendRequest();
		if (webRequest.IsNetworkError || webRequest.IsHttpError)
		{
			callback(webRequest.ErrorString, error: true);
		}
		else
		{
			callback(webRequest.ResponseAsString, error: false);
		}
	}

	public Status GetStatus()
	{
		if (!SHOWS_BREAKING_NEWS)
		{
			return Status.Available;
		}
		if (m_status == Status.Fetching && Time.realtimeSinceStartup - m_timeFetched > 15f)
		{
			m_status = Status.TimedOut;
		}
		return m_status;
	}

	public void Fetch()
	{
		if (!SHOWS_BREAKING_NEWS)
		{
			return;
		}
		m_error = null;
		m_status = Status.Fetching;
		m_text = "";
		m_timeFetched = Time.realtimeSinceStartup;
		FetchBreakingNews(ExternalUrlService.Get().GetBreakingNewsLink(), delegate(string response, bool error)
		{
			if (error)
			{
				s_instance.OnBreakingNewsError(response);
			}
			else
			{
				s_instance.OnBreakingNewsResponse(response);
			}
		});
	}

	public void OnBreakingNewsResponse(string response)
	{
		Log.BreakingNews.Print("Breaking News response received: {0}", response);
		m_text = response;
		if (m_text.Length <= 2 || m_text.ToLowerInvariant().Contains("<html>"))
		{
			m_text = "";
		}
		m_status = Status.Available;
	}

	public void OnBreakingNewsError(string error)
	{
		m_error = error;
		Log.BreakingNews.Print("Breaking News error received: {0}", error);
	}

	public string GetText()
	{
		if (!SHOWS_BREAKING_NEWS)
		{
			return "";
		}
		if (m_status == Status.Fetching || m_status == Status.TimedOut)
		{
			Debug.LogError($"Fetched breaking news when it was unavailable, status={m_status}");
			return "";
		}
		return m_text;
	}

	public string GetError()
	{
		return m_error;
	}
}
