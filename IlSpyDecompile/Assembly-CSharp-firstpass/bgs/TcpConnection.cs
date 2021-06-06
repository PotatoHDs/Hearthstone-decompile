using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace bgs
{
	public class TcpConnection
	{
		public Action<string> LogDebug = delegate
		{
		};

		public Action<string> LogWarning = delegate
		{
		};

		public Action OnFailure = delegate
		{
		};

		public Action OnSuccess = delegate
		{
		};

		private const int SocketErrorHostNotFound = 11001;

		private Socket m_socket;

		private Queue<IPAddress> m_candidateIPAddresses;

		private IPAddress m_resolvedIPAddress;

		public string Host { get; private set; } = string.Empty;


		public uint Port { get; private set; }

		public IPAddress ResolvedAddress => m_resolvedIPAddress;

		public Socket Socket => m_socket;

		public void Connect(string host, uint port, int tryCount)
		{
			LogDebug($"TcpConnection - Connecting to host: {host}, port: {port}");
			Host = host;
			Port = port;
			m_candidateIPAddresses = new Queue<IPAddress>();
			if (IPAddress.TryParse(Host, out var address))
			{
				m_candidateIPAddresses.Enqueue(address);
			}
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(Host);
				Array.Sort(hostEntry.AddressList, delegate(IPAddress x, IPAddress y)
				{
					if (x.AddressFamily < y.AddressFamily)
					{
						return -1;
					}
					return (x.AddressFamily > y.AddressFamily) ? 1 : 0;
				});
				tryCount %= hostEntry.AddressList.Length;
				for (int i = tryCount; i < hostEntry.AddressList.Length; i++)
				{
					m_candidateIPAddresses.Enqueue(hostEntry.AddressList[i]);
				}
				for (int j = 0; j < tryCount; j++)
				{
					m_candidateIPAddresses.Enqueue(hostEntry.AddressList[j]);
				}
			}
			catch (Exception ex)
			{
				LogWarning($"TcpConnection - failed to get possible ip address: {ex.Message}");
			}
			foreach (IPAddress candidateIPAddress in m_candidateIPAddresses)
			{
				LogDebug($"TcpConnection - possible ip address: {candidateIPAddress}");
			}
			ConnectInternal();
		}

		public bool ConnectNext()
		{
			if (m_candidateIPAddresses.Count > 0)
			{
				ConnectInternal();
				return true;
			}
			return false;
		}

		private void ConnectInternal()
		{
			LogDebug($"TcpConnection - ConnectInternal. address-count: {m_candidateIPAddresses.Count}");
			Disconnect();
			if (m_candidateIPAddresses.Count == 0)
			{
				LogWarning($"TcpConnection - Could not connect to ip: {Host}, port: {Port}");
				OnFailure();
				return;
			}
			m_resolvedIPAddress = m_candidateIPAddresses.Dequeue();
			IPEndPoint remoteEP = new IPEndPoint(m_resolvedIPAddress, (int)Port);
			LogDebug($"TcpConnection - Create Socket with ip: {m_resolvedIPAddress}, port: {Port}, af: {m_resolvedIPAddress.AddressFamily}");
			m_socket = new Socket(m_resolvedIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				m_socket.BeginConnect(remoteEP, ConnectCallback, null);
			}
			catch (Exception ex)
			{
				LogDebug($"TcpConnection - BeginConnect() failed. ip: {m_resolvedIPAddress}, port: {Port}, af: {m_resolvedIPAddress.AddressFamily}, exception: {ex.Message}");
				ConnectInternal();
			}
		}

		private void ConnectCallback(IAsyncResult ar)
		{
			Exception ex = null;
			try
			{
				m_socket.EndConnect(ar);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (ex != null || !m_socket.Connected)
			{
				LogDebug($"TcpConnection - EndConnect() failed. ip: {m_resolvedIPAddress}, port: {Port}, af: {m_resolvedIPAddress.AddressFamily}, exception: {ex.Message}");
				ConnectInternal();
			}
			else
			{
				LogDebug($"TcpConnection - Connected to ip: {m_resolvedIPAddress}, port: {Port}, af: {m_resolvedIPAddress.AddressFamily}");
				OnSuccess();
			}
		}

		public bool MatchSslCertName(IEnumerable<string> certNames)
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(Host);
			IPAddress[] array2;
			foreach (string certName in certNames)
			{
				if (!certName.StartsWith("::ffff:"))
				{
					continue;
				}
				string hostNameOrAddress = certName.Substring("::ffff:".Length);
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
				array2 = array;
				foreach (IPAddress obj in array2)
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
			string text = $"TcpConnection - MatchSslCertName failed.";
			foreach (string certName2 in certNames)
			{
				text += $"\n\t certName: {certName2}";
			}
			array2 = hostEntry.AddressList;
			foreach (IPAddress arg in array2)
			{
				text += $"\n\t hostAddress: {arg}";
			}
			LogWarning(text);
			return false;
		}

		public void Disconnect()
		{
			if (m_socket == null)
			{
				return;
			}
			if (m_socket.Connected)
			{
				try
				{
					m_socket.Shutdown(SocketShutdown.Both);
					m_socket.Close();
				}
				catch (SocketException ex)
				{
					LogWarning($"TcpConnection.Disconnect() - SocketException: {ex.Message}");
				}
			}
			m_socket = null;
		}

		public override string ToString()
		{
			return $"[Host={Host} Port={Port}]";
		}
	}
}
