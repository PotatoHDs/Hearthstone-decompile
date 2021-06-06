using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	// Token: 0x02001175 RID: 4469
	public class CrmEvent : IProtoBuf
	{
		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x0600C41D RID: 50205 RVA: 0x003B2CAA File Offset: 0x003B0EAA
		// (set) Token: 0x0600C41E RID: 50206 RVA: 0x003B2CB2 File Offset: 0x003B0EB2
		public string EventName
		{
			get
			{
				return this._EventName;
			}
			set
			{
				this._EventName = value;
				this.HasEventName = (value != null);
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x0600C41F RID: 50207 RVA: 0x003B2CC5 File Offset: 0x003B0EC5
		// (set) Token: 0x0600C420 RID: 50208 RVA: 0x003B2CCD File Offset: 0x003B0ECD
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

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x0600C421 RID: 50209 RVA: 0x003B2CE0 File Offset: 0x003B0EE0
		// (set) Token: 0x0600C422 RID: 50210 RVA: 0x003B2CE8 File Offset: 0x003B0EE8
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

		// Token: 0x0600C423 RID: 50211 RVA: 0x003B2CFC File Offset: 0x003B0EFC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEventName)
			{
				num ^= this.EventName.GetHashCode();
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

		// Token: 0x0600C424 RID: 50212 RVA: 0x003B2D58 File Offset: 0x003B0F58
		public override bool Equals(object obj)
		{
			CrmEvent crmEvent = obj as CrmEvent;
			return crmEvent != null && this.HasEventName == crmEvent.HasEventName && (!this.HasEventName || this.EventName.Equals(crmEvent.EventName)) && this.HasEventPayload == crmEvent.HasEventPayload && (!this.HasEventPayload || this.EventPayload.Equals(crmEvent.EventPayload)) && this.HasApplicationId == crmEvent.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(crmEvent.ApplicationId));
		}

		// Token: 0x0600C425 RID: 50213 RVA: 0x003B2DF3 File Offset: 0x003B0FF3
		public void Deserialize(Stream stream)
		{
			CrmEvent.Deserialize(stream, this);
		}

		// Token: 0x0600C426 RID: 50214 RVA: 0x003B2DFD File Offset: 0x003B0FFD
		public static CrmEvent Deserialize(Stream stream, CrmEvent instance)
		{
			return CrmEvent.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C427 RID: 50215 RVA: 0x003B2E08 File Offset: 0x003B1008
		public static CrmEvent DeserializeLengthDelimited(Stream stream)
		{
			CrmEvent crmEvent = new CrmEvent();
			CrmEvent.DeserializeLengthDelimited(stream, crmEvent);
			return crmEvent;
		}

		// Token: 0x0600C428 RID: 50216 RVA: 0x003B2E24 File Offset: 0x003B1024
		public static CrmEvent DeserializeLengthDelimited(Stream stream, CrmEvent instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CrmEvent.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C429 RID: 50217 RVA: 0x003B2E4C File Offset: 0x003B104C
		public static CrmEvent Deserialize(Stream stream, CrmEvent instance, long limit)
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
					instance.EventName = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C42A RID: 50218 RVA: 0x003B2F1B File Offset: 0x003B111B
		public void Serialize(Stream stream)
		{
			CrmEvent.Serialize(stream, this);
		}

		// Token: 0x0600C42B RID: 50219 RVA: 0x003B2F24 File Offset: 0x003B1124
		public static void Serialize(Stream stream, CrmEvent instance)
		{
			if (instance.HasEventName)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EventName));
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

		// Token: 0x0600C42C RID: 50220 RVA: 0x003B2FB8 File Offset: 0x003B11B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEventName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.EventName);
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

		// Token: 0x04009D11 RID: 40209
		public bool HasEventName;

		// Token: 0x04009D12 RID: 40210
		private string _EventName;

		// Token: 0x04009D13 RID: 40211
		public bool HasEventPayload;

		// Token: 0x04009D14 RID: 40212
		private string _EventPayload;

		// Token: 0x04009D15 RID: 40213
		public bool HasApplicationId;

		// Token: 0x04009D16 RID: 40214
		private string _ApplicationId;
	}
}
