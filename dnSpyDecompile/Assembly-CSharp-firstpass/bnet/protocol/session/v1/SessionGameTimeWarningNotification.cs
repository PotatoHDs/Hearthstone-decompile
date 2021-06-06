using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000318 RID: 792
	public class SessionGameTimeWarningNotification : IProtoBuf
	{
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x000A2A95 File Offset: 0x000A0C95
		// (set) Token: 0x0600302F RID: 12335 RVA: 0x000A2A9D File Offset: 0x000A0C9D
		public Identity Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000A2AB0 File Offset: 0x000A0CB0
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x000A2AB9 File Offset: 0x000A0CB9
		// (set) Token: 0x06003032 RID: 12338 RVA: 0x000A2AC1 File Offset: 0x000A0CC1
		public string SessionId
		{
			get
			{
				return this._SessionId;
			}
			set
			{
				this._SessionId = value;
				this.HasSessionId = (value != null);
			}
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000A2AD4 File Offset: 0x000A0CD4
		public void SetSessionId(string val)
		{
			this.SessionId = val;
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x000A2ADD File Offset: 0x000A0CDD
		// (set) Token: 0x06003035 RID: 12341 RVA: 0x000A2AE5 File Offset: 0x000A0CE5
		public uint RemainingTimeDurationMin
		{
			get
			{
				return this._RemainingTimeDurationMin;
			}
			set
			{
				this._RemainingTimeDurationMin = value;
				this.HasRemainingTimeDurationMin = true;
			}
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000A2AF5 File Offset: 0x000A0CF5
		public void SetRemainingTimeDurationMin(uint val)
		{
			this.RemainingTimeDurationMin = val;
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x000A2AFE File Offset: 0x000A0CFE
		// (set) Token: 0x06003038 RID: 12344 RVA: 0x000A2B06 File Offset: 0x000A0D06
		public uint RestrictionType
		{
			get
			{
				return this._RestrictionType;
			}
			set
			{
				this._RestrictionType = value;
				this.HasRestrictionType = true;
			}
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000A2B16 File Offset: 0x000A0D16
		public void SetRestrictionType(uint val)
		{
			this.RestrictionType = val;
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000A2B20 File Offset: 0x000A0D20
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			if (this.HasSessionId)
			{
				num ^= this.SessionId.GetHashCode();
			}
			if (this.HasRemainingTimeDurationMin)
			{
				num ^= this.RemainingTimeDurationMin.GetHashCode();
			}
			if (this.HasRestrictionType)
			{
				num ^= this.RestrictionType.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000A2B98 File Offset: 0x000A0D98
		public override bool Equals(object obj)
		{
			SessionGameTimeWarningNotification sessionGameTimeWarningNotification = obj as SessionGameTimeWarningNotification;
			return sessionGameTimeWarningNotification != null && this.HasIdentity == sessionGameTimeWarningNotification.HasIdentity && (!this.HasIdentity || this.Identity.Equals(sessionGameTimeWarningNotification.Identity)) && this.HasSessionId == sessionGameTimeWarningNotification.HasSessionId && (!this.HasSessionId || this.SessionId.Equals(sessionGameTimeWarningNotification.SessionId)) && this.HasRemainingTimeDurationMin == sessionGameTimeWarningNotification.HasRemainingTimeDurationMin && (!this.HasRemainingTimeDurationMin || this.RemainingTimeDurationMin.Equals(sessionGameTimeWarningNotification.RemainingTimeDurationMin)) && this.HasRestrictionType == sessionGameTimeWarningNotification.HasRestrictionType && (!this.HasRestrictionType || this.RestrictionType.Equals(sessionGameTimeWarningNotification.RestrictionType));
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000A2C64 File Offset: 0x000A0E64
		public static SessionGameTimeWarningNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionGameTimeWarningNotification>(bs, 0, -1);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000A2C6E File Offset: 0x000A0E6E
		public void Deserialize(Stream stream)
		{
			SessionGameTimeWarningNotification.Deserialize(stream, this);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000A2C78 File Offset: 0x000A0E78
		public static SessionGameTimeWarningNotification Deserialize(Stream stream, SessionGameTimeWarningNotification instance)
		{
			return SessionGameTimeWarningNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000A2C84 File Offset: 0x000A0E84
		public static SessionGameTimeWarningNotification DeserializeLengthDelimited(Stream stream)
		{
			SessionGameTimeWarningNotification sessionGameTimeWarningNotification = new SessionGameTimeWarningNotification();
			SessionGameTimeWarningNotification.DeserializeLengthDelimited(stream, sessionGameTimeWarningNotification);
			return sessionGameTimeWarningNotification;
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000A2CA0 File Offset: 0x000A0EA0
		public static SessionGameTimeWarningNotification DeserializeLengthDelimited(Stream stream, SessionGameTimeWarningNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SessionGameTimeWarningNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000A2CC8 File Offset: 0x000A0EC8
		public static SessionGameTimeWarningNotification Deserialize(Stream stream, SessionGameTimeWarningNotification instance, long limit)
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.SessionId = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.Identity == null)
							{
								instance.Identity = Identity.DeserializeLengthDelimited(stream);
								continue;
							}
							Identity.DeserializeLengthDelimited(stream, instance.Identity);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.RemainingTimeDurationMin = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 32)
						{
							instance.RestrictionType = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000A2DB3 File Offset: 0x000A0FB3
		public void Serialize(Stream stream)
		{
			SessionGameTimeWarningNotification.Serialize(stream, this);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000A2DBC File Offset: 0x000A0FBC
		public static void Serialize(Stream stream, SessionGameTimeWarningNotification instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasSessionId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
			if (instance.HasRemainingTimeDurationMin)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.RemainingTimeDurationMin);
			}
			if (instance.HasRestrictionType)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.RestrictionType);
			}
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000A2E54 File Offset: 0x000A1054
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIdentity)
			{
				num += 1U;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSessionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasRemainingTimeDurationMin)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RemainingTimeDurationMin);
			}
			if (this.HasRestrictionType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RestrictionType);
			}
			return num;
		}

		// Token: 0x04001333 RID: 4915
		public bool HasIdentity;

		// Token: 0x04001334 RID: 4916
		private Identity _Identity;

		// Token: 0x04001335 RID: 4917
		public bool HasSessionId;

		// Token: 0x04001336 RID: 4918
		private string _SessionId;

		// Token: 0x04001337 RID: 4919
		public bool HasRemainingTimeDurationMin;

		// Token: 0x04001338 RID: 4920
		private uint _RemainingTimeDurationMin;

		// Token: 0x04001339 RID: 4921
		public bool HasRestrictionType;

		// Token: 0x0400133A RID: 4922
		private uint _RestrictionType;
	}
}
