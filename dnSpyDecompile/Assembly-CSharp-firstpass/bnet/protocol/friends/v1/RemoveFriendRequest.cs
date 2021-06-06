using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000421 RID: 1057
	public class RemoveFriendRequest : IProtoBuf
	{
		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x060046B5 RID: 18101 RVA: 0x000DD81B File Offset: 0x000DBA1B
		// (set) Token: 0x060046B6 RID: 18102 RVA: 0x000DD823 File Offset: 0x000DBA23
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

		// Token: 0x060046B7 RID: 18103 RVA: 0x000DD836 File Offset: 0x000DBA36
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x060046B8 RID: 18104 RVA: 0x000DD83F File Offset: 0x000DBA3F
		// (set) Token: 0x060046B9 RID: 18105 RVA: 0x000DD847 File Offset: 0x000DBA47
		public EntityId TargetId { get; set; }

		// Token: 0x060046BA RID: 18106 RVA: 0x000DD850 File Offset: 0x000DBA50
		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x000DD85C File Offset: 0x000DBA5C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num ^ this.TargetId.GetHashCode();
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x000DD89C File Offset: 0x000DBA9C
		public override bool Equals(object obj)
		{
			RemoveFriendRequest removeFriendRequest = obj as RemoveFriendRequest;
			return removeFriendRequest != null && this.HasAgentId == removeFriendRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(removeFriendRequest.AgentId)) && this.TargetId.Equals(removeFriendRequest.TargetId);
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x060046BD RID: 18109 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x000DD8F6 File Offset: 0x000DBAF6
		public static RemoveFriendRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveFriendRequest>(bs, 0, -1);
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x000DD900 File Offset: 0x000DBB00
		public void Deserialize(Stream stream)
		{
			RemoveFriendRequest.Deserialize(stream, this);
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x000DD90A File Offset: 0x000DBB0A
		public static RemoveFriendRequest Deserialize(Stream stream, RemoveFriendRequest instance)
		{
			return RemoveFriendRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x000DD918 File Offset: 0x000DBB18
		public static RemoveFriendRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveFriendRequest removeFriendRequest = new RemoveFriendRequest();
			RemoveFriendRequest.DeserializeLengthDelimited(stream, removeFriendRequest);
			return removeFriendRequest;
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x000DD934 File Offset: 0x000DBB34
		public static RemoveFriendRequest DeserializeLengthDelimited(Stream stream, RemoveFriendRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveFriendRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x000DD95C File Offset: 0x000DBB5C
		public static RemoveFriendRequest Deserialize(Stream stream, RemoveFriendRequest instance, long limit)
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
					else if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x000DDA2E File Offset: 0x000DBC2E
		public void Serialize(Stream stream)
		{
			RemoveFriendRequest.Serialize(stream, this);
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x000DDA38 File Offset: 0x000DBC38
		public static void Serialize(Stream stream, RemoveFriendRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x000DDAB0 File Offset: 0x000DBCB0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 1U;
		}

		// Token: 0x040017AB RID: 6059
		public bool HasAgentId;

		// Token: 0x040017AC RID: 6060
		private EntityId _AgentId;
	}
}
