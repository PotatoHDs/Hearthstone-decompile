using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class MemberState : IProtoBuf
	{
		private List<Attribute> _Attribute = new List<Attribute>();

		private List<uint> _Role = new List<uint>();

		public bool HasPrivileges;

		private ulong _Privileges;

		public bool HasInfo;

		private MemberAccountInfo _Info;

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

		public MemberAccountInfo Info
		{
			get
			{
				return _Info;
			}
			set
			{
				_Info = value;
				HasInfo = value != null;
			}
		}

		public bool IsInitialized => true;

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

		public void SetInfo(MemberAccountInfo val)
		{
			Info = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			foreach (uint item2 in Role)
			{
				num ^= item2.GetHashCode();
			}
			if (HasPrivileges)
			{
				num ^= Privileges.GetHashCode();
			}
			if (HasInfo)
			{
				num ^= Info.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MemberState memberState = obj as MemberState;
			if (memberState == null)
			{
				return false;
			}
			if (Attribute.Count != memberState.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(memberState.Attribute[i]))
				{
					return false;
				}
			}
			if (Role.Count != memberState.Role.Count)
			{
				return false;
			}
			for (int j = 0; j < Role.Count; j++)
			{
				if (!Role[j].Equals(memberState.Role[j]))
				{
					return false;
				}
			}
			if (HasPrivileges != memberState.HasPrivileges || (HasPrivileges && !Privileges.Equals(memberState.Privileges)))
			{
				return false;
			}
			if (HasInfo != memberState.HasInfo || (HasInfo && !Info.Equals(memberState.Info)))
			{
				return false;
			}
			return true;
		}

		public static MemberState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberState>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MemberState Deserialize(Stream stream, MemberState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MemberState DeserializeLengthDelimited(Stream stream)
		{
			MemberState memberState = new MemberState();
			DeserializeLengthDelimited(stream, memberState);
			return memberState;
		}

		public static MemberState DeserializeLengthDelimited(Stream stream, MemberState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MemberState Deserialize(Stream stream, MemberState instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
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
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 18:
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
				case 24:
					instance.Privileges = ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					if (instance.Info == null)
					{
						instance.Info = MemberAccountInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						MemberAccountInfo.DeserializeLengthDelimited(stream, instance.Info);
					}
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

		public static void Serialize(Stream stream, MemberState instance)
		{
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(18);
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
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.Privileges);
			}
			if (instance.HasInfo)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Info.GetSerializedSize());
				MemberAccountInfo.Serialize(stream, instance.Info);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
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
			if (HasInfo)
			{
				num++;
				uint serializedSize2 = Info.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
