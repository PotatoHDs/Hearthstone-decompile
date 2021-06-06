using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.friends.v1
{
	public class FriendOfFriend : IProtoBuf
	{
		public bool HasAccountId;

		private EntityId _AccountId;

		private List<uint> _Role = new List<uint>();

		public bool HasPrivileges;

		private ulong _Privileges;

		public bool HasFullName;

		private string _FullName;

		public bool HasBattleTag;

		private string _BattleTag;

		public EntityId AccountId
		{
			get
			{
				return _AccountId;
			}
			set
			{
				_AccountId = value;
				HasAccountId = value != null;
			}
		}

		public List<uint> Role
		{
			get
			{
				return _Role;
			}
			set
			{
				_Role = value;
			}
		}

		public List<uint> RoleList => _Role;

		public int RoleCount => _Role.Count;

		public ulong Privileges
		{
			get
			{
				return _Privileges;
			}
			set
			{
				_Privileges = value;
				HasPrivileges = true;
			}
		}

		public string FullName
		{
			get
			{
				return _FullName;
			}
			set
			{
				_FullName = value;
				HasFullName = value != null;
			}
		}

		public string BattleTag
		{
			get
			{
				return _BattleTag;
			}
			set
			{
				_BattleTag = value;
				HasBattleTag = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAccountId(EntityId val)
		{
			AccountId = val;
		}

		public void AddRole(uint val)
		{
			_Role.Add(val);
		}

		public void ClearRole()
		{
			_Role.Clear();
		}

		public void SetRole(List<uint> val)
		{
			Role = val;
		}

		public void SetPrivileges(ulong val)
		{
			Privileges = val;
		}

		public void SetFullName(string val)
		{
			FullName = val;
		}

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountId)
			{
				num ^= AccountId.GetHashCode();
			}
			foreach (uint item in Role)
			{
				num ^= item.GetHashCode();
			}
			if (HasPrivileges)
			{
				num ^= Privileges.GetHashCode();
			}
			if (HasFullName)
			{
				num ^= FullName.GetHashCode();
			}
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FriendOfFriend friendOfFriend = obj as FriendOfFriend;
			if (friendOfFriend == null)
			{
				return false;
			}
			if (HasAccountId != friendOfFriend.HasAccountId || (HasAccountId && !AccountId.Equals(friendOfFriend.AccountId)))
			{
				return false;
			}
			if (Role.Count != friendOfFriend.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < Role.Count; i++)
			{
				if (!Role[i].Equals(friendOfFriend.Role[i]))
				{
					return false;
				}
			}
			if (HasPrivileges != friendOfFriend.HasPrivileges || (HasPrivileges && !Privileges.Equals(friendOfFriend.Privileges)))
			{
				return false;
			}
			if (HasFullName != friendOfFriend.HasFullName || (HasFullName && !FullName.Equals(friendOfFriend.FullName)))
			{
				return false;
			}
			if (HasBattleTag != friendOfFriend.HasBattleTag || (HasBattleTag && !BattleTag.Equals(friendOfFriend.BattleTag)))
			{
				return false;
			}
			return true;
		}

		public static FriendOfFriend ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendOfFriend>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FriendOfFriend Deserialize(Stream stream, FriendOfFriend instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FriendOfFriend DeserializeLengthDelimited(Stream stream)
		{
			FriendOfFriend friendOfFriend = new FriendOfFriend();
			DeserializeLengthDelimited(stream, friendOfFriend);
			return friendOfFriend;
		}

		public static FriendOfFriend DeserializeLengthDelimited(Stream stream, FriendOfFriend instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FriendOfFriend Deserialize(Stream stream, FriendOfFriend instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					if (instance.AccountId == null)
					{
						instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
					continue;
				case 26:
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.Role.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num2)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
				case 32:
					instance.Privileges = ProtocolParser.ReadUInt64(stream);
					continue;
				case 50:
					instance.FullName = ProtocolParser.ReadString(stream);
					continue;
				case 58:
					instance.BattleTag = ProtocolParser.ReadString(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

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
				uint num = 0u;
				foreach (uint item in instance.Role)
				{
					num += ProtocolParser.SizeOfUInt32(item);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint item2 in instance.Role)
				{
					ProtocolParser.WriteUInt32(stream, item2);
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

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccountId)
			{
				num++;
				uint serializedSize = AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (Role.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (uint item in Role)
				{
					num += ProtocolParser.SizeOfUInt32(item);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (HasPrivileges)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Privileges);
			}
			if (HasFullName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(FullName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasBattleTag)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
