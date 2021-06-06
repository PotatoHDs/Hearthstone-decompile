using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000466 RID: 1126
	public class MemberRemovedNotification : IProtoBuf
	{
		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x000EF66F File Offset: 0x000ED86F
		// (set) Token: 0x06004D1F RID: 19743 RVA: 0x000EF677 File Offset: 0x000ED877
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

		// Token: 0x06004D20 RID: 19744 RVA: 0x000EF68A File Offset: 0x000ED88A
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06004D21 RID: 19745 RVA: 0x000EF693 File Offset: 0x000ED893
		// (set) Token: 0x06004D22 RID: 19746 RVA: 0x000EF69B File Offset: 0x000ED89B
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

		// Token: 0x06004D23 RID: 19747 RVA: 0x000EF6AE File Offset: 0x000ED8AE
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06004D24 RID: 19748 RVA: 0x000EF6B7 File Offset: 0x000ED8B7
		// (set) Token: 0x06004D25 RID: 19749 RVA: 0x000EF6BF File Offset: 0x000ED8BF
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

		// Token: 0x06004D26 RID: 19750 RVA: 0x000EF6D2 File Offset: 0x000ED8D2
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06004D27 RID: 19751 RVA: 0x000EF6DB File Offset: 0x000ED8DB
		// (set) Token: 0x06004D28 RID: 19752 RVA: 0x000EF6E3 File Offset: 0x000ED8E3
		public GameAccountHandle MemberId
		{
			get
			{
				return this._MemberId;
			}
			set
			{
				this._MemberId = value;
				this.HasMemberId = (value != null);
			}
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x000EF6F6 File Offset: 0x000ED8F6
		public void SetMemberId(GameAccountHandle val)
		{
			this.MemberId = val;
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06004D2A RID: 19754 RVA: 0x000EF6FF File Offset: 0x000ED8FF
		// (set) Token: 0x06004D2B RID: 19755 RVA: 0x000EF707 File Offset: 0x000ED907
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

		// Token: 0x06004D2C RID: 19756 RVA: 0x000EF717 File Offset: 0x000ED917
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x000EF720 File Offset: 0x000ED920
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
			if (this.HasMemberId)
			{
				num ^= this.MemberId.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x000EF7AC File Offset: 0x000ED9AC
		public override bool Equals(object obj)
		{
			MemberRemovedNotification memberRemovedNotification = obj as MemberRemovedNotification;
			return memberRemovedNotification != null && this.HasAgentId == memberRemovedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(memberRemovedNotification.AgentId)) && this.HasSubscriberId == memberRemovedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(memberRemovedNotification.SubscriberId)) && this.HasChannelId == memberRemovedNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(memberRemovedNotification.ChannelId)) && this.HasMemberId == memberRemovedNotification.HasMemberId && (!this.HasMemberId || this.MemberId.Equals(memberRemovedNotification.MemberId)) && this.HasReason == memberRemovedNotification.HasReason && (!this.HasReason || this.Reason.Equals(memberRemovedNotification.Reason));
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06004D2F RID: 19759 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x000EF8A0 File Offset: 0x000EDAA0
		public static MemberRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x000EF8AA File Offset: 0x000EDAAA
		public void Deserialize(Stream stream)
		{
			MemberRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x000EF8B4 File Offset: 0x000EDAB4
		public static MemberRemovedNotification Deserialize(Stream stream, MemberRemovedNotification instance)
		{
			return MemberRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x000EF8C0 File Offset: 0x000EDAC0
		public static MemberRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberRemovedNotification memberRemovedNotification = new MemberRemovedNotification();
			MemberRemovedNotification.DeserializeLengthDelimited(stream, memberRemovedNotification);
			return memberRemovedNotification;
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x000EF8DC File Offset: 0x000EDADC
		public static MemberRemovedNotification DeserializeLengthDelimited(Stream stream, MemberRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x000EF904 File Offset: 0x000EDB04
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
						if (num != 34)
						{
							if (num == 40)
							{
								instance.Reason = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (instance.MemberId == null)
							{
								instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
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

		// Token: 0x06004D36 RID: 19766 RVA: 0x000EFA65 File Offset: 0x000EDC65
		public void Serialize(Stream stream)
		{
			MemberRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x000EFA70 File Offset: 0x000EDC70
		public static void Serialize(Stream stream, MemberRemovedNotification instance)
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
			if (instance.HasMemberId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x000EFB50 File Offset: 0x000EDD50
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
			if (this.HasMemberId)
			{
				num += 1U;
				uint serializedSize4 = this.MemberId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			return num;
		}

		// Token: 0x04001918 RID: 6424
		public bool HasAgentId;

		// Token: 0x04001919 RID: 6425
		private GameAccountHandle _AgentId;

		// Token: 0x0400191A RID: 6426
		public bool HasSubscriberId;

		// Token: 0x0400191B RID: 6427
		private GameAccountHandle _SubscriberId;

		// Token: 0x0400191C RID: 6428
		public bool HasChannelId;

		// Token: 0x0400191D RID: 6429
		private ChannelId _ChannelId;

		// Token: 0x0400191E RID: 6430
		public bool HasMemberId;

		// Token: 0x0400191F RID: 6431
		private GameAccountHandle _MemberId;

		// Token: 0x04001920 RID: 6432
		public bool HasReason;

		// Token: 0x04001921 RID: 6433
		private uint _Reason;
	}
}
