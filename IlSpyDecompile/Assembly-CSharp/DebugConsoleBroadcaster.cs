using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using UnityEngine;

public class DebugConsoleBroadcaster
{
	private Socket m_Socket;

	private IPEndPoint m_RemoteEndPoint;

	private byte[] m_RequestBytes;

	private Timer m_Timer;

	private bool m_started;

	private static readonly TimeSpan Interval = new TimeSpan(0, 0, UnityEngine.Random.Range(7, 10));

	public void Start(int destinationPort, string broadCastResponse)
	{
		m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		m_Socket.EnableBroadcast = true;
		ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
		m_RequestBytes = aSCIIEncoding.GetBytes(broadCastResponse);
		m_RemoteEndPoint = new IPEndPoint(IPAddress.Broadcast, destinationPort);
		m_Timer = new Timer(Interval.TotalMilliseconds);
		m_Timer.Elapsed += OnTimerTick;
		m_Timer.Start();
		m_started = true;
	}

	public void Stop()
	{
		if (m_started)
		{
			m_Timer.Stop();
			m_Socket.Close();
		}
	}

	private void OnTimerTick(object sender, ElapsedEventArgs args)
	{
		m_Socket.BeginSendTo(m_RequestBytes, 0, m_RequestBytes.Length, SocketFlags.None, m_RemoteEndPoint, OnSendTo, this);
	}

	private void OnSendTo(IAsyncResult ar)
	{
		try
		{
			m_Socket.EndSendTo(ar);
		}
		catch (Exception ex)
		{
			Debug.LogError("error debug broadcast: " + ex.Message);
		}
	}
}
