using System;
using System.Collections.Generic;
using bgs;
using Hearthstone.Http;
using UnityEngine;

// Token: 0x0200060A RID: 1546
public class UnityUrlDownloader : IUrlDownloader
{
	// Token: 0x0600568D RID: 22157 RVA: 0x001C5E64 File Offset: 0x001C4064
	public void Process()
	{
		foreach (UnityUrlDownloader.DownloadState downloadState in this.m_downloadsToStart)
		{
			downloadState.startTime = Time.realtimeSinceStartup;
			downloadState.handle = HttpRequestFactory.Get().CreateGetRequest(downloadState.url);
			downloadState.handle.SendRequest();
			this.m_downloadsRunning.Add(downloadState);
		}
		this.m_downloadsToStart.Clear();
		if (this.m_downloadsRunning.Count > 0)
		{
			HashSet<UnityUrlDownloader.DownloadState> hashSet = null;
			foreach (UnityUrlDownloader.DownloadState downloadState2 in this.m_downloadsRunning)
			{
				bool flag = false;
				if (downloadState2.handle.IsDone)
				{
					downloadState2.success = (!downloadState2.handle.IsNetworkError && !downloadState2.handle.IsHttpError);
					flag = true;
				}
				else if (downloadState2.timeoutMs >= 0 && Time.realtimeSinceStartup - downloadState2.startTime > (float)downloadState2.timeoutMs / 1000f)
				{
					downloadState2.success = false;
					flag = true;
				}
				if (flag)
				{
					if (hashSet == null)
					{
						hashSet = new HashSet<UnityUrlDownloader.DownloadState>();
					}
					hashSet.Add(downloadState2);
				}
			}
			if (hashSet != null)
			{
				foreach (UnityUrlDownloader.DownloadState item in hashSet)
				{
					this.m_downloadsRunning.Remove(item);
					this.m_downloadsDone.Add(item);
				}
			}
			foreach (UnityUrlDownloader.DownloadState downloadState3 in this.m_downloadsDone)
			{
				if (!downloadState3.success && downloadState3.numRetriesLeft > 0)
				{
					downloadState3.numRetriesLeft--;
					this.m_downloadsToStart.Add(downloadState3);
				}
				else if (downloadState3.cb != null)
				{
					downloadState3.cb(downloadState3.success, downloadState3.handle.ResponseRaw);
				}
			}
			this.m_downloadsDone.Clear();
		}
	}

	// Token: 0x0600568E RID: 22158 RVA: 0x001C60BC File Offset: 0x001C42BC
	public void Download(string url, UrlDownloadCompletedCallback cb)
	{
		UrlDownloaderConfig config = new UrlDownloaderConfig();
		this.Download(url, cb, config);
	}

	// Token: 0x0600568F RID: 22159 RVA: 0x001C60D8 File Offset: 0x001C42D8
	public void Download(string url, UrlDownloadCompletedCallback cb, UrlDownloaderConfig config)
	{
		UnityUrlDownloader.DownloadState downloadState = new UnityUrlDownloader.DownloadState();
		downloadState.url = url;
		downloadState.timeoutMs = config.timeoutMs;
		downloadState.numRetriesLeft = config.numRetries;
		downloadState.cb = cb;
		this.m_downloadsToStart.Add(downloadState);
	}

	// Token: 0x04004A95 RID: 19093
	private HashSet<UnityUrlDownloader.DownloadState> m_downloadsToStart = new HashSet<UnityUrlDownloader.DownloadState>();

	// Token: 0x04004A96 RID: 19094
	private HashSet<UnityUrlDownloader.DownloadState> m_downloadsRunning = new HashSet<UnityUrlDownloader.DownloadState>();

	// Token: 0x04004A97 RID: 19095
	private HashSet<UnityUrlDownloader.DownloadState> m_downloadsDone = new HashSet<UnityUrlDownloader.DownloadState>();

	// Token: 0x02002110 RID: 8464
	internal class DownloadState
	{
		// Token: 0x0400DF28 RID: 57128
		public string url;

		// Token: 0x0400DF29 RID: 57129
		public int numRetriesLeft;

		// Token: 0x0400DF2A RID: 57130
		public int timeoutMs = -1;

		// Token: 0x0400DF2B RID: 57131
		public IHttpRequest handle;

		// Token: 0x0400DF2C RID: 57132
		public bool success;

		// Token: 0x0400DF2D RID: 57133
		public UrlDownloadCompletedCallback cb;

		// Token: 0x0400DF2E RID: 57134
		public float startTime;
	}
}
