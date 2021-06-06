using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2.membership
{
	// Token: 0x020004A6 RID: 1190
	public class GetStateRequest : IProtoBuf
	{
		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x060052F4 RID: 21236 RVA: 0x00100347 File Offset: 0x000FE547
		// (set) Token: 0x060052F5 RID: 21237 RVA: 0x0010034F File Offset: 0x000FE54F
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

		// Token: 0x060052F6 RID: 21238 RVA: 0x00100362 File Offset: 0x000FE562
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x0010036C File Offset: 0x000FE56C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x0010039C File Offset: 0x000FE59C
		public override bool Equals(object obj)
		{
			GetStateRequest getStateRequest = obj as GetStateRequest;
			return getStateRequest != null && this.HasAgentId == getStateRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(getStateRequest.AgentId));
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x060052F9 RID: 21241 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x001003E1 File Offset: 0x000FE5E1
		public static GetStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetStateRequest>(bs, 0, -1);
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x001003EB File Offset: 0x000FE5EB
		public void Deserialize(Stream stream)
		{
			GetStateRequest.Deserialize(stream, this);
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x001003F5 File Offset: 0x000FE5F5
		public static GetStateRequest Deserialize(Stream stream, GetStateRequest instance)
		{
			return GetStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x00100400 File Offset: 0x000FE600
		public static GetStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetStateRequest getStateRequest = new GetStateRequest();
			GetStateRequest.DeserializeLengthDelimited(stream, getStateRequest);
			return getStateRequest;
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x0010041C File Offset: 0x000FE61C
		public static GetStateRequest DeserializeLengthDelimited(Stream stream, GetStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x00100444 File Offset: 0x000FE644
		public static GetStateRequest Deserialize(Stream stream, GetStateRequest instance, long limit)
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

		// Token: 0x06005300 RID: 21248 RVA: 0x001004DE File Offset: 0x000FE6DE
		public void Serialize(Stream stream)
		{
			GetStateRequest.Serialize(stream, this);
		}

		// Token: 0x06005301 RID: 21249 RVA: 0x001004E7 File Offset: 0x000FE6E7
		public static void Serialize(Stream stream, GetStateRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
		}

		// Token: 0x06005302 RID: 21250 RVA: 0x00100518 File Offset: 0x000FE718
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

		// Token: 0x04001A7E RID: 6782
		public bool HasAgentId;

		// Token: 0x04001A7F RID: 6783
		private GameAccountHandle _AgentId;
	}
}
