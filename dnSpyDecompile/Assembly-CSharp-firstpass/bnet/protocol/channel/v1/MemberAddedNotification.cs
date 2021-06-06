using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004CC RID: 1228
	public class MemberAddedNotification : IProtoBuf
	{
		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x0600561C RID: 22044 RVA: 0x0010832B File Offset: 0x0010652B
		// (set) Token: 0x0600561D RID: 22045 RVA: 0x00108333 File Offset: 0x00106533
		public Member Member { get; set; }

		// Token: 0x0600561E RID: 22046 RVA: 0x0010833C File Offset: 0x0010653C
		public void SetMember(Member val)
		{
			this.Member = val;
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x0600561F RID: 22047 RVA: 0x00108345 File Offset: 0x00106545
		// (set) Token: 0x06005620 RID: 22048 RVA: 0x0010834D File Offset: 0x0010654D
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

		// Token: 0x06005621 RID: 22049 RVA: 0x00108360 File Offset: 0x00106560
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06005622 RID: 22050 RVA: 0x00108369 File Offset: 0x00106569
		// (set) Token: 0x06005623 RID: 22051 RVA: 0x00108371 File Offset: 0x00106571
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

		// Token: 0x06005624 RID: 22052 RVA: 0x00108384 File Offset: 0x00106584
		public void SetSubscriberId(SubscriberId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x00108390 File Offset: 0x00106590
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Member.GetHashCode();
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

		// Token: 0x06005626 RID: 22054 RVA: 0x001083E4 File Offset: 0x001065E4
		public override bool Equals(object obj)
		{
			MemberAddedNotification memberAddedNotification = obj as MemberAddedNotification;
			return memberAddedNotification != null && this.Member.Equals(memberAddedNotification.Member) && this.HasChannelId == memberAddedNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(memberAddedNotification.ChannelId)) && this.HasSubscriberId == memberAddedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(memberAddedNotification.SubscriberId));
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06005627 RID: 22055 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x00108469 File Offset: 0x00106669
		public static MemberAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x00108473 File Offset: 0x00106673
		public void Deserialize(Stream stream)
		{
			MemberAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x0010847D File Offset: 0x0010667D
		public static MemberAddedNotification Deserialize(Stream stream, MemberAddedNotification instance)
		{
			return MemberAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x00108488 File Offset: 0x00106688
		public static MemberAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberAddedNotification memberAddedNotification = new MemberAddedNotification();
			MemberAddedNotification.DeserializeLengthDelimited(stream, memberAddedNotification);
			return memberAddedNotification;
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x001084A4 File Offset: 0x001066A4
		public static MemberAddedNotification DeserializeLengthDelimited(Stream stream, MemberAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x001084CC File Offset: 0x001066CC
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
						else if (instance.SubscriberId == null)
						{
							instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
						}
						else
						{
							SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
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
				else if (instance.Member == null)
				{
					instance.Member = Member.DeserializeLengthDelimited(stream);
				}
				else
				{
					Member.DeserializeLengthDelimited(stream, instance.Member);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x001085CE File Offset: 0x001067CE
		public void Serialize(Stream stream)
		{
			MemberAddedNotification.Serialize(stream, this);
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x001085D8 File Offset: 0x001067D8
		public static void Serialize(Stream stream, MemberAddedNotification instance)
		{
			if (instance.Member == null)
			{
				throw new ArgumentNullException("Member", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Member.GetSerializedSize());
			Member.Serialize(stream, instance.Member);
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x0010867C File Offset: 0x0010687C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Member.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize3 = this.SubscriberId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 1U;
		}

		// Token: 0x04001B1D RID: 6941
		public bool HasChannelId;

		// Token: 0x04001B1E RID: 6942
		private ChannelId _ChannelId;

		// Token: 0x04001B1F RID: 6943
		public bool HasSubscriberId;

		// Token: 0x04001B20 RID: 6944
		private SubscriberId _SubscriberId;
	}
}
