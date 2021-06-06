using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200045B RID: 1115
	public class SetMemberAttributeRequest : IProtoBuf
	{
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06004C35 RID: 19509 RVA: 0x000ECED9 File Offset: 0x000EB0D9
		// (set) Token: 0x06004C36 RID: 19510 RVA: 0x000ECEE1 File Offset: 0x000EB0E1
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

		// Token: 0x06004C37 RID: 19511 RVA: 0x000ECEF4 File Offset: 0x000EB0F4
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x000ECEFD File Offset: 0x000EB0FD
		// (set) Token: 0x06004C39 RID: 19513 RVA: 0x000ECF05 File Offset: 0x000EB105
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

		// Token: 0x06004C3A RID: 19514 RVA: 0x000ECF18 File Offset: 0x000EB118
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06004C3B RID: 19515 RVA: 0x000ECF21 File Offset: 0x000EB121
		// (set) Token: 0x06004C3C RID: 19516 RVA: 0x000ECF29 File Offset: 0x000EB129
		public AttributeAssignment Assignment
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

		// Token: 0x06004C3D RID: 19517 RVA: 0x000ECF3C File Offset: 0x000EB13C
		public void SetAssignment(AttributeAssignment val)
		{
			this.Assignment = val;
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x000ECF48 File Offset: 0x000EB148
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

		// Token: 0x06004C3F RID: 19519 RVA: 0x000ECFA4 File Offset: 0x000EB1A4
		public override bool Equals(object obj)
		{
			SetMemberAttributeRequest setMemberAttributeRequest = obj as SetMemberAttributeRequest;
			return setMemberAttributeRequest != null && this.HasAgentId == setMemberAttributeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(setMemberAttributeRequest.AgentId)) && this.HasChannelId == setMemberAttributeRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(setMemberAttributeRequest.ChannelId)) && this.HasAssignment == setMemberAttributeRequest.HasAssignment && (!this.HasAssignment || this.Assignment.Equals(setMemberAttributeRequest.Assignment));
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06004C40 RID: 19520 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x000ED03F File Offset: 0x000EB23F
		public static SetMemberAttributeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetMemberAttributeRequest>(bs, 0, -1);
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x000ED049 File Offset: 0x000EB249
		public void Deserialize(Stream stream)
		{
			SetMemberAttributeRequest.Deserialize(stream, this);
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x000ED053 File Offset: 0x000EB253
		public static SetMemberAttributeRequest Deserialize(Stream stream, SetMemberAttributeRequest instance)
		{
			return SetMemberAttributeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x000ED060 File Offset: 0x000EB260
		public static SetMemberAttributeRequest DeserializeLengthDelimited(Stream stream)
		{
			SetMemberAttributeRequest setMemberAttributeRequest = new SetMemberAttributeRequest();
			SetMemberAttributeRequest.DeserializeLengthDelimited(stream, setMemberAttributeRequest);
			return setMemberAttributeRequest;
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x000ED07C File Offset: 0x000EB27C
		public static SetMemberAttributeRequest DeserializeLengthDelimited(Stream stream, SetMemberAttributeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetMemberAttributeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x000ED0A4 File Offset: 0x000EB2A4
		public static SetMemberAttributeRequest Deserialize(Stream stream, SetMemberAttributeRequest instance, long limit)
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
							instance.Assignment = AttributeAssignment.DeserializeLengthDelimited(stream);
						}
						else
						{
							AttributeAssignment.DeserializeLengthDelimited(stream, instance.Assignment);
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

		// Token: 0x06004C47 RID: 19527 RVA: 0x000ED1A6 File Offset: 0x000EB3A6
		public void Serialize(Stream stream)
		{
			SetMemberAttributeRequest.Serialize(stream, this);
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x000ED1B0 File Offset: 0x000EB3B0
		public static void Serialize(Stream stream, SetMemberAttributeRequest instance)
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
				AttributeAssignment.Serialize(stream, instance.Assignment);
			}
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x000ED244 File Offset: 0x000EB444
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

		// Token: 0x040018DC RID: 6364
		public bool HasAgentId;

		// Token: 0x040018DD RID: 6365
		private GameAccountHandle _AgentId;

		// Token: 0x040018DE RID: 6366
		public bool HasChannelId;

		// Token: 0x040018DF RID: 6367
		private ChannelId _ChannelId;

		// Token: 0x040018E0 RID: 6368
		public bool HasAssignment;

		// Token: 0x040018E1 RID: 6369
		private AttributeAssignment _Assignment;
	}
}
