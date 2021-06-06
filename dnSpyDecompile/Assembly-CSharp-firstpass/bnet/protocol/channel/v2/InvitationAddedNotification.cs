using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200046D RID: 1133
	public class InvitationAddedNotification : IProtoBuf
	{
		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06004DDE RID: 19934 RVA: 0x000F1CC4 File Offset: 0x000EFEC4
		// (set) Token: 0x06004DDF RID: 19935 RVA: 0x000F1CCC File Offset: 0x000EFECC
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

		// Token: 0x06004DE0 RID: 19936 RVA: 0x000F1CDF File Offset: 0x000EFEDF
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06004DE1 RID: 19937 RVA: 0x000F1CE8 File Offset: 0x000EFEE8
		// (set) Token: 0x06004DE2 RID: 19938 RVA: 0x000F1CF0 File Offset: 0x000EFEF0
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

		// Token: 0x06004DE3 RID: 19939 RVA: 0x000F1D03 File Offset: 0x000EFF03
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06004DE4 RID: 19940 RVA: 0x000F1D0C File Offset: 0x000EFF0C
		// (set) Token: 0x06004DE5 RID: 19941 RVA: 0x000F1D14 File Offset: 0x000EFF14
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

		// Token: 0x06004DE6 RID: 19942 RVA: 0x000F1D27 File Offset: 0x000EFF27
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06004DE7 RID: 19943 RVA: 0x000F1D30 File Offset: 0x000EFF30
		// (set) Token: 0x06004DE8 RID: 19944 RVA: 0x000F1D38 File Offset: 0x000EFF38
		public ChannelInvitation Invitation
		{
			get
			{
				return this._Invitation;
			}
			set
			{
				this._Invitation = value;
				this.HasInvitation = (value != null);
			}
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x000F1D4B File Offset: 0x000EFF4B
		public void SetInvitation(ChannelInvitation val)
		{
			this.Invitation = val;
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x000F1D54 File Offset: 0x000EFF54
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
			if (this.HasInvitation)
			{
				num ^= this.Invitation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x000F1DC8 File Offset: 0x000EFFC8
		public override bool Equals(object obj)
		{
			InvitationAddedNotification invitationAddedNotification = obj as InvitationAddedNotification;
			return invitationAddedNotification != null && this.HasAgentId == invitationAddedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(invitationAddedNotification.AgentId)) && this.HasSubscriberId == invitationAddedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(invitationAddedNotification.SubscriberId)) && this.HasChannelId == invitationAddedNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(invitationAddedNotification.ChannelId)) && this.HasInvitation == invitationAddedNotification.HasInvitation && (!this.HasInvitation || this.Invitation.Equals(invitationAddedNotification.Invitation));
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06004DEC RID: 19948 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x000F1E8E File Offset: 0x000F008E
		public static InvitationAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x000F1E98 File Offset: 0x000F0098
		public void Deserialize(Stream stream)
		{
			InvitationAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x000F1EA2 File Offset: 0x000F00A2
		public static InvitationAddedNotification Deserialize(Stream stream, InvitationAddedNotification instance)
		{
			return InvitationAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x000F1EB0 File Offset: 0x000F00B0
		public static InvitationAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			InvitationAddedNotification invitationAddedNotification = new InvitationAddedNotification();
			InvitationAddedNotification.DeserializeLengthDelimited(stream, invitationAddedNotification);
			return invitationAddedNotification;
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x000F1ECC File Offset: 0x000F00CC
		public static InvitationAddedNotification DeserializeLengthDelimited(Stream stream, InvitationAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x000F1EF4 File Offset: 0x000F00F4
		public static InvitationAddedNotification Deserialize(Stream stream, InvitationAddedNotification instance, long limit)
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
							if (instance.Invitation == null)
							{
								instance.Invitation = ChannelInvitation.DeserializeLengthDelimited(stream);
								continue;
							}
							ChannelInvitation.DeserializeLengthDelimited(stream, instance.Invitation);
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

		// Token: 0x06004DF3 RID: 19955 RVA: 0x000F2039 File Offset: 0x000F0239
		public void Serialize(Stream stream)
		{
			InvitationAddedNotification.Serialize(stream, this);
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x000F2044 File Offset: 0x000F0244
		public static void Serialize(Stream stream, InvitationAddedNotification instance)
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
			if (instance.HasInvitation)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
				ChannelInvitation.Serialize(stream, instance.Invitation);
			}
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x000F2108 File Offset: 0x000F0308
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
			if (this.HasInvitation)
			{
				num += 1U;
				uint serializedSize4 = this.Invitation.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		// Token: 0x04001954 RID: 6484
		public bool HasAgentId;

		// Token: 0x04001955 RID: 6485
		private GameAccountHandle _AgentId;

		// Token: 0x04001956 RID: 6486
		public bool HasSubscriberId;

		// Token: 0x04001957 RID: 6487
		private GameAccountHandle _SubscriberId;

		// Token: 0x04001958 RID: 6488
		public bool HasChannelId;

		// Token: 0x04001959 RID: 6489
		private ChannelId _ChannelId;

		// Token: 0x0400195A RID: 6490
		public bool HasInvitation;

		// Token: 0x0400195B RID: 6491
		private ChannelInvitation _Invitation;
	}
}
