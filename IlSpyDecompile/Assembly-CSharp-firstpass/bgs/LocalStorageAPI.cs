using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace bgs
{
	public class LocalStorageAPI : BattleNetAPI
	{
		public delegate void DownloadCompletedCallback(byte[] data, object userContext);

		private static uint m_depotport = 1119u;

		private static List<LocalStorageFileState> m_completedDownloads = new List<LocalStorageFileState>();

		private static readonly int m_bufferSize = 1024;

		private static LogThreadHelper s_log = new LogThreadHelper("LocalStorage");

		private static int m_downloadId = 0;

		public uint DepotPort
		{
			get
			{
				return m_depotport;
			}
			set
			{
				m_depotport = value;
			}
		}

		public LocalStorageAPI(BattleNetCSharp battlenet)
			: base(battlenet, "LocalStorage")
		{
		}

		public bool GetFile(ContentHandle ch, DownloadCompletedCallback cb, object userContext = null)
		{
			try
			{
				LocalStorageFileState localStorageFileState = new LocalStorageFileState(m_downloadId);
				localStorageFileState.CH = ch;
				localStorageFileState.Callback = cb;
				localStorageFileState.UserContext = userContext;
				s_log.LogDebug("Starting GetFile State={0}", localStorageFileState);
				if (!LoadStateFromDrive(localStorageFileState))
				{
					s_log.LogDebug("Unable to load file from disk, starting a download. State={0}", localStorageFileState);
					DownloadFromDepot(localStorageFileState);
				}
			}
			catch (Exception ex)
			{
				s_log.LogWarning("EXCEPTION (GetFile): {0}", ex.Message);
				return false;
			}
			return true;
		}

		public override void Process()
		{
			lock (m_completedDownloads)
			{
				foreach (LocalStorageFileState completedDownload in m_completedDownloads)
				{
					if (completedDownload.FileData != null)
					{
						s_log.LogDebug("Request completed State={0}", completedDownload);
					}
					else
					{
						s_log.LogWarning("Request failed State={0}", completedDownload);
					}
					s_log.Process();
					completedDownload.Callback(completedDownload.FileData, completedDownload.UserContext);
				}
				m_completedDownloads.Clear();
			}
		}

		private bool LoadStateFromDrive(LocalStorageFileState state)
		{
			try
			{
				s_log.LogDebug("LoadState State={0}", state);
				string text = MakeFullPathFromState(state);
				s_log.LogDebug("Attempting to load {0}", text);
				if (!File.Exists(text))
				{
					s_log.LogDebug("File does not exist, unable to load from disk.");
					return false;
				}
				FileStream fileStream = File.OpenRead(text);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (ComputeSHA256(array) != state.CH.Sha256Digest)
				{
					s_log.LogDebug("File was loaded but integrity check failed, attempting to delete ...");
					File.Delete(text);
					return false;
				}
				DecompressStateIfPossible(state, array);
				s_log.LogDebug("Loading completed");
				FinalizeState(state);
			}
			catch (Exception ex)
			{
				s_log.LogWarning("EXCEPTION: {0}", ex.Message);
				return false;
			}
			return true;
		}

		private static void StoreStateToDrive(LocalStorageFileState state)
		{
			try
			{
				s_log.LogDebug("StoreState State={0}", state);
				string text = MakeFullPathFromState(state);
				s_log.LogDebug("Attempting to save {0}", text);
				if (File.Exists(text))
				{
					s_log.LogDebug("Unable to save the file, it already exists");
					return;
				}
				FileStream fileStream = File.Create(text, state.FileData.Length);
				if (state.CompressedData == null)
				{
					s_log.LogDebug("Writing uncompressed file to disk");
					fileStream.Write(state.FileData, 0, state.FileData.Length);
				}
				else
				{
					s_log.LogDebug("Writing compressed file to disk");
					fileStream.Write(state.CompressedData, 0, state.CompressedData.Length);
				}
				fileStream.Flush();
				fileStream.Close();
				s_log.LogDebug("Writing completed");
			}
			catch (Exception ex)
			{
				s_log.LogWarning("EXCEPTION (StoreStateToDrive): {0}", ex.Message);
			}
		}

		private static string MakeFullPathFromState(LocalStorageFileState state)
		{
			string temporaryCachePath = BattleNet.Client().GetTemporaryCachePath();
			string path = state.CH.Sha256Digest + "." + state.CH.Usage;
			return Path.Combine(temporaryCachePath, path);
		}

		private void DownloadFromDepot(LocalStorageFileState state)
		{
			string text = $"{state.CH.Region}.depot.battle.net";
			if (string.Equals(state.CH.Region, "cn", StringComparison.CurrentCultureIgnoreCase))
			{
				text = "cn.depot.battlenet.com.cn";
			}
			s_log.LogDebug("Starting download from {0}", text);
			state.Connection.LogDebug = s_log.LogDebug;
			state.Connection.LogWarning = s_log.LogWarning;
			state.Connection.OnFailure = delegate
			{
				ExecuteFailedDownload(state);
			};
			state.Connection.OnSuccess = delegate
			{
				ConnectCallback(state);
			};
			state.Connection.Connect(text, DepotPort, new Random().Next(0, 9));
		}

		private void ConnectCallback(LocalStorageFileState state)
		{
			try
			{
				s_log.LogDebug("ConnectCallback called for State={0}", state);
				state.ReceiveBuffer = new byte[m_bufferSize];
				state.Socket.BeginReceive(state.ReceiveBuffer, 0, m_bufferSize, SocketFlags.None, ReceiveCallback, state);
				string requestFromState = GetRequestFromState(state);
				byte[] bytes = Encoding.ASCII.GetBytes(requestFromState);
				state.Socket.Send(bytes, 0, bytes.Length, SocketFlags.None);
			}
			catch (Exception ex)
			{
				s_log.LogWarning("EXCEPTION: {0}", ex.Message);
			}
		}

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

		private void ReceiveCallback(IAsyncResult ar)
		{
			LocalStorageFileState localStorageFileState = (LocalStorageFileState)ar.AsyncState;
			try
			{
				s_log.LogDebug("ReceiveCallback called for State={0}", localStorageFileState);
				int num = localStorageFileState.Socket.EndReceive(ar);
				if (num > 0)
				{
					int num2 = ((localStorageFileState.FileData != null) ? localStorageFileState.FileData.Length : 0) + num;
					MemoryStream memoryStream = new MemoryStream(new byte[num2], 0, num2, writable: true, publiclyVisible: true);
					if (localStorageFileState.FileData != null)
					{
						memoryStream.Write(localStorageFileState.FileData, 0, localStorageFileState.FileData.Length);
					}
					memoryStream.Write(localStorageFileState.ReceiveBuffer, 0, num);
					localStorageFileState.FileData = memoryStream.GetBuffer();
					localStorageFileState.Socket.BeginReceive(localStorageFileState.ReceiveBuffer, 0, m_bufferSize, SocketFlags.None, ReceiveCallback, localStorageFileState);
				}
				else
				{
					CompleteDownload(localStorageFileState);
				}
			}
			catch (Exception ex)
			{
				s_log.LogWarning("EXCEPTION: {0}", ex.Message);
				localStorageFileState.FailureMessage = ex.Message;
				ExecuteFailedDownload(localStorageFileState);
			}
		}

		private void CompleteDownload(LocalStorageFileState state)
		{
			bool flag = true;
			s_log.LogDebug("Download completed for State={0}", state);
			HTTPHeader hTTPHeader = ParseHTTPHeader(state.FileData);
			if (hTTPHeader == null)
			{
				s_log.LogWarning("Parsinig of HTTP header failed for State={0} Connection={1} Request=[{2}]", state, state.Connection, GetRequestFromState(state));
			}
			else
			{
				byte[] array = new byte[hTTPHeader.ContentLength];
				Array.Copy(state.FileData, hTTPHeader.ContentStart, array, 0, hTTPHeader.ContentLength);
				if (ComputeSHA256(array) != state.CH.Sha256Digest)
				{
					s_log.LogWarning("Integrity check failed for State={0}", state);
				}
				else
				{
					flag = false;
					DecompressStateIfPossible(state, array);
				}
			}
			if (flag || state.FileData == null)
			{
				ExecuteFailedDownload(state);
			}
			else
			{
				ExecuteSucessfulDownload(state);
			}
		}

		private void DecompressStateIfPossible(LocalStorageFileState state, byte[] data)
		{
			if (IsCompressedStream(data, out var decompressedLength))
			{
				state.CompressedData = data;
				MemoryStream ms = new MemoryStream(data, 16, data.Length - 16);
				state.FileData = Inflate(ms, (int)decompressedLength);
			}
			else
			{
				state.FileData = data;
			}
		}

		private static bool IsCompressedStream(byte[] data, out ulong decompressedLength)
		{
			decompressedLength = 0uL;
			try
			{
				if (data.Length < 16)
				{
					return false;
				}
				if (BitConverter.ToUInt32(data, 0) != 1131245658)
				{
					return false;
				}
				if (BitConverter.ToUInt32(data, 4) != 0)
				{
					return false;
				}
				decompressedLength = BitConverter.ToUInt64(data, 8);
			}
			catch (Exception ex)
			{
				s_log.LogWarning("EXCEPTION: {0}", ex.Message);
				return false;
			}
			return true;
		}

		private static HTTPHeader ParseHTTPHeader(byte[] data)
		{
			try
			{
				int num = SearchForBytePattern(data, new byte[4] { 13, 10, 13, 10 });
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
				s_log.LogWarning("EXCEPTION (ParseHTTPHeader): {0}", ex.Message);
			}
			return null;
		}

		private static int SearchForBytePattern(byte[] source, byte[] pattern)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (pattern[0] != source[i] || source.Length - i < pattern.Length)
				{
					continue;
				}
				bool flag = true;
				for (int j = 1; j < pattern.Length; j++)
				{
					if (!flag)
					{
						break;
					}
					if (source[i + j] != pattern[j])
					{
						flag = false;
					}
				}
				if (flag)
				{
					return i + (pattern.Length - 1);
				}
			}
			return -1;
		}

		private static void ExecuteFailedDownload(LocalStorageFileState state)
		{
			state.FileData = null;
			FinalizeDownload(state);
		}

		private static void ExecuteSucessfulDownload(LocalStorageFileState state)
		{
			StoreStateToDrive(state);
			FinalizeDownload(state);
		}

		private static void FinalizeDownload(LocalStorageFileState state)
		{
			state.Connection.Disconnect();
			state.ReceiveBuffer = null;
			FinalizeState(state);
		}

		private static void FinalizeState(LocalStorageFileState state)
		{
			lock (m_completedDownloads)
			{
				m_completedDownloads.Add(state);
			}
		}

		private static string ComputeSHA256(byte[] bytes)
		{
			byte[] array = SHA256.Create().ComputeHash(bytes, 0, bytes.Length);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		private byte[] Inflate(MemoryStream ms, int length)
		{
			ms.Seek(0L, SeekOrigin.Begin);
			using Stream stream = m_battleNet.CompressionProvider.GetInflateStream(ms);
			byte[] array = new byte[length];
			int num = 0;
			int num2 = array.Length;
			try
			{
				while (true)
				{
					int num3 = stream.Read(array, num, num2);
					if (num3 > 0)
					{
						num += num3;
						num2 -= num3;
						continue;
					}
					break;
				}
			}
			catch (Exception ex)
			{
				s_log.LogWarning("EXCEPTION (Inflate): {0}", ex.Message);
				return null;
			}
			if (num != length)
			{
				s_log.LogWarning("Decompressed size does not equal expected size.");
				return null;
			}
			return array;
		}
	}
}
