using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004CD RID: 1229
	public class LeaveNotification : IProtoBuf
	{
		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06005632 RID: 22066 RVA: 0x001086ED File Offset: 0x001068ED
		// (set) Token: 0x06005633 RID: 22067 RVA: 0x001086F5 File Offset: 0x001068F5
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

		// Token: 0x06005634 RID: 22068 RVA: 0x00108708 File Offset: 0x00106908
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06005635 RID: 22069 RVA: 0x00108711 File Offset: 0x00106911
		// (set) Token: 0x06005636 RID: 22070 RVA: 0x00108719 File Offset: 0x00106919
		public EntityId MemberId { get; set; }

		// Token: 0x06005637 RID: 22071 RVA: 0x00108722 File Offset: 0x00106922
		public void SetMemberId(EntityId val)
		{
			this.MemberId = val;
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06005638 RID: 22072 RVA: 0x0010872B File Offset: 0x0010692B
		// (set) Token: 0x06005639 RID: 22073 RVA: 0x00108733 File Offset: 0x00106933
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

		// Token: 0x0600563A RID: 22074 RVA: 0x00108743 File Offset: 0x00106943
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x0600563B RID: 22075 RVA: 0x0010874C File Offset: 0x0010694C
		// (set) Token: 0x0600563C RID: 22076 RVA: 0x00108754 File Offset: 0x00106954
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

		// Token: 0x0600563D RID: 22077 RVA: 0x00108767 File Offset: 0x00106967
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x0600563E RID: 22078 RVA: 0x00108770 File Offset: 0x00106970
		// (set) Token: 0x0600563F RID: 22079 RVA: 0x00108778 File Offset: 0x00106978
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

		// Token: 0x06005640 RID: 22080 RVA: 0x0010878B File Offset: 0x0010698B
		public void SetSubscriberId(SubscriberId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x00108794 File Offset: 0x00106994
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

		// Token: 0x06005642 RID: 22082 RVA: 0x00108818 File Offset: 0x00106A18
		public override bool Equals(object obj)
		{
			LeaveNotification leaveNotification = obj as LeaveNotification;
			return leaveNotification != null && this.HasAgentId == leaveNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(leaveNotification.AgentId)) && this.MemberId.Equals(leaveNotification.MemberId) && this.HasReason == leaveNotification.HasReason && (!this.HasReason || this.Reason.Equals(leaveNotification.Reason)) && this.HasChannelId == leaveNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(leaveNotification.ChannelId)) && this.HasSubscriberId == leaveNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(leaveNotification.SubscriberId));
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06005643 RID: 22083 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005644 RID: 22084 RVA: 0x001088F6 File Offset: 0x00106AF6
		public static LeaveNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LeaveNotification>(bs, 0, -1);
		}

		// Token: 0x06005645 RID: 22085 RVA: 0x00108900 File Offset: 0x00106B00
		public void Deserialize(Stream stream)
		{
			LeaveNotification.Deserialize(stream, this);
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x0010890A File Offset: 0x00106B0A
		public static LeaveNotification Deserialize(Stream stream, LeaveNotification instance)
		{
			return LeaveNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005647 RID: 22087 RVA: 0x00108918 File Offset: 0x00106B18
		public static LeaveNotification DeserializeLengthDelimited(Stream stream)
		{
			LeaveNotification leaveNotification = new LeaveNotification();
			LeaveNotification.DeserializeLengthDelimited(stream, leaveNotification);
			return leaveNotification;
		}

		// Token: 0x06005648 RID: 22088 RVA: 0x00108934 File Offset: 0x00106B34
		public static LeaveNotification DeserializeLengthDelimited(Stream stream, LeaveNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LeaveNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005649 RID: 22089 RVA: 0x0010895C File Offset: 0x00106B5C
		public static LeaveNotification Deserialize(Stream stream, LeaveNotification instance, long limit)
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

		// Token: 0x0600564A RID: 22090 RVA: 0x00108AB7 File Offset: 0x00106CB7
		public void Serialize(Stream stream)
		{
			LeaveNotification.Serialize(stream, this);
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x00108AC0 File Offset: 0x00106CC0
		public static void Serialize(Stream stream, LeaveNotification instance)
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

		// Token: 0x0600564C RID: 22092 RVA: 0x00108BB0 File Offset: 0x00106DB0
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

		// Token: 0x04001B21 RID: 6945
		public bool HasAgentId;

		// Token: 0x04001B22 RID: 6946
		private EntityId _AgentId;

		// Token: 0x04001B24 RID: 6948
		public bool HasReason;

		// Token: 0x04001B25 RID: 6949
		private uint _Reason;

		// Token: 0x04001B26 RID: 6950
		public bool HasChannelId;

		// Token: 0x04001B27 RID: 6951
		private ChannelId _ChannelId;

		// Token: 0x04001B28 RID: 6952
		public bool HasSubscriberId;

		// Token: 0x04001B29 RID: 6953
		private SubscriberId _SubscriberId;
	}
}
