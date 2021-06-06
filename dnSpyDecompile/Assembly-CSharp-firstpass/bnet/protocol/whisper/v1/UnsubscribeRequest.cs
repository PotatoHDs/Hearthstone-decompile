using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002DE RID: 734
	public class UnsubscribeRequest : IProtoBuf
	{
		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x00095C97 File Offset: 0x00093E97
		// (set) Token: 0x06002B45 RID: 11077 RVA: 0x00095C9F File Offset: 0x00093E9F
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

		// Token: 0x06002B46 RID: 11078 RVA: 0x00095CB2 File Offset: 0x00093EB2
		public void SetAgentId(AccountId val)
		{
			this.AgentId = val;
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x00095CBC File Offset: 0x00093EBC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x00095CEC File Offset: 0x00093EEC
		public override bool Equals(object obj)
		{
			UnsubscribeRequest unsubscribeRequest = obj as UnsubscribeRequest;
			return unsubscribeRequest != null && this.HasAgentId == unsubscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(unsubscribeRequest.AgentId));
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x00095D31 File Offset: 0x00093F31
		public static UnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x00095D3B File Offset: 0x00093F3B
		public void Deserialize(Stream stream)
		{
			UnsubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x00095D45 File Offset: 0x00093F45
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance)
		{
			return UnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x00095D50 File Offset: 0x00093F50
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
			UnsubscribeRequest.DeserializeLengthDelimited(stream, unsubscribeRequest);
			return unsubscribeRequest;
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x00095D6C File Offset: 0x00093F6C
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream, UnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x00095D94 File Offset: 0x00093F94
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance, long limit)
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
				else if (num == 10)
				{
					if (instance.AgentId == null)
					{
						instance.AgentId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
				}
				else
				{
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

		// Token: 0x06002B50 RID: 11088 RVA: 0x00095E2E File Offset: 0x0009402E
		public void Serialize(Stream stream)
		{
			UnsubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x00095E37 File Offset: 0x00094037
		public static void Serialize(Stream stream, UnsubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AgentId);
			}
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x00095E68 File Offset: 0x00094068
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001221 RID: 4641
		public bool HasAgentId;

		// Token: 0x04001222 RID: 4642
		private AccountId _AgentId;
	}
}
