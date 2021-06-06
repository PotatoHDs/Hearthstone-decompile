using System;
using System.IO;
using System.Text;
using bnet.protocol.friends.v2.client.Types;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000417 RID: 1047
	public class FriendOfFriend : IProtoBuf
	{
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060045ED RID: 17901 RVA: 0x000DB7CB File Offset: 0x000D99CB
		// (set) Token: 0x060045EE RID: 17902 RVA: 0x000DB7D3 File Offset: 0x000D99D3
		public ulong Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x000DB7E3 File Offset: 0x000D99E3
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060045F0 RID: 17904 RVA: 0x000DB7EC File Offset: 0x000D99EC
		// (set) Token: 0x060045F1 RID: 17905 RVA: 0x000DB7F4 File Offset: 0x000D99F4
		public FriendLevel Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				this._Level = value;
				this.HasLevel = true;
			}
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x000DB804 File Offset: 0x000D9A04
		public void SetLevel(FriendLevel val)
		{
			this.Level = val;
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x000DB80D File Offset: 0x000D9A0D
		// (set) Token: 0x060045F4 RID: 17908 RVA: 0x000DB815 File Offset: 0x000D9A15
		public string FullName
		{
			get
			{
				return this._FullName;
			}
			set
			{
				this._FullName = value;
				this.HasFullName = (value != null);
			}
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x000DB828 File Offset: 0x000D9A28
		public void SetFullName(string val)
		{
			this.FullName = val;
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060045F6 RID: 17910 RVA: 0x000DB831 File Offset: 0x000D9A31
		// (set) Token: 0x060045F7 RID: 17911 RVA: 0x000DB839 File Offset: 0x000D9A39
		public string BattleTag
		{
			get
			{
				return this._BattleTag;
			}
			set
			{
				this._BattleTag = value;
				this.HasBattleTag = (value != null);
			}
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x000DB84C File Offset: 0x000D9A4C
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x000DB858 File Offset: 0x000D9A58
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasLevel)
			{
				num ^= this.Level.GetHashCode();
			}
			if (this.HasFullName)
			{
				num ^= this.FullName.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			return num;
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x000DB8D8 File Offset: 0x000D9AD8
		public override bool Equals(object obj)
		{
			FriendOfFriend friendOfFriend = obj as FriendOfFriend;
			return friendOfFriend != null && this.HasId == friendOfFriend.HasId && (!this.HasId || this.Id.Equals(friendOfFriend.Id)) && this.HasLevel == friendOfFriend.HasLevel && (!this.HasLevel || this.Level.Equals(friendOfFriend.Level)) && this.HasFullName == friendOfFriend.HasFullName && (!this.HasFullName || this.FullName.Equals(friendOfFriend.FullName)) && this.HasBattleTag == friendOfFriend.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(friendOfFriend.BattleTag));
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x000DB9AF File Offset: 0x000D9BAF
		public static FriendOfFriend ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendOfFriend>(bs, 0, -1);
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x000DB9B9 File Offset: 0x000D9BB9
		public void Deserialize(Stream stream)
		{
			FriendOfFriend.Deserialize(stream, this);
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x000DB9C3 File Offset: 0x000D9BC3
		public static FriendOfFriend Deserialize(Stream stream, FriendOfFriend instance)
		{
			return FriendOfFriend.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x000DB9D0 File Offset: 0x000D9BD0
		public static FriendOfFriend DeserializeLengthDelimited(Stream stream)
		{
			FriendOfFriend friendOfFriend = new FriendOfFriend();
			FriendOfFriend.DeserializeLengthDelimited(stream, friendOfFriend);
			return friendOfFriend;
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x000DB9EC File Offset: 0x000D9BEC
		public static FriendOfFriend DeserializeLengthDelimited(Stream stream, FriendOfFriend instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendOfFriend.Deserialize(stream, instance, num);
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x000DBA14 File Offset: 0x000D9C14
		public static FriendOfFriend Deserialize(Stream stream, FriendOfFriend instance, long limit)
		{
			instance.Level = FriendLevel.FRIEND_LEVEL_BATTLE_TAG;
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.Id = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.FullName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.BattleTag = ProtocolParser.ReadString(stream);
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

		// Token: 0x06004602 RID: 17922 RVA: 0x000DBAEC File Offset: 0x000D9CEC
		public void Serialize(Stream stream)
		{
			FriendOfFriend.Serialize(stream, this);
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x000DBAF8 File Offset: 0x000D9CF8
		public static void Serialize(Stream stream, FriendOfFriend instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Id);
			}
			if (instance.HasLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Level));
			}
			if (instance.HasFullName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FullName));
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x000DBB8C File Offset: 0x000D9D8C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Id);
			}
			if (this.HasLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Level));
			}
			if (this.HasFullName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FullName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasBattleTag)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x0400177E RID: 6014
		public bool HasId;

		// Token: 0x0400177F RID: 6015
		private ulong _Id;

		// Token: 0x04001780 RID: 6016
		public bool HasLevel;

		// Token: 0x04001781 RID: 6017
		private FriendLevel _Level;

		// Token: 0x04001782 RID: 6018
		public bool HasFullName;

		// Token: 0x04001783 RID: 6019
		private string _FullName;

		// Token: 0x04001784 RID: 6020
		public bool HasBattleTag;

		// Token: 0x04001785 RID: 6021
		private string _BattleTag;
	}
}
