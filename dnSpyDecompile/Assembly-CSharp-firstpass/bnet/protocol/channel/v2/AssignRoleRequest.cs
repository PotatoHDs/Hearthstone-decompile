using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200045C RID: 1116
	public class AssignRoleRequest : IProtoBuf
	{
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06004C4B RID: 19531 RVA: 0x000ED2BD File Offset: 0x000EB4BD
		// (set) Token: 0x06004C4C RID: 19532 RVA: 0x000ED2C5 File Offset: 0x000EB4C5
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

		// Token: 0x06004C4D RID: 19533 RVA: 0x000ED2D8 File Offset: 0x000EB4D8
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06004C4E RID: 19534 RVA: 0x000ED2E1 File Offset: 0x000EB4E1
		// (set) Token: 0x06004C4F RID: 19535 RVA: 0x000ED2E9 File Offset: 0x000EB4E9
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x000ED2FC File Offset: 0x000EB4FC
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06004C51 RID: 19537 RVA: 0x000ED305 File Offset: 0x000EB505
		// (set) Token: 0x06004C52 RID: 19538 RVA: 0x000ED30D File Offset: 0x000EB50D
		public RoleAssignment Assignment
		{
			get
			{
				return this._Assignment;
			}
			set
			{
				this._Assignment = value;
				this.HasAssignment = (value != null);
			}
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x000ED320 File Offset: 0x000EB520
		public void SetAssignment(RoleAssignment val)
		{
			this.Assignment = val;
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x000ED32C File Offset: 0x000EB52C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasAssignment)
			{
				num ^= this.Assignment.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x000ED388 File Offset: 0x000EB588
		public override bool Equals(object obj)
		{
			AssignRoleRequest assignRoleRequest = obj as AssignRoleRequest;
			return assignRoleRequest != null && this.HasAgentId == assignRoleRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(assignRoleRequest.AgentId)) && this.HasChannelId == assignRoleRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(assignRoleRequest.ChannelId)) && this.HasAssignment == assignRoleRequest.HasAssignment && (!this.HasAssignment || this.Assignment.Equals(assignRoleRequest.Assignment));
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06004C56 RID: 19542 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x000ED423 File Offset: 0x000EB623
		public static AssignRoleRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AssignRoleRequest>(bs, 0, -1);
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x000ED42D File Offset: 0x000EB62D
		public void Deserialize(Stream stream)
		{
			AssignRoleRequest.Deserialize(stream, this);
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x000ED437 File Offset: 0x000EB637
		public static AssignRoleRequest Deserialize(Stream stream, AssignRoleRequest instance)
		{
			return AssignRoleRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x000ED444 File Offset: 0x000EB644
		public static AssignRoleRequest DeserializeLengthDelimited(Stream stream)
		{
			AssignRoleRequest assignRoleRequest = new AssignRoleRequest();
			AssignRoleRequest.DeserializeLengthDelimited(stream, assignRoleRequest);
			return assignRoleRequest;
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x000ED460 File Offset: 0x000EB660
		public static AssignRoleRequest DeserializeLengthDelimited(Stream stream, AssignRoleRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AssignRoleRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x000ED488 File Offset: 0x000EB688
		public static AssignRoleRequest Deserialize(Stream stream, AssignRoleRequest instance, long limit)
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
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Assignment == null)
						{
							instance.Assignment = RoleAssignment.DeserializeLengthDelimited(stream);
						}
						else
						{
							RoleAssignment.DeserializeLengthDelimited(stream, instance.Assignment);
						}
					}
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x000ED58A File Offset: 0x000EB78A
		public void Serialize(Stream stream)
		{
			AssignRoleRequest.Serialize(stream, this);
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x000ED594 File Offset: 0x000EB794
		public static void Serialize(Stream stream, AssignRoleRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasAssignment)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Assignment.GetSerializedSize());
				RoleAssignment.Serialize(stream, instance.Assignment);
			}
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x000ED628 File Offset: 0x000EB828
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasAssignment)
			{
				num += 1U;
				uint serializedSize3 = this.Assignment.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040018E2 RID: 6370
		public bool HasAgentId;

		// Token: 0x040018E3 RID: 6371
		private GameAccountHandle _AgentId;

		// Token: 0x040018E4 RID: 6372
		public bool HasChannelId;

		// Token: 0x040018E5 RID: 6373
		private ChannelId _ChannelId;

		// Token: 0x040018E6 RID: 6374
		public bool HasAssignment;

		// Token: 0x040018E7 RID: 6375
		private RoleAssignment _Assignment;
	}
}
