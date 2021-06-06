using System;
using Hearthstone.Core;
using UnityEngine;

namespace HearthstoneTelemetry
{
	// Token: 0x02000B4B RID: 2891
	public class TcpQualitySampler
	{
		// Token: 0x06009A51 RID: 39505 RVA: 0x0031AC5B File Offset: 0x00318E5B
		public TcpQualitySampler(float sampleTime)
		{
			this.sampleTime = sampleTime;
		}

		// Token: 0x06009A52 RID: 39506 RVA: 0x0031AC78 File Offset: 0x00318E78
		public void StartConnection(string ipv4, uint port)
		{
			this.ipv4 = ipv4;
			this.port = port;
			this.bytesSent = 0U;
			this.bytesReceived = 0U;
			this.messagesSent = 0U;
			this.messagesReceived = 0U;
			Processor.RegisterUpdateDelegate(new Action(this.TrySampleNetworkQuality));
			Log.Telemetry.Print(string.Concat(new object[]
			{
				"Registered network quality sampler for ",
				ipv4,
				":",
				port
			}), Array.Empty<object>());
		}

		// Token: 0x06009A53 RID: 39507 RVA: 0x0031ACF8 File Offset: 0x00318EF8
		public void EndConnection()
		{
			this.FlushSampler();
			Processor.UnregisterUpdateDelegate(new Action(this.TrySampleNetworkQuality));
			Log.Telemetry.Print(string.Concat(new object[]
			{
				"Successfully unregistered network quality sampler for ",
				this.ipv4,
				":",
				this.port
			}), Array.Empty<object>());
		}

		// Token: 0x06009A54 RID: 39508 RVA: 0x0031AD5D File Offset: 0x00318F5D
		public void FlushSampler()
		{
			this.SendNetworkQualityMessage();
			this.elapsedTimeSinceSampled = 0f;
		}

		// Token: 0x06009A55 RID: 39509 RVA: 0x0031AD70 File Offset: 0x00318F70
		private void TrySampleNetworkQuality()
		{
			this.elapsedTimeSinceSampled += Time.unscaledDeltaTime;
			if (this.elapsedTimeSinceSampled > this.sampleTime)
			{
				this.FlushSampler();
			}
		}

		// Token: 0x06009A56 RID: 39510 RVA: 0x0031AD98 File Offset: 0x00318F98
		private void SendNetworkQualityMessage()
		{
			TelemetryManager.Client().SendTcpQualitySample(this.ipv4, this.port, this.elapsedTimeSinceSampled * 1000f, this.bytesSent, this.bytesReceived, this.messagesSent, this.messagesReceived);
			this.bytesSent = 0U;
			this.bytesReceived = 0U;
			this.messagesSent = 0U;
			this.messagesReceived = 0U;
			Log.Telemetry.Print(string.Concat(new object[]
			{
				"Sent network quality message for ",
				this.ipv4,
				":",
				this.port
			}), Array.Empty<object>());
		}

		// Token: 0x06009A57 RID: 39511 RVA: 0x0031AE3C File Offset: 0x0031903C
		public void OnMessageSent(uint bytes)
		{
			this.messagesSent += 1U;
			this.bytesSent += bytes;
		}

		// Token: 0x06009A58 RID: 39512 RVA: 0x0031AE5A File Offset: 0x0031905A
		public void OnMessageReceived(uint bytes)
		{
			this.messagesReceived += 1U;
			this.bytesReceived += bytes;
		}

		// Token: 0x04008005 RID: 32773
		private float sampleTime;

		// Token: 0x04008006 RID: 32774
		private float elapsedTimeSinceSampled;

		// Token: 0x04008007 RID: 32775
		private string ipv4 = "";

		// Token: 0x04008008 RID: 32776
		private uint port;

		// Token: 0x04008009 RID: 32777
		private uint bytesReceived;

		// Token: 0x0400800A RID: 32778
		private uint bytesSent;

		// Token: 0x0400800B RID: 32779
		private uint messagesReceived;

		// Token: 0x0400800C RID: 32780
		private uint messagesSent;
	}
}
