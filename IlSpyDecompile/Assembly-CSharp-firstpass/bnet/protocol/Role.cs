using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class Role : IProtoBuf
	{
		private List<string> _Privilege = new List<string>();

		private List<uint> _AssignableRole = new List<uint>();

		public bool HasRequired;

		private bool _Required;

		public bool HasUnique;

		private bool _Unique;

		public bool HasRelegationRole;

		private uint _RelegationRole;

		private List<uint> _KickableRole = new List<uint>();

		private List<uint> _RemovableRole = new List<uint>();

		public uint Id { get; set; }

		public string Name { get; set; }

		public List<string> Privilege
		{
			get
			{
				return _Privilege;
			}
			set
			{
				_Privilege = value;
			}
		}

		public List<string> PrivilegeList => _Privilege;

		public int PrivilegeCount => _Privilege.Count;

		public List<uint> AssignableRole
		{
			get
			{
				return _AssignableRole;
			}
			set
			{
				_AssignableRole = value;
			}
		}

		public List<uint> AssignableRoleList => _AssignableRole;

		public int AssignableRoleCount => _AssignableRole.Count;

		public bool Required
		{
			get
			{
				return _Required;
			}
			set
			{
				_Required = value;
				HasRequired = true;
			}
		}

		public bool Unique
		{
			get
			{
				return _Unique;
			}
			set
			{
				_Unique = value;
				HasUnique = true;
			}
		}

		public uint RelegationRole
		{
			get
			{
				return _RelegationRole;
			}
			set
			{
				_RelegationRole = value;
				HasRelegationRole = true;
			}
		}

		public List<uint> KickableRole
		{
			get
			{
				return _KickableRole;
			}
			set
			{
				_KickableRole = value;
			}
		}

		public List<uint> KickableRoleList => _KickableRole;

		public int KickableRoleCount => _KickableRole.Count;

		public List<uint> RemovableRole
		{
			get
			{
				return _RemovableRole;
			}
			set
			{
				_RemovableRole = value;
			}
		}

		public List<uint> RemovableRoleList => _RemovableRole;

		public int RemovableRoleCount => _RemovableRole.Count;

		public bool IsInitialized => true;

		public void SetId(uint val)
		{
			Id = val;
		}

		public void SetName(string val)
		{
			Name = val;
		}

		public void AddPrivilege(string val)
		{
			_Privilege.Add(val);
		}

		public void ClearPrivilege()
		{
			_Privilege.Clear();
		}

		public void SetPrivilege(List<string> val)
		{
			Privilege = val;
		}

		public void AddAssignableRole(uint val)
		{
			_AssignableRole.Add(val);
		}

		public void ClearAssignableRole()
		{
			_AssignableRole.Clear();
		}

		public void SetAssignableRole(List<uint> val)
		{
			AssignableRole = val;
		}

		public void SetRequired(bool val)
		{
			Required = val;
		}

		public void SetUnique(bool val)
		{
			Unique = val;
		}

		public void SetRelegationRole(uint val)
		{
			RelegationRole = val;
		}

		public void AddKickableRole(uint val)
		{
			_KickableRole.Add(val);
		}

		public void ClearKickableRole()
		{
			_KickableRole.Clear();
		}

		public void SetKickableRole(List<uint> val)
		{
			KickableRole = val;
		}

		public void AddRemovableRole(uint val)
		{
			_RemovableRole.Add(val);
		}

		public void ClearRemovableRole()
		{
			_RemovableRole.Clear();
		}

		public void SetRemovableRole(List<uint> val)
		{
			RemovableRole = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			hashCode ^= Name.GetHashCode();
			foreach (string item in Privilege)
			{
				hashCode ^= item.GetHashCode();
			}
			foreach (uint item2 in AssignableRole)
			{
				hashCode ^= item2.GetHashCode();
			}
			if (HasRequired)
			{
				hashCode ^= Required.GetHashCode();
			}
			if (HasUnique)
			{
				hashCode ^= Unique.GetHashCode();
			}
			if (HasRelegationRole)
			{
				hashCode ^= RelegationRole.GetHashCode();
			}
			foreach (uint item3 in KickableRole)
			{
				hashCode ^= item3.GetHashCode();
			}
			foreach (uint item4 in RemovableRole)
			{
				hashCode ^= item4.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Role role = obj as Role;
			if (role == null)
			{
				return false;
			}
			if (!Id.Equals(role.Id))
			{
				return false;
			}
			if (!Name.Equals(role.Name))
			{
				return false;
			}
			if (Privilege.Count != role.Privilege.Count)
			{
				return false;
			}
			for (int i = 0; i < Privilege.Count; i++)
			{
				if (!Privilege[i].Equals(role.Privilege[i]))
				{
					return false;
				}
			}
			if (AssignableRole.Count != role.AssignableRole.Count)
			{
				return false;
			}
			for (int j = 0; j < AssignableRole.Count; j++)
			{
				if (!AssignableRole[j].Equals(role.AssignableRole[j]))
				{
					return false;
				}
			}
			if (HasRequired != role.HasRequired || (HasRequired && !Required.Equals(role.Required)))
			{
				return false;
			}
			if (HasUnique != role.HasUnique || (HasUnique && !Unique.Equals(role.Unique)))
			{
				return false;
			}
			if (HasRelegationRole != role.HasRelegationRole || (HasRelegationRole && !RelegationRole.Equals(role.RelegationRole)))
			{
				return false;
			}
			if (KickableRole.Count != role.KickableRole.Count)
			{
				return false;
			}
			for (int k = 0; k < KickableRole.Count; k++)
			{
				if (!KickableRole[k].Equals(role.KickableRole[k]))
				{
					return false;
				}
			}
			if (RemovableRole.Count != role.RemovableRole.Count)
			{
				return false;
			}
			for (int l = 0; l < RemovableRole.Count; l++)
			{
				if (!RemovableRole[l].Equals(role.RemovableRole[l]))
				{
					return false;
				}
			}
			return true;
		}

		public static Role ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Role>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Role Deserialize(Stream stream, Role instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Role DeserializeLengthDelimited(Stream stream)
		{
			Role role = new Role();
			DeserializeLengthDelimited(stream, role);
			return role;
		}

		public static Role DeserializeLengthDelimited(Stream stream, Role instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Role Deserialize(Stream stream, Role instance, long limit)
		{
			if (instance.Privilege == null)
			{
				instance.Privilege = new List<string>();
			}
			if (instance.AssignableRole == null)
			{
				instance.AssignableRole = new List<uint>();
			}
			if (instance.KickableRole == null)
			{
				instance.KickableRole = new List<uint>();
			}
			if (instance.RemovableRole == null)
			{
				instance.RemovableRole = new List<uint>();
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
				case 8:
					instance.Id = ProtocolParser.ReadUInt32(stream);
					continue;
				case 18:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Privilege.Add(ProtocolParser.ReadString(stream));
					continue;
				case 34:
				{
					long num3 = ProtocolParser.ReadUInt32(stream);
					num3 += stream.Position;
					while (stream.Position < num3)
					{
						instance.AssignableRole.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num3)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
				case 40:
					instance.Required = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.Unique = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.RelegationRole = ProtocolParser.ReadUInt32(stream);
					continue;
				case 74:
				{
					long num4 = ProtocolParser.ReadUInt32(stream);
					num4 += stream.Position;
					while (stream.Position < num4)
					{
						instance.KickableRole.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num4)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
				case 82:
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.RemovableRole.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num2)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
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

		public static void Serialize(Stream stream, Role instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Id);
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.Privilege.Count > 0)
			{
				foreach (string item in instance.Privilege)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item));
				}
			}
			if (instance.AssignableRole.Count > 0)
			{
				stream.WriteByte(34);
				uint num = 0u;
				foreach (uint item2 in instance.AssignableRole)
				{
					num += ProtocolParser.SizeOfUInt32(item2);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint item3 in instance.AssignableRole)
				{
					ProtocolParser.WriteUInt32(stream, item3);
				}
			}
			if (instance.HasRequired)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Required);
			}
			if (instance.HasUnique)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.Unique);
			}
			if (instance.HasRelegationRole)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.RelegationRole);
			}
			if (instance.KickableRole.Count > 0)
			{
				stream.WriteByte(74);
				uint num2 = 0u;
				foreach (uint item4 in instance.KickableRole)
				{
					num2 += ProtocolParser.SizeOfUInt32(item4);
				}
				ProtocolParser.WriteUInt32(stream, num2);
				foreach (uint item5 in instance.KickableRole)
				{
					ProtocolParser.WriteUInt32(stream, item5);
				}
			}
			if (instance.RemovableRole.Count <= 0)
			{
				return;
			}
			stream.WriteByte(82);
			uint num3 = 0u;
			foreach (uint item6 in instance.RemovableRole)
			{
				num3 += ProtocolParser.SizeOfUInt32(item6);
			}
			ProtocolParser.WriteUInt32(stream, num3);
			foreach (uint item7 in instance.RemovableRole)
			{
				ProtocolParser.WriteUInt32(stream, item7);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(Id);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (Privilege.Count > 0)
			{
				foreach (string item in Privilege)
				{
					num++;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(item);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			if (AssignableRole.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (uint item2 in AssignableRole)
				{
					num += ProtocolParser.SizeOfUInt32(item2);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (HasRequired)
			{
				num++;
				num++;
			}
			if (HasUnique)
			{
				num++;
				num++;
			}
			if (HasRelegationRole)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(RelegationRole);
			}
			if (KickableRole.Count > 0)
			{
				num++;
				uint num3 = num;
				foreach (uint item3 in KickableRole)
				{
					num += ProtocolParser.SizeOfUInt32(item3);
				}
				num += ProtocolParser.SizeOfUInt32(num - num3);
			}
			if (RemovableRole.Count > 0)
			{
				num++;
				uint num4 = num;
				foreach (uint item4 in RemovableRole)
				{
					num += ProtocolParser.SizeOfUInt32(item4);
				}
				num += ProtocolParser.SizeOfUInt32(num - num4);
			}
			return num + 2;
		}
	}
}
