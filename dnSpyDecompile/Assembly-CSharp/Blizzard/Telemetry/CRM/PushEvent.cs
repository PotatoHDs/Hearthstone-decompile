using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	// Token: 0x02001176 RID: 4470
	public class PushEvent : IProtoBuf
	{
		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x0600C42E RID: 50222 RVA: 0x003B3040 File Offset: 0x003B1240
		// (set) Token: 0x0600C42F RID: 50223 RVA: 0x003B3048 File Offset: 0x003B1248
		public string CampaignId
		{
			get
			{
				return this._CampaignId;
			}
			set
			{
				this._CampaignId = value;
				this.HasCampaignId = (value != null);
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x0600C430 RID: 50224 RVA: 0x003B305B File Offset: 0x003B125B
		// (set) Token: 0x0600C431 RID: 50225 RVA: 0x003B3063 File Offset: 0x003B1263
		public string EventPayload
		{
			get
			{
				return this._EventPayload;
			}
			set
			{
				this._EventPayload = value;
				this.HasEventPayload = (value != null);
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x0600C432 RID: 50226 RVA: 0x003B3076 File Offset: 0x003B1276
		// (set) Token: 0x0600C433 RID: 50227 RVA: 0x003B307E File Offset: 0x003B127E
		public string ApplicationId
		{
			get
			{
				return this._ApplicationId;
			}
			set
			{
				this._ApplicationId = value;
				this.HasApplicationId = (value != null);
			}
		}

		// Token: 0x0600C434 RID: 50228 RVA: 0x003B3094 File Offset: 0x003B1294
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCampaignId)
			{
				num ^= this.CampaignId.GetHashCode();
			}
			if (this.HasEventPayload)
			{
				num ^= this.EventPayload.GetHashCode();
			}
			if (this.HasApplicationId)
			{
				num ^= this.ApplicationId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C435 RID: 50229 RVA: 0x003B30F0 File Offset: 0x003B12F0
		public override bool Equals(object obj)
		{
			PushEvent pushEvent = obj as PushEvent;
			return pushEvent != null && this.HasCampaignId == pushEvent.HasCampaignId && (!this.HasCampaignId || this.CampaignId.Equals(pushEvent.CampaignId)) && this.HasEventPayload == pushEvent.HasEventPayload && (!this.HasEventPayload || this.EventPayload.Equals(pushEvent.EventPayload)) && this.HasApplicationId == pushEvent.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(pushEvent.ApplicationId));
		}

		// Token: 0x0600C436 RID: 50230 RVA: 0x003B318B File Offset: 0x003B138B
		public void Deserialize(Stream stream)
		{
			PushEvent.Deserialize(stream, this);
		}

		// Token: 0x0600C437 RID: 50231 RVA: 0x003B3195 File Offset: 0x003B1395
		public static PushEvent Deserialize(Stream stream, PushEvent instance)
		{
			return PushEvent.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C438 RID: 50232 RVA: 0x003B31A0 File Offset: 0x003B13A0
		public static PushEvent DeserializeLengthDelimited(Stream stream)
		{
			PushEvent pushEvent = new PushEvent();
			PushEvent.DeserializeLengthDelimited(stream, pushEvent);
			return pushEvent;
		}

		// Token: 0x0600C439 RID: 50233 RVA: 0x003B31BC File Offset: 0x003B13BC
		public static PushEvent DeserializeLengthDelimited(Stream stream, PushEvent instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PushEvent.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C43A RID: 50234 RVA: 0x003B31E4 File Offset: 0x003B13E4
		public static PushEvent Deserialize(Stream stream, PushEvent instance, long limit)
		{
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num == 82)
				{
					instance.CampaignId = ProtocolParser.ReadString(stream);
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 20U)
					{
						if (field != 30U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.LengthDelimited)
						{
							instance.ApplicationId = ProtocolParser.ReadString(stream);
						}
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						instance.EventPayload = ProtocolParser.ReadString(stream);
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C43B RID: 50235 RVA: 0x003B32B3 File Offset: 0x003B14B3
		public void Serialize(Stream stream)
		{
			PushEvent.Serialize(stream, this);
		}

		// Token: 0x0600C43C RID: 50236 RVA: 0x003B32BC File Offset: 0x003B14BC
		public static void Serialize(Stream stream, PushEvent instance)
		{
			if (instance.HasCampaignId)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CampaignId));
			}
			if (instance.HasEventPayload)
			{
				stream.WriteByte(162);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EventPayload));
			}
			if (instance.HasApplicationId)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
		}

		// Token: 0x0600C43D RID: 50237 RVA: 0x003B3350 File Offset: 0x003B1550
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCampaignId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.CampaignId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasEventPayload)
			{
				num += 2U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.EventPayload);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasApplicationId)
			{
				num += 2U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x04009D17 RID: 40215
		public bool HasCampaignId;

		// Token: 0x04009D18 RID: 40216
		private string _CampaignId;

		// Token: 0x04009D19 RID: 40217
		public bool HasEventPayload;

		// Token: 0x04009D1A RID: 40218
		private string _EventPayload;

		// Token: 0x04009D1B RID: 40219
		public bool HasApplicationId;

		// Token: 0x04009D1C RID: 40220
		private string _ApplicationId;
	}
}
