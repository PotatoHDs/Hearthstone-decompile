using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004D0 RID: 1232
	public class UpdateChannelStateNotification : IProtoBuf
	{
		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06005689 RID: 22153 RVA: 0x00109820 File Offset: 0x00107A20
		// (set) Token: 0x0600568A RID: 22154 RVA: 0x00109828 File Offset: 0x00107A28
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

		// Token: 0x0600568B RID: 22155 RVA: 0x0010983B File Offset: 0x00107A3B
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x0600568C RID: 22156 RVA: 0x00109844 File Offset: 0x00107A44
		// (set) Token: 0x0600568D RID: 22157 RVA: 0x0010984C File Offset: 0x00107A4C
		public ChannelState StateChange { get; set; }

		// Token: 0x0600568E RID: 22158 RVA: 0x00109855 File Offset: 0x00107A55
		public void SetStateChange(ChannelState val)
		{
			this.StateChange = val;
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x0600568F RID: 22159 RVA: 0x0010985E File Offset: 0x00107A5E
		// (set) Token: 0x06005690 RID: 22160 RVA: 0x00109866 File Offset: 0x00107A66
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

		// Token: 0x06005691 RID: 22161 RVA: 0x00109879 File Offset: 0x00107A79
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06005692 RID: 22162 RVA: 0x00109882 File Offset: 0x00107A82
		// (set) Token: 0x06005693 RID: 22163 RVA: 0x0010988A File Offset: 0x00107A8A
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

		// Token: 0x06005694 RID: 22164 RVA: 0x0010989D File Offset: 0x00107A9D
		public void SetSubscriberId(SubscriberId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06005695 RID: 22165 RVA: 0x001098A6 File Offset: 0x00107AA6
		// (set) Token: 0x06005696 RID: 22166 RVA: 0x001098AE File Offset: 0x00107AAE
		public AccountId PresenceSubscriberId
		{
			get
			{
				return this._PresenceSubscriberId;
			}
			set
			{
				this._PresenceSubscriberId = value;
				this.HasPresenceSubscriberId = (value != null);
			}
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x001098C1 File Offset: 0x00107AC1
		public void SetPresenceSubscriberId(AccountId val)
		{
			this.PresenceSubscriberId = val;
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x001098CC File Offset: 0x00107ACC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.StateChange.GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasPresenceSubscriberId)
			{
				num ^= this.PresenceSubscriberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x0010994C File Offset: 0x00107B4C
		public override bool Equals(object obj)
		{
			UpdateChannelStateNotification updateChannelStateNotification = obj as UpdateChannelStateNotification;
			return updateChannelStateNotification != null && this.HasAgentId == updateChannelStateNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(updateChannelStateNotification.AgentId)) && this.StateChange.Equals(updateChannelStateNotification.StateChange) && this.HasChannelId == updateChannelStateNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(updateChannelStateNotification.ChannelId)) && this.HasSubscriberId == updateChannelStateNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(updateChannelStateNotification.SubscriberId)) && this.HasPresenceSubscriberId == updateChannelStateNotification.HasPresenceSubscriberId && (!this.HasPresenceSubscriberId || this.PresenceSubscriberId.Equals(updateChannelStateNotification.PresenceSubscriberId));
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x0600569A RID: 22170 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x00109A27 File Offset: 0x00107C27
		public static UpdateChannelStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateChannelStateNotification>(bs, 0, -1);
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x00109A31 File Offset: 0x00107C31
		public void Deserialize(Stream stream)
		{
			UpdateChannelStateNotification.Deserialize(stream, this);
		}

		// Token: 0x0600569D RID: 22173 RVA: 0x00109A3B File Offset: 0x00107C3B
		public static UpdateChannelStateNotification Deserialize(Stream stream, UpdateChannelStateNotification instance)
		{
			return UpdateChannelStateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600569E RID: 22174 RVA: 0x00109A48 File Offset: 0x00107C48
		public static UpdateChannelStateNotification DeserializeLengthDelimited(Stream stream)
		{
			UpdateChannelStateNotification updateChannelStateNotification = new UpdateChannelStateNotification();
			UpdateChannelStateNotification.DeserializeLengthDelimited(stream, updateChannelStateNotification);
			return updateChannelStateNotification;
		}

		// Token: 0x0600569F RID: 22175 RVA: 0x00109A64 File Offset: 0x00107C64
		public static UpdateChannelStateNotification DeserializeLengthDelimited(Stream stream, UpdateChannelStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateChannelStateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x060056A0 RID: 22176 RVA: 0x00109A8C File Offset: 0x00107C8C
		public static UpdateChannelStateNotification Deserialize(Stream stream, UpdateChannelStateNotification instance, long limit)
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
								if (instance.StateChange == null)
								{
									instance.StateChange = ChannelState.DeserializeLengthDelimited(stream);
									continue;
								}
								ChannelState.DeserializeLengthDelimited(stream, instance.StateChange);
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
					else if (num != 26)
					{
						if (num != 34)
						{
							if (num == 42)
							{
								if (instance.PresenceSubscriberId == null)
								{
									instance.PresenceSubscriberId = AccountId.DeserializeLengthDelimited(stream);
									continue;
								}
								AccountId.DeserializeLengthDelimited(stream, instance.PresenceSubscriberId);
								continue;
							}
						}
						else
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

		// Token: 0x060056A1 RID: 22177 RVA: 0x00109C07 File Offset: 0x00107E07
		public void Serialize(Stream stream)
		{
			UpdateChannelStateNotification.Serialize(stream, this);
		}

		// Token: 0x060056A2 RID: 22178 RVA: 0x00109C10 File Offset: 0x00107E10
		public static void Serialize(Stream stream, UpdateChannelStateNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.StateChange == null)
			{
				throw new ArgumentNullException("StateChange", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.StateChange.GetSerializedSize());
			ChannelState.Serialize(stream, instance.StateChange);
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasPresenceSubscriberId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.PresenceSubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.PresenceSubscriberId);
			}
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x00109D10 File Offset: 0x00107F10
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.StateChange.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
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
			if (this.HasPresenceSubscriberId)
			{
				num += 1U;
				uint serializedSize5 = this.PresenceSubscriberId.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			return num + 1U;
		}

		// Token: 0x04001B3E RID: 6974
		public bool HasAgentId;

		// Token: 0x04001B3F RID: 6975
		private EntityId _AgentId;

		// Token: 0x04001B41 RID: 6977
		public bool HasChannelId;

		// Token: 0x04001B42 RID: 6978
		private ChannelId _ChannelId;

		// Token: 0x04001B43 RID: 6979
		public bool HasSubscriberId;

		// Token: 0x04001B44 RID: 6980
		private SubscriberId _SubscriberId;

		// Token: 0x04001B45 RID: 6981
		public bool HasPresenceSubscriberId;

		// Token: 0x04001B46 RID: 6982
		private AccountId _PresenceSubscriberId;
	}
}
