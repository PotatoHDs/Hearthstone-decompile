using System;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002F1 RID: 753
	public class BlockPlayerRequest : IProtoBuf
	{
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x0009A171 File Offset: 0x00098371
		// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x0009A179 File Offset: 0x00098379
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

		// Token: 0x06002CE3 RID: 11491 RVA: 0x0009A18C File Offset: 0x0009838C
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x0009A195 File Offset: 0x00098395
		// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x0009A19D File Offset: 0x0009839D
		public EntityId TargetId { get; set; }

		// Token: 0x06002CE6 RID: 11494 RVA: 0x0009A1A6 File Offset: 0x000983A6
		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x0009A1AF File Offset: 0x000983AF
		// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x0009A1B7 File Offset: 0x000983B7
		public uint Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				this._Role = value;
				this.HasRole = true;
			}
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x0009A1C7 File Offset: 0x000983C7
		public void SetRole(uint val)
		{
			this.Role = val;
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x0009A1D0 File Offset: 0x000983D0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.TargetId.GetHashCode();
			if (this.HasRole)
			{
				num ^= this.Role.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x0009A228 File Offset: 0x00098428
		public override bool Equals(object obj)
		{
			BlockPlayerRequest blockPlayerRequest = obj as BlockPlayerRequest;
			return blockPlayerRequest != null && this.HasAgentId == blockPlayerRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(blockPlayerRequest.AgentId)) && this.TargetId.Equals(blockPlayerRequest.TargetId) && this.HasRole == blockPlayerRequest.HasRole && (!this.HasRole || this.Role.Equals(blockPlayerRequest.Role));
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x0009A2B0 File Offset: 0x000984B0
		public static BlockPlayerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BlockPlayerRequest>(bs, 0, -1);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x0009A2BA File Offset: 0x000984BA
		public void Deserialize(Stream stream)
		{
			BlockPlayerRequest.Deserialize(stream, this);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x0009A2C4 File Offset: 0x000984C4
		public static BlockPlayerRequest Deserialize(Stream stream, BlockPlayerRequest instance)
		{
			return BlockPlayerRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x0009A2D0 File Offset: 0x000984D0
		public static BlockPlayerRequest DeserializeLengthDelimited(Stream stream)
		{
			BlockPlayerRequest blockPlayerRequest = new BlockPlayerRequest();
			BlockPlayerRequest.DeserializeLengthDelimited(stream, blockPlayerRequest);
			return blockPlayerRequest;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x0009A2EC File Offset: 0x000984EC
		public static BlockPlayerRequest DeserializeLengthDelimited(Stream stream, BlockPlayerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BlockPlayerRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x0009A314 File Offset: 0x00098514
		public static BlockPlayerRequest Deserialize(Stream stream, BlockPlayerRequest instance, long limit)
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
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.Role = ProtocolParser.ReadUInt32(stream);
						}
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

		// Token: 0x06002CF3 RID: 11507 RVA: 0x0009A3FC File Offset: 0x000985FC
		public void Serialize(Stream stream)
		{
			BlockPlayerRequest.Serialize(stream, this);
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x0009A408 File Offset: 0x00098608
		public static void Serialize(Stream stream, BlockPlayerRequest instance)
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
			if (instance.HasRole)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Role);
			}
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x0009A49C File Offset: 0x0009869C
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
			if (this.HasRole)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Role);
			}
			return num + 1U;
		}

		// Token: 0x04001275 RID: 4725
		public bool HasAgentId;

		// Token: 0x04001276 RID: 4726
		private EntityId _AgentId;

		// Token: 0x04001278 RID: 4728
		public bool HasRole;

		// Token: 0x04001279 RID: 4729
		private uint _Role;
	}
}
