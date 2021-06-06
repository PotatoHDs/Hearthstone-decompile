using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002D7 RID: 727
	public class WhisperEchoNotification : IProtoBuf
	{
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x0009463E File Offset: 0x0009283E
		// (set) Token: 0x06002AB9 RID: 10937 RVA: 0x00094646 File Offset: 0x00092846
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

		// Token: 0x06002ABA RID: 10938 RVA: 0x00094659 File Offset: 0x00092859
		public void SetSubscriberId(AccountId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x00094662 File Offset: 0x00092862
		// (set) Token: 0x06002ABC RID: 10940 RVA: 0x0009466A File Offset: 0x0009286A
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

		// Token: 0x06002ABD RID: 10941 RVA: 0x0009467D File Offset: 0x0009287D
		public void SetWhisper(Whisper val)
		{
			this.Whisper = val;
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x00094688 File Offset: 0x00092888
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

		// Token: 0x06002ABF RID: 10943 RVA: 0x000946D0 File Offset: 0x000928D0
		public override bool Equals(object obj)
		{
			WhisperEchoNotification whisperEchoNotification = obj as WhisperEchoNotification;
			return whisperEchoNotification != null && this.HasSubscriberId == whisperEchoNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(whisperEchoNotification.SubscriberId)) && this.HasWhisper == whisperEchoNotification.HasWhisper && (!this.HasWhisper || this.Whisper.Equals(whisperEchoNotification.Whisper));
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x00094740 File Offset: 0x00092940
		public static WhisperEchoNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WhisperEchoNotification>(bs, 0, -1);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x0009474A File Offset: 0x0009294A
		public void Deserialize(Stream stream)
		{
			WhisperEchoNotification.Deserialize(stream, this);
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x00094754 File Offset: 0x00092954
		public static WhisperEchoNotification Deserialize(Stream stream, WhisperEchoNotification instance)
		{
			return WhisperEchoNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x00094760 File Offset: 0x00092960
		public static WhisperEchoNotification DeserializeLengthDelimited(Stream stream)
		{
			WhisperEchoNotification whisperEchoNotification = new WhisperEchoNotification();
			WhisperEchoNotification.DeserializeLengthDelimited(stream, whisperEchoNotification);
			return whisperEchoNotification;
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x0009477C File Offset: 0x0009297C
		public static WhisperEchoNotification DeserializeLengthDelimited(Stream stream, WhisperEchoNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return WhisperEchoNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000947A4 File Offset: 0x000929A4
		public static WhisperEchoNotification Deserialize(Stream stream, WhisperEchoNotification instance, long limit)
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

		// Token: 0x06002AC7 RID: 10951 RVA: 0x00094876 File Offset: 0x00092A76
		public void Serialize(Stream stream)
		{
			WhisperEchoNotification.Serialize(stream, this);
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x00094880 File Offset: 0x00092A80
		public static void Serialize(Stream stream, WhisperEchoNotification instance)
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

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000948E8 File Offset: 0x00092AE8
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

		// Token: 0x04001204 RID: 4612
		public bool HasSubscriberId;

		// Token: 0x04001205 RID: 4613
		private AccountId _SubscriberId;

		// Token: 0x04001206 RID: 4614
		public bool HasWhisper;

		// Token: 0x04001207 RID: 4615
		private Whisper _Whisper;
	}
}
