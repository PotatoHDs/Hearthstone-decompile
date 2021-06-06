using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000423 RID: 1059
	public class ViewFriendsRequest : IProtoBuf
	{
		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x060046D8 RID: 18136 RVA: 0x000DDD03 File Offset: 0x000DBF03
		// (set) Token: 0x060046D9 RID: 18137 RVA: 0x000DDD0B File Offset: 0x000DBF0B
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

		// Token: 0x060046DA RID: 18138 RVA: 0x000DDD1E File Offset: 0x000DBF1E
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x060046DB RID: 18139 RVA: 0x000DDD27 File Offset: 0x000DBF27
		// (set) Token: 0x060046DC RID: 18140 RVA: 0x000DDD2F File Offset: 0x000DBF2F
		public EntityId TargetId { get; set; }

		// Token: 0x060046DD RID: 18141 RVA: 0x000DDD38 File Offset: 0x000DBF38
		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x000DDD44 File Offset: 0x000DBF44
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num ^ this.TargetId.GetHashCode();
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x000DDD84 File Offset: 0x000DBF84
		public override bool Equals(object obj)
		{
			ViewFriendsRequest viewFriendsRequest = obj as ViewFriendsRequest;
			return viewFriendsRequest != null && this.HasAgentId == viewFriendsRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(viewFriendsRequest.AgentId)) && this.TargetId.Equals(viewFriendsRequest.TargetId);
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x060046E0 RID: 18144 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x000DDDDE File Offset: 0x000DBFDE
		public static ViewFriendsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ViewFriendsRequest>(bs, 0, -1);
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x000DDDE8 File Offset: 0x000DBFE8
		public void Deserialize(Stream stream)
		{
			ViewFriendsRequest.Deserialize(stream, this);
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x000DDDF2 File Offset: 0x000DBFF2
		public static ViewFriendsRequest Deserialize(Stream stream, ViewFriendsRequest instance)
		{
			return ViewFriendsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x000DDE00 File Offset: 0x000DC000
		public static ViewFriendsRequest DeserializeLengthDelimited(Stream stream)
		{
			ViewFriendsRequest viewFriendsRequest = new ViewFriendsRequest();
			ViewFriendsRequest.DeserializeLengthDelimited(stream, viewFriendsRequest);
			return viewFriendsRequest;
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x000DDE1C File Offset: 0x000DC01C
		public static ViewFriendsRequest DeserializeLengthDelimited(Stream stream, ViewFriendsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ViewFriendsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x000DDE44 File Offset: 0x000DC044
		public static ViewFriendsRequest Deserialize(Stream stream, ViewFriendsRequest instance, long limit)
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

		// Token: 0x060046E7 RID: 18151 RVA: 0x000DDF16 File Offset: 0x000DC116
		public void Serialize(Stream stream)
		{
			ViewFriendsRequest.Serialize(stream, this);
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x000DDF20 File Offset: 0x000DC120
		public static void Serialize(Stream stream, ViewFriendsRequest instance)
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

		// Token: 0x060046E9 RID: 18153 RVA: 0x000DDF98 File Offset: 0x000DC198
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

		// Token: 0x040017B0 RID: 6064
		public bool HasAgentId;

		// Token: 0x040017B1 RID: 6065
		private EntityId _AgentId;
	}
}
