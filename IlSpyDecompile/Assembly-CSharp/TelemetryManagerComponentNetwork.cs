using System.Collections.Generic;
using System.Text;
using bgs;
using Hearthstone.Telemetry;
using HearthstoneTelemetry;

public class TelemetryManagerComponentNetwork : ITelemetryManagerComponent, ISocketEventListener
{
	private Map<string, TcpQualitySampler> m_samplers;

	public const float SAMPLE_TIME = 60f;

	public void Initialize()
	{
		m_samplers = new Map<string, TcpQualitySampler>();
	}

	public void Shutdown()
	{
		foreach (KeyValuePair<string, TcpQualitySampler> sampler in m_samplers)
		{
			sampler.Value.EndConnection();
		}
	}

	public void ConnectEvent(string address, uint port)
	{
		string key = GetKey(address, port);
		if (!m_samplers.ContainsKey(key))
		{
			TcpQualitySampler tcpQualitySampler = new TcpQualitySampler(60f);
			m_samplers.Add(key, tcpQualitySampler);
			tcpQualitySampler.StartConnection(address, port);
		}
	}

	public void DisconnectEvent(string address, uint port)
	{
		string key = GetKey(address, port);
		if (m_samplers.TryGetValue(key, out var value))
		{
			value.EndConnection();
			m_samplers.Remove(key);
		}
	}

	public void FlushSamplers()
	{
		foreach (KeyValuePair<string, TcpQualitySampler> sampler in m_samplers)
		{
			sampler.Value.FlushSampler();
		}
	}

	public void SendPacketEvent(string address, uint port, uint bytes)
	{
		string key = GetKey(address, port);
		if (m_samplers.TryGetValue(key, out var value))
		{
			value.OnMessageReceived(bytes);
		}
	}

	public void ReceivePacketEvent(string address, uint port, uint bytes)
	{
		string key = GetKey(address, port);
		if (m_samplers.TryGetValue(key, out var value))
		{
			value.OnMessageSent(bytes);
		}
	}

	private string GetKey(string address, uint host)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(address);
		stringBuilder.Append(':');
		stringBuilder.Append(host);
		return stringBuilder.ToString();
	}

	public void Update()
	{
	}
}
