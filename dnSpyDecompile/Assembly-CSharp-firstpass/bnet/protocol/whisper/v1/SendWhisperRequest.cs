using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002DF RID: 735
	public class SendWhisperRequest : IProtoBuf
	{
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x00095E9B File Offset: 0x0009409B
		// (set) Token: 0x06002B55 RID: 11093 RVA: 0x00095EA3 File Offset: 0x000940A3
		public AccountId AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x00095EB6 File Offset: 0x000940B6
		public void SetAgentId(AccountId val)
		{
			this.AgentId = val;
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x00095EBF File Offset: 0x000940BF
		// (set) Token: 0x06002B58 RID: 11096 RVA: 0x00095EC7 File Offset: 0x000940C7
		public SendOptions Whisper
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

		// Token: 0x06002B59 RID: 11097 RVA: 0x00095EDA File Offset: 0x000940DA
		public void SetWhisper(SendOptions val)
		{
			this.Whisper = val;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x00095EE4 File Offset: 0x000940E4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasWhisper)
			{
				num ^= this.Whisper.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x00095F2C File Offset: 0x0009412C
		public override bool Equals(object obj)
		{
			SendWhisperRequest sendWhisperRequest = obj as SendWhisperRequest;
			return sendWhisperRequest != null && this.HasAgentId == sendWhisperRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(sendWhisperRequest.AgentId)) && this.HasWhisper == sendWhisperRequest.HasWhisper && (!this.HasWhisper || this.Whisper.Equals(sendWhisperRequest.Whisper));
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x00095F9C File Offset: 0x0009419C
		public static SendWhisperRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendWhisperRequest>(bs, 0, -1);
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x00095FA6 File Offset: 0x000941A6
		public void Deserialize(Stream stream)
		{
			SendWhisperRequest.Deserialize(stream, this);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x00095FB0 File Offset: 0x000941B0
		public static SendWhisperRequest Deserialize(Stream stream, SendWhisperRequest instance)
		{
			return SendWhisperRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x00095FBC File Offset: 0x000941BC
		public static SendWhisperRequest DeserializeLengthDelimited(Stream stream)
		{
			SendWhisperRequest sendWhisperRequest = new SendWhisperRequest();
			SendWhisperRequest.DeserializeLengthDelimited(stream, sendWhisperRequest);
			return sendWhisperRequest;
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x00095FD8 File Offset: 0x000941D8
		public static SendWhisperRequest DeserializeLengthDelimited(Stream stream, SendWhisperRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendWhisperRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x00096000 File Offset: 0x00094200
		public static SendWhisperRequest Deserialize(Stream stream, SendWhisperRequest instance, long limit)
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
						instance.Whisper = SendOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						SendOptions.DeserializeLengthDelimited(stream, instance.Whisper);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000960D2 File Offset: 0x000942D2
		public void Serialize(Stream stream)
		{
			SendWhisperRequest.Serialize(stream, this);
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000960DC File Offset: 0x000942DC
		public static void Serialize(Stream stream, SendWhisperRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasWhisper)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Whisper.GetSerializedSize());
				SendOptions.Serialize(stream, instance.Whisper);
			}
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x00096144 File Offset: 0x00094344
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
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

		// Token: 0x04001223 RID: 4643
		public bool HasAgentId;

		// Token: 0x04001224 RID: 4644
		private AccountId _AgentId;

		// Token: 0x04001225 RID: 4645
		public bool HasWhisper;

		// Token: 0x04001226 RID: 4646
		private SendOptions _Whisper;
	}
}
