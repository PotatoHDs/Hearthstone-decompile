using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2.membership
{
	// Token: 0x020004AA RID: 1194
	public class ReceivedInvitationAddedNotification : IProtoBuf
	{
		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06005343 RID: 21315 RVA: 0x00100FF8 File Offset: 0x000FF1F8
		// (set) Token: 0x06005344 RID: 21316 RVA: 0x00101000 File Offset: 0x000FF200
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

		// Token: 0x06005345 RID: 21317 RVA: 0x00101013 File Offset: 0x000FF213
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06005346 RID: 21318 RVA: 0x0010101C File Offset: 0x000FF21C
		// (set) Token: 0x06005347 RID: 21319 RVA: 0x00101024 File Offset: 0x000FF224
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

		// Token: 0x06005348 RID: 21320 RVA: 0x00101037 File Offset: 0x000FF237
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06005349 RID: 21321 RVA: 0x00101040 File Offset: 0x000FF240
		// (set) Token: 0x0600534A RID: 21322 RVA: 0x00101048 File Offset: 0x000FF248
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

		// Token: 0x0600534B RID: 21323 RVA: 0x0010105B File Offset: 0x000FF25B
		public void SetInvitation(ChannelInvitation val)
		{
			this.Invitation = val;
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x00101064 File Offset: 0x000FF264
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
			if (this.HasInvitation)
			{
				num ^= this.Invitation.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x001010C0 File Offset: 0x000FF2C0
		public override bool Equals(object obj)
		{
			ReceivedInvitationAddedNotification receivedInvitationAddedNotification = obj as ReceivedInvitationAddedNotification;
			return receivedInvitationAddedNotification != null && this.HasAgentId == receivedInvitationAddedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(receivedInvitationAddedNotification.AgentId)) && this.HasSubscriberId == receivedInvitationAddedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(receivedInvitationAddedNotification.SubscriberId)) && this.HasInvitation == receivedInvitationAddedNotification.HasInvitation && (!this.HasInvitation || this.Invitation.Equals(receivedInvitationAddedNotification.Invitation));
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x0600534E RID: 21326 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0010115B File Offset: 0x000FF35B
		public static ReceivedInvitationAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReceivedInvitationAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x00101165 File Offset: 0x000FF365
		public void Deserialize(Stream stream)
		{
			ReceivedInvitationAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0010116F File Offset: 0x000FF36F
		public static ReceivedInvitationAddedNotification Deserialize(Stream stream, ReceivedInvitationAddedNotification instance)
		{
			return ReceivedInvitationAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0010117C File Offset: 0x000FF37C
		public static ReceivedInvitationAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			ReceivedInvitationAddedNotification receivedInvitationAddedNotification = new ReceivedInvitationAddedNotification();
			ReceivedInvitationAddedNotification.DeserializeLengthDelimited(stream, receivedInvitationAddedNotification);
			return receivedInvitationAddedNotification;
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x00101198 File Offset: 0x000FF398
		public static ReceivedInvitationAddedNotification DeserializeLengthDelimited(Stream stream, ReceivedInvitationAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReceivedInvitationAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x001011C0 File Offset: 0x000FF3C0
		public static ReceivedInvitationAddedNotification Deserialize(Stream stream, ReceivedInvitationAddedNotification instance, long limit)
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
						else if (instance.Invitation == null)
						{
							instance.Invitation = ChannelInvitation.DeserializeLengthDelimited(stream);
						}
						else
						{
							ChannelInvitation.DeserializeLengthDelimited(stream, instance.Invitation);
						}
					}
					else if (instance.SubscriberId == null)
					{
						instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
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

		// Token: 0x06005355 RID: 21333 RVA: 0x001012C2 File Offset: 0x000FF4C2
		public void Serialize(Stream stream)
		{
			ReceivedInvitationAddedNotification.Serialize(stream, this);
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x001012CC File Offset: 0x000FF4CC
		public static void Serialize(Stream stream, ReceivedInvitationAddedNotification instance)
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
			if (instance.HasInvitation)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
				ChannelInvitation.Serialize(stream, instance.Invitation);
			}
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x00101360 File Offset: 0x000FF560
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
			if (this.HasInvitation)
			{
				num += 1U;
				uint serializedSize3 = this.Invitation.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001A90 RID: 6800
		public bool HasAgentId;

		// Token: 0x04001A91 RID: 6801
		private GameAccountHandle _AgentId;

		// Token: 0x04001A92 RID: 6802
		public bool HasSubscriberId;

		// Token: 0x04001A93 RID: 6803
		private GameAccountHandle _SubscriberId;

		// Token: 0x04001A94 RID: 6804
		public bool HasInvitation;

		// Token: 0x04001A95 RID: 6805
		private ChannelInvitation _Invitation;
	}
}
