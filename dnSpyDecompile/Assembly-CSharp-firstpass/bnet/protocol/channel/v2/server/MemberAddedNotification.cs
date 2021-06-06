using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x0200048F RID: 1167
	public class MemberAddedNotification : IProtoBuf
	{
		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06005128 RID: 20776 RVA: 0x000FBADD File Offset: 0x000F9CDD
		// (set) Token: 0x06005129 RID: 20777 RVA: 0x000FBAE5 File Offset: 0x000F9CE5
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

		// Token: 0x0600512A RID: 20778 RVA: 0x000FBAF8 File Offset: 0x000F9CF8
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x0600512B RID: 20779 RVA: 0x000FBB01 File Offset: 0x000F9D01
		// (set) Token: 0x0600512C RID: 20780 RVA: 0x000FBB09 File Offset: 0x000F9D09
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

		// Token: 0x0600512D RID: 20781 RVA: 0x000FBB1C File Offset: 0x000F9D1C
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x0600512E RID: 20782 RVA: 0x000FBB25 File Offset: 0x000F9D25
		// (set) Token: 0x0600512F RID: 20783 RVA: 0x000FBB2D File Offset: 0x000F9D2D
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

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06005130 RID: 20784 RVA: 0x000FBB25 File Offset: 0x000F9D25
		public List<Member> MemberList
		{
			get
			{
				return this._Member;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06005131 RID: 20785 RVA: 0x000FBB36 File Offset: 0x000F9D36
		public int MemberCount
		{
			get
			{
				return this._Member.Count;
			}
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x000FBB43 File Offset: 0x000F9D43
		public void AddMember(Member val)
		{
			this._Member.Add(val);
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x000FBB51 File Offset: 0x000F9D51
		public void ClearMember()
		{
			this._Member.Clear();
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x000FBB5E File Offset: 0x000F9D5E
		public void SetMember(List<Member> val)
		{
			this.Member = val;
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x000FBB68 File Offset: 0x000F9D68
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			foreach (Member member in this.Member)
			{
				num ^= member.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x000FBBF8 File Offset: 0x000F9DF8
		public override bool Equals(object obj)
		{
			MemberAddedNotification memberAddedNotification = obj as MemberAddedNotification;
			if (memberAddedNotification == null)
			{
				return false;
			}
			if (this.HasAgentId != memberAddedNotification.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(memberAddedNotification.AgentId)))
			{
				return false;
			}
			if (this.HasChannelId != memberAddedNotification.HasChannelId || (this.HasChannelId && !this.ChannelId.Equals(memberAddedNotification.ChannelId)))
			{
				return false;
			}
			if (this.Member.Count != memberAddedNotification.Member.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Member.Count; i++)
			{
				if (!this.Member[i].Equals(memberAddedNotification.Member[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06005137 RID: 20791 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x000FBCB9 File Offset: 0x000F9EB9
		public static MemberAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x000FBCC3 File Offset: 0x000F9EC3
		public void Deserialize(Stream stream)
		{
			MemberAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x000FBCCD File Offset: 0x000F9ECD
		public static MemberAddedNotification Deserialize(Stream stream, MemberAddedNotification instance)
		{
			return MemberAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x000FBCD8 File Offset: 0x000F9ED8
		public static MemberAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberAddedNotification memberAddedNotification = new MemberAddedNotification();
			MemberAddedNotification.DeserializeLengthDelimited(stream, memberAddedNotification);
			return memberAddedNotification;
		}

		// Token: 0x0600513C RID: 20796 RVA: 0x000FBCF4 File Offset: 0x000F9EF4
		public static MemberAddedNotification DeserializeLengthDelimited(Stream stream, MemberAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600513D RID: 20797 RVA: 0x000FBD1C File Offset: 0x000F9F1C
		public static MemberAddedNotification Deserialize(Stream stream, MemberAddedNotification instance, long limit)
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
				else if (num != 10)
				{
					if (num != 26)
					{
						if (num != 34)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.Member.Add(bnet.protocol.channel.v2.Member.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600513E RID: 20798 RVA: 0x000FBE1C File Offset: 0x000FA01C
		public void Serialize(Stream stream)
		{
			MemberAddedNotification.Serialize(stream, this);
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x000FBE28 File Offset: 0x000FA028
		public static void Serialize(Stream stream, MemberAddedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.Member.Count > 0)
			{
				foreach (Member member in instance.Member)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, member.GetSerializedSize());
					bnet.protocol.channel.v2.Member.Serialize(stream, member);
				}
			}
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x000FBEF8 File Offset: 0x000FA0F8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.Member.Count > 0)
			{
				foreach (Member member in this.Member)
				{
					num += 1U;
					uint serializedSize3 = member.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x04001A25 RID: 6693
		public bool HasAgentId;

		// Token: 0x04001A26 RID: 6694
		private GameAccountHandle _AgentId;

		// Token: 0x04001A27 RID: 6695
		public bool HasChannelId;

		// Token: 0x04001A28 RID: 6696
		private ChannelId _ChannelId;

		// Token: 0x04001A29 RID: 6697
		private List<Member> _Member = new List<Member>();
	}
}
