using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002DB RID: 731
	public class AdvanceClearTimeNotification : IProtoBuf
	{
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x000953C2 File Offset: 0x000935C2
		// (set) Token: 0x06002B0B RID: 11019 RVA: 0x000953CA File Offset: 0x000935CA
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

		// Token: 0x06002B0C RID: 11020 RVA: 0x000953DD File Offset: 0x000935DD
		public void SetSubscriberId(AccountId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002B0D RID: 11021 RVA: 0x000953E6 File Offset: 0x000935E6
		// (set) Token: 0x06002B0E RID: 11022 RVA: 0x000953EE File Offset: 0x000935EE
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

		// Token: 0x06002B0F RID: 11023 RVA: 0x00095401 File Offset: 0x00093601
		public void SetSenderId(AccountId val)
		{
			this.SenderId = val;
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x0009540A File Offset: 0x0009360A
		// (set) Token: 0x06002B11 RID: 11025 RVA: 0x00095412 File Offset: 0x00093612
		public ulong ClearTime
		{
			get
			{
				return this._ClearTime;
			}
			set
			{
				this._ClearTime = value;
				this.HasClearTime = true;
			}
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x00095422 File Offset: 0x00093622
		public void SetClearTime(ulong val)
		{
			this.ClearTime = val;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0009542C File Offset: 0x0009362C
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
			if (this.HasClearTime)
			{
				num ^= this.ClearTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x0009548C File Offset: 0x0009368C
		public override bool Equals(object obj)
		{
			AdvanceClearTimeNotification advanceClearTimeNotification = obj as AdvanceClearTimeNotification;
			return advanceClearTimeNotification != null && this.HasSubscriberId == advanceClearTimeNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(advanceClearTimeNotification.SubscriberId)) && this.HasSenderId == advanceClearTimeNotification.HasSenderId && (!this.HasSenderId || this.SenderId.Equals(advanceClearTimeNotification.SenderId)) && this.HasClearTime == advanceClearTimeNotification.HasClearTime && (!this.HasClearTime || this.ClearTime.Equals(advanceClearTimeNotification.ClearTime));
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x0009552A File Offset: 0x0009372A
		public static AdvanceClearTimeNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AdvanceClearTimeNotification>(bs, 0, -1);
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x00095534 File Offset: 0x00093734
		public void Deserialize(Stream stream)
		{
			AdvanceClearTimeNotification.Deserialize(stream, this);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x0009553E File Offset: 0x0009373E
		public static AdvanceClearTimeNotification Deserialize(Stream stream, AdvanceClearTimeNotification instance)
		{
			return AdvanceClearTimeNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x0009554C File Offset: 0x0009374C
		public static AdvanceClearTimeNotification DeserializeLengthDelimited(Stream stream)
		{
			AdvanceClearTimeNotification advanceClearTimeNotification = new AdvanceClearTimeNotification();
			AdvanceClearTimeNotification.DeserializeLengthDelimited(stream, advanceClearTimeNotification);
			return advanceClearTimeNotification;
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x00095568 File Offset: 0x00093768
		public static AdvanceClearTimeNotification DeserializeLengthDelimited(Stream stream, AdvanceClearTimeNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AdvanceClearTimeNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x00095590 File Offset: 0x00093790
		public static AdvanceClearTimeNotification Deserialize(Stream stream, AdvanceClearTimeNotification instance, long limit)
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
							instance.ClearTime = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06002B1C RID: 11036 RVA: 0x00095678 File Offset: 0x00093878
		public void Serialize(Stream stream)
		{
			AdvanceClearTimeNotification.Serialize(stream, this);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x00095684 File Offset: 0x00093884
		public static void Serialize(Stream stream, AdvanceClearTimeNotification instance)
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
			if (instance.HasClearTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.ClearTime);
			}
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x00095708 File Offset: 0x00093908
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
			if (this.HasClearTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ClearTime);
			}
			return num;
		}

		// Token: 0x04001218 RID: 4632
		public bool HasSubscriberId;

		// Token: 0x04001219 RID: 4633
		private AccountId _SubscriberId;

		// Token: 0x0400121A RID: 4634
		public bool HasSenderId;

		// Token: 0x0400121B RID: 4635
		private AccountId _SenderId;

		// Token: 0x0400121C RID: 4636
		public bool HasClearTime;

		// Token: 0x0400121D RID: 4637
		private ulong _ClearTime;
	}
}
