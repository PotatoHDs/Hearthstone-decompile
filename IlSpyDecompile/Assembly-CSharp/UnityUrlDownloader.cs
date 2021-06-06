using System.Collections.Generic;
using bgs;
using Hearthstone.Http;
using UnityEngine;

public class UnityUrlDownloader : IUrlDownloader
{
	internal class DownloadState
	{
		public string url;

		public int numRetriesLeft;

		public int timeoutMs = -1;

		public IHttpRequest handle;

		public bool success;

		public UrlDownloadCompletedCallback cb;

		public float startTime;
	}

	private HashSet<DownloadState> m_downloadsToStart = new HashSet<DownloadState>();

	private HashSet<DownloadState> m_downloadsRunning = new HashSet<DownloadState>();

	private HashSet<DownloadState> m_downloadsDone = new HashSet<DownloadState>();

	public void Process()
	{
		foreach (DownloadState item in m_downloadsToStart)
		{
			item.startTime = Time.realtimeSinceStartup;
			item.handle = HttpRequestFactory.Get().CreateGetRequest(item.url);
			item.handle.SendRequest();
			m_downloadsRunning.Add(item);
		}
		m_downloadsToStart.Clear();
		if (m_downloadsRunning.Count <= 0)
		{
			return;
		}
		HashSet<DownloadState> hashSet = null;
		foreach (DownloadState item2 in m_downloadsRunning)
		{
			bool flag = false;
			if (item2.handle.IsDone)
			{
				item2.success = !item2.handle.IsNetworkError && !item2.handle.IsHttpError;
				flag = true;
			}
			else if (item2.timeoutMs >= 0 && Time.realtimeSinceStartup - item2.startTime > (float)item2.timeoutMs / 1000f)
			{
				item2.success = false;
				flag = true;
			}
			if (flag)
			{
				if (hashSet == null)
				{
					hashSet = new HashSet<DownloadState>();
				}
				hashSet.Add(item2);
			}
		}
		if (hashSet != null)
		{
			foreach (DownloadState item3 in hashSet)
			{
				m_downloadsRunning.Remove(item3);
				m_downloadsDone.Add(item3);
			}
		}
		foreach (DownloadState item4 in m_downloadsDone)
		{
			if (!item4.success && item4.numRetriesLeft > 0)
			{
				item4.numRetriesLeft--;
				m_downloadsToStart.Add(item4);
			}
			else if (item4.cb != null)
			{
				item4.cb(item4.success, item4.handle.ResponseRaw);
			}
		}
		m_downloadsDone.Clear();
	}

	public void Download(string url, UrlDownloadCompletedCallback cb)
	{
		UrlDownloaderConfig config = new UrlDownloaderConfig();
		Download(url, cb, config);
	}

	public void Download(string url, UrlDownloadCompletedCallback cb, UrlDownloaderConfig config)
	{
		DownloadState downloadState = new DownloadState();
		downloadState.url = url;
		downloadState.timeoutMs = config.timeoutMs;
		downloadState.numRetriesLeft = config.numRetries;
		downloadState.cb = cb;
		m_downloadsToStart.Add(downloadState);
	}
}
