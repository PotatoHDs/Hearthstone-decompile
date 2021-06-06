using Hearthstone.Core;
using UnityEngine;

namespace HearthstoneTelemetry
{
	public class TcpQualitySampler
	{
		private float sampleTime;

		private float elapsedTimeSinceSampled;

		private string ipv4 = "";

		private uint port;

		private uint bytesReceived;

		private uint bytesSent;

		private uint messagesReceived;

		private uint messagesSent;

		public TcpQualitySampler(float sampleTime)
		{
			this.sampleTime = sampleTime;
		}

		public void StartConnection(string ipv4, uint port)
		{
			this.ipv4 = ipv4;
			this.port = port;
			bytesSent = 0u;
			bytesReceived = 0u;
			messagesSent = 0u;
			messagesReceived = 0u;
			Processor.RegisterUpdateDelegate(TrySampleNetworkQuality);
			Log.Telemetry.Print("Registered network quality sampler for " + ipv4 + ":" + port);
		}

		public void EndConnection()
		{
			FlushSampler();
			Processor.UnregisterUpdateDelegate(TrySampleNetworkQuality);
			Log.Telemetry.Print("Successfully unregistered network quality sampler for " + ipv4 + ":" + port);
		}

		public void FlushSampler()
		{
			SendNetworkQualityMessage();
			elapsedTimeSinceSampled = 0f;
		}

		private void TrySampleNetworkQuality()
		{
			elapsedTimeSinceSampled += Time.unscaledDeltaTime;
			if (elapsedTimeSinceSampled > sampleTime)
			{
				FlushSampler();
			}
		}

		private void SendNetworkQualityMessage()
		{
			TelemetryManager.Client().SendTcpQualitySample(ipv4, port, elapsedTimeSinceSampled * 1000f, bytesSent, bytesReceived, messagesSent, messagesReceived);
			bytesSent = 0u;
			bytesReceived = 0u;
			messagesSent = 0u;
			messagesReceived = 0u;
			Log.Telemetry.Print("Sent network quality message for " + ipv4 + ":" + port);
		}

		public void OnMessageSent(uint bytes)
		{
			messagesSent++;
			bytesSent += bytes;
		}

		public void OnMessageReceived(uint bytes)
		{
			messagesReceived++;
			bytesReceived += bytes;
		}
	}
}
