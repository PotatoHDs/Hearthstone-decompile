using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace bgs
{
	// Token: 0x02000266 RID: 614
	public class TcpConnection
	{
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x0008562F File Offset: 0x0008382F
		// (set) Token: 0x06002569 RID: 9577 RVA: 0x00085637 File Offset: 0x00083837
		public string Host { get; private set; } = string.Empty;

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x00085640 File Offset: 0x00083840
		// (set) Token: 0x0600256B RID: 9579 RVA: 0x00085648 File Offset: 0x00083848
		public uint Port { get; private set; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x00085651 File Offset: 0x00083851
		public IPAddress ResolvedAddress
		{
			get
			{
				return this.m_resolvedIPAddress;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x00085659 File Offset: 0x00083859
		public Socket Socket
		{
			get
			{
				return this.m_socket;
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x00085664 File Offset: 0x00083864
		public void Connect(string host, uint port, int tryCount)
		{
			this.LogDebug(string.Format("TcpConnection - Connecting to host: {0}, port: {1}", host, port));
			this.Host = host;
			this.Port = port;
			this.m_candidateIPAddresses = new Queue<IPAddress>();
			IPAddress item;
			if (IPAddress.TryParse(this.Host, out item))
			{
				this.m_candidateIPAddresses.Enqueue(item);
			}
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(this.Host);
				Array.Sort<IPAddress>(hostEntry.AddressList, delegate(IPAddress x, IPAddress y)
				{
					if (x.AddressFamily < y.AddressFamily)
					{
						return -1;
					}
					if (x.AddressFamily > y.AddressFamily)
					{
						return 1;
					}
					return 0;
				});
				tryCount %= hostEntry.AddressList.Length;
				for (int i = tryCount; i < hostEntry.AddressList.Length; i++)
				{
					this.m_candidateIPAddresses.Enqueue(hostEntry.AddressList[i]);
				}
				for (int j = 0; j < tryCount; j++)
				{
					this.m_candidateIPAddresses.Enqueue(hostEntry.AddressList[j]);
				}
			}
			catch (Exception ex)
			{
				this.LogWarning(string.Format("TcpConnection - failed to get possible ip address: {0}", ex.Message));
			}
			foreach (IPAddress arg in this.m_candidateIPAddresses)
			{
				this.LogDebug(string.Format("TcpConnection - possible ip address: {0}", arg));
			}
			this.ConnectInternal();
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000857D8 File Offset: 0x000839D8
		public bool ConnectNext()
		{
			if (this.m_candidateIPAddresses.Count > 0)
			{
				this.ConnectInternal();
				return true;
			}
			return false;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000857F4 File Offset: 0x000839F4
		private void ConnectInternal()
		{
			this.LogDebug(string.Format("TcpConnection - ConnectInternal. address-count: {0}", this.m_candidateIPAddresses.Count));
			this.Disconnect();
			if (this.m_candidateIPAddresses.Count == 0)
			{
				this.LogWarning(string.Format("TcpConnection - Could not connect to ip: {0}, port: {1}", this.Host, this.Port));
				this.OnFailure();
				return;
			}
			this.m_resolvedIPAddress = this.m_candidateIPAddresses.Dequeue();
			IPEndPoint remoteEP = new IPEndPoint(this.m_resolvedIPAddress, (int)this.Port);
			this.LogDebug(string.Format("TcpConnection - Create Socket with ip: {0}, port: {1}, af: {2}", this.m_resolvedIPAddress, this.Port, this.m_resolvedIPAddress.AddressFamily));
			this.m_socket = new Socket(this.m_resolvedIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				this.m_socket.BeginConnect(remoteEP, new AsyncCallback(this.ConnectCallback), null);
			}
			catch (Exception ex)
			{
				this.LogDebug(string.Format("TcpConnection - BeginConnect() failed. ip: {0}, port: {1}, af: {2}, exception: {3}", new object[]
				{
					this.m_resolvedIPAddress,
					this.Port,
					this.m_resolvedIPAddress.AddressFamily,
					ex.Message
				}));
				this.ConnectInternal();
			}
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00085960 File Offset: 0x00083B60
		private void ConnectCallback(IAsyncResult ar)
		{
			Exception ex = null;
			try
			{
				this.m_socket.EndConnect(ar);
			}
			catch (Exception ex)
			{
			}
			if (ex != null || !this.m_socket.Connected)
			{
				this.LogDebug(string.Format("TcpConnection - EndConnect() failed. ip: {0}, port: {1}, af: {2}, exception: {3}", new object[]
				{
					this.m_resolvedIPAddress,
					this.Port,
					this.m_resolvedIPAddress.AddressFamily,
					ex.Message
				}));
				this.ConnectInternal();
				return;
			}
			this.LogDebug(string.Format("TcpConnection - Connected to ip: {0}, port: {1}, af: {2}", this.m_resolvedIPAddress, this.Port, this.m_resolvedIPAddress.AddressFamily));
			this.OnSuccess();
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x00085A38 File Offset: 0x00083C38
		public bool MatchSslCertName(IEnumerable<string> certNames)
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(this.Host);
			foreach (string text in certNames)
			{
				if (text.StartsWith("::ffff:"))
				{
					string hostNameOrAddress = text.Substring("::ffff:".Length);
					IPAddress[] array;
					try
					{
						array = Dns.GetHostEntry(hostNameOrAddress).AddressList;
					}
					catch (SocketException ex)
					{
						if (ex.ErrorCode != 11001)
						{
							throw;
						}
						array = Dns.GetHostAddresses(hostNameOrAddress);
					}
					foreach (IPAddress obj in array)
					{
						IPAddress[] addressList = hostEntry.AddressList;
						for (int j = 0; j < addressList.Length; j++)
						{
							if (addressList[j].Equals(obj))
							{
								return true;
							}
						}
					}
				}
			}
			string text2 = string.Format("TcpConnection - MatchSslCertName failed.", Array.Empty<object>());
			foreach (string arg in certNames)
			{
				text2 += string.Format("\n\t certName: {0}", arg);
			}
			foreach (IPAddress arg2 in hostEntry.AddressList)
			{
				text2 += string.Format("\n\t hostAddress: {0}", arg2);
			}
			this.LogWarning(text2);
			return false;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x00085BCC File Offset: 0x00083DCC
		public void Disconnect()
		{
			if (this.m_socket == null)
			{
				return;
			}
			if (this.m_socket.Connected)
			{
				try
				{
					this.m_socket.Shutdown(SocketShutdown.Both);
					this.m_socket.Close();
				}
				catch (SocketException ex)
				{
					this.LogWarning(string.Format("TcpConnection.Disconnect() - SocketException: {0}", ex.Message));
				}
			}
			this.m_socket = null;
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x00085C40 File Offset: 0x00083E40
		public override string ToString()
		{
			return string.Format("[Host={0} Port={1}]", this.Host, this.Port);
		}

		// Token: 0x04000FA2 RID: 4002
		public Action<string> LogDebug = delegate(string <p0>)
		{
		};

		// Token: 0x04000FA3 RID: 4003
		public Action<string> LogWarning = delegate(string <p0>)
		{
		};

		// Token: 0x04000FA4 RID: 4004
		public Action OnFailure = delegate()
		{
		};

		// Token: 0x04000FA5 RID: 4005
		public Action OnSuccess = delegate()
		{
		};

		// Token: 0x04000FA8 RID: 4008
		private const int SocketErrorHostNotFound = 11001;

		// Token: 0x04000FA9 RID: 4009
		private Socket m_socket;

		// Token: 0x04000FAA RID: 4010
		private Queue<IPAddress> m_candidateIPAddresses;

		// Token: 0x04000FAB RID: 4011
		private IPAddress m_resolvedIPAddress;
	}
}
