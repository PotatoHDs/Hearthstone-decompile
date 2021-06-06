using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.user_manager.v1
{
	public class BlockedPlayer : IProtoBuf
	{
		public bool HasName;

		private string _Name;

		private List<uint> _Role = new List<uint>();

		public bool HasPrivileges;

		private ulong _Privileges;

		public EntityId AccountId { get; set; }

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
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

		public bool IsInitialized => true;

		public void SetAccountId(EntityId val)
		{
			AccountId = val;
		}

		public void SetName(string val)
		{
			Name = val;
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

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= AccountId.GetHashCode();
			if (HasName)
			{
				hashCode ^= Name.GetHashCode();
			}
			foreach (uint item in Role)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasPrivileges)
			{
				hashCode ^= Privileges.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			BlockedPlayer blockedPlayer = obj as BlockedPlayer;
			if (blockedPlayer == null)
			{
				return false;
			}
			if (!AccountId.Equals(blockedPlayer.AccountId))
			{
				return false;
			}
			if (HasName != blockedPlayer.HasName || (HasName && !Name.Equals(blockedPlayer.Name)))
			{
				return false;
			}
			if (Role.Count != blockedPlayer.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < Role.Count; i++)
			{
				if (!Role[i].Equals(blockedPlayer.Role[i]))
				{
					return false;
				}
			}
			if (HasPrivileges != blockedPlayer.HasPrivileges || (HasPrivileges && !Privileges.Equals(blockedPlayer.Privileges)))
			{
				return false;
			}
			return true;
		}

		public static BlockedPlayer ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BlockedPlayer>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BlockedPlayer Deserialize(Stream stream, BlockedPlayer instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BlockedPlayer DeserializeLengthDelimited(Stream stream)
		{
			BlockedPlayer blockedPlayer = new BlockedPlayer();
			DeserializeLengthDelimited(stream, blockedPlayer);
			return blockedPlayer;
		}

		public static BlockedPlayer DeserializeLengthDelimited(Stream stream, BlockedPlayer instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BlockedPlayer Deserialize(Stream stream, BlockedPlayer instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
			instance.Privileges = 0uL;
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
				case 18:
					instance.Name = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, BlockedPlayer instance)
		{
			if (instance.AccountId == null)
			{
				throw new ArgumentNullException("AccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.AccountId);
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
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
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = AccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
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
			return num + 1;
		}
	}
}
