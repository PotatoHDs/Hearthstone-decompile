using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003FF RID: 1023
	public class GetFriendsResponse : IProtoBuf
	{
		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060043E4 RID: 17380 RVA: 0x000D65C9 File Offset: 0x000D47C9
		// (set) Token: 0x060043E5 RID: 17381 RVA: 0x000D65D1 File Offset: 0x000D47D1
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

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x000D65C9 File Offset: 0x000D47C9
		public List<Friend> FriendList
		{
			get
			{
				return this._Friend;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060043E7 RID: 17383 RVA: 0x000D65DA File Offset: 0x000D47DA
		public int FriendCount
		{
			get
			{
				return this._Friend.Count;
			}
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x000D65E7 File Offset: 0x000D47E7
		public void AddFriend(Friend val)
		{
			this._Friend.Add(val);
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x000D65F5 File Offset: 0x000D47F5
		public void ClearFriend()
		{
			this._Friend.Clear();
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x000D6602 File Offset: 0x000D4802
		public void SetFriend(List<Friend> val)
		{
			this.Friend = val;
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x000D660B File Offset: 0x000D480B
		// (set) Token: 0x060043EC RID: 17388 RVA: 0x000D6613 File Offset: 0x000D4813
		public ulong Continuation
		{
			get
			{
				return this._Continuation;
			}
			set
			{
				this._Continuation = value;
				this.HasContinuation = true;
			}
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x000D6623 File Offset: 0x000D4823
		public void SetContinuation(ulong val)
		{
			this.Continuation = val;
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x000D662C File Offset: 0x000D482C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Friend friend in this.Friend)
			{
				num ^= friend.GetHashCode();
			}
			if (this.HasContinuation)
			{
				num ^= this.Continuation.GetHashCode();
			}
			return num;
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x000D66A8 File Offset: 0x000D48A8
		public override bool Equals(object obj)
		{
			GetFriendsResponse getFriendsResponse = obj as GetFriendsResponse;
			if (getFriendsResponse == null)
			{
				return false;
			}
			if (this.Friend.Count != getFriendsResponse.Friend.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Friend.Count; i++)
			{
				if (!this.Friend[i].Equals(getFriendsResponse.Friend[i]))
				{
					return false;
				}
			}
			return this.HasContinuation == getFriendsResponse.HasContinuation && (!this.HasContinuation || this.Continuation.Equals(getFriendsResponse.Continuation));
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060043F0 RID: 17392 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x000D6741 File Offset: 0x000D4941
		public static GetFriendsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFriendsResponse>(bs, 0, -1);
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x000D674B File Offset: 0x000D494B
		public void Deserialize(Stream stream)
		{
			GetFriendsResponse.Deserialize(stream, this);
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x000D6755 File Offset: 0x000D4955
		public static GetFriendsResponse Deserialize(Stream stream, GetFriendsResponse instance)
		{
			return GetFriendsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x000D6760 File Offset: 0x000D4960
		public static GetFriendsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFriendsResponse getFriendsResponse = new GetFriendsResponse();
			GetFriendsResponse.DeserializeLengthDelimited(stream, getFriendsResponse);
			return getFriendsResponse;
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x000D677C File Offset: 0x000D497C
		public static GetFriendsResponse DeserializeLengthDelimited(Stream stream, GetFriendsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFriendsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x000D67A4 File Offset: 0x000D49A4
		public static GetFriendsResponse Deserialize(Stream stream, GetFriendsResponse instance, long limit)
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
				else if (num != 10)
				{
					if (num != 16)
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
						instance.Continuation = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Friend.Add(bnet.protocol.friends.v2.client.Friend.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x000D6854 File Offset: 0x000D4A54
		public void Serialize(Stream stream)
		{
			GetFriendsResponse.Serialize(stream, this);
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x000D6860 File Offset: 0x000D4A60
		public static void Serialize(Stream stream, GetFriendsResponse instance)
		{
			if (instance.Friend.Count > 0)
			{
				foreach (Friend friend in instance.Friend)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, friend.GetSerializedSize());
					bnet.protocol.friends.v2.client.Friend.Serialize(stream, friend);
				}
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x000D68F4 File Offset: 0x000D4AF4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Friend.Count > 0)
			{
				foreach (Friend friend in this.Friend)
				{
					num += 1U;
					uint serializedSize = friend.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasContinuation)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Continuation);
			}
			return num;
		}

		// Token: 0x04001710 RID: 5904
		private List<Friend> _Friend = new List<Friend>();

		// Token: 0x04001711 RID: 5905
		public bool HasContinuation;

		// Token: 0x04001712 RID: 5906
		private ulong _Continuation;
	}
}
