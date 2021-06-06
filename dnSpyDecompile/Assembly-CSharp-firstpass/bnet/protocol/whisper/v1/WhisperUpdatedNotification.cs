using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002DA RID: 730
	public class WhisperUpdatedNotification : IProtoBuf
	{
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x000950C4 File Offset: 0x000932C4
		// (set) Token: 0x06002AF8 RID: 11000 RVA: 0x000950CC File Offset: 0x000932CC
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

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000950DF File Offset: 0x000932DF
		public void SetSubscriberId(AccountId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000950E8 File Offset: 0x000932E8
		// (set) Token: 0x06002AFB RID: 11003 RVA: 0x000950F0 File Offset: 0x000932F0
		public Whisper Whisper
		{
			get
			{
				return this._Whisper;
			}
			set
			{
				this._Whisper = value;
				this.HasWhisper = (value != null);
			}
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x00095103 File Offset: 0x00093303
		public void SetWhisper(Whisper val)
		{
			this.Whisper = val;
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x0009510C File Offset: 0x0009330C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasWhisper)
			{
				num ^= this.Whisper.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x00095154 File Offset: 0x00093354
		public override bool Equals(object obj)
		{
			WhisperUpdatedNotification whisperUpdatedNotification = obj as WhisperUpdatedNotification;
			return whisperUpdatedNotification != null && this.HasSubscriberId == whisperUpdatedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(whisperUpdatedNotification.SubscriberId)) && this.HasWhisper == whisperUpdatedNotification.HasWhisper && (!this.HasWhisper || this.Whisper.Equals(whisperUpdatedNotification.Whisper));
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000951C4 File Offset: 0x000933C4
		public static WhisperUpdatedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WhisperUpdatedNotification>(bs, 0, -1);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000951CE File Offset: 0x000933CE
		public void Deserialize(Stream stream)
		{
			WhisperUpdatedNotification.Deserialize(stream, this);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000951D8 File Offset: 0x000933D8
		public static WhisperUpdatedNotification Deserialize(Stream stream, WhisperUpdatedNotification instance)
		{
			return WhisperUpdatedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000951E4 File Offset: 0x000933E4
		public static WhisperUpdatedNotification DeserializeLengthDelimited(Stream stream)
		{
			WhisperUpdatedNotification whisperUpdatedNotification = new WhisperUpdatedNotification();
			WhisperUpdatedNotification.DeserializeLengthDelimited(stream, whisperUpdatedNotification);
			return whisperUpdatedNotification;
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x00095200 File Offset: 0x00093400
		public static WhisperUpdatedNotification DeserializeLengthDelimited(Stream stream, WhisperUpdatedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return WhisperUpdatedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x00095228 File Offset: 0x00093428
		public static WhisperUpdatedNotification Deserialize(Stream stream, WhisperUpdatedNotification instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Whisper == null)
					{
						instance.Whisper = Whisper.DeserializeLengthDelimited(stream);
					}
					else
					{
						Whisper.DeserializeLengthDelimited(stream, instance.Whisper);
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

		// Token: 0x06002B06 RID: 11014 RVA: 0x000952FA File Offset: 0x000934FA
		public void Serialize(Stream stream)
		{
			WhisperUpdatedNotification.Serialize(stream, this);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x00095304 File Offset: 0x00093504
		public static void Serialize(Stream stream, WhisperUpdatedNotification instance)
		{
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasWhisper)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Whisper.GetSerializedSize());
				Whisper.Serialize(stream, instance.Whisper);
			}
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0009536C File Offset: 0x0009356C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize = this.SubscriberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasWhisper)
			{
				num += 1U;
				uint serializedSize2 = this.Whisper.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001214 RID: 4628
		public bool HasSubscriberId;

		// Token: 0x04001215 RID: 4629
		private AccountId _SubscriberId;

		// Token: 0x04001216 RID: 4630
		public bool HasWhisper;

		// Token: 0x04001217 RID: 4631
		private Whisper _Whisper;
	}
}
