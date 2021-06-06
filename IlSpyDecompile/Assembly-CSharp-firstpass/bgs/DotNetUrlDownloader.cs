using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace bgs
{
	public class DotNetUrlDownloader : IUrlDownloader
	{
		internal class DownloadState
		{
			public DotNetUrlDownloader downloader;

			public string host;

			public WebRequest request;

			public Stream responseStream;

			public int numRetriesLeft;

			public int timeoutMs;

			public RegisteredWaitHandle timeoutWaitHandle;

			public WaitHandle timeoutWatchHandle;

			private const int bufferSize = 1024;

			public byte[] readBuffer;

			public int readPos;

			public DownloadResult downloadResult;

			public DownloadState()
			{
				readBuffer = new byte[1024];
			}

			public bool UnregisterTimeout()
			{
				bool result = false;
				if (timeoutWaitHandle != null && timeoutWatchHandle != null)
				{
					result = timeoutWaitHandle.Unregister(timeoutWatchHandle);
					timeoutWaitHandle = null;
					timeoutWatchHandle = null;
				}
				return result;
			}
		}

		internal class DownloadResult
		{
			public UrlDownloadCompletedCallback callback;

			public byte[] downloadData;

			public bool succeeded;
		}

		private List<DownloadResult> m_completedDownloads = new List<DownloadResult>();

		public void Process()
		{
			lock (m_completedDownloads)
			{
				foreach (DownloadResult completedDownload in m_completedDownloads)
				{
					completedDownload.callback(completedDownload.succeeded, completedDownload.downloadData);
				}
				m_completedDownloads.Clear();
			}
		}

		public void Download(string url, UrlDownloadCompletedCallback cb)
		{
			UrlDownloaderConfig config = new UrlDownloaderConfig();
			Download(url, cb, config);
		}

		public void Download(string url, UrlDownloadCompletedCallback cb, UrlDownloaderConfig config)
		{
			WebRequest request = WebRequest.Create(url);
			DownloadResult downloadResult = new DownloadResult();
			downloadResult.callback = cb;
			Download(new DownloadState
			{
				downloader = this,
				host = url,
				downloadResult = downloadResult,
				request = request,
				numRetriesLeft = config.numRetries,
				timeoutMs = config.timeoutMs
			});
		}

		private static void Download(DownloadState state)
		{
			try
			{
				IAsyncResult asyncResult = state.request.BeginGetResponse(ResponseCallback, state);
				int num = state.timeoutMs;
				if (num < 0)
				{
					num = -1;
				}
				state.timeoutWatchHandle = asyncResult.AsyncWaitHandle;
				state.timeoutWaitHandle = ThreadPool.RegisterWaitForSingleObject(state.timeoutWatchHandle, TimeoutCallback, state, num, executeOnlyOnce: true);
			}
			catch (Exception)
			{
				FinishDownload(state);
			}
		}

		private static void ResponseCallback(IAsyncResult ar)
		{
			DownloadState downloadState = (DownloadState)ar.AsyncState;
			try
			{
				WebResponse webResponse = downloadState.request.EndGetResponse(ar);
				Stream stream = (downloadState.responseStream = webResponse.GetResponseStream());
				downloadState.downloadResult.downloadData = new byte[webResponse.ContentLength];
				stream.BeginRead(downloadState.readBuffer, 0, downloadState.readBuffer.Length, ReadCallback, downloadState);
			}
			catch (Exception)
			{
				FinishDownload(downloadState);
			}
		}

		private static void ReadCallback(IAsyncResult ar)
		{
			DownloadState downloadState = (DownloadState)ar.AsyncState;
			bool flag = true;
			try
			{
				Stream responseStream = downloadState.responseStream;
				int num = responseStream.EndRead(ar);
				if (num > 0)
				{
					flag = false;
					Array.Copy(downloadState.readBuffer, 0, downloadState.downloadResult.downloadData, downloadState.readPos, num);
					downloadState.readPos += num;
					responseStream.BeginRead(downloadState.readBuffer, 0, downloadState.readBuffer.Length, ReadCallback, downloadState);
				}
				else if (num == 0)
				{
					downloadState.downloadResult.succeeded = true;
				}
			}
			catch (Exception)
			{
			}
			if (flag)
			{
				FinishDownload(downloadState);
			}
		}

		private static void TimeoutCallback(object context, bool timedOut)
		{
			DownloadState downloadState = (DownloadState)context;
			downloadState.UnregisterTimeout();
			if (timedOut)
			{
				downloadState.request.Abort();
			}
		}

		private static void FinishDownload(DownloadState state)
		{
			if (!state.downloadResult.succeeded && state.numRetriesLeft > 0)
			{
				state.numRetriesLeft--;
				Download(state);
			}
			else
			{
				lock (state.downloader.m_completedDownloads)
				{
					state.downloader.m_completedDownloads.Add(state.downloadResult);
				}
			}
		}
	}
}
