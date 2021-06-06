using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000426 RID: 1062
	public class GetFriendListRequest : IProtoBuf
	{
		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06004719 RID: 18201 RVA: 0x000DE7CB File Offset: 0x000DC9CB
		// (set) Token: 0x0600471A RID: 18202 RVA: 0x000DE7D3 File Offset: 0x000DC9D3
		public EntityId AgentId
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

		// Token: 0x0600471B RID: 18203 RVA: 0x000DE7E6 File Offset: 0x000DC9E6
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x000DE7F0 File Offset: 0x000DC9F0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x000DE820 File Offset: 0x000DCA20
		public override bool Equals(object obj)
		{
			GetFriendListRequest getFriendListRequest = obj as GetFriendListRequest;
			return getFriendListRequest != null && this.HasAgentId == getFriendListRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(getFriendListRequest.AgentId));
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x0600471E RID: 18206 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x000DE865 File Offset: 0x000DCA65
		public static GetFriendListRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFriendListRequest>(bs, 0, -1);
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x000DE86F File Offset: 0x000DCA6F
		public void Deserialize(Stream stream)
		{
			GetFriendListRequest.Deserialize(stream, this);
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x000DE879 File Offset: 0x000DCA79
		public static GetFriendListRequest Deserialize(Stream stream, GetFriendListRequest instance)
		{
			return GetFriendListRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x000DE884 File Offset: 0x000DCA84
		public static GetFriendListRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFriendListRequest getFriendListRequest = new GetFriendListRequest();
			GetFriendListRequest.DeserializeLengthDelimited(stream, getFriendListRequest);
			return getFriendListRequest;
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x000DE8A0 File Offset: 0x000DCAA0
		public static GetFriendListRequest DeserializeLengthDelimited(Stream stream, GetFriendListRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFriendListRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x000DE8C8 File Offset: 0x000DCAC8
		public static GetFriendListRequest Deserialize(Stream stream, GetFriendListRequest instance, long limit)
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
				else if (num == 18)
				{
					if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
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

		// Token: 0x06004725 RID: 18213 RVA: 0x000DE962 File Offset: 0x000DCB62
		public void Serialize(Stream stream)
		{
			GetFriendListRequest.Serialize(stream, this);
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x000DE96B File Offset: 0x000DCB6B
		public static void Serialize(Stream stream, GetFriendListRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x000DE99C File Offset: 0x000DCB9C
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

		// Token: 0x040017B8 RID: 6072
		public bool HasAgentId;

		// Token: 0x040017B9 RID: 6073
		private EntityId _AgentId;
	}
}
