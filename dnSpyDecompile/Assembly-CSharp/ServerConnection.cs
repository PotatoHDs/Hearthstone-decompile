using System;
using System.Net;
using System.Net.Sockets;
using bgs;
using UnityEngine;

// Token: 0x02000603 RID: 1539
public class ServerConnection<PacketType> where PacketType : PacketFormat, new()
{
	// Token: 0x0600540C RID: 21516 RVA: 0x001B75C4 File Offset: 0x001B57C4
	~ServerConnection()
	{
		this.Disconnect();
	}

	// Token: 0x0600540D RID: 21517 RVA: 0x001B75F0 File Offset: 0x001B57F0
	public IPEndPoint GetLocalEndPoint()
	{
		if (this.m_socket == null)
		{
			return null;
		}
		return this.m_socket.LocalEndPoint as IPEndPoint;
	}

	// Token: 0x0600540E RID: 21518 RVA: 0x001B760C File Offset: 0x001B580C
	public bool Open(int port)
	{
		if (this.m_socket != null)
		{
			return false;
		}
		IPEndPoint localEP = new IPEndPoint(IPAddress.Any, port);
		try
		{
			this.m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			this.m_socket.Bind(localEP);
			this.m_socket.Listen(16);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("SeverConnection: error opening inbound connection: " + ex.Message + " (this probably occurred because you have multiple game instances running)");
			this.m_socket = null;
			return false;
		}
		return this.Listen();
	}

	// Token: 0x0600540F RID: 21519 RVA: 0x001B7698 File Offset: 0x001B5898
	public void Disconnect()
	{
		if (this.m_socket != null && this.m_socket.Connected)
		{
			this.m_socket.Shutdown(SocketShutdown.Both);
			this.m_socket.Close();
		}
	}

	// Token: 0x06005410 RID: 21520 RVA: 0x001B76C8 File Offset: 0x001B58C8
	public bool Listen()
	{
		object @lock = this.m_lock;
		lock (@lock)
		{
			if (this.m_listening)
			{
				return true;
			}
			this.m_listening = true;
		}
		if (this.m_socket == null)
		{
			return false;
		}
		try
		{
			this.m_socket.BeginAccept(new AsyncCallback(ServerConnection<PacketType>.OnAccept), this);
		}
		catch (Exception ex)
		{
			@lock = this.m_lock;
			lock (@lock)
			{
				this.m_listening = false;
			}
			Debug.LogError("error listening for incoming connections: " + ex.Message);
			this.m_socket = null;
			return false;
		}
		return true;
	}

	// Token: 0x06005411 RID: 21521 RVA: 0x001B77A0 File Offset: 0x001B59A0
	private static void OnAccept(IAsyncResult ar)
	{
		ServerConnection<PacketType> serverConnection = (ServerConnection<PacketType>)ar.AsyncState;
		if (serverConnection == null || serverConnection.m_socket == null)
		{
			return;
		}
		try
		{
			Socket socket = serverConnection.m_socket.EndAccept(ar);
			serverConnection.m_currentConnection = new ClientConnection<PacketType>(socket);
		}
		catch (Exception ex)
		{
			Debug.LogError("error accepting connection: " + ex.Message);
		}
		serverConnection.m_listening = false;
	}

	// Token: 0x06005412 RID: 21522 RVA: 0x001B7810 File Offset: 0x001B5A10
	public ClientConnection<PacketType> GetNextAcceptedConnection()
	{
		if (this.m_currentConnection != null)
		{
			ClientConnection<PacketType> currentConnection = this.m_currentConnection;
			this.m_currentConnection = null;
			return currentConnection;
		}
		this.Listen();
		return null;
	}

	// Token: 0x04004A46 RID: 19014
	private Socket m_socket;

	// Token: 0x04004A47 RID: 19015
	private int m_port;

	// Token: 0x04004A48 RID: 19016
	private ClientConnection<PacketType> m_currentConnection;

	// Token: 0x04004A49 RID: 19017
	private bool m_listening;

	// Token: 0x04004A4A RID: 19018
	private object m_lock = new object();
}
