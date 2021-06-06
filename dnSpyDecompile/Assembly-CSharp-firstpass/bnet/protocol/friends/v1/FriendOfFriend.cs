using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200042F RID: 1071
	public class FriendOfFriend : IProtoBuf
	{
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x060047F2 RID: 18418 RVA: 0x000E0F32 File Offset: 0x000DF132
		// (set) Token: 0x060047F3 RID: 18419 RVA: 0x000E0F3A File Offset: 0x000DF13A
		public EntityId AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = (value != null);
			}
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x000E0F4D File Offset: 0x000DF14D
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x060047F5 RID: 18421 RVA: 0x000E0F56 File Offset: 0x000DF156
		// (set) Token: 0x060047F6 RID: 18422 RVA: 0x000E0F5E File Offset: 0x000DF15E
		public List<uint> Role
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

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x060047F7 RID: 18423 RVA: 0x000E0F56 File Offset: 0x000DF156
		public List<uint> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x060047F8 RID: 18424 RVA: 0x000E0F67 File Offset: 0x000DF167
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x000E0F74 File Offset: 0x000DF174
		public void AddRole(uint val)
		{
			this._Role.Add(val);
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x000E0F82 File Offset: 0x000DF182
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x060047FB RID: 18427 RVA: 0x000E0F8F File Offset: 0x000DF18F
		public void SetRole(List<uint> val)
		{
			this.Role = val;
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x060047FC RID: 18428 RVA: 0x000E0F98 File Offset: 0x000DF198
		// (set) Token: 0x060047FD RID: 18429 RVA: 0x000E0FA0 File Offset: 0x000DF1A0
		public ulong Privileges
		{
			get
			{
				return this._Privileges;
			}
			set
			{
				this._Privileges = value;
				this.HasPrivileges = true;
			}
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x000E0FB0 File Offset: 0x000DF1B0
		public void SetPrivileges(ulong val)
		{
			this.Privileges = val;
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x060047FF RID: 18431 RVA: 0x000E0FB9 File Offset: 0x000DF1B9
		// (set) Token: 0x06004800 RID: 18432 RVA: 0x000E0FC1 File Offset: 0x000DF1C1
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

		// Token: 0x06004801 RID: 18433 RVA: 0x000E0FD4 File Offset: 0x000DF1D4
		public void SetFullName(string val)
		{
			this.FullName = val;
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06004802 RID: 18434 RVA: 0x000E0FDD File Offset: 0x000DF1DD
		// (set) Token: 0x06004803 RID: 18435 RVA: 0x000E0FE5 File Offset: 0x000DF1E5
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

		// Token: 0x06004804 RID: 18436 RVA: 0x000E0FF8 File Offset: 0x000DF1F8
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x000E1004 File Offset: 0x000DF204
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasPrivileges)
			{
				num ^= this.Privileges.GetHashCode();
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

		// Token: 0x06004806 RID: 18438 RVA: 0x000E10C4 File Offset: 0x000DF2C4
		public override bool Equals(object obj)
		{
			FriendOfFriend friendOfFriend = obj as FriendOfFriend;
			if (friendOfFriend == null)
			{
				return false;
			}
			if (this.HasAccountId != friendOfFriend.HasAccountId || (this.HasAccountId && !this.AccountId.Equals(friendOfFriend.AccountId)))
			{
				return false;
			}
			if (this.Role.Count != friendOfFriend.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(friendOfFriend.Role[i]))
				{
					return false;
				}
			}
			return this.HasPrivileges == friendOfFriend.HasPrivileges && (!this.HasPrivileges || this.Privileges.Equals(friendOfFriend.Privileges)) && this.HasFullName == friendOfFriend.HasFullName && (!this.HasFullName || this.FullName.Equals(friendOfFriend.FullName)) && this.HasBattleTag == friendOfFriend.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(friendOfFriend.BattleTag));
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x000E11E1 File Offset: 0x000DF3E1
		public static FriendOfFriend ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendOfFriend>(bs, 0, -1);
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x000E11EB File Offset: 0x000DF3EB
		public void Deserialize(Stream stream)
		{
			FriendOfFriend.Deserialize(stream, this);
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x000E11F5 File Offset: 0x000DF3F5
		public static FriendOfFriend Deserialize(Stream stream, FriendOfFriend instance)
		{
			return FriendOfFriend.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x000E1200 File Offset: 0x000DF400
		public static FriendOfFriend DeserializeLengthDelimited(Stream stream)
		{
			FriendOfFriend friendOfFriend = new FriendOfFriend();
			FriendOfFriend.DeserializeLengthDelimited(stream, friendOfFriend);
			return friendOfFriend;
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x000E121C File Offset: 0x000DF41C
		public static FriendOfFriend DeserializeLengthDelimited(Stream stream, FriendOfFriend instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendOfFriend.Deserialize(stream, instance, num);
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x000E1244 File Offset: 0x000DF444
		public static FriendOfFriend Deserialize(Stream stream, FriendOfFriend instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
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
							if (num == 26)
							{
								long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
								num2 += stream.Position;
								while (stream.Position < num2)
								{
									instance.Role.Add(ProtocolParser.ReadUInt32(stream));
								}
								if (stream.Position != num2)
								{
									throw new ProtocolBufferException("Read too many bytes in packed data");
								}
								continue;
							}
						}
						else
						{
							if (instance.AccountId == null)
							{
								instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.Privileges = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 50)
						{
							instance.FullName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 58)
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

		// Token: 0x0600480E RID: 18446 RVA: 0x000E13A0 File Offset: 0x000DF5A0
		public void Serialize(Stream stream)
		{
			FriendOfFriend.Serialize(stream, this);
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x000E13AC File Offset: 0x000DF5AC
		public static void Serialize(Stream stream, FriendOfFriend instance)
		{
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(26);
				uint num = 0U;
				foreach (uint val in instance.Role)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.Role)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
			if (instance.HasPrivileges)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.Privileges);
			}
			if (instance.HasFullName)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FullName));
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x000E14F8 File Offset: 0x000DF6F8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize = this.AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Role.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (uint val in this.Role)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.HasPrivileges)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Privileges);
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

		// Token: 0x040017E8 RID: 6120
		public bool HasAccountId;

		// Token: 0x040017E9 RID: 6121
		private EntityId _AccountId;

		// Token: 0x040017EA RID: 6122
		private List<uint> _Role = new List<uint>();

		// Token: 0x040017EB RID: 6123
		public bool HasPrivileges;

		// Token: 0x040017EC RID: 6124
		private ulong _Privileges;

		// Token: 0x040017ED RID: 6125
		public bool HasFullName;

		// Token: 0x040017EE RID: 6126
		private string _FullName;

		// Token: 0x040017EF RID: 6127
		public bool HasBattleTag;

		// Token: 0x040017F0 RID: 6128
		private string _BattleTag;
	}
}
