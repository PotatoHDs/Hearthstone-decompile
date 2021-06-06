using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200045D RID: 1117
	public class UnassignRoleRequest : IProtoBuf
	{
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06004C61 RID: 19553 RVA: 0x000ED6A1 File Offset: 0x000EB8A1
		// (set) Token: 0x06004C62 RID: 19554 RVA: 0x000ED6A9 File Offset: 0x000EB8A9
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

		// Token: 0x06004C63 RID: 19555 RVA: 0x000ED6BC File Offset: 0x000EB8BC
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06004C64 RID: 19556 RVA: 0x000ED6C5 File Offset: 0x000EB8C5
		// (set) Token: 0x06004C65 RID: 19557 RVA: 0x000ED6CD File Offset: 0x000EB8CD
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

		// Token: 0x06004C66 RID: 19558 RVA: 0x000ED6E0 File Offset: 0x000EB8E0
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06004C67 RID: 19559 RVA: 0x000ED6E9 File Offset: 0x000EB8E9
		// (set) Token: 0x06004C68 RID: 19560 RVA: 0x000ED6F1 File Offset: 0x000EB8F1
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

		// Token: 0x06004C69 RID: 19561 RVA: 0x000ED704 File Offset: 0x000EB904
		public void SetAssignment(RoleAssignment val)
		{
			this.Assignment = val;
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x000ED710 File Offset: 0x000EB910
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

		// Token: 0x06004C6B RID: 19563 RVA: 0x000ED76C File Offset: 0x000EB96C
		public override bool Equals(object obj)
		{
			UnassignRoleRequest unassignRoleRequest = obj as UnassignRoleRequest;
			return unassignRoleRequest != null && this.HasAgentId == unassignRoleRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(unassignRoleRequest.AgentId)) && this.HasChannelId == unassignRoleRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(unassignRoleRequest.ChannelId)) && this.HasAssignment == unassignRoleRequest.HasAssignment && (!this.HasAssignment || this.Assignment.Equals(unassignRoleRequest.Assignment));
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06004C6C RID: 19564 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x000ED807 File Offset: 0x000EBA07
		public static UnassignRoleRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnassignRoleRequest>(bs, 0, -1);
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x000ED811 File Offset: 0x000EBA11
		public void Deserialize(Stream stream)
		{
			UnassignRoleRequest.Deserialize(stream, this);
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x000ED81B File Offset: 0x000EBA1B
		public static UnassignRoleRequest Deserialize(Stream stream, UnassignRoleRequest instance)
		{
			return UnassignRoleRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x000ED828 File Offset: 0x000EBA28
		public static UnassignRoleRequest DeserializeLengthDelimited(Stream stream)
		{
			UnassignRoleRequest unassignRoleRequest = new UnassignRoleRequest();
			UnassignRoleRequest.DeserializeLengthDelimited(stream, unassignRoleRequest);
			return unassignRoleRequest;
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x000ED844 File Offset: 0x000EBA44
		public static UnassignRoleRequest DeserializeLengthDelimited(Stream stream, UnassignRoleRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnassignRoleRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x000ED86C File Offset: 0x000EBA6C
		public static UnassignRoleRequest Deserialize(Stream stream, UnassignRoleRequest instance, long limit)
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

		// Token: 0x06004C73 RID: 19571 RVA: 0x000ED96E File Offset: 0x000EBB6E
		public void Serialize(Stream stream)
		{
			UnassignRoleRequest.Serialize(stream, this);
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x000ED978 File Offset: 0x000EBB78
		public static void Serialize(Stream stream, UnassignRoleRequest instance)
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

		// Token: 0x06004C75 RID: 19573 RVA: 0x000EDA0C File Offset: 0x000EBC0C
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

		// Token: 0x040018E8 RID: 6376
		public bool HasAgentId;

		// Token: 0x040018E9 RID: 6377
		private GameAccountHandle _AgentId;

		// Token: 0x040018EA RID: 6378
		public bool HasChannelId;

		// Token: 0x040018EB RID: 6379
		private ChannelId _ChannelId;

		// Token: 0x040018EC RID: 6380
		public bool HasAssignment;

		// Token: 0x040018ED RID: 6381
		private RoleAssignment _Assignment;
	}
}
