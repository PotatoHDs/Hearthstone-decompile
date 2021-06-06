using System.Net.Sockets;

namespace bgs
{
	internal class LocalStorageFileState
	{
		public byte[] ReceiveBuffer;

		public ContentHandle CH;

		public TcpConnection Connection = new TcpConnection();

		public byte[] FileData;

		public byte[] CompressedData;

		public object UserContext;

		private int m_ID;

		public Socket Socket => Connection.Socket;

		public string FailureMessage { get; set; }

		public LocalStorageAPI.DownloadCompletedCallback Callback { get; set; }

		public int ID => m_ID;

		public LocalStorageFileState(int id)
		{
			m_ID = id;
		}

		public override string ToString()
		{
			return $"[Region={CH.Region} Usage={CH.Usage} SHA256={CH.Sha256Digest} ID={m_ID}]";
		}
	}
}
