using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004CE RID: 1230
	public class MemberRemovedNotification : IProtoBuf
	{
		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x0600564E RID: 22094 RVA: 0x00108C61 File Offset: 0x00106E61
		// (set) Token: 0x0600564F RID: 22095 RVA: 0x00108C69 File Offset: 0x00106E69
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

		// Token: 0x06005650 RID: 22096 RVA: 0x00108C7C File Offset: 0x00106E7C
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06005651 RID: 22097 RVA: 0x00108C85 File Offset: 0x00106E85
		// (set) Token: 0x06005652 RID: 22098 RVA: 0x00108C8D File Offset: 0x00106E8D
		public EntityId MemberId { get; set; }

		// Token: 0x06005653 RID: 22099 RVA: 0x00108C96 File Offset: 0x00106E96
		public void SetMemberId(EntityId val)
		{
			this.MemberId = val;
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06005654 RID: 22100 RVA: 0x00108C9F File Offset: 0x00106E9F
		// (set) Token: 0x06005655 RID: 22101 RVA: 0x00108CA7 File Offset: 0x00106EA7
		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x06005656 RID: 22102 RVA: 0x00108CB7 File Offset: 0x00106EB7
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06005657 RID: 22103 RVA: 0x00108CC0 File Offset: 0x00106EC0
		// (set) Token: 0x06005658 RID: 22104 RVA: 0x00108CC8 File Offset: 0x00106EC8
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

		// Token: 0x06005659 RID: 22105 RVA: 0x00108CDB File Offset: 0x00106EDB
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x0600565A RID: 22106 RVA: 0x00108CE4 File Offset: 0x00106EE4
		// (set) Token: 0x0600565B RID: 22107 RVA: 0x00108CEC File Offset: 0x00106EEC
		public SubscriberId SubscriberId
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

		// Token: 0x0600565C RID: 22108 RVA: 0x00108CFF File Offset: 0x00106EFF
		public void SetSubscriberId(SubscriberId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x00108D08 File Offset: 0x00106F08
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.MemberId.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x00108D8C File Offset: 0x00106F8C
		public override bool Equals(object obj)
		{
			MemberRemovedNotification memberRemovedNotification = obj as MemberRemovedNotification;
			return memberRemovedNotification != null && this.HasAgentId == memberRemovedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(memberRemovedNotification.AgentId)) && this.MemberId.Equals(memberRemovedNotification.MemberId) && this.HasReason == memberRemovedNotification.HasReason && (!this.HasReason || this.Reason.Equals(memberRemovedNotification.Reason)) && this.HasChannelId == memberRemovedNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(memberRemovedNotification.ChannelId)) && this.HasSubscriberId == memberRemovedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(memberRemovedNotification.SubscriberId));
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x0600565F RID: 22111 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x00108E6A File Offset: 0x0010706A
		public static MemberRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x00108E74 File Offset: 0x00107074
		public void Deserialize(Stream stream)
		{
			MemberRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x00108E7E File Offset: 0x0010707E
		public static MemberRemovedNotification Deserialize(Stream stream, MemberRemovedNotification instance)
		{
			return MemberRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x00108E8C File Offset: 0x0010708C
		public static MemberRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberRemovedNotification memberRemovedNotification = new MemberRemovedNotification();
			MemberRemovedNotification.DeserializeLengthDelimited(stream, memberRemovedNotification);
			return memberRemovedNotification;
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x00108EA8 File Offset: 0x001070A8
		public static MemberRemovedNotification DeserializeLengthDelimited(Stream stream, MemberRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x00108ED0 File Offset: 0x001070D0
		public static MemberRemovedNotification Deserialize(Stream stream, MemberRemovedNotification instance, long limit)
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
								if (instance.MemberId == null)
								{
									instance.MemberId = EntityId.DeserializeLengthDelimited(stream);
									continue;
								}
								EntityId.DeserializeLengthDelimited(stream, instance.MemberId);
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Reason = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num != 34)
						{
							if (num == 42)
							{
								if (instance.SubscriberId == null)
								{
									instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
									continue;
								}
								SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
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

		// Token: 0x06005666 RID: 22118 RVA: 0x0010902B File Offset: 0x0010722B
		public void Serialize(Stream stream)
		{
			MemberRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x00109034 File Offset: 0x00107234
		public static void Serialize(Stream stream, MemberRemovedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.MemberId == null)
			{
				throw new ArgumentNullException("MemberId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
			EntityId.Serialize(stream, instance.MemberId);
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x00109124 File Offset: 0x00107324
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.MemberId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize4 = this.SubscriberId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num + 1U;
		}

		// Token: 0x04001B2A RID: 6954
		public bool HasAgentId;

		// Token: 0x04001B2B RID: 6955
		private EntityId _AgentId;

		// Token: 0x04001B2D RID: 6957
		public bool HasReason;

		// Token: 0x04001B2E RID: 6958
		private uint _Reason;

		// Token: 0x04001B2F RID: 6959
		public bool HasChannelId;

		// Token: 0x04001B30 RID: 6960
		private ChannelId _ChannelId;

		// Token: 0x04001B31 RID: 6961
		public bool HasSubscriberId;

		// Token: 0x04001B32 RID: 6962
		private SubscriberId _SubscriberId;
	}
}
