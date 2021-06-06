using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class RoleState : IProtoBuf
	{
		public bool HasName;

		private string _Name;

		private List<uint> _AssignableRole = new List<uint>();

		public bool HasRequired;

		private bool _Required;

		public bool HasUnique;

		private bool _Unique;

		public bool HasRelegationRole;

		private uint _RelegationRole;

		private List<uint> _KickableRole = new List<uint>();

		private List<uint> _RemovableRole = new List<uint>();

		private List<uint> _MentionableRole = new List<uint>();

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

		public List<uint> MentionableRole
		{
			get
			{
				return _MentionableRole;
			}
			set
			{
				_MentionableRole = value;
			}
		}

		public List<uint> MentionableRoleList => _MentionableRole;

		public int MentionableRoleCount => _MentionableRole.Count;

		public bool IsInitialized => true;

		public void SetName(string val)
		{
			Name = val;
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

		public void AddMentionableRole(uint val)
		{
			_MentionableRole.Add(val);
		}

		public void ClearMentionableRole()
		{
			_MentionableRole.Clear();
		}

		public void SetMentionableRole(List<uint> val)
		{
			MentionableRole = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			foreach (uint item in AssignableRole)
			{
				num ^= item.GetHashCode();
			}
			if (HasRequired)
			{
				num ^= Required.GetHashCode();
			}
			if (HasUnique)
			{
				num ^= Unique.GetHashCode();
			}
			if (HasRelegationRole)
			{
				num ^= RelegationRole.GetHashCode();
			}
			foreach (uint item2 in KickableRole)
			{
				num ^= item2.GetHashCode();
			}
			foreach (uint item3 in RemovableRole)
			{
				num ^= item3.GetHashCode();
			}
			foreach (uint item4 in MentionableRole)
			{
				num ^= item4.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RoleState roleState = obj as RoleState;
			if (roleState == null)
			{
				return false;
			}
			if (HasName != roleState.HasName || (HasName && !Name.Equals(roleState.Name)))
			{
				return false;
			}
			if (AssignableRole.Count != roleState.AssignableRole.Count)
			{
				return false;
			}
			for (int i = 0; i < AssignableRole.Count; i++)
			{
				if (!AssignableRole[i].Equals(roleState.AssignableRole[i]))
				{
					return false;
				}
			}
			if (HasRequired != roleState.HasRequired || (HasRequired && !Required.Equals(roleState.Required)))
			{
				return false;
			}
			if (HasUnique != roleState.HasUnique || (HasUnique && !Unique.Equals(roleState.Unique)))
			{
				return false;
			}
			if (HasRelegationRole != roleState.HasRelegationRole || (HasRelegationRole && !RelegationRole.Equals(roleState.RelegationRole)))
			{
				return false;
			}
			if (KickableRole.Count != roleState.KickableRole.Count)
			{
				return false;
			}
			for (int j = 0; j < KickableRole.Count; j++)
			{
				if (!KickableRole[j].Equals(roleState.KickableRole[j]))
				{
					return false;
				}
			}
			if (RemovableRole.Count != roleState.RemovableRole.Count)
			{
				return false;
			}
			for (int k = 0; k < RemovableRole.Count; k++)
			{
				if (!RemovableRole[k].Equals(roleState.RemovableRole[k]))
				{
					return false;
				}
			}
			if (MentionableRole.Count != roleState.MentionableRole.Count)
			{
				return false;
			}
			for (int l = 0; l < MentionableRole.Count; l++)
			{
				if (!MentionableRole[l].Equals(roleState.MentionableRole[l]))
				{
					return false;
				}
			}
			return true;
		}

		public static RoleState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RoleState>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RoleState Deserialize(Stream stream, RoleState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RoleState DeserializeLengthDelimited(Stream stream)
		{
			RoleState roleState = new RoleState();
			DeserializeLengthDelimited(stream, roleState);
			return roleState;
		}

		public static RoleState DeserializeLengthDelimited(Stream stream, RoleState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RoleState Deserialize(Stream stream, RoleState instance, long limit)
		{
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
			if (instance.MentionableRole == null)
			{
				instance.MentionableRole = new List<uint>();
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
				case 18:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 34:
				{
					long num4 = ProtocolParser.ReadUInt32(stream);
					num4 += stream.Position;
					while (stream.Position < num4)
					{
						instance.AssignableRole.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num4)
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
					long num3 = ProtocolParser.ReadUInt32(stream);
					num3 += stream.Position;
					while (stream.Position < num3)
					{
						instance.KickableRole.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num3)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
				case 82:
				{
					long num5 = ProtocolParser.ReadUInt32(stream);
					num5 += stream.Position;
					while (stream.Position < num5)
					{
						instance.RemovableRole.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num5)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
				case 90:
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.MentionableRole.Add(ProtocolParser.ReadUInt32(stream));
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

		public static void Serialize(Stream stream, RoleState instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.AssignableRole.Count > 0)
			{
				stream.WriteByte(34);
				uint num = 0u;
				foreach (uint item in instance.AssignableRole)
				{
					num += ProtocolParser.SizeOfUInt32(item);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint item2 in instance.AssignableRole)
				{
					ProtocolParser.WriteUInt32(stream, item2);
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
				foreach (uint item3 in instance.KickableRole)
				{
					num2 += ProtocolParser.SizeOfUInt32(item3);
				}
				ProtocolParser.WriteUInt32(stream, num2);
				foreach (uint item4 in instance.KickableRole)
				{
					ProtocolParser.WriteUInt32(stream, item4);
				}
			}
			if (instance.RemovableRole.Count > 0)
			{
				stream.WriteByte(82);
				uint num3 = 0u;
				foreach (uint item5 in instance.RemovableRole)
				{
					num3 += ProtocolParser.SizeOfUInt32(item5);
				}
				ProtocolParser.WriteUInt32(stream, num3);
				foreach (uint item6 in instance.RemovableRole)
				{
					ProtocolParser.WriteUInt32(stream, item6);
				}
			}
			if (instance.MentionableRole.Count <= 0)
			{
				return;
			}
			stream.WriteByte(90);
			uint num4 = 0u;
			foreach (uint item7 in instance.MentionableRole)
			{
				num4 += ProtocolParser.SizeOfUInt32(item7);
			}
			ProtocolParser.WriteUInt32(stream, num4);
			foreach (uint item8 in instance.MentionableRole)
			{
				ProtocolParser.WriteUInt32(stream, item8);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (AssignableRole.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (uint item in AssignableRole)
				{
					num += ProtocolParser.SizeOfUInt32(item);
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
				foreach (uint item2 in KickableRole)
				{
					num += ProtocolParser.SizeOfUInt32(item2);
				}
				num += ProtocolParser.SizeOfUInt32(num - num3);
			}
			if (RemovableRole.Count > 0)
			{
				num++;
				uint num4 = num;
				foreach (uint item3 in RemovableRole)
				{
					num += ProtocolParser.SizeOfUInt32(item3);
				}
				num += ProtocolParser.SizeOfUInt32(num - num4);
			}
			if (MentionableRole.Count > 0)
			{
				num++;
				uint num5 = num;
				foreach (uint item4 in MentionableRole)
				{
					num += ProtocolParser.SizeOfUInt32(item4);
				}
				num += ProtocolParser.SizeOfUInt32(num - num5);
			}
			return num;
		}
	}
}
