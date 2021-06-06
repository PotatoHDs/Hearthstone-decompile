using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000465 RID: 1125
	public class MemberAddedNotification : IProtoBuf
	{
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06004D05 RID: 19717 RVA: 0x000EF18B File Offset: 0x000ED38B
		// (set) Token: 0x06004D06 RID: 19718 RVA: 0x000EF193 File Offset: 0x000ED393
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

		// Token: 0x06004D07 RID: 19719 RVA: 0x000EF1A6 File Offset: 0x000ED3A6
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06004D08 RID: 19720 RVA: 0x000EF1AF File Offset: 0x000ED3AF
		// (set) Token: 0x06004D09 RID: 19721 RVA: 0x000EF1B7 File Offset: 0x000ED3B7
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

		// Token: 0x06004D0A RID: 19722 RVA: 0x000EF1CA File Offset: 0x000ED3CA
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06004D0B RID: 19723 RVA: 0x000EF1D3 File Offset: 0x000ED3D3
		// (set) Token: 0x06004D0C RID: 19724 RVA: 0x000EF1DB File Offset: 0x000ED3DB
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

		// Token: 0x06004D0D RID: 19725 RVA: 0x000EF1EE File Offset: 0x000ED3EE
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06004D0E RID: 19726 RVA: 0x000EF1F7 File Offset: 0x000ED3F7
		// (set) Token: 0x06004D0F RID: 19727 RVA: 0x000EF1FF File Offset: 0x000ED3FF
		public Member Member
		{
			get
			{
				return this._Member;
			}
			set
			{
				this._Member = value;
				this.HasMember = (value != null);
			}
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x000EF212 File Offset: 0x000ED412
		public void SetMember(Member val)
		{
			this.Member = val;
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x000EF21C File Offset: 0x000ED41C
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
			if (this.HasMember)
			{
				num ^= this.Member.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x000EF290 File Offset: 0x000ED490
		public override bool Equals(object obj)
		{
			MemberAddedNotification memberAddedNotification = obj as MemberAddedNotification;
			return memberAddedNotification != null && this.HasAgentId == memberAddedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(memberAddedNotification.AgentId)) && this.HasSubscriberId == memberAddedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(memberAddedNotification.SubscriberId)) && this.HasChannelId == memberAddedNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(memberAddedNotification.ChannelId)) && this.HasMember == memberAddedNotification.HasMember && (!this.HasMember || this.Member.Equals(memberAddedNotification.Member));
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06004D13 RID: 19731 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x000EF356 File Offset: 0x000ED556
		public static MemberAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x000EF360 File Offset: 0x000ED560
		public void Deserialize(Stream stream)
		{
			MemberAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x000EF36A File Offset: 0x000ED56A
		public static MemberAddedNotification Deserialize(Stream stream, MemberAddedNotification instance)
		{
			return MemberAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x000EF378 File Offset: 0x000ED578
		public static MemberAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberAddedNotification memberAddedNotification = new MemberAddedNotification();
			MemberAddedNotification.DeserializeLengthDelimited(stream, memberAddedNotification);
			return memberAddedNotification;
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x000EF394 File Offset: 0x000ED594
		public static MemberAddedNotification DeserializeLengthDelimited(Stream stream, MemberAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004D19 RID: 19737 RVA: 0x000EF3BC File Offset: 0x000ED5BC
		public static MemberAddedNotification Deserialize(Stream stream, MemberAddedNotification instance, long limit)
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
							if (instance.Member == null)
							{
								instance.Member = Member.DeserializeLengthDelimited(stream);
								continue;
							}
							Member.DeserializeLengthDelimited(stream, instance.Member);
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

		// Token: 0x06004D1A RID: 19738 RVA: 0x000EF501 File Offset: 0x000ED701
		public void Serialize(Stream stream)
		{
			MemberAddedNotification.Serialize(stream, this);
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x000EF50C File Offset: 0x000ED70C
		public static void Serialize(Stream stream, MemberAddedNotification instance)
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
			if (instance.HasMember)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Member.GetSerializedSize());
				Member.Serialize(stream, instance.Member);
			}
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x000EF5D0 File Offset: 0x000ED7D0
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
			if (this.HasMember)
			{
				num += 1U;
				uint serializedSize4 = this.Member.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		// Token: 0x04001910 RID: 6416
		public bool HasAgentId;

		// Token: 0x04001911 RID: 6417
		private GameAccountHandle _AgentId;

		// Token: 0x04001912 RID: 6418
		public bool HasSubscriberId;

		// Token: 0x04001913 RID: 6419
		private GameAccountHandle _SubscriberId;

		// Token: 0x04001914 RID: 6420
		public bool HasChannelId;

		// Token: 0x04001915 RID: 6421
		private ChannelId _ChannelId;

		// Token: 0x04001916 RID: 6422
		public bool HasMember;

		// Token: 0x04001917 RID: 6423
		private Member _Member;
	}
}
