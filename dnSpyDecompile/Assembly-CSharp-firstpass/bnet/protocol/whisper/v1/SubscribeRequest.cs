using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002DC RID: 732
	public class SubscribeRequest : IProtoBuf
	{
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x00095778 File Offset: 0x00093978
		// (set) Token: 0x06002B21 RID: 11041 RVA: 0x00095780 File Offset: 0x00093980
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

		// Token: 0x06002B22 RID: 11042 RVA: 0x00095793 File Offset: 0x00093993
		public void SetAgentId(AccountId val)
		{
			this.AgentId = val;
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x0009579C File Offset: 0x0009399C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000957CC File Offset: 0x000939CC
		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			return subscribeRequest != null && this.HasAgentId == subscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(subscribeRequest.AgentId));
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x00095811 File Offset: 0x00093A11
		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x0009581B File Offset: 0x00093A1B
		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x00095825 File Offset: 0x00093A25
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x00095830 File Offset: 0x00093A30
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x0009584C File Offset: 0x00093A4C
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x00095874 File Offset: 0x00093A74
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
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

		// Token: 0x06002B2C RID: 11052 RVA: 0x0009590E File Offset: 0x00093B0E
		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x00095917 File Offset: 0x00093B17
		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AgentId);
			}
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x00095948 File Offset: 0x00093B48
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

		// Token: 0x0400121E RID: 4638
		public bool HasAgentId;

		// Token: 0x0400121F RID: 4639
		private AccountId _AgentId;
	}
}
