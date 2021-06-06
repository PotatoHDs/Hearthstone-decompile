using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace bgs
{
	// Token: 0x02000251 RID: 593
	public class DotNetUrlDownloader : IUrlDownloader
	{
		// Token: 0x060024B4 RID: 9396 RVA: 0x00081A4C File Offset: 0x0007FC4C
		public void Process()
		{
			List<DotNetUrlDownloader.DownloadResult> completedDownloads = this.m_completedDownloads;
			lock (completedDownloads)
			{
				foreach (DotNetUrlDownloader.DownloadResult downloadResult in this.m_completedDownloads)
				{
					downloadResult.callback(downloadResult.succeeded, downloadResult.downloadData);
				}
				this.m_completedDownloads.Clear();
			}
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x00081AE4 File Offset: 0x0007FCE4
		public void Download(string url, UrlDownloadCompletedCallback cb)
		{
			UrlDownloaderConfig config = new UrlDownloaderConfig();
			this.Download(url, cb, config);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x00081B00 File Offset: 0x0007FD00
		public void Download(string url, UrlDownloadCompletedCallback cb, UrlDownloaderConfig config)
		{
			WebRequest request = WebRequest.Create(url);
			DotNetUrlDownloader.DownloadResult downloadResult = new DotNetUrlDownloader.DownloadResult();
			downloadResult.callback = cb;
			DotNetUrlDownloader.Download(new DotNetUrlDownloader.DownloadState
			{
				downloader = this,
				host = url,
				downloadResult = downloadResult,
				request = request,
				numRetriesLeft = config.numRetries,
				timeoutMs = config.timeoutMs
			});
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x00081B60 File Offset: 0x0007FD60
		private static void Download(DotNetUrlDownloader.DownloadState state)
		{
			try
			{
				IAsyncResult asyncResult = state.request.BeginGetResponse(new AsyncCallback(DotNetUrlDownloader.ResponseCallback), state);
				int num = state.timeoutMs;
				if (num < 0)
				{
					num = -1;
				}
				state.timeoutWatchHandle = asyncResult.AsyncWaitHandle;
				state.timeoutWaitHandle = ThreadPool.RegisterWaitForSingleObject(state.timeoutWatchHandle, new WaitOrTimerCallback(DotNetUrlDownloader.TimeoutCallback), state, num, true);
			}
			catch (Exception)
			{
				DotNetUrlDownloader.FinishDownload(state);
			}
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x00081BDC File Offset: 0x0007FDDC
		private static void ResponseCallback(IAsyncResult ar)
		{
			DotNetUrlDownloader.DownloadState downloadState = (DotNetUrlDownloader.DownloadState)ar.AsyncState;
			try
			{
				WebResponse webResponse = downloadState.request.EndGetResponse(ar);
				Stream responseStream = webResponse.GetResponseStream();
				downloadState.responseStream = responseStream;
				downloadState.downloadResult.downloadData = new byte[webResponse.ContentLength];
				responseStream.BeginRead(downloadState.readBuffer, 0, downloadState.readBuffer.Length, new AsyncCallback(DotNetUrlDownloader.ReadCallback), downloadState);
			}
			catch (Exception)
			{
				DotNetUrlDownloader.FinishDownload(downloadState);
			}
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x00081C68 File Offset: 0x0007FE68
		private static void ReadCallback(IAsyncResult ar)
		{
			DotNetUrlDownloader.DownloadState downloadState = (DotNetUrlDownloader.DownloadState)ar.AsyncState;
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
					responseStream.BeginRead(downloadState.readBuffer, 0, downloadState.readBuffer.Length, new AsyncCallback(DotNetUrlDownloader.ReadCallback), downloadState);
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
				DotNetUrlDownloader.FinishDownload(downloadState);
			}
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x00081D18 File Offset: 0x0007FF18
		private static void TimeoutCallback(object context, bool timedOut)
		{
			DotNetUrlDownloader.DownloadState downloadState = (DotNetUrlDownloader.DownloadState)context;
			downloadState.UnregisterTimeout();
			if (timedOut)
			{
				downloadState.request.Abort();
			}
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x00081D44 File Offset: 0x0007FF44
		private static void FinishDownload(DotNetUrlDownloader.DownloadState state)
		{
			if (!state.downloadResult.succeeded && state.numRetriesLeft > 0)
			{
				state.numRetriesLeft--;
				DotNetUrlDownloader.Download(state);
				return;
			}
			List<DotNetUrlDownloader.DownloadResult> completedDownloads = state.downloader.m_completedDownloads;
			lock (completedDownloads)
			{
				state.downloader.m_completedDownloads.Add(state.downloadResult);
			}
		}

		// Token: 0x04000F48 RID: 3912
		private List<DotNetUrlDownloader.DownloadResult> m_completedDownloads = new List<DotNetUrlDownloader.DownloadResult>();

		// Token: 0x020006DC RID: 1756
		internal class DownloadState
		{
			// Token: 0x06006307 RID: 25351 RVA: 0x00128EC9 File Offset: 0x001270C9
			public DownloadState()
			{
				this.readBuffer = new byte[1024];
			}

			// Token: 0x06006308 RID: 25352 RVA: 0x00128EE4 File Offset: 0x001270E4
			public bool UnregisterTimeout()
			{
				bool result = false;
				if (this.timeoutWaitHandle != null && this.timeoutWatchHandle != null)
				{
					result = this.timeoutWaitHandle.Unregister(this.timeoutWatchHandle);
					this.timeoutWaitHandle = null;
					this.timeoutWatchHandle = null;
				}
				return result;
			}

			// Token: 0x04002258 RID: 8792
			public DotNetUrlDownloader downloader;

			// Token: 0x04002259 RID: 8793
			public string host;

			// Token: 0x0400225A RID: 8794
			public WebRequest request;

			// Token: 0x0400225B RID: 8795
			public Stream responseStream;

			// Token: 0x0400225C RID: 8796
			public int numRetriesLeft;

			// Token: 0x0400225D RID: 8797
			public int timeoutMs;

			// Token: 0x0400225E RID: 8798
			public RegisteredWaitHandle timeoutWaitHandle;

			// Token: 0x0400225F RID: 8799
			public WaitHandle timeoutWatchHandle;

			// Token: 0x04002260 RID: 8800
			private const int bufferSize = 1024;

			// Token: 0x04002261 RID: 8801
			public byte[] readBuffer;

			// Token: 0x04002262 RID: 8802
			public int readPos;

			// Token: 0x04002263 RID: 8803
			public DotNetUrlDownloader.DownloadResult downloadResult;
		}

		// Token: 0x020006DD RID: 1757
		internal class DownloadResult
		{
			// Token: 0x04002264 RID: 8804
			public UrlDownloadCompletedCallback callback;

			// Token: 0x04002265 RID: 8805
			public byte[] downloadData;

			// Token: 0x04002266 RID: 8806
			public bool succeeded;
		}
	}
}
