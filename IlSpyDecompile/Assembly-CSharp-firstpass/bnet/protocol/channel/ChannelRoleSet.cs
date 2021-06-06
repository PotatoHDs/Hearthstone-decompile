using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.channel
{
	public class ChannelRoleSet : IProtoBuf
	{
		private List<ChannelRole> _Role = new List<ChannelRole>();

		private List<uint> _DefaultRole = new List<uint>();

		public bool HasName;

		private string _Name;

		public List<ChannelRole> Role
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

		public List<ChannelRole> RoleList => _Role;

		public int RoleCount => _Role.Count;

		public List<uint> DefaultRole
		{
			get
			{
				return _DefaultRole;
			}
			set
			{
				_DefaultRole = value;
			}
		}

		public List<uint> DefaultRoleList => _DefaultRole;

		public int DefaultRoleCount => _DefaultRole.Count;

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

		public bool IsInitialized => true;

		public void AddRole(ChannelRole val)
		{
			_Role.Add(val);
		}

		public void ClearRole()
		{
			_Role.Clear();
		}

		public void SetRole(List<ChannelRole> val)
		{
			Role = val;
		}

		public void AddDefaultRole(uint val)
		{
			_DefaultRole.Add(val);
		}

		public void ClearDefaultRole()
		{
			_DefaultRole.Clear();
		}

		public void SetDefaultRole(List<uint> val)
		{
			DefaultRole = val;
		}

		public void SetName(string val)
		{
			Name = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (ChannelRole item in Role)
			{
				num ^= item.GetHashCode();
			}
			foreach (uint item2 in DefaultRole)
			{
				num ^= item2.GetHashCode();
			}
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelRoleSet channelRoleSet = obj as ChannelRoleSet;
			if (channelRoleSet == null)
			{
				return false;
			}
			if (Role.Count != channelRoleSet.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < Role.Count; i++)
			{
				if (!Role[i].Equals(channelRoleSet.Role[i]))
				{
					return false;
				}
			}
			if (DefaultRole.Count != channelRoleSet.DefaultRole.Count)
			{
				return false;
			}
			for (int j = 0; j < DefaultRole.Count; j++)
			{
				if (!DefaultRole[j].Equals(channelRoleSet.DefaultRole[j]))
				{
					return false;
				}
			}
			if (HasName != channelRoleSet.HasName || (HasName && !Name.Equals(channelRoleSet.Name)))
			{
				return false;
			}
			return true;
		}

		public static ChannelRoleSet ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelRoleSet>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelRoleSet Deserialize(Stream stream, ChannelRoleSet instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelRoleSet DeserializeLengthDelimited(Stream stream)
		{
			ChannelRoleSet channelRoleSet = new ChannelRoleSet();
			DeserializeLengthDelimited(stream, channelRoleSet);
			return channelRoleSet;
		}

		public static ChannelRoleSet DeserializeLengthDelimited(Stream stream, ChannelRoleSet instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelRoleSet Deserialize(Stream stream, ChannelRoleSet instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<ChannelRole>();
			}
			if (instance.DefaultRole == null)
			{
				instance.DefaultRole = new List<uint>();
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
					instance.Role.Add(ChannelRole.DeserializeLengthDelimited(stream));
					continue;
				case 18:
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.DefaultRole.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num2)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
				case 26:
					instance.Name = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ChannelRoleSet instance)
		{
			if (instance.Role.Count > 0)
			{
				foreach (ChannelRole item in instance.Role)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					ChannelRole.Serialize(stream, item);
				}
			}
			if (instance.DefaultRole.Count > 0)
			{
				stream.WriteByte(18);
				uint num = 0u;
				foreach (uint item2 in instance.DefaultRole)
				{
					num += ProtocolParser.SizeOfUInt32(item2);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint item3 in instance.DefaultRole)
				{
					ProtocolParser.WriteUInt32(stream, item3);
				}
			}
			if (instance.HasName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Role.Count > 0)
			{
				foreach (ChannelRole item in Role)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (DefaultRole.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (uint item2 in DefaultRole)
				{
					num += ProtocolParser.SizeOfUInt32(item2);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
