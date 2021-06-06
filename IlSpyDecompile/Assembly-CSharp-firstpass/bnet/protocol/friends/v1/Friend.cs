using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	public class Friend : IProtoBuf
	{
		private List<Attribute> _Attribute = new List<Attribute>();

		private List<uint> _Role = new List<uint>();

		public bool HasPrivileges;

		private ulong _Privileges;

		public bool HasAttributesEpoch;

		private ulong _AttributesEpoch;

		public bool HasCreationTime;

		private ulong _CreationTime;

		public EntityId AccountId { get; set; }

		public List<Attribute> Attribute
		{
			get
			{
				return _Attribute;
			}
			set
			{
				_Attribute = value;
			}
		}

		public List<Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

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

		public ulong AttributesEpoch
		{
			get
			{
				return _AttributesEpoch;
			}
			set
			{
				_AttributesEpoch = value;
				HasAttributesEpoch = true;
			}
		}

		public ulong CreationTime
		{
			get
			{
				return _CreationTime;
			}
			set
			{
				_CreationTime = value;
				HasCreationTime = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAccountId(EntityId val)
		{
			AccountId = val;
		}

		public void AddAttribute(Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<Attribute> val)
		{
			Attribute = val;
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

		public void SetAttributesEpoch(ulong val)
		{
			AttributesEpoch = val;
		}

		public void SetCreationTime(ulong val)
		{
			CreationTime = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= AccountId.GetHashCode();
			foreach (Attribute item in Attribute)
			{
				hashCode ^= item.GetHashCode();
			}
			foreach (uint item2 in Role)
			{
				hashCode ^= item2.GetHashCode();
			}
			if (HasPrivileges)
			{
				hashCode ^= Privileges.GetHashCode();
			}
			if (HasAttributesEpoch)
			{
				hashCode ^= AttributesEpoch.GetHashCode();
			}
			if (HasCreationTime)
			{
				hashCode ^= CreationTime.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Friend friend = obj as Friend;
			if (friend == null)
			{
				return false;
			}
			if (!AccountId.Equals(friend.AccountId))
			{
				return false;
			}
			if (Attribute.Count != friend.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(friend.Attribute[i]))
				{
					return false;
				}
			}
			if (Role.Count != friend.Role.Count)
			{
				return false;
			}
			for (int j = 0; j < Role.Count; j++)
			{
				if (!Role[j].Equals(friend.Role[j]))
				{
					return false;
				}
			}
			if (HasPrivileges != friend.HasPrivileges || (HasPrivileges && !Privileges.Equals(friend.Privileges)))
			{
				return false;
			}
			if (HasAttributesEpoch != friend.HasAttributesEpoch || (HasAttributesEpoch && !AttributesEpoch.Equals(friend.AttributesEpoch)))
			{
				return false;
			}
			if (HasCreationTime != friend.HasCreationTime || (HasCreationTime && !CreationTime.Equals(friend.CreationTime)))
			{
				return false;
			}
			return true;
		}

		public static Friend ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Friend>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Friend Deserialize(Stream stream, Friend instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Friend DeserializeLengthDelimited(Stream stream)
		{
			Friend friend = new Friend();
			DeserializeLengthDelimited(stream, friend);
			return friend;
		}

		public static Friend DeserializeLengthDelimited(Stream stream, Friend instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Friend Deserialize(Stream stream, Friend instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
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
				case 18:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
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
				case 40:
					instance.AttributesEpoch = ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.CreationTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, Friend instance)
		{
			if (instance.AccountId == null)
			{
				throw new ArgumentNullException("AccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.AccountId);
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(26);
				uint num = 0u;
				foreach (uint item2 in instance.Role)
				{
					num += ProtocolParser.SizeOfUInt32(item2);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint item3 in instance.Role)
				{
					ProtocolParser.WriteUInt32(stream, item3);
				}
			}
			if (instance.HasPrivileges)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.Privileges);
			}
			if (instance.HasAttributesEpoch)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.AttributesEpoch);
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = AccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (Role.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (uint item2 in Role)
				{
					num += ProtocolParser.SizeOfUInt32(item2);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (HasPrivileges)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Privileges);
			}
			if (HasAttributesEpoch)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(AttributesEpoch);
			}
			if (HasCreationTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(CreationTime);
			}
			return num + 1;
		}
	}
}
