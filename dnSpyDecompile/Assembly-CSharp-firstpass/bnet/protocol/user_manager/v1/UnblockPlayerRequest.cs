using System;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002F2 RID: 754
	public class UnblockPlayerRequest : IProtoBuf
	{
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x0009A504 File Offset: 0x00098704
		// (set) Token: 0x06002CF8 RID: 11512 RVA: 0x0009A50C File Offset: 0x0009870C
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

		// Token: 0x06002CF9 RID: 11513 RVA: 0x0009A51F File Offset: 0x0009871F
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002CFA RID: 11514 RVA: 0x0009A528 File Offset: 0x00098728
		// (set) Token: 0x06002CFB RID: 11515 RVA: 0x0009A530 File Offset: 0x00098730
		public EntityId TargetId { get; set; }

		// Token: 0x06002CFC RID: 11516 RVA: 0x0009A539 File Offset: 0x00098739
		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x0009A544 File Offset: 0x00098744
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num ^ this.TargetId.GetHashCode();
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x0009A584 File Offset: 0x00098784
		public override bool Equals(object obj)
		{
			UnblockPlayerRequest unblockPlayerRequest = obj as UnblockPlayerRequest;
			return unblockPlayerRequest != null && this.HasAgentId == unblockPlayerRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(unblockPlayerRequest.AgentId)) && this.TargetId.Equals(unblockPlayerRequest.TargetId);
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x0009A5DE File Offset: 0x000987DE
		public static UnblockPlayerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnblockPlayerRequest>(bs, 0, -1);
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x0009A5E8 File Offset: 0x000987E8
		public void Deserialize(Stream stream)
		{
			UnblockPlayerRequest.Deserialize(stream, this);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x0009A5F2 File Offset: 0x000987F2
		public static UnblockPlayerRequest Deserialize(Stream stream, UnblockPlayerRequest instance)
		{
			return UnblockPlayerRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0009A600 File Offset: 0x00098800
		public static UnblockPlayerRequest DeserializeLengthDelimited(Stream stream)
		{
			UnblockPlayerRequest unblockPlayerRequest = new UnblockPlayerRequest();
			UnblockPlayerRequest.DeserializeLengthDelimited(stream, unblockPlayerRequest);
			return unblockPlayerRequest;
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x0009A61C File Offset: 0x0009881C
		public static UnblockPlayerRequest DeserializeLengthDelimited(Stream stream, UnblockPlayerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnblockPlayerRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x0009A644 File Offset: 0x00098844
		public static UnblockPlayerRequest Deserialize(Stream stream, UnblockPlayerRequest instance, long limit)
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

		// Token: 0x06002D06 RID: 11526 RVA: 0x0009A716 File Offset: 0x00098916
		public void Serialize(Stream stream)
		{
			UnblockPlayerRequest.Serialize(stream, this);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x0009A720 File Offset: 0x00098920
		public static void Serialize(Stream stream, UnblockPlayerRequest instance)
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

		// Token: 0x06002D08 RID: 11528 RVA: 0x0009A798 File Offset: 0x00098998
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

		// Token: 0x0400127A RID: 4730
		public bool HasAgentId;

		// Token: 0x0400127B RID: 4731
		private EntityId _AgentId;
	}
}
