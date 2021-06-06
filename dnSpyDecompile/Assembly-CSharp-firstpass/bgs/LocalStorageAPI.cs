using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace bgs
{
	// Token: 0x02000207 RID: 519
	public class LocalStorageAPI : BattleNetAPI
	{
		// Token: 0x06001FE8 RID: 8168 RVA: 0x00070BDF File Offset: 0x0006EDDF
		public LocalStorageAPI(BattleNetCSharp battlenet) : base(battlenet, "LocalStorage")
		{
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x00070BED File Offset: 0x0006EDED
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x00070BF4 File Offset: 0x0006EDF4
		public uint DepotPort
		{
			get
			{
				return LocalStorageAPI.m_depotport;
			}
			set
			{
				LocalStorageAPI.m_depotport = value;
			}
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x00070BFC File Offset: 0x0006EDFC
		public bool GetFile(ContentHandle ch, LocalStorageAPI.DownloadCompletedCallback cb, object userContext = null)
		{
			try
			{
				LocalStorageFileState localStorageFileState = new LocalStorageFileState(LocalStorageAPI.m_downloadId);
				localStorageFileState.CH = ch;
				localStorageFileState.Callback = cb;
				localStorageFileState.UserContext = userContext;
				LocalStorageAPI.s_log.LogDebug("Starting GetFile State={0}", new object[]
				{
					localStorageFileState
				});
				if (!this.LoadStateFromDrive(localStorageFileState))
				{
					LocalStorageAPI.s_log.LogDebug("Unable to load file from disk, starting a download. State={0}", new object[]
					{
						localStorageFileState
					});
					this.DownloadFromDepot(localStorageFileState);
				}
			}
			catch (Exception ex)
			{
				LocalStorageAPI.s_log.LogWarning("EXCEPTION (GetFile): {0}", new object[]
				{
					ex.Message
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x00070CA4 File Offset: 0x0006EEA4
		public override void Process()
		{
			List<LocalStorageFileState> completedDownloads = LocalStorageAPI.m_completedDownloads;
			lock (completedDownloads)
			{
				foreach (LocalStorageFileState localStorageFileState in LocalStorageAPI.m_completedDownloads)
				{
					if (localStorageFileState.FileData != null)
					{
						LocalStorageAPI.s_log.LogDebug("Request completed State={0}", new object[]
						{
							localStorageFileState
						});
					}
					else
					{
						LocalStorageAPI.s_log.LogWarning("Request failed State={0}", new object[]
						{
							localStorageFileState
						});
					}
					LocalStorageAPI.s_log.Process();
					localStorageFileState.Callback(localStorageFileState.FileData, localStorageFileState.UserContext);
				}
				LocalStorageAPI.m_completedDownloads.Clear();
			}
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x00070D80 File Offset: 0x0006EF80
		private bool LoadStateFromDrive(LocalStorageFileState state)
		{
			try
			{
				LocalStorageAPI.s_log.LogDebug("LoadState State={0}", new object[]
				{
					state
				});
				string text = LocalStorageAPI.MakeFullPathFromState(state);
				LocalStorageAPI.s_log.LogDebug("Attempting to load {0}", new object[]
				{
					text
				});
				if (!File.Exists(text))
				{
					LocalStorageAPI.s_log.LogDebug("File does not exist, unable to load from disk.");
					return false;
				}
				FileStream fileStream = File.OpenRead(text);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (LocalStorageAPI.ComputeSHA256(array) != state.CH.Sha256Digest)
				{
					LocalStorageAPI.s_log.LogDebug("File was loaded but integrity check failed, attempting to delete ...");
					File.Delete(text);
					return false;
				}
				this.DecompressStateIfPossible(state, array);
				LocalStorageAPI.s_log.LogDebug("Loading completed");
				LocalStorageAPI.FinalizeState(state);
			}
			catch (Exception ex)
			{
				LocalStorageAPI.s_log.LogWarning("EXCEPTION: {0}", new object[]
				{
					ex.Message
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x00070E90 File Offset: 0x0006F090
		private static void StoreStateToDrive(LocalStorageFileState state)
		{
			try
			{
				LocalStorageAPI.s_log.LogDebug("StoreState State={0}", new object[]
				{
					state
				});
				string text = LocalStorageAPI.MakeFullPathFromState(state);
				LocalStorageAPI.s_log.LogDebug("Attempting to save {0}", new object[]
				{
					text
				});
				if (File.Exists(text))
				{
					LocalStorageAPI.s_log.LogDebug("Unable to save the file, it already exists");
				}
				else
				{
					FileStream fileStream = File.Create(text, state.FileData.Length);
					if (state.CompressedData == null)
					{
						LocalStorageAPI.s_log.LogDebug("Writing uncompressed file to disk");
						fileStream.Write(state.FileData, 0, state.FileData.Length);
					}
					else
					{
						LocalStorageAPI.s_log.LogDebug("Writing compressed file to disk");
						fileStream.Write(state.CompressedData, 0, state.CompressedData.Length);
					}
					fileStream.Flush();
					fileStream.Close();
					LocalStorageAPI.s_log.LogDebug("Writing completed");
				}
			}
			catch (Exception ex)
			{
				LocalStorageAPI.s_log.LogWarning("EXCEPTION (StoreStateToDrive): {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x00070FA4 File Offset: 0x0006F1A4
		private static string MakeFullPathFromState(LocalStorageFileState state)
		{
			string temporaryCachePath = BattleNet.Client().GetTemporaryCachePath();
			string path = state.CH.Sha256Digest + "." + state.CH.Usage;
			return Path.Combine(temporaryCachePath, path);
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00070FE4 File Offset: 0x0006F1E4
		private void DownloadFromDepot(LocalStorageFileState state)
		{
			string text = string.Format("{0}.depot.battle.net", state.CH.Region);
			if (string.Equals(state.CH.Region, "cn", StringComparison.CurrentCultureIgnoreCase))
			{
				text = "cn.depot.battlenet.com.cn";
			}
			LocalStorageAPI.s_log.LogDebug("Starting download from {0}", new object[]
			{
				text
			});
			state.Connection.LogDebug = new Action<string>(LocalStorageAPI.s_log.LogDebug);
			state.Connection.LogWarning = new Action<string>(LocalStorageAPI.s_log.LogWarning);
			state.Connection.OnFailure = delegate()
			{
				LocalStorageAPI.ExecuteFailedDownload(state);
			};
			state.Connection.OnSuccess = delegate()
			{
				this.ConnectCallback(state);
			};
			state.Connection.Connect(text, this.DepotPort, new Random().Next(0, 9));
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x000710F8 File Offset: 0x0006F2F8
		private void ConnectCallback(LocalStorageFileState state)
		{
			try
			{
				LocalStorageAPI.s_log.LogDebug("ConnectCallback called for State={0}", new object[]
				{
					state
				});
				state.ReceiveBuffer = new byte[LocalStorageAPI.m_bufferSize];
				state.Socket.BeginReceive(state.ReceiveBuffer, 0, LocalStorageAPI.m_bufferSize, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), state);
				string requestFromState = this.GetRequestFromState(state);
				byte[] bytes = Encoding.ASCII.GetBytes(requestFromState);
				state.Socket.Send(bytes, 0, bytes.Length, SocketFlags.None);
			}
			catch (Exception ex)
			{
				LocalStorageAPI.s_log.LogWarning("EXCEPTION: {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000711B0 File Offset: 0x0006F3B0
		private string GetRequestFromState(LocalStorageFileState state)
		{
			if (state == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("GET /{0}.{1} HTTP/1.1\r\n", state.CH.Sha256Digest, state.CH.Usage);
			stringBuilder.AppendFormat("Host: {0}\r\n", state.Connection.Host);
			stringBuilder.Append("User-Agent: HearthStone\r\n");
			stringBuilder.Append("Connection: close\r\n");
			stringBuilder.Append("\r\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x00071230 File Offset: 0x0006F430
		private void ReceiveCallback(IAsyncResult ar)
		{
			LocalStorageFileState localStorageFileState = (LocalStorageFileState)ar.AsyncState;
			try
			{
				LocalStorageAPI.s_log.LogDebug("ReceiveCallback called for State={0}", new object[]
				{
					localStorageFileState
				});
				int num = localStorageFileState.Socket.EndReceive(ar);
				if (num > 0)
				{
					int num2 = ((localStorageFileState.FileData == null) ? 0 : localStorageFileState.FileData.Length) + num;
					MemoryStream memoryStream = new MemoryStream(new byte[num2], 0, num2, true, true);
					if (localStorageFileState.FileData != null)
					{
						memoryStream.Write(localStorageFileState.FileData, 0, localStorageFileState.FileData.Length);
					}
					memoryStream.Write(localStorageFileState.ReceiveBuffer, 0, num);
					localStorageFileState.FileData = memoryStream.GetBuffer();
					localStorageFileState.Socket.BeginReceive(localStorageFileState.ReceiveBuffer, 0, LocalStorageAPI.m_bufferSize, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), localStorageFileState);
				}
				else
				{
					this.CompleteDownload(localStorageFileState);
				}
			}
			catch (Exception ex)
			{
				LocalStorageAPI.s_log.LogWarning("EXCEPTION: {0}", new object[]
				{
					ex.Message
				});
				localStorageFileState.FailureMessage = ex.Message;
				LocalStorageAPI.ExecuteFailedDownload(localStorageFileState);
			}
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0007134C File Offset: 0x0006F54C
		private void CompleteDownload(LocalStorageFileState state)
		{
			bool flag = true;
			LocalStorageAPI.s_log.LogDebug("Download completed for State={0}", new object[]
			{
				state
			});
			HTTPHeader httpheader = LocalStorageAPI.ParseHTTPHeader(state.FileData);
			if (httpheader == null)
			{
				LocalStorageAPI.s_log.LogWarning("Parsinig of HTTP header failed for State={0} Connection={1} Request=[{2}]", new object[]
				{
					state,
					state.Connection,
					this.GetRequestFromState(state)
				});
			}
			else
			{
				byte[] array = new byte[httpheader.ContentLength];
				Array.Copy(state.FileData, httpheader.ContentStart, array, 0, httpheader.ContentLength);
				if (LocalStorageAPI.ComputeSHA256(array) != state.CH.Sha256Digest)
				{
					LocalStorageAPI.s_log.LogWarning("Integrity check failed for State={0}", new object[]
					{
						state
					});
				}
				else
				{
					flag = false;
					this.DecompressStateIfPossible(state, array);
				}
			}
			if (flag || state.FileData == null)
			{
				LocalStorageAPI.ExecuteFailedDownload(state);
				return;
			}
			LocalStorageAPI.ExecuteSucessfulDownload(state);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0007142C File Offset: 0x0006F62C
		private void DecompressStateIfPossible(LocalStorageFileState state, byte[] data)
		{
			ulong num;
			if (LocalStorageAPI.IsCompressedStream(data, out num))
			{
				state.CompressedData = data;
				MemoryStream ms = new MemoryStream(data, 16, data.Length - 16);
				state.FileData = this.Inflate(ms, (int)num);
				return;
			}
			state.FileData = data;
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x00071470 File Offset: 0x0006F670
		private static bool IsCompressedStream(byte[] data, out ulong decompressedLength)
		{
			decompressedLength = 0UL;
			try
			{
				if (data.Length < 16)
				{
					return false;
				}
				if (BitConverter.ToUInt32(data, 0) != 1131245658U)
				{
					return false;
				}
				if (BitConverter.ToUInt32(data, 4) != 0U)
				{
					return false;
				}
				decompressedLength = BitConverter.ToUInt64(data, 8);
			}
			catch (Exception ex)
			{
				LocalStorageAPI.s_log.LogWarning("EXCEPTION: {0}", new object[]
				{
					ex.Message
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000714EC File Offset: 0x0006F6EC
		private static HTTPHeader ParseHTTPHeader(byte[] data)
		{
			try
			{
				int num = LocalStorageAPI.SearchForBytePattern(data, new byte[]
				{
					13,
					10,
					13,
					10
				});
				if (num == -1)
				{
					return null;
				}
				int num2 = num + 1;
				if (num2 >= data.Length)
				{
					return null;
				}
				string @string = Encoding.ASCII.GetString(data, 0, num);
				if (@string.IndexOf("200 OK") == -1)
				{
					return null;
				}
				Match match = new Regex("(?<=Content-Length:\\s)\\d+", RegexOptions.IgnoreCase).Match(@string);
				if (!match.Success)
				{
					return null;
				}
				int num3 = (int)uint.Parse(match.Value);
				int num4 = data.Length - num2;
				if (num3 != num4)
				{
					return null;
				}
				return new HTTPHeader
				{
					ContentLength = num3,
					ContentStart = num2
				};
			}
			catch (Exception ex)
			{
				LocalStorageAPI.s_log.LogWarning("EXCEPTION (ParseHTTPHeader): {0}", new object[]
				{
					ex.Message
				});
			}
			return null;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000715E0 File Offset: 0x0006F7E0
		private static int SearchForBytePattern(byte[] source, byte[] pattern)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (pattern[0] == source[i] && source.Length - i >= pattern.Length)
				{
					bool flag = true;
					int num = 1;
					while (num < pattern.Length && flag)
					{
						if (source[i + num] != pattern[num])
						{
							flag = false;
						}
						num++;
					}
					if (flag)
					{
						return i + (pattern.Length - 1);
					}
				}
			}
			return -1;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x0007163A File Offset: 0x0006F83A
		private static void ExecuteFailedDownload(LocalStorageFileState state)
		{
			state.FileData = null;
			LocalStorageAPI.FinalizeDownload(state);
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00071649 File Offset: 0x0006F849
		private static void ExecuteSucessfulDownload(LocalStorageFileState state)
		{
			LocalStorageAPI.StoreStateToDrive(state);
			LocalStorageAPI.FinalizeDownload(state);
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x00071657 File Offset: 0x0006F857
		private static void FinalizeDownload(LocalStorageFileState state)
		{
			state.Connection.Disconnect();
			state.ReceiveBuffer = null;
			LocalStorageAPI.FinalizeState(state);
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x00071674 File Offset: 0x0006F874
		private static void FinalizeState(LocalStorageFileState state)
		{
			List<LocalStorageFileState> completedDownloads = LocalStorageAPI.m_completedDownloads;
			lock (completedDownloads)
			{
				LocalStorageAPI.m_completedDownloads.Add(state);
			}
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x000716B8 File Offset: 0x0006F8B8
		private static string ComputeSHA256(byte[] bytes)
		{
			byte[] array = SHA256.Create().ComputeHash(bytes, 0, bytes.Length);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x00071708 File Offset: 0x0006F908
		private byte[] Inflate(MemoryStream ms, int length)
		{
			ms.Seek(0L, SeekOrigin.Begin);
			byte[] result;
			using (Stream inflateStream = this.m_battleNet.CompressionProvider.GetInflateStream(ms))
			{
				byte[] array = new byte[length];
				int num = 0;
				int num2 = array.Length;
				try
				{
					for (;;)
					{
						int num3 = inflateStream.Read(array, num, num2);
						if (num3 <= 0)
						{
							break;
						}
						num += num3;
						num2 -= num3;
					}
				}
				catch (Exception ex)
				{
					LocalStorageAPI.s_log.LogWarning("EXCEPTION (Inflate): {0}", new object[]
					{
						ex.Message
					});
					return null;
				}
				if (num != length)
				{
					LocalStorageAPI.s_log.LogWarning("Decompressed size does not equal expected size.");
					result = null;
				}
				else
				{
					result = array;
				}
			}
			return result;
		}

		// Token: 0x04000B92 RID: 2962
		private static uint m_depotport = 1119U;

		// Token: 0x04000B93 RID: 2963
		private static List<LocalStorageFileState> m_completedDownloads = new List<LocalStorageFileState>();

		// Token: 0x04000B94 RID: 2964
		private static readonly int m_bufferSize = 1024;

		// Token: 0x04000B95 RID: 2965
		private static LogThreadHelper s_log = new LogThreadHelper("LocalStorage");

		// Token: 0x04000B96 RID: 2966
		private static int m_downloadId = 0;

		// Token: 0x020006A3 RID: 1699
		// (Invoke) Token: 0x06006229 RID: 25129
		public delegate void DownloadCompletedCallback(byte[] data, object userContext);
	}
}
