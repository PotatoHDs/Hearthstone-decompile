using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class RoleAssignment : IProtoBuf
	{
		public bool HasMemberId;

		private GameAccountHandle _MemberId;

		private List<uint> _Role = new List<uint>();

		public GameAccountHandle MemberId
		{
			get
			{
				return _MemberId;
			}
			set
			{
				_MemberId = value;
				HasMemberId = value != null;
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

		public bool IsInitialized => true;

		public void SetMemberId(GameAccountHandle val)
		{
			MemberId = val;
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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMemberId)
			{
				num ^= MemberId.GetHashCode();
			}
			foreach (uint item in Role)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RoleAssignment roleAssignment = obj as RoleAssignment;
			if (roleAssignment == null)
			{
				return false;
			}
			if (HasMemberId != roleAssignment.HasMemberId || (HasMemberId && !MemberId.Equals(roleAssignment.MemberId)))
			{
				return false;
			}
			if (Role.Count != roleAssignment.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < Role.Count; i++)
			{
				if (!Role[i].Equals(roleAssignment.Role[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static RoleAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RoleAssignment>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RoleAssignment Deserialize(Stream stream, RoleAssignment instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RoleAssignment DeserializeLengthDelimited(Stream stream)
		{
			RoleAssignment roleAssignment = new RoleAssignment();
			DeserializeLengthDelimited(stream, roleAssignment);
			return roleAssignment;
		}

		public static RoleAssignment DeserializeLengthDelimited(Stream stream, RoleAssignment instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RoleAssignment Deserialize(Stream stream, RoleAssignment instance, long limit)
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
					if (instance.MemberId == null)
					{
						instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
					}
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

		public static void Serialize(Stream stream, RoleAssignment instance)
		{
			if (instance.HasMemberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
			}
			if (instance.Role.Count <= 0)
			{
				return;
			}
			stream.WriteByte(18);
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

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMemberId)
			{
				num++;
				uint serializedSize = MemberId.GetSerializedSize();
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
			return num;
		}
	}
}
