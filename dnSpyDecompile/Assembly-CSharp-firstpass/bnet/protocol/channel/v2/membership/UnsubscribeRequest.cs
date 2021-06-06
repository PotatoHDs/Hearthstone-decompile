using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2.membership
{
	// Token: 0x020004A5 RID: 1189
	public class UnsubscribeRequest : IProtoBuf
	{
		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x060052E4 RID: 21220 RVA: 0x00100143 File Offset: 0x000FE343
		// (set) Token: 0x060052E5 RID: 21221 RVA: 0x0010014B File Offset: 0x000FE34B
		public GameAccountHandle AgentId
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

		// Token: 0x060052E6 RID: 21222 RVA: 0x0010015E File Offset: 0x000FE35E
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x00100168 File Offset: 0x000FE368
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x00100198 File Offset: 0x000FE398
		public override bool Equals(object obj)
		{
			UnsubscribeRequest unsubscribeRequest = obj as UnsubscribeRequest;
			return unsubscribeRequest != null && this.HasAgentId == unsubscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(unsubscribeRequest.AgentId));
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x060052E9 RID: 21225 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x001001DD File Offset: 0x000FE3DD
		public static UnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x001001E7 File Offset: 0x000FE3E7
		public void Deserialize(Stream stream)
		{
			UnsubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x001001F1 File Offset: 0x000FE3F1
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance)
		{
			return UnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x001001FC File Offset: 0x000FE3FC
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
			UnsubscribeRequest.DeserializeLengthDelimited(stream, unsubscribeRequest);
			return unsubscribeRequest;
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x00100218 File Offset: 0x000FE418
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream, UnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x00100240 File Offset: 0x000FE440
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
						instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
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

		// Token: 0x060052F0 RID: 21232 RVA: 0x001002DA File Offset: 0x000FE4DA
		public void Serialize(Stream stream)
		{
			UnsubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x001002E3 File Offset: 0x000FE4E3
		public static void Serialize(Stream stream, UnsubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x00100314 File Offset: 0x000FE514
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

		// Token: 0x04001A7C RID: 6780
		public bool HasAgentId;

		// Token: 0x04001A7D RID: 6781
		private GameAccountHandle _AgentId;
	}
}
