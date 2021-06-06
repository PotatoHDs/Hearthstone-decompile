using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000418 RID: 1048
	public class FriendsState : IProtoBuf
	{
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x000DBC21 File Offset: 0x000D9E21
		// (set) Token: 0x06004607 RID: 17927 RVA: 0x000DBC29 File Offset: 0x000D9E29
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

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x000DBC21 File Offset: 0x000D9E21
		public List<ReceivedInvitation> ReceivedInvitationsList
		{
			get
			{
				return this._ReceivedInvitations;
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x000DBC32 File Offset: 0x000D9E32
		public int ReceivedInvitationsCount
		{
			get
			{
				return this._ReceivedInvitations.Count;
			}
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x000DBC3F File Offset: 0x000D9E3F
		public void AddReceivedInvitations(ReceivedInvitation val)
		{
			this._ReceivedInvitations.Add(val);
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x000DBC4D File Offset: 0x000D9E4D
		public void ClearReceivedInvitations()
		{
			this._ReceivedInvitations.Clear();
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x000DBC5A File Offset: 0x000D9E5A
		public void SetReceivedInvitations(List<ReceivedInvitation> val)
		{
			this.ReceivedInvitations = val;
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x0600460D RID: 17933 RVA: 0x000DBC63 File Offset: 0x000D9E63
		// (set) Token: 0x0600460E RID: 17934 RVA: 0x000DBC6B File Offset: 0x000D9E6B
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

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x000DBC63 File Offset: 0x000D9E63
		public List<SentInvitation> SentInvitationsList
		{
			get
			{
				return this._SentInvitations;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06004610 RID: 17936 RVA: 0x000DBC74 File Offset: 0x000D9E74
		public int SentInvitationsCount
		{
			get
			{
				return this._SentInvitations.Count;
			}
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x000DBC81 File Offset: 0x000D9E81
		public void AddSentInvitations(SentInvitation val)
		{
			this._SentInvitations.Add(val);
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x000DBC8F File Offset: 0x000D9E8F
		public void ClearSentInvitations()
		{
			this._SentInvitations.Clear();
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x000DBC9C File Offset: 0x000D9E9C
		public void SetSentInvitations(List<SentInvitation> val)
		{
			this.SentInvitations = val;
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x000DBCA8 File Offset: 0x000D9EA8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
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

		// Token: 0x06004615 RID: 17941 RVA: 0x000DBD50 File Offset: 0x000D9F50
		public override bool Equals(object obj)
		{
			FriendsState friendsState = obj as FriendsState;
			if (friendsState == null)
			{
				return false;
			}
			if (this.ReceivedInvitations.Count != friendsState.ReceivedInvitations.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ReceivedInvitations.Count; i++)
			{
				if (!this.ReceivedInvitations[i].Equals(friendsState.ReceivedInvitations[i]))
				{
					return false;
				}
			}
			if (this.SentInvitations.Count != friendsState.SentInvitations.Count)
			{
				return false;
			}
			for (int j = 0; j < this.SentInvitations.Count; j++)
			{
				if (!this.SentInvitations[j].Equals(friendsState.SentInvitations[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004616 RID: 17942 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x000DBE0C File Offset: 0x000DA00C
		public static FriendsState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendsState>(bs, 0, -1);
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x000DBE16 File Offset: 0x000DA016
		public void Deserialize(Stream stream)
		{
			FriendsState.Deserialize(stream, this);
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x000DBE20 File Offset: 0x000DA020
		public static FriendsState Deserialize(Stream stream, FriendsState instance)
		{
			return FriendsState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x000DBE2C File Offset: 0x000DA02C
		public static FriendsState DeserializeLengthDelimited(Stream stream)
		{
			FriendsState friendsState = new FriendsState();
			FriendsState.DeserializeLengthDelimited(stream, friendsState);
			return friendsState;
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x000DBE48 File Offset: 0x000DA048
		public static FriendsState DeserializeLengthDelimited(Stream stream, FriendsState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendsState.Deserialize(stream, instance, num);
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x000DBE70 File Offset: 0x000DA070
		public static FriendsState Deserialize(Stream stream, FriendsState instance, long limit)
		{
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
				else if (num != 18)
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
					else
					{
						instance.SentInvitations.Add(SentInvitation.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.ReceivedInvitations.Add(ReceivedInvitation.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x000DBF38 File Offset: 0x000DA138
		public void Serialize(Stream stream)
		{
			FriendsState.Serialize(stream, this);
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x000DBF44 File Offset: 0x000DA144
		public static void Serialize(Stream stream, FriendsState instance)
		{
			if (instance.ReceivedInvitations.Count > 0)
			{
				foreach (ReceivedInvitation receivedInvitation in instance.ReceivedInvitations)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, receivedInvitation.GetSerializedSize());
					ReceivedInvitation.Serialize(stream, receivedInvitation);
				}
			}
			if (instance.SentInvitations.Count > 0)
			{
				foreach (SentInvitation sentInvitation in instance.SentInvitations)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, sentInvitation.GetSerializedSize());
					SentInvitation.Serialize(stream, sentInvitation);
				}
			}
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x000DC020 File Offset: 0x000DA220
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.ReceivedInvitations.Count > 0)
			{
				foreach (ReceivedInvitation receivedInvitation in this.ReceivedInvitations)
				{
					num += 1U;
					uint serializedSize = receivedInvitation.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.SentInvitations.Count > 0)
			{
				foreach (SentInvitation sentInvitation in this.SentInvitations)
				{
					num += 1U;
					uint serializedSize2 = sentInvitation.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04001786 RID: 6022
		private List<ReceivedInvitation> _ReceivedInvitations = new List<ReceivedInvitation>();

		// Token: 0x04001787 RID: 6023
		private List<SentInvitation> _SentInvitations = new List<SentInvitation>();
	}
}
