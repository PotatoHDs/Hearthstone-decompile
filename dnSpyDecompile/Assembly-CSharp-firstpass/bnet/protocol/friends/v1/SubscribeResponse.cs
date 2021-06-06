using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000434 RID: 1076
	public class SubscribeResponse : IProtoBuf
	{
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060048A2 RID: 18594 RVA: 0x000E33DE File Offset: 0x000E15DE
		// (set) Token: 0x060048A3 RID: 18595 RVA: 0x000E33E6 File Offset: 0x000E15E6
		public uint MaxFriends
		{
			get
			{
				return this._MaxFriends;
			}
			set
			{
				this._MaxFriends = value;
				this.HasMaxFriends = true;
			}
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x000E33F6 File Offset: 0x000E15F6
		public void SetMaxFriends(uint val)
		{
			this.MaxFriends = val;
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060048A5 RID: 18597 RVA: 0x000E33FF File Offset: 0x000E15FF
		// (set) Token: 0x060048A6 RID: 18598 RVA: 0x000E3407 File Offset: 0x000E1607
		public uint MaxReceivedInvitations
		{
			get
			{
				return this._MaxReceivedInvitations;
			}
			set
			{
				this._MaxReceivedInvitations = value;
				this.HasMaxReceivedInvitations = true;
			}
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x000E3417 File Offset: 0x000E1617
		public void SetMaxReceivedInvitations(uint val)
		{
			this.MaxReceivedInvitations = val;
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060048A8 RID: 18600 RVA: 0x000E3420 File Offset: 0x000E1620
		// (set) Token: 0x060048A9 RID: 18601 RVA: 0x000E3428 File Offset: 0x000E1628
		public uint MaxSentInvitations
		{
			get
			{
				return this._MaxSentInvitations;
			}
			set
			{
				this._MaxSentInvitations = value;
				this.HasMaxSentInvitations = true;
			}
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x000E3438 File Offset: 0x000E1638
		public void SetMaxSentInvitations(uint val)
		{
			this.MaxSentInvitations = val;
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060048AB RID: 18603 RVA: 0x000E3441 File Offset: 0x000E1641
		// (set) Token: 0x060048AC RID: 18604 RVA: 0x000E3449 File Offset: 0x000E1649
		public List<Role> Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				this._Role = value;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060048AD RID: 18605 RVA: 0x000E3441 File Offset: 0x000E1641
		public List<Role> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060048AE RID: 18606 RVA: 0x000E3452 File Offset: 0x000E1652
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x000E345F File Offset: 0x000E165F
		public void AddRole(Role val)
		{
			this._Role.Add(val);
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x000E346D File Offset: 0x000E166D
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x000E347A File Offset: 0x000E167A
		public void SetRole(List<Role> val)
		{
			this.Role = val;
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060048B2 RID: 18610 RVA: 0x000E3483 File Offset: 0x000E1683
		// (set) Token: 0x060048B3 RID: 18611 RVA: 0x000E348B File Offset: 0x000E168B
		public List<Friend> Friends
		{
			get
			{
				return this._Friends;
			}
			set
			{
				this._Friends = value;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060048B4 RID: 18612 RVA: 0x000E3483 File Offset: 0x000E1683
		public List<Friend> FriendsList
		{
			get
			{
				return this._Friends;
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060048B5 RID: 18613 RVA: 0x000E3494 File Offset: 0x000E1694
		public int FriendsCount
		{
			get
			{
				return this._Friends.Count;
			}
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x000E34A1 File Offset: 0x000E16A1
		public void AddFriends(Friend val)
		{
			this._Friends.Add(val);
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x000E34AF File Offset: 0x000E16AF
		public void ClearFriends()
		{
			this._Friends.Clear();
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x000E34BC File Offset: 0x000E16BC
		public void SetFriends(List<Friend> val)
		{
			this.Friends = val;
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060048B9 RID: 18617 RVA: 0x000E34C5 File Offset: 0x000E16C5
		// (set) Token: 0x060048BA RID: 18618 RVA: 0x000E34CD File Offset: 0x000E16CD
		public List<ReceivedInvitation> ReceivedInvitations
		{
			get
			{
				return this._ReceivedInvitations;
			}
			set
			{
				this._ReceivedInvitations = value;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060048BB RID: 18619 RVA: 0x000E34C5 File Offset: 0x000E16C5
		public List<ReceivedInvitation> ReceivedInvitationsList
		{
			get
			{
				return this._ReceivedInvitations;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060048BC RID: 18620 RVA: 0x000E34D6 File Offset: 0x000E16D6
		public int ReceivedInvitationsCount
		{
			get
			{
				return this._ReceivedInvitations.Count;
			}
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x000E34E3 File Offset: 0x000E16E3
		public void AddReceivedInvitations(ReceivedInvitation val)
		{
			this._ReceivedInvitations.Add(val);
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x000E34F1 File Offset: 0x000E16F1
		public void ClearReceivedInvitations()
		{
			this._ReceivedInvitations.Clear();
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x000E34FE File Offset: 0x000E16FE
		public void SetReceivedInvitations(List<ReceivedInvitation> val)
		{
			this.ReceivedInvitations = val;
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060048C0 RID: 18624 RVA: 0x000E3507 File Offset: 0x000E1707
		// (set) Token: 0x060048C1 RID: 18625 RVA: 0x000E350F File Offset: 0x000E170F
		public List<SentInvitation> SentInvitations
		{
			get
			{
				return this._SentInvitations;
			}
			set
			{
				this._SentInvitations = value;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060048C2 RID: 18626 RVA: 0x000E3507 File Offset: 0x000E1707
		public List<SentInvitation> SentInvitationsList
		{
			get
			{
				return this._SentInvitations;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060048C3 RID: 18627 RVA: 0x000E3518 File Offset: 0x000E1718
		public int SentInvitationsCount
		{
			get
			{
				return this._SentInvitations.Count;
			}
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x000E3525 File Offset: 0x000E1725
		public void AddSentInvitations(SentInvitation val)
		{
			this._SentInvitations.Add(val);
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x000E3533 File Offset: 0x000E1733
		public void ClearSentInvitations()
		{
			this._SentInvitations.Clear();
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x000E3540 File Offset: 0x000E1740
		public void SetSentInvitations(List<SentInvitation> val)
		{
			this.SentInvitations = val;
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x000E354C File Offset: 0x000E174C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMaxFriends)
			{
				num ^= this.MaxFriends.GetHashCode();
			}
			if (this.HasMaxReceivedInvitations)
			{
				num ^= this.MaxReceivedInvitations.GetHashCode();
			}
			if (this.HasMaxSentInvitations)
			{
				num ^= this.MaxSentInvitations.GetHashCode();
			}
			foreach (Role role in this.Role)
			{
				num ^= role.GetHashCode();
			}
			foreach (Friend friend in this.Friends)
			{
				num ^= friend.GetHashCode();
			}
			foreach (ReceivedInvitation receivedInvitation in this.ReceivedInvitations)
			{
				num ^= receivedInvitation.GetHashCode();
			}
			foreach (SentInvitation sentInvitation in this.SentInvitations)
			{
				num ^= sentInvitation.GetHashCode();
			}
			return num;
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x000E36D0 File Offset: 0x000E18D0
		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			if (subscribeResponse == null)
			{
				return false;
			}
			if (this.HasMaxFriends != subscribeResponse.HasMaxFriends || (this.HasMaxFriends && !this.MaxFriends.Equals(subscribeResponse.MaxFriends)))
			{
				return false;
			}
			if (this.HasMaxReceivedInvitations != subscribeResponse.HasMaxReceivedInvitations || (this.HasMaxReceivedInvitations && !this.MaxReceivedInvitations.Equals(subscribeResponse.MaxReceivedInvitations)))
			{
				return false;
			}
			if (this.HasMaxSentInvitations != subscribeResponse.HasMaxSentInvitations || (this.HasMaxSentInvitations && !this.MaxSentInvitations.Equals(subscribeResponse.MaxSentInvitations)))
			{
				return false;
			}
			if (this.Role.Count != subscribeResponse.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(subscribeResponse.Role[i]))
				{
					return false;
				}
			}
			if (this.Friends.Count != subscribeResponse.Friends.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Friends.Count; j++)
			{
				if (!this.Friends[j].Equals(subscribeResponse.Friends[j]))
				{
					return false;
				}
			}
			if (this.ReceivedInvitations.Count != subscribeResponse.ReceivedInvitations.Count)
			{
				return false;
			}
			for (int k = 0; k < this.ReceivedInvitations.Count; k++)
			{
				if (!this.ReceivedInvitations[k].Equals(subscribeResponse.ReceivedInvitations[k]))
				{
					return false;
				}
			}
			if (this.SentInvitations.Count != subscribeResponse.SentInvitations.Count)
			{
				return false;
			}
			for (int l = 0; l < this.SentInvitations.Count; l++)
			{
				if (!this.SentInvitations[l].Equals(subscribeResponse.SentInvitations[l]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060048C9 RID: 18633 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x000E38C4 File Offset: 0x000E1AC4
		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs, 0, -1);
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x000E38CE File Offset: 0x000E1ACE
		public void Deserialize(Stream stream)
		{
			SubscribeResponse.Deserialize(stream, this);
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x000E38D8 File Offset: 0x000E1AD8
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return SubscribeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x000E38E4 File Offset: 0x000E1AE4
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			SubscribeResponse.DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x000E3900 File Offset: 0x000E1B00
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x000E3928 File Offset: 0x000E1B28
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<Role>();
			}
			if (instance.Friends == null)
			{
				instance.Friends = new List<Friend>();
			}
			if (instance.ReceivedInvitations == null)
			{
				instance.ReceivedInvitations = new List<ReceivedInvitation>();
			}
			if (instance.SentInvitations == null)
			{
				instance.SentInvitations = new List<SentInvitation>();
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
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.MaxFriends = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 16)
						{
							instance.MaxReceivedInvitations = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 24)
						{
							instance.MaxSentInvitations = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else if (num <= 42)
					{
						if (num == 34)
						{
							instance.Role.Add(bnet.protocol.Role.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 42)
						{
							instance.Friends.Add(Friend.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else
					{
						if (num == 58)
						{
							instance.ReceivedInvitations.Add(ReceivedInvitation.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 66)
						{
							instance.SentInvitations.Add(SentInvitation.DeserializeLengthDelimited(stream));
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

		// Token: 0x060048D0 RID: 18640 RVA: 0x000E3AB0 File Offset: 0x000E1CB0
		public void Serialize(Stream stream)
		{
			SubscribeResponse.Serialize(stream, this);
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x000E3ABC File Offset: 0x000E1CBC
		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.HasMaxFriends)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.MaxFriends);
			}
			if (instance.HasMaxReceivedInvitations)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MaxReceivedInvitations);
			}
			if (instance.HasMaxSentInvitations)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.MaxSentInvitations);
			}
			if (instance.Role.Count > 0)
			{
				foreach (Role role in instance.Role)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, role.GetSerializedSize());
					bnet.protocol.Role.Serialize(stream, role);
				}
			}
			if (instance.Friends.Count > 0)
			{
				foreach (Friend friend in instance.Friends)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, friend.GetSerializedSize());
					Friend.Serialize(stream, friend);
				}
			}
			if (instance.ReceivedInvitations.Count > 0)
			{
				foreach (ReceivedInvitation receivedInvitation in instance.ReceivedInvitations)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, receivedInvitation.GetSerializedSize());
					ReceivedInvitation.Serialize(stream, receivedInvitation);
				}
			}
			if (instance.SentInvitations.Count > 0)
			{
				foreach (SentInvitation sentInvitation in instance.SentInvitations)
				{
					stream.WriteByte(66);
					ProtocolParser.WriteUInt32(stream, sentInvitation.GetSerializedSize());
					SentInvitation.Serialize(stream, sentInvitation);
				}
			}
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x000E3CB8 File Offset: 0x000E1EB8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMaxFriends)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MaxFriends);
			}
			if (this.HasMaxReceivedInvitations)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MaxReceivedInvitations);
			}
			if (this.HasMaxSentInvitations)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MaxSentInvitations);
			}
			if (this.Role.Count > 0)
			{
				foreach (Role role in this.Role)
				{
					num += 1U;
					uint serializedSize = role.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.Friends.Count > 0)
			{
				foreach (Friend friend in this.Friends)
				{
					num += 1U;
					uint serializedSize2 = friend.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.ReceivedInvitations.Count > 0)
			{
				foreach (ReceivedInvitation receivedInvitation in this.ReceivedInvitations)
				{
					num += 1U;
					uint serializedSize3 = receivedInvitation.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.SentInvitations.Count > 0)
			{
				foreach (SentInvitation sentInvitation in this.SentInvitations)
				{
					num += 1U;
					uint serializedSize4 = sentInvitation.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			return num;
		}

		// Token: 0x04001819 RID: 6169
		public bool HasMaxFriends;

		// Token: 0x0400181A RID: 6170
		private uint _MaxFriends;

		// Token: 0x0400181B RID: 6171
		public bool HasMaxReceivedInvitations;

		// Token: 0x0400181C RID: 6172
		private uint _MaxReceivedInvitations;

		// Token: 0x0400181D RID: 6173
		public bool HasMaxSentInvitations;

		// Token: 0x0400181E RID: 6174
		private uint _MaxSentInvitations;

		// Token: 0x0400181F RID: 6175
		private List<Role> _Role = new List<Role>();

		// Token: 0x04001820 RID: 6176
		private List<Friend> _Friends = new List<Friend>();

		// Token: 0x04001821 RID: 6177
		private List<ReceivedInvitation> _ReceivedInvitations = new List<ReceivedInvitation>();

		// Token: 0x04001822 RID: 6178
		private List<SentInvitation> _SentInvitations = new List<SentInvitation>();
	}
}
