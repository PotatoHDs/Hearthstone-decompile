using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.friends.v1
{
	public class FriendInvitationParams : IProtoBuf
	{
		public bool HasTargetEmail;

		private string _TargetEmail;

		public bool HasTargetBattleTag;

		private string _TargetBattleTag;

		private List<uint> _Role = new List<uint>();

		private List<Attribute> _Attribute = new List<Attribute>();

		public bool HasTargetName;

		private string _TargetName;

		public bool HasProgram;

		private uint _Program;

		public string TargetEmail
		{
			get
			{
				return _TargetEmail;
			}
			set
			{
				_TargetEmail = value;
				HasTargetEmail = value != null;
			}
		}

		public string TargetBattleTag
		{
			get
			{
				return _TargetBattleTag;
			}
			set
			{
				_TargetBattleTag = value;
				HasTargetBattleTag = value != null;
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

		public string TargetName
		{
			get
			{
				return _TargetName;
			}
			set
			{
				_TargetName = value;
				HasTargetName = value != null;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public bool IsInitialized => true;

		public void SetTargetEmail(string val)
		{
			TargetEmail = val;
		}

		public void SetTargetBattleTag(string val)
		{
			TargetBattleTag = val;
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

		public void SetTargetName(string val)
		{
			TargetName = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTargetEmail)
			{
				num ^= TargetEmail.GetHashCode();
			}
			if (HasTargetBattleTag)
			{
				num ^= TargetBattleTag.GetHashCode();
			}
			foreach (uint item in Role)
			{
				num ^= item.GetHashCode();
			}
			foreach (Attribute item2 in Attribute)
			{
				num ^= item2.GetHashCode();
			}
			if (HasTargetName)
			{
				num ^= TargetName.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FriendInvitationParams friendInvitationParams = obj as FriendInvitationParams;
			if (friendInvitationParams == null)
			{
				return false;
			}
			if (HasTargetEmail != friendInvitationParams.HasTargetEmail || (HasTargetEmail && !TargetEmail.Equals(friendInvitationParams.TargetEmail)))
			{
				return false;
			}
			if (HasTargetBattleTag != friendInvitationParams.HasTargetBattleTag || (HasTargetBattleTag && !TargetBattleTag.Equals(friendInvitationParams.TargetBattleTag)))
			{
				return false;
			}
			if (Role.Count != friendInvitationParams.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < Role.Count; i++)
			{
				if (!Role[i].Equals(friendInvitationParams.Role[i]))
				{
					return false;
				}
			}
			if (Attribute.Count != friendInvitationParams.Attribute.Count)
			{
				return false;
			}
			for (int j = 0; j < Attribute.Count; j++)
			{
				if (!Attribute[j].Equals(friendInvitationParams.Attribute[j]))
				{
					return false;
				}
			}
			if (HasTargetName != friendInvitationParams.HasTargetName || (HasTargetName && !TargetName.Equals(friendInvitationParams.TargetName)))
			{
				return false;
			}
			if (HasProgram != friendInvitationParams.HasProgram || (HasProgram && !Program.Equals(friendInvitationParams.Program)))
			{
				return false;
			}
			return true;
		}

		public static FriendInvitationParams ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendInvitationParams>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FriendInvitationParams Deserialize(Stream stream, FriendInvitationParams instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FriendInvitationParams DeserializeLengthDelimited(Stream stream)
		{
			FriendInvitationParams friendInvitationParams = new FriendInvitationParams();
			DeserializeLengthDelimited(stream, friendInvitationParams);
			return friendInvitationParams;
		}

		public static FriendInvitationParams DeserializeLengthDelimited(Stream stream, FriendInvitationParams instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FriendInvitationParams Deserialize(Stream stream, FriendInvitationParams instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
					instance.TargetEmail = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.TargetBattleTag = ProtocolParser.ReadString(stream);
					continue;
				case 50:
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
				case 66:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 74:
					instance.TargetName = ProtocolParser.ReadString(stream);
					continue;
				case 85:
					instance.Program = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, FriendInvitationParams instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTargetEmail)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetEmail));
			}
			if (instance.HasTargetBattleTag)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetBattleTag));
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(50);
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
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item3 in instance.Attribute)
				{
					stream.WriteByte(66);
					ProtocolParser.WriteUInt32(stream, item3.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item3);
				}
			}
			if (instance.HasTargetName)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetName));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(85);
				binaryWriter.Write(instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTargetEmail)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(TargetEmail);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasTargetBattleTag)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(TargetBattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
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
			if (Attribute.Count > 0)
			{
				foreach (Attribute item2 in Attribute)
				{
					num++;
					uint serializedSize = item2.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasTargetName)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(TargetName);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
