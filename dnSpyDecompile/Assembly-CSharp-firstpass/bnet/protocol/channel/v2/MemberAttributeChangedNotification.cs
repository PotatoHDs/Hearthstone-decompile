using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000467 RID: 1127
	public class MemberAttributeChangedNotification : IProtoBuf
	{
		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06004D3A RID: 19770 RVA: 0x000EFC09 File Offset: 0x000EDE09
		// (set) Token: 0x06004D3B RID: 19771 RVA: 0x000EFC11 File Offset: 0x000EDE11
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

		// Token: 0x06004D3C RID: 19772 RVA: 0x000EFC24 File Offset: 0x000EDE24
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06004D3D RID: 19773 RVA: 0x000EFC2D File Offset: 0x000EDE2D
		// (set) Token: 0x06004D3E RID: 19774 RVA: 0x000EFC35 File Offset: 0x000EDE35
		public GameAccountHandle SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = (value != null);
			}
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x000EFC48 File Offset: 0x000EDE48
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06004D40 RID: 19776 RVA: 0x000EFC51 File Offset: 0x000EDE51
		// (set) Token: 0x06004D41 RID: 19777 RVA: 0x000EFC59 File Offset: 0x000EDE59
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

		// Token: 0x06004D42 RID: 19778 RVA: 0x000EFC6C File Offset: 0x000EDE6C
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06004D43 RID: 19779 RVA: 0x000EFC75 File Offset: 0x000EDE75
		// (set) Token: 0x06004D44 RID: 19780 RVA: 0x000EFC7D File Offset: 0x000EDE7D
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

		// Token: 0x06004D45 RID: 19781 RVA: 0x000EFC90 File Offset: 0x000EDE90
		public void SetAssignment(AttributeAssignment val)
		{
			this.Assignment = val;
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x000EFC9C File Offset: 0x000EDE9C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
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

		// Token: 0x06004D47 RID: 19783 RVA: 0x000EFD10 File Offset: 0x000EDF10
		public override bool Equals(object obj)
		{
			MemberAttributeChangedNotification memberAttributeChangedNotification = obj as MemberAttributeChangedNotification;
			return memberAttributeChangedNotification != null && this.HasAgentId == memberAttributeChangedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(memberAttributeChangedNotification.AgentId)) && this.HasSubscriberId == memberAttributeChangedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(memberAttributeChangedNotification.SubscriberId)) && this.HasChannelId == memberAttributeChangedNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(memberAttributeChangedNotification.ChannelId)) && this.HasAssignment == memberAttributeChangedNotification.HasAssignment && (!this.HasAssignment || this.Assignment.Equals(memberAttributeChangedNotification.Assignment));
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06004D48 RID: 19784 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x000EFDD6 File Offset: 0x000EDFD6
		public static MemberAttributeChangedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberAttributeChangedNotification>(bs, 0, -1);
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x000EFDE0 File Offset: 0x000EDFE0
		public void Deserialize(Stream stream)
		{
			MemberAttributeChangedNotification.Deserialize(stream, this);
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x000EFDEA File Offset: 0x000EDFEA
		public static MemberAttributeChangedNotification Deserialize(Stream stream, MemberAttributeChangedNotification instance)
		{
			return MemberAttributeChangedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x000EFDF8 File Offset: 0x000EDFF8
		public static MemberAttributeChangedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberAttributeChangedNotification memberAttributeChangedNotification = new MemberAttributeChangedNotification();
			MemberAttributeChangedNotification.DeserializeLengthDelimited(stream, memberAttributeChangedNotification);
			return memberAttributeChangedNotification;
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x000EFE14 File Offset: 0x000EE014
		public static MemberAttributeChangedNotification DeserializeLengthDelimited(Stream stream, MemberAttributeChangedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberAttributeChangedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x000EFE3C File Offset: 0x000EE03C
		public static MemberAttributeChangedNotification Deserialize(Stream stream, MemberAttributeChangedNotification instance, long limit)
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.SubscriberId == null)
								{
									instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
									continue;
								}
								GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 34)
						{
							if (instance.Assignment == null)
							{
								instance.Assignment = AttributeAssignment.DeserializeLengthDelimited(stream);
								continue;
							}
							AttributeAssignment.DeserializeLengthDelimited(stream, instance.Assignment);
							continue;
						}
					}
					else
					{
						if (instance.ChannelId == null)
						{
							instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
							continue;
						}
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
						continue;
					}
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

		// Token: 0x06004D4F RID: 19791 RVA: 0x000EFF81 File Offset: 0x000EE181
		public void Serialize(Stream stream)
		{
			MemberAttributeChangedNotification.Serialize(stream, this);
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x000EFF8C File Offset: 0x000EE18C
		public static void Serialize(Stream stream, MemberAttributeChangedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasAssignment)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Assignment.GetSerializedSize());
				AttributeAssignment.Serialize(stream, instance.Assignment);
			}
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x000F0050 File Offset: 0x000EE250
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize2 = this.SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasAssignment)
			{
				num += 1U;
				uint serializedSize4 = this.Assignment.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		// Token: 0x04001922 RID: 6434
		public bool HasAgentId;

		// Token: 0x04001923 RID: 6435
		private GameAccountHandle _AgentId;

		// Token: 0x04001924 RID: 6436
		public bool HasSubscriberId;

		// Token: 0x04001925 RID: 6437
		private GameAccountHandle _SubscriberId;

		// Token: 0x04001926 RID: 6438
		public bool HasChannelId;

		// Token: 0x04001927 RID: 6439
		private ChannelId _ChannelId;

		// Token: 0x04001928 RID: 6440
		public bool HasAssignment;

		// Token: 0x04001929 RID: 6441
		private AttributeAssignment _Assignment;
	}
}
