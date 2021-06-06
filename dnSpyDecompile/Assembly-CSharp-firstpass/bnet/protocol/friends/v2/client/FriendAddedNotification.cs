using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003F2 RID: 1010
	public class FriendAddedNotification : IProtoBuf
	{
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x060042E9 RID: 17129 RVA: 0x000D41E3 File Offset: 0x000D23E3
		// (set) Token: 0x060042EA RID: 17130 RVA: 0x000D41EB File Offset: 0x000D23EB
		public ulong AgentAccountId
		{
			get
			{
				return this._AgentAccountId;
			}
			set
			{
				this._AgentAccountId = value;
				this.HasAgentAccountId = true;
			}
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x000D41FB File Offset: 0x000D23FB
		public void SetAgentAccountId(ulong val)
		{
			this.AgentAccountId = val;
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060042EC RID: 17132 RVA: 0x000D4204 File Offset: 0x000D2404
		// (set) Token: 0x060042ED RID: 17133 RVA: 0x000D420C File Offset: 0x000D240C
		public List<Friend> Friend
		{
			get
			{
				return this._Friend;
			}
			set
			{
				this._Friend = value;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x060042EE RID: 17134 RVA: 0x000D4204 File Offset: 0x000D2404
		public List<Friend> FriendList
		{
			get
			{
				return this._Friend;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x060042EF RID: 17135 RVA: 0x000D4215 File Offset: 0x000D2415
		public int FriendCount
		{
			get
			{
				return this._Friend.Count;
			}
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x000D4222 File Offset: 0x000D2422
		public void AddFriend(Friend val)
		{
			this._Friend.Add(val);
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x000D4230 File Offset: 0x000D2430
		public void ClearFriend()
		{
			this._Friend.Clear();
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x000D423D File Offset: 0x000D243D
		public void SetFriend(List<Friend> val)
		{
			this.Friend = val;
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x000D4248 File Offset: 0x000D2448
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentAccountId)
			{
				num ^= this.AgentAccountId.GetHashCode();
			}
			foreach (Friend friend in this.Friend)
			{
				num ^= friend.GetHashCode();
			}
			return num;
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x000D42C4 File Offset: 0x000D24C4
		public override bool Equals(object obj)
		{
			FriendAddedNotification friendAddedNotification = obj as FriendAddedNotification;
			if (friendAddedNotification == null)
			{
				return false;
			}
			if (this.HasAgentAccountId != friendAddedNotification.HasAgentAccountId || (this.HasAgentAccountId && !this.AgentAccountId.Equals(friendAddedNotification.AgentAccountId)))
			{
				return false;
			}
			if (this.Friend.Count != friendAddedNotification.Friend.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Friend.Count; i++)
			{
				if (!this.Friend[i].Equals(friendAddedNotification.Friend[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x060042F5 RID: 17141 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x000D435D File Offset: 0x000D255D
		public static FriendAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendAddedNotification>(bs, 0, -1);
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x000D4367 File Offset: 0x000D2567
		public void Deserialize(Stream stream)
		{
			FriendAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x000D4371 File Offset: 0x000D2571
		public static FriendAddedNotification Deserialize(Stream stream, FriendAddedNotification instance)
		{
			return FriendAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x000D437C File Offset: 0x000D257C
		public static FriendAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			FriendAddedNotification friendAddedNotification = new FriendAddedNotification();
			FriendAddedNotification.DeserializeLengthDelimited(stream, friendAddedNotification);
			return friendAddedNotification;
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000D4398 File Offset: 0x000D2598
		public static FriendAddedNotification DeserializeLengthDelimited(Stream stream, FriendAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x000D43C0 File Offset: 0x000D25C0
		public static FriendAddedNotification Deserialize(Stream stream, FriendAddedNotification instance, long limit)
		{
			if (instance.Friend == null)
			{
				instance.Friend = new List<Friend>();
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
				else if (num != 8)
				{
					if (num != 18)
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
						instance.Friend.Add(bnet.protocol.friends.v2.client.Friend.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.AgentAccountId = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x000D446F File Offset: 0x000D266F
		public void Serialize(Stream stream)
		{
			FriendAddedNotification.Serialize(stream, this);
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x000D4478 File Offset: 0x000D2678
		public static void Serialize(Stream stream, FriendAddedNotification instance)
		{
			if (instance.HasAgentAccountId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AgentAccountId);
			}
			if (instance.Friend.Count > 0)
			{
				foreach (Friend friend in instance.Friend)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, friend.GetSerializedSize());
					bnet.protocol.friends.v2.client.Friend.Serialize(stream, friend);
				}
			}
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x000D4508 File Offset: 0x000D2708
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentAccountId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.AgentAccountId);
			}
			if (this.Friend.Count > 0)
			{
				foreach (Friend friend in this.Friend)
				{
					num += 1U;
					uint serializedSize = friend.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040016F3 RID: 5875
		public bool HasAgentAccountId;

		// Token: 0x040016F4 RID: 5876
		private ulong _AgentAccountId;

		// Token: 0x040016F5 RID: 5877
		private List<Friend> _Friend = new List<Friend>();
	}
}
