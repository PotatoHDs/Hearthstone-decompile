using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000427 RID: 1063
	public class GetFriendListResponse : IProtoBuf
	{
		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06004729 RID: 18217 RVA: 0x000DE9CF File Offset: 0x000DCBCF
		// (set) Token: 0x0600472A RID: 18218 RVA: 0x000DE9D7 File Offset: 0x000DCBD7
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

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x0600472B RID: 18219 RVA: 0x000DE9CF File Offset: 0x000DCBCF
		public List<Friend> FriendsList
		{
			get
			{
				return this._Friends;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x0600472C RID: 18220 RVA: 0x000DE9E0 File Offset: 0x000DCBE0
		public int FriendsCount
		{
			get
			{
				return this._Friends.Count;
			}
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x000DE9ED File Offset: 0x000DCBED
		public void AddFriends(Friend val)
		{
			this._Friends.Add(val);
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x000DE9FB File Offset: 0x000DCBFB
		public void ClearFriends()
		{
			this._Friends.Clear();
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x000DEA08 File Offset: 0x000DCC08
		public void SetFriends(List<Friend> val)
		{
			this.Friends = val;
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x000DEA14 File Offset: 0x000DCC14
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Friend friend in this.Friends)
			{
				num ^= friend.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x000DEA78 File Offset: 0x000DCC78
		public override bool Equals(object obj)
		{
			GetFriendListResponse getFriendListResponse = obj as GetFriendListResponse;
			if (getFriendListResponse == null)
			{
				return false;
			}
			if (this.Friends.Count != getFriendListResponse.Friends.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Friends.Count; i++)
			{
				if (!this.Friends[i].Equals(getFriendListResponse.Friends[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06004732 RID: 18226 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x000DEAE3 File Offset: 0x000DCCE3
		public static GetFriendListResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFriendListResponse>(bs, 0, -1);
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x000DEAED File Offset: 0x000DCCED
		public void Deserialize(Stream stream)
		{
			GetFriendListResponse.Deserialize(stream, this);
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x000DEAF7 File Offset: 0x000DCCF7
		public static GetFriendListResponse Deserialize(Stream stream, GetFriendListResponse instance)
		{
			return GetFriendListResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x000DEB04 File Offset: 0x000DCD04
		public static GetFriendListResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFriendListResponse getFriendListResponse = new GetFriendListResponse();
			GetFriendListResponse.DeserializeLengthDelimited(stream, getFriendListResponse);
			return getFriendListResponse;
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x000DEB20 File Offset: 0x000DCD20
		public static GetFriendListResponse DeserializeLengthDelimited(Stream stream, GetFriendListResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFriendListResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x000DEB48 File Offset: 0x000DCD48
		public static GetFriendListResponse Deserialize(Stream stream, GetFriendListResponse instance, long limit)
		{
			if (instance.Friends == null)
			{
				instance.Friends = new List<Friend>();
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
				else if (num == 10)
				{
					instance.Friends.Add(Friend.DeserializeLengthDelimited(stream));
				}
				else
				{
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

		// Token: 0x06004739 RID: 18233 RVA: 0x000DEBE0 File Offset: 0x000DCDE0
		public void Serialize(Stream stream)
		{
			GetFriendListResponse.Serialize(stream, this);
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x000DEBEC File Offset: 0x000DCDEC
		public static void Serialize(Stream stream, GetFriendListResponse instance)
		{
			if (instance.Friends.Count > 0)
			{
				foreach (Friend friend in instance.Friends)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, friend.GetSerializedSize());
					Friend.Serialize(stream, friend);
				}
			}
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x000DEC64 File Offset: 0x000DCE64
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Friends.Count > 0)
			{
				foreach (Friend friend in this.Friends)
				{
					num += 1U;
					uint serializedSize = friend.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040017BA RID: 6074
		private List<Friend> _Friends = new List<Friend>();
	}
}
