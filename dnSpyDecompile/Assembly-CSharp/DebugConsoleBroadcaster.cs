using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using UnityEngine;

// Token: 0x02000600 RID: 1536
public class DebugConsoleBroadcaster
{
	// Token: 0x060053FA RID: 21498 RVA: 0x001B7260 File Offset: 0x001B5460
	public void Start(int destinationPort, string broadCastResponse)
	{
		this.m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		this.m_Socket.EnableBroadcast = true;
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		this.m_RequestBytes = asciiencoding.GetBytes(broadCastResponse);
		this.m_RemoteEndPoint = new IPEndPoint(IPAddress.Broadcast, destinationPort);
		this.m_Timer = new Timer(DebugConsoleBroadcaster.Interval.TotalMilliseconds);
		this.m_Timer.Elapsed += this.OnTimerTick;
		this.m_Timer.Start();
		this.m_started = true;
	}

	// Token: 0x060053FB RID: 21499 RVA: 0x001B72ED File Offset: 0x001B54ED
	public void Stop()
	{
		if (!this.m_started)
		{
			return;
		}
		this.m_Timer.Stop();
		this.m_Socket.Close();
	}

	// Token: 0x060053FC RID: 21500 RVA: 0x001B730E File Offset: 0x001B550E
	private void OnTimerTick(object sender, ElapsedEventArgs args)
	{
		this.m_Socket.BeginSendTo(this.m_RequestBytes, 0, this.m_RequestBytes.Length, SocketFlags.None, this.m_RemoteEndPoint, new AsyncCallback(this.OnSendTo), this);
	}

	// Token: 0x060053FD RID: 21501 RVA: 0x001B7340 File Offset: 0x001B5540
	private void OnSendTo(IAsyncResult ar)
	{
		try
		{
			this.m_Socket.EndSendTo(ar);
		}
		catch (Exception ex)
		{
			Debug.LogError("error debug broadcast: " + ex.Message);
		}
	}

	// Token: 0x04004A36 RID: 18998
	private Socket m_Socket;

	// Token: 0x04004A37 RID: 18999
	private IPEndPoint m_RemoteEndPoint;

	// Token: 0x04004A38 RID: 19000
	private byte[] m_RequestBytes;

	// Token: 0x04004A39 RID: 19001
	private Timer m_Timer;

	// Token: 0x04004A3A RID: 19002
	private bool m_started;

	// Token: 0x04004A3B RID: 19003
	private static readonly TimeSpan Interval = new TimeSpan(0, 0, UnityEngine.Random.Range(7, 10));
}
