using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002D9 RID: 729
	public class AdvanceViewTimeNotification : IProtoBuf
	{
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x00094D0D File Offset: 0x00092F0D
		// (set) Token: 0x06002AE2 RID: 10978 RVA: 0x00094D15 File Offset: 0x00092F15
		public AccountId SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = (value != null);
			}
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x00094D28 File Offset: 0x00092F28
		public void SetSubscriberId(AccountId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x00094D31 File Offset: 0x00092F31
		// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x00094D39 File Offset: 0x00092F39
		public AccountId SenderId
		{
			get
			{
				return this._SenderId;
			}
			set
			{
				this._SenderId = value;
				this.HasSenderId = (value != null);
			}
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x00094D4C File Offset: 0x00092F4C
		public void SetSenderId(AccountId val)
		{
			this.SenderId = val;
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x00094D55 File Offset: 0x00092F55
		// (set) Token: 0x06002AE8 RID: 10984 RVA: 0x00094D5D File Offset: 0x00092F5D
		public ulong ViewTime
		{
			get
			{
				return this._ViewTime;
			}
			set
			{
				this._ViewTime = value;
				this.HasViewTime = true;
			}
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x00094D6D File Offset: 0x00092F6D
		public void SetViewTime(ulong val)
		{
			this.ViewTime = val;
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x00094D78 File Offset: 0x00092F78
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasSenderId)
			{
				num ^= this.SenderId.GetHashCode();
			}
			if (this.HasViewTime)
			{
				num ^= this.ViewTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x00094DD8 File Offset: 0x00092FD8
		public override bool Equals(object obj)
		{
			AdvanceViewTimeNotification advanceViewTimeNotification = obj as AdvanceViewTimeNotification;
			return advanceViewTimeNotification != null && this.HasSubscriberId == advanceViewTimeNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(advanceViewTimeNotification.SubscriberId)) && this.HasSenderId == advanceViewTimeNotification.HasSenderId && (!this.HasSenderId || this.SenderId.Equals(advanceViewTimeNotification.SenderId)) && this.HasViewTime == advanceViewTimeNotification.HasViewTime && (!this.HasViewTime || this.ViewTime.Equals(advanceViewTimeNotification.ViewTime));
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x00094E76 File Offset: 0x00093076
		public static AdvanceViewTimeNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AdvanceViewTimeNotification>(bs, 0, -1);
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x00094E80 File Offset: 0x00093080
		public void Deserialize(Stream stream)
		{
			AdvanceViewTimeNotification.Deserialize(stream, this);
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x00094E8A File Offset: 0x0009308A
		public static AdvanceViewTimeNotification Deserialize(Stream stream, AdvanceViewTimeNotification instance)
		{
			return AdvanceViewTimeNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x00094E98 File Offset: 0x00093098
		public static AdvanceViewTimeNotification DeserializeLengthDelimited(Stream stream)
		{
			AdvanceViewTimeNotification advanceViewTimeNotification = new AdvanceViewTimeNotification();
			AdvanceViewTimeNotification.DeserializeLengthDelimited(stream, advanceViewTimeNotification);
			return advanceViewTimeNotification;
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x00094EB4 File Offset: 0x000930B4
		public static AdvanceViewTimeNotification DeserializeLengthDelimited(Stream stream, AdvanceViewTimeNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AdvanceViewTimeNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x00094EDC File Offset: 0x000930DC
		public static AdvanceViewTimeNotification Deserialize(Stream stream, AdvanceViewTimeNotification instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.ViewTime = ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.SenderId == null)
					{
						instance.SenderId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.SenderId);
					}
				}
				else if (instance.SubscriberId == null)
				{
					instance.SubscriberId = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.SubscriberId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x00094FC4 File Offset: 0x000931C4
		public void Serialize(Stream stream)
		{
			AdvanceViewTimeNotification.Serialize(stream, this);
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x00094FD0 File Offset: 0x000931D0
		public static void Serialize(Stream stream, AdvanceViewTimeNotification instance)
		{
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasSenderId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SenderId);
			}
			if (instance.HasViewTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.ViewTime);
			}
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x00095054 File Offset: 0x00093254
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize = this.SubscriberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSenderId)
			{
				num += 1U;
				uint serializedSize2 = this.SenderId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasViewTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ViewTime);
			}
			return num;
		}

		// Token: 0x0400120E RID: 4622
		public bool HasSubscriberId;

		// Token: 0x0400120F RID: 4623
		private AccountId _SubscriberId;

		// Token: 0x04001210 RID: 4624
		public bool HasSenderId;

		// Token: 0x04001211 RID: 4625
		private AccountId _SenderId;

		// Token: 0x04001212 RID: 4626
		public bool HasViewTime;

		// Token: 0x04001213 RID: 4627
		private ulong _ViewTime;
	}
}
