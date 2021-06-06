using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004CB RID: 1227
	public class JoinNotification : IProtoBuf
	{
		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060055F9 RID: 22009 RVA: 0x00107B8D File Offset: 0x00105D8D
		// (set) Token: 0x060055FA RID: 22010 RVA: 0x00107B95 File Offset: 0x00105D95
		public Member Self
		{
			get
			{
				return this._Self;
			}
			set
			{
				this._Self = value;
				this.HasSelf = (value != null);
			}
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x00107BA8 File Offset: 0x00105DA8
		public void SetSelf(Member val)
		{
			this.Self = val;
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x060055FC RID: 22012 RVA: 0x00107BB1 File Offset: 0x00105DB1
		// (set) Token: 0x060055FD RID: 22013 RVA: 0x00107BB9 File Offset: 0x00105DB9
		public List<Member> Member
		{
			get
			{
				return this._Member;
			}
			set
			{
				this._Member = value;
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x060055FE RID: 22014 RVA: 0x00107BB1 File Offset: 0x00105DB1
		public List<Member> MemberList
		{
			get
			{
				return this._Member;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x060055FF RID: 22015 RVA: 0x00107BC2 File Offset: 0x00105DC2
		public int MemberCount
		{
			get
			{
				return this._Member.Count;
			}
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x00107BCF File Offset: 0x00105DCF
		public void AddMember(Member val)
		{
			this._Member.Add(val);
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x00107BDD File Offset: 0x00105DDD
		public void ClearMember()
		{
			this._Member.Clear();
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x00107BEA File Offset: 0x00105DEA
		public void SetMember(List<Member> val)
		{
			this.Member = val;
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06005603 RID: 22019 RVA: 0x00107BF3 File Offset: 0x00105DF3
		// (set) Token: 0x06005604 RID: 22020 RVA: 0x00107BFB File Offset: 0x00105DFB
		public ChannelState ChannelState { get; set; }

		// Token: 0x06005605 RID: 22021 RVA: 0x00107C04 File Offset: 0x00105E04
		public void SetChannelState(ChannelState val)
		{
			this.ChannelState = val;
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06005606 RID: 22022 RVA: 0x00107C0D File Offset: 0x00105E0D
		// (set) Token: 0x06005607 RID: 22023 RVA: 0x00107C15 File Offset: 0x00105E15
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

		// Token: 0x06005608 RID: 22024 RVA: 0x00107C28 File Offset: 0x00105E28
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06005609 RID: 22025 RVA: 0x00107C31 File Offset: 0x00105E31
		// (set) Token: 0x0600560A RID: 22026 RVA: 0x00107C39 File Offset: 0x00105E39
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

		// Token: 0x0600560B RID: 22027 RVA: 0x00107C4C File Offset: 0x00105E4C
		public void SetSubscriberId(SubscriberId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x0600560C RID: 22028 RVA: 0x00107C55 File Offset: 0x00105E55
		// (set) Token: 0x0600560D RID: 22029 RVA: 0x00107C5D File Offset: 0x00105E5D
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

		// Token: 0x0600560E RID: 22030 RVA: 0x00107C70 File Offset: 0x00105E70
		public void SetPresenceSubscriberId(AccountId val)
		{
			this.PresenceSubscriberId = val;
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x00107C7C File Offset: 0x00105E7C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSelf)
			{
				num ^= this.Self.GetHashCode();
			}
			foreach (Member member in this.Member)
			{
				num ^= member.GetHashCode();
			}
			num ^= this.ChannelState.GetHashCode();
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

		// Token: 0x06005610 RID: 22032 RVA: 0x00107D44 File Offset: 0x00105F44
		public override bool Equals(object obj)
		{
			JoinNotification joinNotification = obj as JoinNotification;
			if (joinNotification == null)
			{
				return false;
			}
			if (this.HasSelf != joinNotification.HasSelf || (this.HasSelf && !this.Self.Equals(joinNotification.Self)))
			{
				return false;
			}
			if (this.Member.Count != joinNotification.Member.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Member.Count; i++)
			{
				if (!this.Member[i].Equals(joinNotification.Member[i]))
				{
					return false;
				}
			}
			return this.ChannelState.Equals(joinNotification.ChannelState) && this.HasChannelId == joinNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(joinNotification.ChannelId)) && this.HasSubscriberId == joinNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(joinNotification.SubscriberId)) && this.HasPresenceSubscriberId == joinNotification.HasPresenceSubscriberId && (!this.HasPresenceSubscriberId || this.PresenceSubscriberId.Equals(joinNotification.PresenceSubscriberId));
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06005611 RID: 22033 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x00107E70 File Offset: 0x00106070
		public static JoinNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinNotification>(bs, 0, -1);
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x00107E7A File Offset: 0x0010607A
		public void Deserialize(Stream stream)
		{
			JoinNotification.Deserialize(stream, this);
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x00107E84 File Offset: 0x00106084
		public static JoinNotification Deserialize(Stream stream, JoinNotification instance)
		{
			return JoinNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x00107E90 File Offset: 0x00106090
		public static JoinNotification DeserializeLengthDelimited(Stream stream)
		{
			JoinNotification joinNotification = new JoinNotification();
			JoinNotification.DeserializeLengthDelimited(stream, joinNotification);
			return joinNotification;
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x00107EAC File Offset: 0x001060AC
		public static JoinNotification DeserializeLengthDelimited(Stream stream, JoinNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x00107ED4 File Offset: 0x001060D4
		public static JoinNotification Deserialize(Stream stream, JoinNotification instance, long limit)
		{
			if (instance.Member == null)
			{
				instance.Member = new List<Member>();
			}
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
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.Member.Add(bnet.protocol.channel.v1.Member.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 26)
							{
								if (instance.ChannelState == null)
								{
									instance.ChannelState = ChannelState.DeserializeLengthDelimited(stream);
									continue;
								}
								ChannelState.DeserializeLengthDelimited(stream, instance.ChannelState);
								continue;
							}
						}
						else
						{
							if (instance.Self == null)
							{
								instance.Self = bnet.protocol.channel.v1.Member.DeserializeLengthDelimited(stream);
								continue;
							}
							bnet.protocol.channel.v1.Member.DeserializeLengthDelimited(stream, instance.Self);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num != 42)
						{
							if (num == 50)
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

		// Token: 0x06005618 RID: 22040 RVA: 0x00108080 File Offset: 0x00106280
		public void Serialize(Stream stream)
		{
			JoinNotification.Serialize(stream, this);
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x0010808C File Offset: 0x0010628C
		public static void Serialize(Stream stream, JoinNotification instance)
		{
			if (instance.HasSelf)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Self.GetSerializedSize());
				bnet.protocol.channel.v1.Member.Serialize(stream, instance.Self);
			}
			if (instance.Member.Count > 0)
			{
				foreach (Member member in instance.Member)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, member.GetSerializedSize());
					bnet.protocol.channel.v1.Member.Serialize(stream, member);
				}
			}
			if (instance.ChannelState == null)
			{
				throw new ArgumentNullException("ChannelState", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.ChannelState.GetSerializedSize());
			ChannelState.Serialize(stream, instance.ChannelState);
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
			if (instance.HasPresenceSubscriberId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.PresenceSubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.PresenceSubscriberId);
			}
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x001081F4 File Offset: 0x001063F4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSelf)
			{
				num += 1U;
				uint serializedSize = this.Self.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Member.Count > 0)
			{
				foreach (Member member in this.Member)
				{
					num += 1U;
					uint serializedSize2 = member.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			uint serializedSize3 = this.ChannelState.GetSerializedSize();
			num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize4 = this.ChannelId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize5 = this.SubscriberId.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasPresenceSubscriberId)
			{
				num += 1U;
				uint serializedSize6 = this.PresenceSubscriberId.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			num += 1U;
			return num;
		}

		// Token: 0x04001B12 RID: 6930
		public bool HasSelf;

		// Token: 0x04001B13 RID: 6931
		private Member _Self;

		// Token: 0x04001B14 RID: 6932
		private List<Member> _Member = new List<Member>();

		// Token: 0x04001B16 RID: 6934
		public bool HasChannelId;

		// Token: 0x04001B17 RID: 6935
		private ChannelId _ChannelId;

		// Token: 0x04001B18 RID: 6936
		public bool HasSubscriberId;

		// Token: 0x04001B19 RID: 6937
		private SubscriberId _SubscriberId;

		// Token: 0x04001B1A RID: 6938
		public bool HasPresenceSubscriberId;

		// Token: 0x04001B1B RID: 6939
		private AccountId _PresenceSubscriberId;
	}
}
