using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	// Token: 0x0200117A RID: 4474
	public class SessionStart : IProtoBuf
	{
		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x0600C488 RID: 50312 RVA: 0x003B4854 File Offset: 0x003B2A54
		// (set) Token: 0x0600C489 RID: 50313 RVA: 0x003B485C File Offset: 0x003B2A5C
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

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x0600C48A RID: 50314 RVA: 0x003B486F File Offset: 0x003B2A6F
		// (set) Token: 0x0600C48B RID: 50315 RVA: 0x003B4877 File Offset: 0x003B2A77
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

		// Token: 0x0600C48C RID: 50316 RVA: 0x003B488C File Offset: 0x003B2A8C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
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

		// Token: 0x0600C48D RID: 50317 RVA: 0x003B48D4 File Offset: 0x003B2AD4
		public override bool Equals(object obj)
		{
			SessionStart sessionStart = obj as SessionStart;
			return sessionStart != null && this.HasEventPayload == sessionStart.HasEventPayload && (!this.HasEventPayload || this.EventPayload.Equals(sessionStart.EventPayload)) && this.HasApplicationId == sessionStart.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(sessionStart.ApplicationId));
		}

		// Token: 0x0600C48E RID: 50318 RVA: 0x003B4944 File Offset: 0x003B2B44
		public void Deserialize(Stream stream)
		{
			SessionStart.Deserialize(stream, this);
		}

		// Token: 0x0600C48F RID: 50319 RVA: 0x003B494E File Offset: 0x003B2B4E
		public static SessionStart Deserialize(Stream stream, SessionStart instance)
		{
			return SessionStart.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C490 RID: 50320 RVA: 0x003B495C File Offset: 0x003B2B5C
		public static SessionStart DeserializeLengthDelimited(Stream stream)
		{
			SessionStart sessionStart = new SessionStart();
			SessionStart.DeserializeLengthDelimited(stream, sessionStart);
			return sessionStart;
		}

		// Token: 0x0600C491 RID: 50321 RVA: 0x003B4978 File Offset: 0x003B2B78
		public static SessionStart DeserializeLengthDelimited(Stream stream, SessionStart instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SessionStart.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C492 RID: 50322 RVA: 0x003B49A0 File Offset: 0x003B2BA0
		public static SessionStart Deserialize(Stream stream, SessionStart instance, long limit)
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

		// Token: 0x0600C493 RID: 50323 RVA: 0x003B4A53 File Offset: 0x003B2C53
		public void Serialize(Stream stream)
		{
			SessionStart.Serialize(stream, this);
		}

		// Token: 0x0600C494 RID: 50324 RVA: 0x003B4A5C File Offset: 0x003B2C5C
		public static void Serialize(Stream stream, SessionStart instance)
		{
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

		// Token: 0x0600C495 RID: 50325 RVA: 0x003B4ACC File Offset: 0x003B2CCC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEventPayload)
			{
				num += 2U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.EventPayload);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasApplicationId)
			{
				num += 2U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04009D45 RID: 40261
		public bool HasEventPayload;

		// Token: 0x04009D46 RID: 40262
		private string _EventPayload;

		// Token: 0x04009D47 RID: 40263
		public bool HasApplicationId;

		// Token: 0x04009D48 RID: 40264
		private string _ApplicationId;
	}
}
