using System;
using System.Collections.Generic;
using System.Text;
using bgs;
using Hearthstone.Telemetry;
using HearthstoneTelemetry;

// Token: 0x0200092E RID: 2350
public class TelemetryManagerComponentNetwork : ITelemetryManagerComponent, ISocketEventListener
{
	// Token: 0x060081B8 RID: 33208 RVA: 0x002A358B File Offset: 0x002A178B
	public void Initialize()
	{
		this.m_samplers = new global::Map<string, TcpQualitySampler>();
	}

	// Token: 0x060081B9 RID: 33209 RVA: 0x002A3598 File Offset: 0x002A1798
	public void Shutdown()
	{
		foreach (KeyValuePair<string, TcpQualitySampler> keyValuePair in this.m_samplers)
		{
			keyValuePair.Value.EndConnection();
		}
	}

	// Token: 0x060081BA RID: 33210 RVA: 0x002A35F0 File Offset: 0x002A17F0
	public void ConnectEvent(string address, uint port)
	{
		string key = this.GetKey(address, port);
		if (!this.m_samplers.ContainsKey(key))
		{
			TcpQualitySampler tcpQualitySampler = new TcpQualitySampler(60f);
			this.m_samplers.Add(key, tcpQualitySampler);
			tcpQualitySampler.StartConnection(address, port);
		}
	}

	// Token: 0x060081BB RID: 33211 RVA: 0x002A3634 File Offset: 0x002A1834
	public void DisconnectEvent(string address, uint port)
	{
		string key = this.GetKey(address, port);
		TcpQualitySampler tcpQualitySampler;
		if (this.m_samplers.TryGetValue(key, out tcpQualitySampler))
		{
			tcpQualitySampler.EndConnection();
			this.m_samplers.Remove(key);
		}
	}

	// Token: 0x060081BC RID: 33212 RVA: 0x002A3670 File Offset: 0x002A1870
	public void FlushSamplers()
	{
		foreach (KeyValuePair<string, TcpQualitySampler> keyValuePair in this.m_samplers)
		{
			keyValuePair.Value.FlushSampler();
		}
	}

	// Token: 0x060081BD RID: 33213 RVA: 0x002A36C8 File Offset: 0x002A18C8
	public void SendPacketEvent(string address, uint port, uint bytes)
	{
		string key = this.GetKey(address, port);
		TcpQualitySampler tcpQualitySampler;
		if (this.m_samplers.TryGetValue(key, out tcpQualitySampler))
		{
			tcpQualitySampler.OnMessageReceived(bytes);
		}
	}

	// Token: 0x060081BE RID: 33214 RVA: 0x002A36F8 File Offset: 0x002A18F8
	public void ReceivePacketEvent(string address, uint port, uint bytes)
	{
		string key = this.GetKey(address, port);
		TcpQualitySampler tcpQualitySampler;
		if (this.m_samplers.TryGetValue(key, out tcpQualitySampler))
		{
			tcpQualitySampler.OnMessageSent(bytes);
		}
	}

	// Token: 0x060081BF RID: 33215 RVA: 0x002A3725 File Offset: 0x002A1925
	private string GetKey(string address, uint host)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(address);
		stringBuilder.Append(':');
		stringBuilder.Append(host);
		return stringBuilder.ToString();
	}

	// Token: 0x060081C0 RID: 33216 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Update()
	{
	}

	// Token: 0x04006CE2 RID: 27874
	private global::Map<string, TcpQualitySampler> m_samplers;

	// Token: 0x04006CE3 RID: 27875
	public const float SAMPLE_TIME = 60f;
}
