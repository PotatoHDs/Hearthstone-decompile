using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000424 RID: 1060
	public class ViewFriendsResponse : IProtoBuf
	{
		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x060046EB RID: 18155 RVA: 0x000DDFE6 File Offset: 0x000DC1E6
		// (set) Token: 0x060046EC RID: 18156 RVA: 0x000DDFEE File Offset: 0x000DC1EE
		public List<FriendOfFriend> Friends
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

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x060046ED RID: 18157 RVA: 0x000DDFE6 File Offset: 0x000DC1E6
		public List<FriendOfFriend> FriendsList
		{
			get
			{
				return this._Friends;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x060046EE RID: 18158 RVA: 0x000DDFF7 File Offset: 0x000DC1F7
		public int FriendsCount
		{
			get
			{
				return this._Friends.Count;
			}
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x000DE004 File Offset: 0x000DC204
		public void AddFriends(FriendOfFriend val)
		{
			this._Friends.Add(val);
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x000DE012 File Offset: 0x000DC212
		public void ClearFriends()
		{
			this._Friends.Clear();
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x000DE01F File Offset: 0x000DC21F
		public void SetFriends(List<FriendOfFriend> val)
		{
			this.Friends = val;
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x000DE028 File Offset: 0x000DC228
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (FriendOfFriend friendOfFriend in this.Friends)
			{
				num ^= friendOfFriend.GetHashCode();
			}
			return num;
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x000DE08C File Offset: 0x000DC28C
		public override bool Equals(object obj)
		{
			ViewFriendsResponse viewFriendsResponse = obj as ViewFriendsResponse;
			if (viewFriendsResponse == null)
			{
				return false;
			}
			if (this.Friends.Count != viewFriendsResponse.Friends.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Friends.Count; i++)
			{
				if (!this.Friends[i].Equals(viewFriendsResponse.Friends[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x000DE0F7 File Offset: 0x000DC2F7
		public static ViewFriendsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ViewFriendsResponse>(bs, 0, -1);
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x000DE101 File Offset: 0x000DC301
		public void Deserialize(Stream stream)
		{
			ViewFriendsResponse.Deserialize(stream, this);
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x000DE10B File Offset: 0x000DC30B
		public static ViewFriendsResponse Deserialize(Stream stream, ViewFriendsResponse instance)
		{
			return ViewFriendsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x000DE118 File Offset: 0x000DC318
		public static ViewFriendsResponse DeserializeLengthDelimited(Stream stream)
		{
			ViewFriendsResponse viewFriendsResponse = new ViewFriendsResponse();
			ViewFriendsResponse.DeserializeLengthDelimited(stream, viewFriendsResponse);
			return viewFriendsResponse;
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x000DE134 File Offset: 0x000DC334
		public static ViewFriendsResponse DeserializeLengthDelimited(Stream stream, ViewFriendsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ViewFriendsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x000DE15C File Offset: 0x000DC35C
		public static ViewFriendsResponse Deserialize(Stream stream, ViewFriendsResponse instance, long limit)
		{
			if (instance.Friends == null)
			{
				instance.Friends = new List<FriendOfFriend>();
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
					instance.Friends.Add(FriendOfFriend.DeserializeLengthDelimited(stream));
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

		// Token: 0x060046FB RID: 18171 RVA: 0x000DE1F4 File Offset: 0x000DC3F4
		public void Serialize(Stream stream)
		{
			ViewFriendsResponse.Serialize(stream, this);
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x000DE200 File Offset: 0x000DC400
		public static void Serialize(Stream stream, ViewFriendsResponse instance)
		{
			if (instance.Friends.Count > 0)
			{
				foreach (FriendOfFriend friendOfFriend in instance.Friends)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, friendOfFriend.GetSerializedSize());
					FriendOfFriend.Serialize(stream, friendOfFriend);
				}
			}
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x000DE278 File Offset: 0x000DC478
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Friends.Count > 0)
			{
				foreach (FriendOfFriend friendOfFriend in this.Friends)
				{
					num += 1U;
					uint serializedSize = friendOfFriend.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040017B3 RID: 6067
		private List<FriendOfFriend> _Friends = new List<FriendOfFriend>();
	}
}
