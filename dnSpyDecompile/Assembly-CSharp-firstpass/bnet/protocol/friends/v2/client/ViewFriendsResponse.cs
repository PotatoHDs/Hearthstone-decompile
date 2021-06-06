using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000403 RID: 1027
	public class ViewFriendsResponse : IProtoBuf
	{
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600442E RID: 17454 RVA: 0x000D6FC0 File Offset: 0x000D51C0
		// (set) Token: 0x0600442F RID: 17455 RVA: 0x000D6FC8 File Offset: 0x000D51C8
		public List<FriendOfFriend> Friend
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

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004430 RID: 17456 RVA: 0x000D6FC0 File Offset: 0x000D51C0
		public List<FriendOfFriend> FriendList
		{
			get
			{
				return this._Friend;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004431 RID: 17457 RVA: 0x000D6FD1 File Offset: 0x000D51D1
		public int FriendCount
		{
			get
			{
				return this._Friend.Count;
			}
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x000D6FDE File Offset: 0x000D51DE
		public void AddFriend(FriendOfFriend val)
		{
			this._Friend.Add(val);
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x000D6FEC File Offset: 0x000D51EC
		public void ClearFriend()
		{
			this._Friend.Clear();
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x000D6FF9 File Offset: 0x000D51F9
		public void SetFriend(List<FriendOfFriend> val)
		{
			this.Friend = val;
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004435 RID: 17461 RVA: 0x000D7002 File Offset: 0x000D5202
		// (set) Token: 0x06004436 RID: 17462 RVA: 0x000D700A File Offset: 0x000D520A
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

		// Token: 0x06004437 RID: 17463 RVA: 0x000D701A File Offset: 0x000D521A
		public void SetContinuation(ulong val)
		{
			this.Continuation = val;
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x000D7024 File Offset: 0x000D5224
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (FriendOfFriend friendOfFriend in this.Friend)
			{
				num ^= friendOfFriend.GetHashCode();
			}
			if (this.HasContinuation)
			{
				num ^= this.Continuation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x000D70A0 File Offset: 0x000D52A0
		public override bool Equals(object obj)
		{
			ViewFriendsResponse viewFriendsResponse = obj as ViewFriendsResponse;
			if (viewFriendsResponse == null)
			{
				return false;
			}
			if (this.Friend.Count != viewFriendsResponse.Friend.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Friend.Count; i++)
			{
				if (!this.Friend[i].Equals(viewFriendsResponse.Friend[i]))
				{
					return false;
				}
			}
			return this.HasContinuation == viewFriendsResponse.HasContinuation && (!this.HasContinuation || this.Continuation.Equals(viewFriendsResponse.Continuation));
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x000D7139 File Offset: 0x000D5339
		public static ViewFriendsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ViewFriendsResponse>(bs, 0, -1);
		}

		// Token: 0x0600443C RID: 17468 RVA: 0x000D7143 File Offset: 0x000D5343
		public void Deserialize(Stream stream)
		{
			ViewFriendsResponse.Deserialize(stream, this);
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x000D714D File Offset: 0x000D534D
		public static ViewFriendsResponse Deserialize(Stream stream, ViewFriendsResponse instance)
		{
			return ViewFriendsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x000D7158 File Offset: 0x000D5358
		public static ViewFriendsResponse DeserializeLengthDelimited(Stream stream)
		{
			ViewFriendsResponse viewFriendsResponse = new ViewFriendsResponse();
			ViewFriendsResponse.DeserializeLengthDelimited(stream, viewFriendsResponse);
			return viewFriendsResponse;
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x000D7174 File Offset: 0x000D5374
		public static ViewFriendsResponse DeserializeLengthDelimited(Stream stream, ViewFriendsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ViewFriendsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x000D719C File Offset: 0x000D539C
		public static ViewFriendsResponse Deserialize(Stream stream, ViewFriendsResponse instance, long limit)
		{
			if (instance.Friend == null)
			{
				instance.Friend = new List<FriendOfFriend>();
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
					instance.Friend.Add(FriendOfFriend.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x000D724C File Offset: 0x000D544C
		public void Serialize(Stream stream)
		{
			ViewFriendsResponse.Serialize(stream, this);
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x000D7258 File Offset: 0x000D5458
		public static void Serialize(Stream stream, ViewFriendsResponse instance)
		{
			if (instance.Friend.Count > 0)
			{
				foreach (FriendOfFriend friendOfFriend in instance.Friend)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, friendOfFriend.GetSerializedSize());
					FriendOfFriend.Serialize(stream, friendOfFriend);
				}
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x000D72EC File Offset: 0x000D54EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Friend.Count > 0)
			{
				foreach (FriendOfFriend friendOfFriend in this.Friend)
				{
					num += 1U;
					uint serializedSize = friendOfFriend.GetSerializedSize();
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

		// Token: 0x0400171B RID: 5915
		private List<FriendOfFriend> _Friend = new List<FriendOfFriend>();

		// Token: 0x0400171C RID: 5916
		public bool HasContinuation;

		// Token: 0x0400171D RID: 5917
		private ulong _Continuation;
	}
}
