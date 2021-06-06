using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200046E RID: 1134
	public class InvitationRemovedNotification : IProtoBuf
	{
		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06004DF7 RID: 19959 RVA: 0x000F21A7 File Offset: 0x000F03A7
		// (set) Token: 0x06004DF8 RID: 19960 RVA: 0x000F21AF File Offset: 0x000F03AF
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

		// Token: 0x06004DF9 RID: 19961 RVA: 0x000F21C2 File Offset: 0x000F03C2
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x000F21CB File Offset: 0x000F03CB
		// (set) Token: 0x06004DFB RID: 19963 RVA: 0x000F21D3 File Offset: 0x000F03D3
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

		// Token: 0x06004DFC RID: 19964 RVA: 0x000F21E6 File Offset: 0x000F03E6
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06004DFD RID: 19965 RVA: 0x000F21EF File Offset: 0x000F03EF
		// (set) Token: 0x06004DFE RID: 19966 RVA: 0x000F21F7 File Offset: 0x000F03F7
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

		// Token: 0x06004DFF RID: 19967 RVA: 0x000F220A File Offset: 0x000F040A
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06004E00 RID: 19968 RVA: 0x000F2213 File Offset: 0x000F0413
		// (set) Token: 0x06004E01 RID: 19969 RVA: 0x000F221B File Offset: 0x000F041B
		public ulong InvitationId
		{
			get
			{
				return this._InvitationId;
			}
			set
			{
				this._InvitationId = value;
				this.HasInvitationId = true;
			}
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x000F222B File Offset: 0x000F042B
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06004E03 RID: 19971 RVA: 0x000F2234 File Offset: 0x000F0434
		// (set) Token: 0x06004E04 RID: 19972 RVA: 0x000F223C File Offset: 0x000F043C
		public InvitationRemovedReason Reason
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

		// Token: 0x06004E05 RID: 19973 RVA: 0x000F224C File Offset: 0x000F044C
		public void SetReason(InvitationRemovedReason val)
		{
			this.Reason = val;
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x000F2258 File Offset: 0x000F0458
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
			if (this.HasInvitationId)
			{
				num ^= this.InvitationId.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x000F22EC File Offset: 0x000F04EC
		public override bool Equals(object obj)
		{
			InvitationRemovedNotification invitationRemovedNotification = obj as InvitationRemovedNotification;
			return invitationRemovedNotification != null && this.HasAgentId == invitationRemovedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(invitationRemovedNotification.AgentId)) && this.HasSubscriberId == invitationRemovedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(invitationRemovedNotification.SubscriberId)) && this.HasChannelId == invitationRemovedNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(invitationRemovedNotification.ChannelId)) && this.HasInvitationId == invitationRemovedNotification.HasInvitationId && (!this.HasInvitationId || this.InvitationId.Equals(invitationRemovedNotification.InvitationId)) && this.HasReason == invitationRemovedNotification.HasReason && (!this.HasReason || this.Reason.Equals(invitationRemovedNotification.Reason));
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06004E08 RID: 19976 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x000F23EE File Offset: 0x000F05EE
		public static InvitationRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x000F23F8 File Offset: 0x000F05F8
		public void Deserialize(Stream stream)
		{
			InvitationRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x000F2402 File Offset: 0x000F0602
		public static InvitationRemovedNotification Deserialize(Stream stream, InvitationRemovedNotification instance)
		{
			return InvitationRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x000F2410 File Offset: 0x000F0610
		public static InvitationRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			InvitationRemovedNotification invitationRemovedNotification = new InvitationRemovedNotification();
			InvitationRemovedNotification.DeserializeLengthDelimited(stream, invitationRemovedNotification);
			return invitationRemovedNotification;
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x000F242C File Offset: 0x000F062C
		public static InvitationRemovedNotification DeserializeLengthDelimited(Stream stream, InvitationRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x000F2454 File Offset: 0x000F0654
		public static InvitationRemovedNotification Deserialize(Stream stream, InvitationRemovedNotification instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Reason = InvitationRemovedReason.INVITATION_REMOVED_REASON_ACCEPTED;
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
						if (num == 33)
						{
							instance.InvitationId = binaryReader.ReadUInt64();
							continue;
						}
						if (num == 40)
						{
							instance.Reason = (InvitationRemovedReason)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06004E0F RID: 19983 RVA: 0x000F25AA File Offset: 0x000F07AA
		public void Serialize(Stream stream)
		{
			InvitationRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x000F25B4 File Offset: 0x000F07B4
		public static void Serialize(Stream stream, InvitationRemovedNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
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
			if (instance.HasInvitationId)
			{
				stream.WriteByte(33);
				binaryWriter.Write(instance.InvitationId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Reason));
			}
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x000F2688 File Offset: 0x000F0888
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
			if (this.HasInvitationId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Reason));
			}
			return num;
		}

		// Token: 0x0400195C RID: 6492
		public bool HasAgentId;

		// Token: 0x0400195D RID: 6493
		private GameAccountHandle _AgentId;

		// Token: 0x0400195E RID: 6494
		public bool HasSubscriberId;

		// Token: 0x0400195F RID: 6495
		private GameAccountHandle _SubscriberId;

		// Token: 0x04001960 RID: 6496
		public bool HasChannelId;

		// Token: 0x04001961 RID: 6497
		private ChannelId _ChannelId;

		// Token: 0x04001962 RID: 6498
		public bool HasInvitationId;

		// Token: 0x04001963 RID: 6499
		private ulong _InvitationId;

		// Token: 0x04001964 RID: 6500
		public bool HasReason;

		// Token: 0x04001965 RID: 6501
		private InvitationRemovedReason _Reason;
	}
}
